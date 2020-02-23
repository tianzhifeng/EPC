﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Interface.Logic
{
    public enum SynType
    {
        Save,
        BatchSave,
        GetData,
        Remove,
        Upload,
        Download
    }
    public enum SynState
    {
        New,
        Error
    }
    public enum DataState
    {
        Normal,
        Deleted
    }

    #region EPM业务
    [Description("项目角色")]
    public enum ProjectRole
    {
        [Description("设计人")]
        Designer,
        [Description("校核人")]
        Collactor,
        [Description("审核人")]
        Auditor,
        [Description("审定人")]
        Approver,
        [Description("专业负责人")]
        MajorPrinciple,
        [Description("专业总工程师")]
        MajorEngineer,
        [Description("项目负责人")]
        ProjectManager,
        [Description("制图人")]
        Mapper,
        [Description("设总")]
        DesignManager,
        [Description("协作云项目经理")]
        CloudManager,
        [Description("协作云用户")]
        CloudUser
    }

    /// <summary>
    /// WBS节点类型
    /// </summary>
    [Description("WBS节点类型")]
    public enum WBSNodeType
    {
        /// <summary>
        /// 项目
        /// </summary>
        [Description("项目")]
        Project,
        [Description("子项")]
        SubProject,
        [Description("单体")]
        Entity,
        [Description("阶段")]
        Phase,
        [Description("专业")]
        Major,
        [Description("工作包")]
        Work,
        [Description("提资包")]
        CooperationPackage,
        [Description("工作类型")]
        PackageType,
        [Description("其它")]
        Other
    }

    /// <summary>
    /// 计划类别
    /// </summary>
    [Description("计划类别")]
    public enum MileStoneType
    {
        /// <summary>
        /// 项目计划
        /// </summary>
        [Description("项目计划")]
        Normal,

        /// <summary>
        /// 发图计划
        /// </summary>
        [Description("发图计划")]
        Major,

        /// <summary>
        /// 提资计划
        /// </summary>
        [Description("提资计划")]
        Cooperation

    }
    
    [Description("设计输入文件夹类别")]
    public enum DesignInputType
    {
        Virtual,
        Actual
    }

    public enum AuditType
    {
        /// <summary>
        /// 设计
        /// </summary>
        [Description("设计提交")]
        Design,

        /// <summary>
        /// 校核
        /// </summary>
        [Description("校核")]
        Collact,

        /// <summary>
        /// 审核
        /// </summary>
        [Description("审核")]
        Audit,

        /// <summary>
        /// 审定
        /// </summary>
        [Description("审定")]
        Approve,
        /// <summary>
        /// 项目经理
        /// </summary>
        [Description("项目经理")]
        ProjectManager,
        /// <summary>
        /// 设总
        /// </summary>
        [Description("设总")]
        DesignManager,
        /// <summary>
        /// 制图人
        /// </summary>
        [Description("制图人")]
        Mapper,
        /// <summary>
        /// 专业负责人
        /// </summary>
        [Description("专业负责人")]
        MajorPrinciple,
        /// <summary>
        /// 专业总工程师
        /// </summary>
        [Description("专业总工程师")]
        MajorEngineer,
        /// <summary>
        /// 部门负责人
        /// </summary>
        [Description("部门负责人")]
        DeptManager,
        /// <summary>
        /// 批准
        /// </summary>
        [Description("批准")]
        Agree,
        /// <summary>
        /// 会签
        /// </summary>
        [Description("会签")]
        CounterSign

    }

    #endregion
}
