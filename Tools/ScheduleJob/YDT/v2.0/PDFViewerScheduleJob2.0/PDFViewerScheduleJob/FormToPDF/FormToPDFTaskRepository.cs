﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace PDFViewer
{
    public class FormToPDFTaskRepository
    {
        public FormToPDFTask GetTask(string Id = "")
        {
            var sql = "";
            if (string.IsNullOrWhiteSpace(Id))
            {
                sql = @"select top 1 * from S_D_FormToPDFTask Where State = 'New'";
            }
            else
            {
                sql = string.Format("select * from S_D_FormToPDFTask where ID = '{0}'", Id);
            }

            FormToPDFTask task = null;
            var db = SqlHelper.Create("Base");
            var reader = db.ExecuteReader(sql);
            if (reader.Read())
            {
                task = new FormToPDFTask();
                task.ID = reader.Get("ID", "");
                task.TempCode = reader.Get("TempCode", "");
                task.FormID = reader.Get("FormID", "");
                task.PDFFileID = reader.Get("PDFFileID", "");
                task.FormLastModifyDate = reader.Get<DateTime>("FormLastModifyDate");
                task.BeginTime = reader.Get<DateTime>("BeginTime");
                task.EndTime = reader.Get<DateTime>("EndTime");
                task.DoneLog = reader.Get("DoneLog", "");
                task.State = reader.Get("State", "");
            }
            reader.Close();
            return task;
        }
        public void BeginTask(string ID)
        {
            var sql = string.Format("Update S_D_FormToPDFTask set BeginTime='{1}',State='{2}' where ID = '{0}'", ID, DateTime.Now, "start");
            var db = SqlHelper.Create("Base");
            var reader = db.ExecuteNonQuery(sql);
        }
        public void EndTask(string ID, string pdfFileID, string imgFileIDs)
        {
            var sql = string.Format("Update S_D_FormToPDFTask set State='{2}',EndTime='{1}',PDFFileID='{3}',IMGFileIDs='{4}' where ID = '{0}'", ID, DateTime.Now, "end", pdfFileID, imgFileIDs);
            var db = SqlHelper.Create("Base");
            var reader = db.ExecuteNonQuery(sql);
        }
        public void AddTask()
        {
            //新版移动通写法
            var sql = "select * from S_S_EntryMenu where Type='2' and Component='0'"; //Type='2'是审批任务
            var db = SqlHelper.Create("Terminal");
            var dt = db.ExecuteDataTable(sql);
            string flowCodes = ",";
            bool isAll = false;
            foreach (DataRow row in dt.Rows)
            {
                if (string.IsNullOrEmpty(row["Code"].ToString()))
                {
                    isAll = true;
                    break;
                }
                else
                    flowCodes += row["Code"] + ",";
            }
            var dbFlow = SqlHelper.Create("Workflow");
            if (isAll)
            {
                var dtCodes = dbFlow.ExecuteDataTable(@"select S_WF_DefFlow.Code 
                from S_WF_DefStep,S_WF_DefFlow
                where S_WF_DefStep.DefFlowID = S_WF_DefFlow.ID 
                and S_WF_DefStep.AllowToMobile = 1 
                and S_WF_DefStep.DefFlowID is not null
                group by S_WF_DefFlow.Code");
                foreach (DataRow row in dtCodes.Rows)
                {
                    flowCodes += row["Code"] + ",";
                }
            }

            sql = string.Format("select * from S_WF_DefFlow where '{0}' like '%,'+ Code+',%'", flowCodes);

            var dtFlow = dbFlow.ExecuteDataTable(sql);
            foreach (DataRow row in dtFlow.Rows)
            {
                AddFormTask(row["ConnName"].ToString(), row["TableName"].ToString(), row["Code"].ToString());
            }
        }
        private void AddFormTask(string connName, string tableName, string tempCode)
        {
            var db = SqlHelper.Create(connName);
            var sql = "";//最近

            sql = "SELECT ID,CreateDate,ModifyDate FROM " + tableName + " WHERE (CreateDate> dateadd(day,-7,getdate())  OR ModifyDate> dateadd(day,-7,getdate()))";
            DataTable dt = db.ExecuteDataTable(sql);

            #region 解决一个表单对应多个流程和表单word模板问题 过滤dt 2017-3-8

            sql = "select * from S_WF_InsFlow where FormInstanceID in('{0}') and InsDefFlowID in(select ID from S_WF_InsDefFlow where Code='{1}')";
            sql = string.Format(sql, string.Join("','", dt.AsEnumerable().Select(c => c["ID"].ToString()).ToArray()), tempCode);

            var dbFlow = SqlHelper.Create("Workflow");
            var dtFlow = dbFlow.ExecuteDataTable(sql).AsEnumerable();
            for (var i = dt.Rows.Count - 1; i >= 0; i--)
            {
                var formId = dt.Rows[i]["ID"].ToString();
                if (dtFlow.Count(c => c["FormInstanceID"].ToString() == formId) == 0)
                    dt.Rows.RemoveAt(i);
            }

            #endregion

            sql = string.Format("SELECT * FROM S_D_FormToPDFTask WHERE TempCode='{0}'", tempCode);
            db = SqlHelper.Create("Base");
            DataTable tasks = db.ExecuteDataTable(sql);
            foreach (DataRow row in dt.Rows)
            {
                var formID = row["ID"].ToString();
                var formLastModifyDate = string.IsNullOrEmpty(row["ModifyDate"].ToString()) ? row["CreateDate"].ToString() : row["ModifyDate"].ToString();
                DataRow[] task = tasks.Select("FormID='" + formID + "'");

                var taskSql = "";
                if (task.Count() == 0)
                {
                    taskSql = string.Format("INSERT INTO S_D_FormToPDFTask(ID, TempCode, FormID, FormLastModifyDate, State) VALUES('{0}','{1}','{2}','{3}','{4}')", Tools.GuidTool.CreateGuid(), tempCode, formID, formLastModifyDate, "New");
                }
                else
                {
                    if (task[0]["FormLastModifyDate"].ToString() != formLastModifyDate)
                    {
                        taskSql = string.Format("UPDATE S_D_FormToPDFTask SET PDFFileID='', FormLastModifyDate='{0}',BeginTime='',EndTime='',State='New' WHERE ID='{1}'", formLastModifyDate, task[0]["ID"].ToString());
                    }
                }
                if (!string.IsNullOrEmpty(taskSql))
                    db.ExecuteNonQuery(taskSql);
            }

        }

        public void Log(string ID, string log)
        {
            var sql = string.Format("UPDATE S_D_FormToPDFTask SET DoneLog='{1}',State='New' WHERE ID='{0}'", ID, log);//增加异常回复处理（job中加了continue）
            var db = SqlHelper.Create("Base");
            db.ExecuteNonQuery(sql);
        }
    }

    public class FormToPDFTask
    {
        public string ID { get; set; }
        public string TempCode { get; set; }
        public string FormID { get; set; }
        public string PDFFileID { get; set; }
        public DateTime FormLastModifyDate { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DoneLog { get; set; }
        public string State { get; set; }
    }
}
