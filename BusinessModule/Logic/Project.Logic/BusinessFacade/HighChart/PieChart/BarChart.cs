﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Config;
using Config.Logic;
using Formula;
using Formula.Helper;

namespace Project.Logic
{
    public class BarChart : BaseChart
    {
        public xAxis xAxisInfo
        { get; set; }

        public yAxis yAxisInfo
        { get; set; }

        PlotOptions _plotOption;
        public override PlotOptions PlotOption
        {
            get
            {
                if (_plotOption == null)
                    _plotOption = new PlotOptions();
                var columPlot = new Dictionary<string, object>();
                columPlot.SetValue("pointPadding", 0.1);
                columPlot.SetValue("borderWidth", 0);
                columPlot.SetValue("softThreshold", false);

                _plotOption.SetValue("bar", columPlot);
                return _plotOption;
            }
        }

        public override Dictionary<string, object> Render()
        {
            var result = new Dictionary<string, object>();
            if (xAxisInfo == null) throw new Formula.Exceptions.BusinessException("条形图必须指定X轴对象");
            if (yAxisInfo == null) throw new Formula.Exceptions.BusinessException("条形图必须指定Y轴对象");
            this.Chart.Type = "bar";
            this.Chart.Is3D = this.Is3D;
            result.SetValue("chart", this.Chart.ToDic());
            if (this.TitleInfo != null)
                result.SetValue("title", TitleInfo.ToDic());
            if (this.SubTitleInfo != null)
                result.SetValue("subtitle", SubTitleInfo.ToDic());
            var seriesInfos = new List<Dictionary<string, object>>();
            foreach (var item in this.SeriesList)
                seriesInfos.Add(item.ToDic());
            result.SetValue("plotOptions", this.PlotOption.ToDic());
            result.SetValue("series", seriesInfos);
            result.SetValue("xAxis",xAxisInfo.ToDic());
            result.SetValue("yAxis", yAxisInfo.ToDic());
            var credits = new Dictionary<string, object>();
            credits.SetValue("enabled", false);
            result.SetValue("credits", credits);
            //if (this.Colors != null && this.Colors.Count > 0)
            //{
            //    result.SetValue("colors", this.Colors);
            //}
            return result;
        }
    }
}