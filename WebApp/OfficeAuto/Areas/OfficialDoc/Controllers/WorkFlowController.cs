﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Workflow.Logic.Domain;
using OfficeAuto.Logic;
using Config;
using System.Data;
using Formula;

namespace OfficeAuto.Areas.OfficialDoc.Controllers
{
    public class WorkFlowController : BaseWorkFlowController
    {
        public JsonResult SaveTaskExecList()
        {
            List<Dictionary<string, object>> rows = PostingFO.GetTaskExec(Request.Form["ListData"]);
            UpdateList<S_WF_InsTaskExec>(rows);
            entities.SaveChanges();
            return Json(string.Empty);
        }

        string sqlFlowExecList = @"
select 
S_WF_InsTaskExec.ID as ID
,S_WF_InsTask.InsFlowID
,S_WF_InsTaskExec.CreateTime as CreateTime
,TaskUserID
,TaskUserName
,ExecUserID
,ExecUserName
,ExecTime
,ExecComment
,S_WF_InsTaskExec.Type as Type
,S_WF_InsTask.ID as TaskID
,TaskName
,TaskCategory
,TaskSubCategory
,SendTaskUserNames
,S_WF_InsTask.Status as TaskStatus
,FlowName
,FlowCategory
,FlowSubCategory
,S_WF_InsDefStep.Name as StepName
,S_WF_InsDefStep.ID as StepID
,ExecRoutingIDs
,S_WF_InsFlow.InsDefFlowID
from S_WF_InsTaskExec
right join S_WF_InsTask on InsTaskID=S_WF_InsTask.ID
join S_WF_InsFlow on S_WF_InsTask.InsFlowId=S_WF_InsFlow.ID
join S_WF_InsDefStep on InsDefStepID=S_WF_InsDefStep.ID
where FormInstanceID='{0}' and (WaitingRoutings is null or WaitingRoutings='') and (WaitingSteps is null or WaitingSteps='')
order by S_WF_InsTaskExec.ID
";

        public JsonResult GetFlowExecList(string id)
        {
            string sql = string.Format(sqlFlowExecList, id);
            SQLHelper sqlHelper = SQLHelper.CreateSqlHelper("Workflow");
            DataTable dt = sqlHelper.ExecuteDataTable(sql);

            if (dt.Rows.Count == 0)
                return Json(dt);

            string insDefFlowID = dt.Rows[0]["InsDefFlowID"].ToString();
            sql = string.Format("select ID,Name from S_WF_InsDefRouting where InsDefFlowID='{0}'", insDefFlowID);
            DataTable dtRouting = sqlHelper.ExecuteDataTable(sql);

            dt.Columns.Add("ExecRoutingName");
            dt.Columns.Add("UseTime");
            dt.Columns.Add("TaskUserDept");

            var userService = FormulaHelper.GetService<IUserService>();

            foreach (DataRow row in dt.Rows)
            {
                string ExecRoutingIDs = row["ExecRoutingIDs"].ToString().Trim(',');
                if (ExecRoutingIDs != "")
                    row["ExecRoutingName"] = dtRouting.AsEnumerable().SingleOrDefault(c => c["ID"].ToString() == ExecRoutingIDs.Split(',').LastOrDefault())["Name"];
                string CreateTime = row["CreateTime"].ToString();
                string ExecTime = row["ExecTime"].ToString();
                if (!string.IsNullOrEmpty(ExecTime))
                {
                    var span = DateTime.Parse(ExecTime) - DateTime.Parse(CreateTime);
                    row["UseTime"] = string.Format("{0}天{1}小时{2}分", span.Days, span.Hours, span.Minutes);
                }
                if (row["TaskUserID"].ToString() != "")
                {
                    row["TaskUserDept"] = userService.GetUserInfoByID(row["TaskUserID"].ToString()).UserOrgName;
                }
                else
                {
                    row["TaskUserName"] = "";
                }

            }
            return Json(dt);
        }

    }
}