using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Collections;

namespace ChartsAndGraphsTestingAzureFunction
{
    public  class Function1
    {
        static List<double> SortedList = new List<double>();
        static List<double> list;
        static ChartData Chartdata = new ChartData();
        static string UnitValue = string.Empty;
        static string GraphUrl = string.Empty;

        [FunctionName("Function1")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            try
            {
                //While getting data from MarsBot To Return ChartUrl
                string body = await req.Content.ReadAsStringAsync();
                JObject json = JObject.Parse(body);
                //-------------------------------------------------------               
                //Data-(body) From Local Json File
                //string body1 = File.ReadAllText("G:/BOT/ChartsAndGraphsTestingAzureFunction/ChartsAndGraphsTestingAzureFunction/Image/Graph.json");
                var graphtypeObject = JObject.Parse(body)["xvals"];
                string graphtype = json["type"].ToString();
                Chartdata = JsonConvert.DeserializeObject<ChartData>(body);
                ArrayList Xvalue = Chartdata.xvals;
                ArrayList Yvalue = Chartdata.yvals;
                list = new List<double>(Yvalue.Count);
                for (int i = 0; i < Yvalue.Count; i++)
                {
                    list.Add(Convert.ToDouble(Yvalue[i]));
                }
                double MaxValue = list.Max();
                double NoOfDigits = Math.Ceiling(Math.Log10(MaxValue));
                if (NoOfDigits > 0)
                {
                    if (NoOfDigits >= 4 && NoOfDigits <= 6)
                    {
                        SortedList = ConvertedList(NoOfDigits);
                        UnitValue = "Thousands";
                    }
                    else if (NoOfDigits >= 7 && NoOfDigits <= 9)
                    {
                        SortedList = ConvertedList(NoOfDigits);
                        UnitValue = "Millions";
                    }
                    else if (NoOfDigits >= 10 && NoOfDigits <= 12)
                    {
                        SortedList = ConvertedList(NoOfDigits);
                        UnitValue = "Billions";
                    }
                    else if (NoOfDigits >= 13 && NoOfDigits <= 15)
                    {
                        SortedList = ConvertedList(NoOfDigits);
                        UnitValue = "Trillions";
                    }
                    else
                    {
                        SortedList = list;
                    }
                }
                else
                {

                    return null;
                }
                ArrayList YvalsList = new ArrayList(SortedList);
                if (graphtype.Equals("bar", StringComparison.InvariantCultureIgnoreCase) && body != null)
                {
                    chartCreation(Chartdata.xvals, YvalsList);
                    
                    var webClient = new WebClient();
                    byte[] imageBytes = webClient.DownloadData(@"../../../Image/GraphImageCard.png");
                    GraphUrl = "data:image/png;base64," + Convert.ToBase64String(imageBytes);
                    string GraphImageUrl = GraphUrl;
                    GraphUrl = string.Empty;
                    return req.CreateResponse(HttpStatusCode.OK, GraphImageUrl);
                }
                else
                {
                    return req.CreateResponse(HttpStatusCode.BadRequest, "Please Pass The Data Required To display the Graph");
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<double> ConvertedList(double NoOfDigits)
        {
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (NoOfDigits >= 4 && NoOfDigits <= 6)
                    {
                        double ConvertedNum = list[i] / 1000;
                        SortedList.Add(ConvertedNum);
                    }
                    else if (NoOfDigits >= 7 && NoOfDigits <= 9)
                    {
                        double ConvertedNum = list[i] / 1000000;
                        SortedList.Add(ConvertedNum);
                    }
                    else if (NoOfDigits >= 10 && NoOfDigits <= 12)
                    {
                        double ConvertedNum = list[i] / 1000000000;
                        SortedList.Add(ConvertedNum);
                    }
                    else if (NoOfDigits >= 13 && NoOfDigits <= 15)
                    {
                        double ConvertedNum = list[i] / 1000000000000;
                        SortedList.Add(ConvertedNum);
                    }

                }
                return SortedList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Chart chartCreation(ArrayList xaxislist, ArrayList yaxislist)
        {
            try
            {

                var chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
                chart.Size = new Size(700, 400);

                var chartArea = new ChartArea();
                chartArea.AxisX.MajorGrid.LineColor = Color.LightGray;
                chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
                chartArea.AxisY.Title = "Values in (" + UnitValue + ")";
                chartArea.AxisY.TitleFont = new Font("Trebuchet MS", 20F, System.Drawing.FontStyle.Bold);
                chartArea.BorderWidth = 600;
                chartArea.AlignmentOrientation = AreaAlignmentOrientations.Vertical;
                chartArea.AlignmentStyle = AreaAlignmentStyles.Position;
                chartArea.Area3DStyle.Enable3D = true;
                chartArea.AxisX.LabelStyle.Font = new Font("Trebuchet MS", 15F, System.Drawing.FontStyle.Bold);
                chartArea.AxisY.LabelStyle.Font = new Font("Trebuchet MS", 15F, System.Drawing.FontStyle.Bold);

                chart.ChartAreas.Add(chartArea);
                //List<Series> Serieslist = new List<Series>();
                Series series = new Series();
               // Series series2 = new Series();
                //Serieslist.Add(series);
                //Serieslist.Add(series2);

                series.Name = "Series1";
                //series.LegendText = "2017";
                //series.Legend = series.LegendText;
                series.LegendText = "2018";
                
                series.Legend = series.LegendText;
                series.ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), Chartdata.type);
                series.XValueType = ChartValueType.String;
                chart.Series.Add(series);
                chart.Series["Series1"].Points.DataBindXY(xaxislist, yaxislist);


                //series2.Name = "Series2";
                //series2.LegendText = "2017";
                //series.IsVisibleInLegend=true;

                ////series2.LegendText = "2018";
                ////series2.Legend = series.LegendText;
                //series2.ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), Chartdata1.type);
                //series2.XValueType = ChartValueType.String;
                //chart.Series.Add(series2);
                //chart.Series["Series2"].Points.DataBindXY(Chartdata1.xvals,Chartdata1.yvals);
                chart.Legends.Add(series.Legend);

                chart.Invalidate();
                chart.SaveImage(@"../../../Image/GraphImageCard.png", ChartImageFormat.Png);
                SortedList.Clear();
                list.Clear();
                UnitValue = string.Empty;
                return chart;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //public static string bingPathToAppDir(string localPath)
        //{
        //    string currentDir = Environment.CurrentDirectory;
        //    DirectoryInfo directory = new DirectoryInfo(
        //        Path.GetFullPath(Path.Combine(currentDir, @"./" +localPath)));
        //    return directory.ToString();
        //}
    }
}
