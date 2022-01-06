﻿using System.Collections.Generic;
using System.Linq;
using XPlot.Plotly;

namespace Charting
{

    public static class ChartingService
    {
        public static PlotlyChart GetChart(ChartRequest request)
        {
            var prediction = new Scatter
            {
                name = request.YOneName,
                x = request.YOneData.Select((_, idx) => idx).ToList(),
                y = request.YOneData.Select(val => val).ToList(),
            };

            var actualValues = new Scatter
            {
                name = request.YTwoName,
                x = request.YTwoData.Select((_, idx) => idx).ToList(),
                y = request.YTwoData.Select(val => val).ToList(),
                yaxis = "y2"
            };

            var layout = new Layout.Layout
            {
                title = request.Title,
                yaxis = new Yaxis
                {
                    title = request.YOneName
                },
                yaxis2 = new Yaxis
                {
                    side = "right",
                    overlaying = "y",
                    title = request.YTwoName
                }
            };

            var chart = Chart.Plot(new[] { prediction, actualValues });
            chart.WithTitle("");
            chart.WithLayout(layout);

            return chart;
        }

        public static PlotlyChart GetChart(List<int>[] clusters, float[,][] centers, float[,][] datapoints)
        {
            var clusterPoints = datapoints
                .Select(c => c.Select(idx => datapoints[idx]))
                .ToList();

            return null;
            //var chart = Chart.Plot(new[] { prediction, actualValues });
            //chart.WithTitle("");
            //chart.WithLayout(layout);

            //return chart;
        }

    }
}
