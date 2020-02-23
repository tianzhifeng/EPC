﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Data;
using Formula.Helper;
using Formula;
using MvcAdapter;
using Config;
using Config.Logic;
using Market.Logic;
using Market.Logic.Domain;
using System.Collections;

namespace Market.Areas.Analysis.Controllers
{
    public class ProjectAnalysisController : MarketController
    {
        public ActionResult ProjectAnalysisList()
        {
            var tab = new Tab();
            //var levelCategory = CategoryFactory.GetCategory("Market.CustomerLevel", "项目等级", "CustomerLevel");
            //levelCategory.SetDefaultItem();
            //tab.Categories.Add(levelCategory);

            //业务类型
            var category = CategoryFactory.GetCategory("Market.BusinessType", "业务类型", "ProjectClass");
            category.SetDefaultItem();
            tab.Categories.Add(category);
            //主责部门
            category = CategoryFactory.GetCategory("Market.ManDept", "主责部门", "ChargerDept");
            category.SetDefaultItem();
            tab.Categories.Add(category);
            //设计阶段
            category = CategoryFactory.GetCategory("Project.Phase", "设计阶段", "Phase");
            category.SetDefaultItem();
            tab.Categories.Add(category);

            tab.IsDisplay = true;
            ViewBag.Tab = tab;
            return View();
        }

        public JsonResult GetList(QueryBuilder qb)
        {
            var costQb = new QueryBuilder();
            foreach (var condition in qb.Items.Where(a => a.Field == "CostDate"))
            {
                costQb.Add(condition.Field, condition.Method, condition.Value, condition.OrGroup, condition.BaseOrGroup);
            }
            qb.Items.RemoveWhere(a => a.Field == "CostDate");
            string costWhereStr = costQb.GetWhereString(false);
            string joinType = "left";
            if (!string.IsNullOrEmpty(costWhereStr))
            {
                joinType = "inner";
            }

            string sql = @"  select * from (select prj.ID,prj.Name,prj.Code,prj.ChargerDept,prj.ChargerDeptName,prj.ChargerUserName,
prj.Phase,prj.CreateDate,prj.ProjectClass,year(prj.CreateDate) BelongYear,
isnull(ProjectContractValue,0) as ProjectContractValue,
isnull(ProjectReceiptValue,0) as ProjectReceiptValue,
isnull(ProjectInvoiceValue,0) as ProjectInvoiceValue,
isnull(ProjectBadDebtValue,0) as ProjectBadDebtValue,
case when isnull(ProjectContractValue,0)=0 then 0 else 
Round(isnull(ProjectReceiptValue,0)/isnull(ProjectContractValue,0)*100,2) end as ReceiptRate,
isnull(ProjectContractValue,0)-isnull(ProjectReceiptValue,0)-isnull(ProjectBadDebtValue,0) as RemainValue,
isnull(ProjectInvoiceValue,0) - isnull(ProjectReceiptValue,0)-isnull(ProjectBadDebtValue,0) as ReceivableValue,
isnull(MarketReceiptValue,0) - isnull(MarketFactReceiptValue,0)-isnull(MarketBadDebtValue,0) as MarketReceivableValue,
isnull(ProjectCostDirectValue,0) as ProjectCostDirectValue,
isnull(ProjectCostPaymentValue,0) as ProjectCostPaymentValue,
isnull(ProjectCostValue,0) as ProjectCostValue,
case when isnull(ProjectReceiptValue,0)=0 then 0 else 
Round(isnull(ProjectCostDirectValue,0)/isnull(ProjectReceiptValue,0)*100,2) end as CostRate
 from S_I_Project prj

left join (select Sum(ProjectValue) as ProjectContractValue,ProjectID
from S_C_ManageContract_ProjectRelation 
where ProjectID is not null group by ProjectID ) ProjectContract 
on prj.ID=ProjectContract.ProjectID

left join (select 
Sum(FactReceiptValue) as ProjectReceiptValue,
Sum(FactInvoiceValue) as ProjectInvoiceValue,
Sum(case when MileStoneState='True' then ReceiptValue else 0 end) as MarketReceiptValue,
Sum(case when MileStoneState='True' then FactReceiptValue else 0 end) as MarketFactReceiptValue,
Sum(case when MileStoneState='True' then FactBadValue else 0 end) as MarketBadDebtValue,
Sum(FactBadValue) as ProjectBadDebtValue,ProjectInfo from S_C_ManageContract_ReceiptObj 
where ProjectInfo is not null group by ProjectInfo) ProjectValueInfo 
on prj.ID=ProjectValueInfo.ProjectInfo

{1} join (select Sum(case when CostType='{2}' then CostValue else 0 end) as ProjectCostDirectValue,
Sum(case when CostType='Payment' then CostValue else 0 end) as ProjectCostPaymentValue,
Sum(CostValue) as ProjectCostValue,ProjectID from s_fc_costinfo 
where ProjectID is not null and  ProjectType='{2}'  {0}
group by ProjectID) ProjectCostInfo 
on prj.ID=ProjectCostInfo.ProjectID) TableInfo  ";
            sql = string.Format(sql, costWhereStr, joinType, Const.defaultDirectCostType);
            var data = this.SqlHelper.ExecuteGridData(sql, qb);

            string sumSql = @"select isnull(Sum(ProjectContractValue),0) as ProjectContractValue,
 isnull(Sum(ProjectInvoiceValue),0) as ProjectInvoiceValue,
 isnull(Sum(ProjectReceiptValue),0) as ProjectReceiptValue,
 isnull(Sum(ProjectBadDebtValue),0) as ProjectBadDebtValue,
 isnull(Sum(RemainValue),0) as RemainValue,
 isnull(Sum(ReceivableValue),0) as ReceivableValue,
 isnull(Sum(MarketReceivableValue),0) as MarketReceivableValue,
 isnull(Sum(ProjectCostDirectValue),0) as ProjectCostDirectValue,
 isnull(Sum(ProjectCostPaymentValue),0) as ProjectCostPaymentValue,
 isnull(Sum(ProjectCostValue),0) as ProjectCostValue     
            from (" + sql + qb.GetWhereString() + ") sumTableInfo ";
            var sumDt = this.SqlHelper.ExecuteDataTable(sumSql);
            if (sumDt.Rows.Count > 0)
            {
                var sumRow = sumDt.Rows[0];
                data.sumData.SetValue("ProjectContractValue", Convert.ToDecimal(sumRow["ProjectContractValue"]).ToString("c"));
                data.sumData.SetValue("ProjectInvoiceValue", Convert.ToDecimal(sumRow["ProjectInvoiceValue"]).ToString("c"));
                data.sumData.SetValue("ProjectReceiptValue", Convert.ToDecimal(sumRow["ProjectReceiptValue"]).ToString("c"));
                data.sumData.SetValue("ProjectBadDebtValue", Convert.ToDecimal(sumRow["ProjectBadDebtValue"]).ToString("c"));
                data.sumData.SetValue("RemainValue", Convert.ToDecimal(sumRow["RemainValue"]).ToString("c"));
                data.sumData.SetValue("ReceivableValue", Convert.ToDecimal(sumRow["ReceivableValue"]).ToString("c"));
                data.sumData.SetValue("MarketReceivableValue", Convert.ToDecimal(sumRow["MarketReceivableValue"]).ToString("c"));
                data.sumData.SetValue("ProjectCostDirectValue", Convert.ToDecimal(sumRow["ProjectCostDirectValue"]).ToString("c"));
                data.sumData.SetValue("ProjectCostPaymentValue", Convert.ToDecimal(sumRow["ProjectCostPaymentValue"]).ToString("c"));
                data.sumData.SetValue("ProjectCostValue", Convert.ToDecimal(sumRow["ProjectCostValue"]).ToString("c"));
            }
            return Json(data);
        }
        
        public JsonResult GetCostItemSummaryInfo(QueryBuilder qb)
        {
            qb.PageSize = 0;
            string whereStr = qb.GetWhereString(false);
            string sql = @"select SubjectName,SubjectCode,ProjectID,Sum(CostValue) CostValue,'' id from s_fc_costinfo
where ProjectType='{1}' {0}
group by SubjectName,SubjectCode,ProjectID";
            sql = string.Format(sql, whereStr, Const.defaultDirectCostType);
            var data = this.SqlHelper.ExecuteDataTable(sql);
            return Json(data);
        }
    }
}
