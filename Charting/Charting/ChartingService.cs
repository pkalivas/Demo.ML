using System.Collections.Generic;
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

        public static PlotlyChart GetChart(List<int>[] clusters, List<List<float>> centers, List<List<float>> dataPoints)
        {
            var colors = new[] { "red", "green", "blue" };
            var clusterPoints = clusters
                .Select(c => c.Select(idx => dataPoints[idx]).ToList())
                .ToList();

            var scatters = clusterPoints
                .Select((clus, idx) => new Scatter
                {
                    mode = "markers",
                    x = clus.Select(point => point.First()),
                    y = clus.Select(point => point.Last()),
                    marker = new Marker { color = colors[idx] }
                })
                .ToList();

            var t = new Scatter
            {
                mode = "markers",
                x = centers.Select(p => p.First()),
                y = centers.Select(p => p.Last()),
                marker = new Marker { color = "black", size = 10, symbol = "cross" }
            };

            var chart = Chart.Plot(scatters.Concat(new []{t}).ToList());
            chart.WithTitle("");

            return chart;
        }

    }
}
