﻿using System.ComponentModel;
namespace Monitor.Logic.Enum
{
    /// <summary>
    /// 统计条件
    /// </summary>
    public enum EnumBusiness
    {
        /// <summary>
        /// 收款总览
        /// </summary>
        [Description("收款总览")]
        Receipt,

        /// <summary>
        /// 当前合同余额
        /// </summary>
        [Description("当前合同余额")]
        CurrentSurplusContract,

        /// <summary>
        /// 当前应收款
        /// </summary>
        [Description("当前应收款")]
        CurrentAccountReceivable,

        /// <summary>
        /// 当季实际收款
        /// </summary>
        [Description("当季实际收款")]
        CurrentQuarterActualReceipt,

        /// <summary>
        /// 当月实际收款
        /// </summary>
        [Description("当月实际收款")]
        CurrentMonthActualReceipt,

        /// <summary>
        /// 本周实际到款
        /// </summary>
        [Description("本周实际到款")]
        CurrentWeekActualReceipt,

        /// <summary>
        /// 本年计划收款
        /// </summary>
        [Description("本年计划收款")]
        CurrentYearPlanReceipt,

        /// <summary>
        /// 本年后计划收款
        /// </summary>
        [Description("本年后计划收款")]
        CurrentYearAfterPlanReceipt,

        /// <summary>
        /// 当季计划收款
        /// </summary>
        [Description("当季计划收款")]
        CurrentQuarterPlanReceipt,

        /// <summary>
        /// 本季后计划收款
        /// </summary>
        [Description("本季后计划收款")]
        CurrentQuarterAfterPlanReceipt,

        /// <summary>
        /// 本月计划收款
        /// </summary>
        [Description("本月计划收款")]
        CurrentMonthPlanReceipt,

        /// <summary>
        /// 本月后计划收款
        /// </summary>
        [Description("本月后计划收款")]
        CurrentMonthAfterPlanReceipt,

        /// <summary>
        /// 合同总览
        /// </summary>
        [Description("合同总览")]
        Contract,

        /// <summary>
        /// 当年待签合同额
        /// </summary>
        [Description("当年待签合同额")]
        CurrentUnSignContract,

        /// <summary>
        /// 当季已签合同
        /// </summary>
        [Description("当季已签合同")]
        CurrentQuarterSignContract,

        /// <summary>
        /// 当月已签合同
        /// </summary>
        [Description("当月已签合同")]
        CurrentMonthSignContract,

        /// <summary>
        /// 本周已签合同额
        /// </summary>
        [Description("本周已签合同额")]
        CurrentWeekSignContract
    }

    /// <summary>
    /// 查询类型
    /// </summary>
    public enum EnumQueryType
    {
        /// <summary>
        /// 客户
        /// </summary>
        [Description("客户")]
        Customer,

        /// <summary>
        /// 部门
        /// </summary>
        [Description("部门")]
        Department,

        /// <summary>
        /// 责任人
        /// </summary>
        [Description("责任人")]
        ManagerUser,

        /// <summary>
        /// 不分组
        /// </summary>
        [Description("不分组")]
        NotGroup
    }


    /// <summary>
    /// 指标类别
    /// </summary>
    [Description("指标类别")]
    public enum IndicatorType
    {
        /// <summary>
        /// 年度指标
        /// </summary>
        [Description("年度指标")]
        YearIndicator,
        /// <summary>
        /// 季度指标
        /// </summary>
        [Description("季度指标")]
        QuarterIndicator,
        /// <summary>
        /// 月度指标
        /// </summary>
        [Description("月度指标")]
        MonthIndicator
    }

    /// <summary>
    ///  指标组织类别
    /// </summary>
    [Description("指标组织类别")]
    public enum IndicatorOrgType
    {   /// <summary>
        /// 公司指标
        /// </summary>
        [Description("公司指标")]
        Company,
        /// <summary>
        /// 部门指标
        /// </summary>
        [Description("部门指标")]
        Org,
        /// <summary>
        /// 个人指标
        /// </summary>
        [Description("个人指标")]
        Person
    }

    /// <summary>
    /// 发票状态
    /// </summary>
    [Description("发票状态")]
    public enum InvoiceState
    {
        [Description("正常")]
        Normal,
        [Description("作废")]
        Invalid
    }

    [Description("发票申请状态")]
    public enum InvoiceApplyState
    {
        [Description("申请未通过")]
        New,
        [Description("申请通过")]
        Wait,
        [Description("已开票")]
        Complete
    }

    /// <summary>
    /// 
    /// </summary>
    [Description("发票类型")]
    public enum InvoiceType
    {
        /// <summary>
        /// 增值税产品发票
        /// </summary>
        [Description("增值税产品发票")]
        ProductVAT = 6,

        /// <summary>
        /// 增值税专用发票
        /// </summary>
        [Description("增值税专用发票")]
        SpecialVat = 17,

        /// <summary>
        /// 增值税普通发票
        /// </summary>
        [Description("增值税普通发票")]
        VAT = 0,

        /// <summary>
        /// 收据
        /// </summary>
        [Description("收据")]
        Receipt = 0
    }

    /// <summary>
    /// 发票收款状态
    /// </summary>
    [Description("发票收款状态")]
    public enum InvoiceReceiptState
    {
        [Description("全部到款")]
        Receipt,
        [Description("未到款")]
        None,
        [Description("部分到款")]
        Part
    }

    /// <summary>
    /// 合同是否已签约
    /// </summary>
    [Description("是否已签约")]
    public enum ContractIsSigned
    {
        [Description("未签约")]
        UnSigned,
        [Description("已签约")]
        Signed,
    }

    [Description("是否")]
    public enum YesOrNo
    {
        [Description("是")]
        Yes,
        [Description("否")]
        No
    }

    /// <summary>
    /// 合同状态
    /// </summary>
    [Description("合同状态")]
    public enum ContractState
    {
        /// <summary>
        /// 登记
        /// </summary>
        [Description("登记")]
        Regist,

        /// <summary>
        /// 签约
        /// </summary>
        [Description("签约")]
        Sign,

        /// <summary>
        /// 评审
        /// </summary>
        [Description("评审")]
        Review,

        /// <summary>
        /// 履行
        /// </summary>
        [Description("履行")]
        Execute,

        /// <summary>
        /// 关闭
        /// </summary>
        [Description("关闭")]
        Finish,

        /// <summary>
        /// 暂停
        /// </summary>
        [Description("暂停")]
        Pause,

        /// <summary>
        /// 终止
        /// </summary>
        [Description("终止")]
        Terminate
    }

    /// <summary>
    /// 收款计划是否坏账
    /// </summary>
    [Description("收款计划是否坏账")]
    public enum PlanReceiptIsBadDebt
    {
        [Description("是")]
        T,
        [Description("否")]
        F,
    }
    /// <summary>
    /// 收款计划状态
    /// </summary>
    [Description("收款计划状态")]
    public enum PlanReceiptState
    {

        /// <summary>
        /// 未到款
        /// </summary>
        [Description("部分到款")]
        PartReceipted,

        /// <summary>
        /// 未完成
        /// </summary>
        [Description("未完成")]
        UnFinished,

        /// <summary>
        /// 未到款
        /// </summary>
        [Description("未到款")]
        UnReceipt,

        /// <summary>
        /// 坏账
        /// </summary>
        [Description("坏账")]
        BadDebt,

        /// <summary>
        /// 已到款
        /// </summary>
        [Description("已到款")]
        Receipted
    }

    /// <summary>
    /// 前期项目状态
    /// </summary>
    [Description("前期项目状态")]
    public enum ProjectState
    {
        /// <summary>
        /// 创建
        /// </summary>
        [Description("创建")]
        Create,

        /// <summary>
        /// 策划
        /// </summary>
        [Description("策划")]
        Plan,

        /// <summary>
        /// 执行
        /// </summary>
        [Description("执行")]
        Execute,

        /// <summary>
        /// 完成
        /// </summary>
        [Description("完成")]
        Finish,

        /// <summary>
        /// 暂停
        /// </summary>
        [Description("暂停")]
        Pause,

        /// <summary>
        /// 终止
        /// </summary>
        [Description("终止")]
        Terminate
    }

    [Description("线索状态")]
    public enum ClueState
    {
        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Failed,
        /// <summary>
        /// 暂停
        /// </summary>
        [Description("暂停")]
        Pause,
        /// <summary>
        /// 延缓
        /// </summary>
        [Description("延缓")]
        Delay,
        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Succeed,
        /// <summary>
        /// 终止
        /// </summary>
        [Description("终止")]
        Terminate
    }

    /// <summary>
    /// 经营项目状态(需要杨总确定这个枚举是否有必要)
    /// </summary>
    public enum MarketProjectState
    {
        /// <summary>
        /// 盘摸
        /// </summary>
        [Description("盘摸")]
        Create,

        /// <summary>
        /// 跟踪
        /// </summary>
        [Description("跟踪")]
        Tracking,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Failed,

        /// <summary>
        /// 延缓
        /// </summary>
        [Description("延缓")]
        Delay,

        /// <summary>
        /// 中止
        /// </summary>
        [Description("中止")]
        Suspend,

        /// <summary>
        /// 已签
        /// </summary>
        [Description("已签")]
        Signed,

        /// <summary>
        /// 已立项
        /// </summary>
        [Description("已立项")]
        Builded
    }


    public enum AnlysisType
    {
        [Description("实际收款")]
        ReceiptAmount,
        [Description("已签合同")]
        ContractAmount
    }

    public enum GraphicPieGroupType
    {
        [Description("行业")]
        Industry,
        [Description("地域")]
        Province
    }

    public enum PieAnlysisType
    {
        [Description("当年实际收款额")]
        CurrentYearReceiptValue,
        [Description("当年已签合同额")]
        CurrentYearSignContractValue,
        [Description("当前应收款")]
        ReceivableValue,
        [Description("当前合同余额")]
        RemainContractValue,
        [Description("当前待签合同额")]
        UnSignContractValue
    }

    public enum CustomerAnlysisType
    {
        [Description("实际收款额")]
        ReceiptValue,
        [Description("已签合同额")]
        ContractValue,
        [Description("应收款")]
        ReceivableValue,
        [Description("合同余额")]
        RemainContractValue,
        [Description("待签合同额")]
        UnContractValue
    }

    public enum ContractAnlysisType
    {
        [Description("合同额")]
        ContractValue,
        [Description("实际收款")]
        ReceiptValue,
        [Description("应收款")]
        ReceivableValue,
        [Description("合同余额")]
        RemainContractValue
    }
}