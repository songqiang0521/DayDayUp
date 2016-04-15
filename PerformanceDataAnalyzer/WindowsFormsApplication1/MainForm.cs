using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


using LumenWorks.Framework.IO.Csv;


using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;
using OxyPlot.Axes;
using System.Diagnostics;




namespace Hollysys.LeakAnalyzer
{

    public partial class MainForm : Form
    {
        private string _fileName;
        private ColumnItem[] _columns;
        private DateTime[] _times;
        private bool allItemsBeSeen;


        private List<string> allItems;
        private List<string> suspiciousItems;
        private bool hasBeenAnalyzed;
        /// <summary>
        /// 容量为20，保存最近20个访问过的model
        /// </summary>
        private List<PlotModel> models;
        private const int CACHE_COUNT = 20;

        private Point downPoint;
        private Point upPoint;



        /// <summary>
        /// 填充时间、数据
        /// </summary>
        /// <param name="filename"></param>
        private bool FillDatas(string filename)
        {

            //打开CSV文件,带标题,64K缓冲
            using (var fs = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var s = new StreamReader(fs))
            using (CsvReader csv = new CsvReader(s, true, 0xFFFF))
            {
                int fieldCount = csv.FieldCount;
                _columns = new ColumnItem[fieldCount];

                string[] headers = csv.GetFieldHeaders();

                //填充每列的标题
                for (int i = 0; i < fieldCount; i++)
                {
                    _columns[i].HeaderName = headers[i];

                    //DataType type = (_columns[i].HeaderName.Contains(@"%")
                    //                || _columns[i].HeaderName.Contains(@"/")
                    //                || _columns[i].HeaderName.Contains("Elapsed Time")
                    //                ||_columns[i].HeaderName.Contains(@"Avg")
                    //                ||_columns[i].HeaderName.Contains(@"System Up Time"))
                    //                ? DataType.DOUBLE : DataType.UINT64;

                    //将所有数据当作DOUBLE来处理
                    DataType type = DataType.DOUBLE;

                    _columns[i].DataType = type;

                    //就先估计10*1000行数据吧
                    _columns[i].Values = new List<string>(10 * 1000);
                }

                while (csv.ReadNextRecord())
                {
                    for (int i = 0; i < fieldCount; i++)
                    {
                        _columns[i].Values.Add(csv[i]);
                    }

                }
            }//end csvreader


            //填充时间
            string firstTime = _columns[0].Values[0];
            string secondTime = _columns[0].Values[1];
            if (firstTime != string.Empty && secondTime != string.Empty)
            {
                DateTime dt1;
                DateTime dt2;

                bool firstSuccess = DateTime.TryParse(firstTime, out dt1);
                bool secondSuccess = DateTime.TryParse(secondTime, out dt2);
                if (firstSuccess && secondSuccess)
                {
                    TimeSpan span = dt2 - dt1;
                    int spanSecond = span.Seconds;
                    _times = new DateTime[_columns[0].Values.Count];

                    _times[0] = dt1;
                    for (int i = 1; i < _times.Length; i++)
                    {
                        //不依次解析字符串，而是按照时间间隔递增填充
                        _times[i] = _times[i - 1].AddSeconds(spanSecond);
                    }
                }
                else
                {
                    MessageBox.Show("CSV文件解析错误，时间格式不正确");
                    return false;
                }
            }

            return true;
        }


        public MainForm()
        {
            InitializeComponent();
            models = new List<PlotModel>(CACHE_COUNT);

            this.allItems = new List<string>(1000);
            this.suspiciousItems = new List<string>(100);
            allItemsBeSeen = true;
            hasBeenAnalyzed = false;

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        #region 同步版本
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            _fileName = Path.GetFullPath(dlg.FileName);
            textBox1.Text = _fileName;
            if (!FillDatas(_fileName))
            {
                return;
            }

            listBox1.Items.Clear();
            allItems.Clear();
            suspiciousItems.Clear();
            for (int i = 1; i < _columns.Length; i++)
            {
                this.allItems.Add(_columns[i].HeaderName);
                listBox1.Items.Add(_columns[i].HeaderName);
            }



            //for (int i = 1; i < _columns.Length; i++)
            //{
            //    _columns[i].bgColor = SystemColors.WindowText;
            //}

            //_columns[1].bgColor = Color.Red;
            //_columns[10].bgColor = Color.RoyalBlue;
            //_columns[20].bgColor = Color.Purple;
            //_columns[21].bgColor = Color.Pink;
            //_columns[25].bgColor = Color.Salmon;






            listBox1.SelectedIndex = 0;
            allItemsBeSeen = true;
            hasBeenAnalyzed = false;


        }
        #endregion

        #region //异步版本
        //private async void button1_Click(object sender, EventArgs e)
        //{

        //    OpenFileDialog dlg = new OpenFileDialog();
        //    dlg.CheckPathExists = true;
        //    dlg.ReadOnlyChecked = true;
        //    dlg.DefaultExt = ".csv";
        //    if (dlg.ShowDialog() != DialogResult.OK)
        //    {
        //        return;
        //    }

        //    _fileName = Path.GetFullPath(dlg.FileName);
        //    textBox1.Text = _fileName;
        //    await FillDatasAsync(_fileName);

        //    for (int i = 1; i < _columns.Length; i++)
        //    {
        //        listBox1.Items.Add(_columns[i].HeaderName);
        //    }
        //}
        #endregion

        #region //异步函数

        /// <summary>
        /// 异步填充数据
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        //private Task FillDatasAsync(string fileName)
        //{
        //    return Task.Run(new Action(() =>
        //    {
        //        FillDatas(fileName);
        //    }));
        //}
        #endregion

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;

            //版本2-增加缓存



            for (int i = 1; i < _columns.Length; i++)
            {
                if (_columns[i].HeaderName == (string)listBox1.SelectedItem)
                {
                    index = i;
                    break;
                }
            }


            //index += 1;






            #region DOUBLE
            if (_columns[index].DataType == DataType.DOUBLE)
            {
                int count = _columns[index].Values.Count;
                List<DoubleItem> data = new List<DoubleItem>(count + 10);//capacity多十个余量

                for (int i = 0; i < count; i++)
                {
                    DoubleItem item = new DoubleItem();
                    item.Date = _times[i];
                    
                    if (_columns[index].Values[i] == " " ||_columns[index].Values[i]==""|| _columns[index].Values[i] == "0" || string.IsNullOrEmpty(_columns[index].Values[i]))
                    {

                        item.Value = 0.0;
                    }
                    else
                    {
                        item.Value = Convert.ToDouble(_columns[index].Values[i]);
                    }

                    data.Add(item);
                }


                var pm = new PlotModel();
                pm.Title = _columns[index].HeaderName;  //"Trigonometric functions";
                pm.PlotType = PlotType.XY;
                pm.Background = OxyColors.White;
                //pm.Culture = CultureInfo.CurrentCulture;


                //横坐标
                var dateTimeAxis1 = new DateTimeAxis
                {
                    //CalendarWeekRule = CalendarWeekRule.FirstDay,
                    FirstDayOfWeek = DayOfWeek.Monday,
                    Position = AxisPosition.Bottom,
                    Title = "Date",
                    StringFormat = "yyyy-MM-dd\nhh:mm:ss",


                };
                pm.Axes.Add(dateTimeAxis1);

                //纵坐标
                var linearAxis1 = new LinearAxis();
                linearAxis1.Position = AxisPosition.Left;
                linearAxis1.MaximumPadding = 0.3;
                linearAxis1.MinimumPadding = 0.2;
                linearAxis1.Title = "Value";
                pm.Axes.Add(linearAxis1);

                //数据
                LineSeries line = new LineSeries();
                line.Color = OxyColor.FromArgb(255, 78, 154, 6);
                line.MarkerFill = OxyColor.FromArgb(255, 78, 154, 6);
                line.MarkerStroke = OxyColors.ForestGreen;
                line.StrokeThickness = 2;
                line.DataFieldX = "Date";//dt
                line.DataFieldY = "Value";//value
                line.ItemsSource = data;
                line.TrackerFormatString = "{1}: {2:G}\n{3}: {4:0.00}";


                pm.Series.Add(line);

                this.plotView1.Model = pm;

                //最起码应该有十个以上的点才能够分析
                Debug.Assert(_columns[index].Values.Count > 100);
                //统计double[]的统计信息
                Analyzer analyzer = new Analyzer();

                const int excludeCount = 5;

                //values去除了前excludeCount个值和后excludeCount个值
                double[] values = new double[_columns[index].Values.Count - excludeCount * 2];
                for (int i = 0; i < values.Length; i++)
                {
                    if (_columns[index].Values[i] == " " ||_columns[index].Values[i]==""|| _columns[index].Values[i] == "0" || string.IsNullOrEmpty(_columns[index].Values[i + excludeCount]))
                    {
                        values[i] = 0.0;
                    }
                    else
                    {
                        values[i] = Convert.ToDouble(_columns[index].Values[i]);
                    }
                }

                unsafe
                {

                    //分析
                    fixed (double* pValues = values)
                    {

                        textBox2.BackColor = SystemColors.Window;
                        textBox2.Text = "";

                        //认为趋势泄漏的可能性大小对应的颜色从低到高为【蓝色->黄色->红色】


                        //if(条件...){蓝色}
                        //
                        //if(条件...){黄色}
                        //
                        //if(条件...){红色}


                        //判定条件1---LBTF

                        //判定条件2---LBTFBS


                        //判定条件3
                        //最大值、最小值、平均值，三者相差多不多
                        //最大值-平均值~=平均值-最小值，且此差值为最大值减去最小值的二分之一

                        //判定条件4
                        //

                        double max = 0.0;
                        double min = 0.0;
                        analyzer.GetMaxMinValue(values, ref max, ref min);

                        double percent_1to1 = analyzer.GetPercentOfLatterBigThanFormer(pValues, values.Length);
                        double percent_10to1 = analyzer.GetPercentOfLatterBigThanFormerBySections(pValues, values.Length, values.Length / 10);
                        double percent_20to1 = analyzer.GetPercentOfLatterBigThanFormerBySections(pValues, values.Length, values.Length / 20);
                        double percent_50to1 = analyzer.GetPercentOfLatterBigThanFormerBySections(pValues, values.Length, values.Length / 50);
                        
                        
                        //double100 要成立的话，至少需要500以上的点了
                        //double percent_100to1 = analyzer.GetPercentOfLatterBigThanFormerBySections(pValues, values.Length, values.Length / 100);

                        #region mean
                        double[] valuesFront = new double[values.Length / 10];
                        double[] valuesBetween = new double[values.Length / 10];
                        double[] valuesEnd = new double[values.Length / 10];

                        for (int i = 0; i < valuesFront.Length; i++)
                        {
                            valuesFront[i] = values[i];
                        }

                        for (int i = 0; i < valuesBetween.Length; i++)
                        {
                            valuesBetween[i] = values[values.Length / 2 - valuesBetween.Length / 2 + i];
                        }

                        for (int i = 0; i < valuesEnd.Length; i++)
                        {
                            valuesEnd[i] = values[values.Length - 1 - i];
                        }
                        #endregion
                        double frontMean = analyzer.GetMeanValue(valuesFront);
                        double betweenMean = analyzer.GetMeanValue(valuesBetween);
                        double endMean = analyzer.GetMeanValue(valuesEnd);

                        //
                        //1.后十分之一点的均值比前十分之一点的均值大，且差值大约为最大值减去最小值的二分之一以上
                        //比如
                        //
                        double dif = max - min;
                        if ((endMean - betweenMean) > dif / 2
                            && (betweenMean - frontMean) > dif / 2
                            || (endMean - frontMean) > dif * 2 / 3)
                        {
                            //textBox2.BackColor = Color.Blue;
                            textBox2.Text += "总体趋势增大" + Environment.NewLine;
                        }


                        //
                        //2.如果LBTF和LBTFBS都很大，则基本可以肯定曲线是上升的
                        //
                        if (percent_1to1 > 0.8
                            ||
                            ( percent_10to1 > 0.8
                            && percent_10to1 <= percent_20to1  //LBTFBS逐渐增大
                            && percent_20to1 <= percent_50to1
                            //&& percent_50to1 <= percent_100to1
                            && frontMean < betweenMean
                            && betweenMean < endMean)
                            )
                        {
                            //textBox2.BackColor = Color.Red;
                            textBox2.Text += "直线上升" + Environment.NewLine;

                        }

                        ////3.如果LBTF很小，而LBTFBS比较大，那么基本可以肯定曲线是阶梯形状
                        ////比如perf.csv中的Total virtual bytes peak
                        //if (percent_1to1<0.3                //LBTF较小
                        //    &&percent_10to1>0.7             //LBTFBS较大
                        //    &&percent_10to1<=percent_20to1  //LBTFBS逐渐增大
                        //    &&percent_20to1<=percent_50to1
                        //    &&percent_50to1<=percent_100to1
                        //    &&frontMean<betweenMean
                        //    &&betweenMean<endMean)
                        //{
                        //    textBox2.BackColor = Color.Yellow;
                        //}



                        textBox2.Text += "最大值:" + max.ToString("F3")+Environment.NewLine;
                        textBox2.Text += "最小值:" + min.ToString("F3") + Environment.NewLine;
                        textBox2.Text += "差值百分比[(max-min)/min]:" + ((max - min) / min).ToString("F3") + Environment.NewLine;




                    }
                }

            }

            #endregion

            #region UINT64
            else if (_columns[index].DataType == DataType.UINT64)
            {
                int count = _columns[index].Values.Count;
                List<UInt64Item> data = new List<UInt64Item>(count + 10);//capacity多十个余量

                for (int i = 0; i < count; i++)
                {
                    UInt64Item item = new UInt64Item();
                    item.Date = _times[i];
                    if (_columns[index].Values[i] == " ")
                    {
                        item.Value = 0;
                    }
                    else
                    {
                        item.Value = Convert.ToUInt64(_columns[index].Values[i]);
                    }

                    data.Add(item);
                }

                var pm = new PlotModel();
                pm.Title = _columns[index].HeaderName;//"Trigonometric functions";
                pm.PlotType = PlotType.XY;
                pm.Background = OxyColors.White;


                //横坐标
                var dateTimeAxis1 = new DateTimeAxis
                {
                    //CalendarWeekRule = CalendarWeekRule.FirstDay,
                    FirstDayOfWeek = DayOfWeek.Monday,
                    Position = AxisPosition.Bottom,
                    Title = "Date",
                    StringFormat = "yyyy-MM-dd\nhh:mm:ss",
                };
                pm.Axes.Add(dateTimeAxis1);

                //纵坐标
                var linearAxis1 = new LinearAxis();
                linearAxis1.Position = AxisPosition.Left;
                linearAxis1.MaximumPadding = 0.3;
                linearAxis1.MinimumPadding = 0.2;
                linearAxis1.Title = "Value";
                pm.Axes.Add(linearAxis1);

                //数据
                LineSeries line = new LineSeries();
                line.Color = OxyColor.FromArgb(255, 78, 154, 6);
                line.MarkerFill = OxyColor.FromArgb(255, 78, 154, 6);
                line.MarkerStroke = OxyColors.ForestGreen;
                line.StrokeThickness = 2;
                line.DataFieldX = "Date";
                line.DataFieldY = "Value";
                line.ItemsSource = data;
                line.TrackerFormatString = "{1}: {2:G}\n{3}: {4:0.00}";

                pm.Series.Add(line);
                this.plotView1.Model = pm;
            }
            #endregion

        }

        private void plotView1_MouseDown(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {


                downPoint = new Point(e.X, e.Y);
            }
        }


        /// <summary>
        /// 如果鼠标抬起位置和按下时位置不同，则不显示EXPORT菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plotView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                upPoint = new Point(e.X, e.Y);
                if (downPoint != upPoint)
                {
                    this._cms.Visible = false;

                }
                else
                {
                    this._cms.Visible = true;
                }

            }

        }

        private void _cms_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Name == "Export")
            {
                if (this.plotView1.Model == null)
                {
                    return;
                }

                var pngExporter = new PngExporter();
                string pngFilePath = Path.GetDirectoryName(_fileName);
                string item = listBox1.SelectedItem.ToString().Replace('%', '-').Replace('\\', '-').Replace('*', '-');
                var stream = File.Create(pngFilePath + "\\" + item + ".png");
                pngExporter.Export(this.plotView1.Model, stream);
                stream.Dispose();

            }
        }


        /// <summary>
        ///仅显示包含过滤字符串的条目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

            listBox1.Items.Clear();
            if (string.IsNullOrWhiteSpace(textBox3.Text))
            {
                //listBox1
            }



            IEnumerable<string> query;
            if (allItemsBeSeen)
            {
                query = from col in allItems
                        where col.Contains(textBox3.Text)
                        select col;

            }
            else
            {
                query = from col in suspiciousItems
                        where col.Contains(textBox3.Text)
                        select col;

            }


            listBox1.Items.AddRange(query.ToArray());

        }


        /// <summary>
        /// 分析，并在textbox中显示可疑条目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAnalyze_Click(object sender, EventArgs e)
        {

            if (_columns == null)
            {
                return;
            }
            if (hasBeenAnalyzed)
            {
                listBox1.Items.Clear();
                listBox1.Items.AddRange(suspiciousItems.ToArray());
                return;
            }

            const int excludeCount = 5;
            Analyzer analyzer = new Analyzer();


            for (int index = 1; index < _columns.Length; index++)
            {
                //values去除了前excludeCount个值和后excludeCount个值
                double[] values = new double[_columns[index].Values.Count - excludeCount * 2];

                Debug.Assert(values.Length > 100);

                for (int i = 0; i < values.Length; i++)
                {
                    if (_columns[index].Values[i] == " " ||_columns[index].Values[i]==""|| _columns[index].Values[i] == "0" || string.IsNullOrEmpty(_columns[index].Values[i + excludeCount]))
                    {
                        values[i] = 0.0;
                    }
                    else
                    {
                        values[i] = Convert.ToDouble(_columns[index].Values[i]);
                    }
                }

                unsafe
                {

                    //分析
                    fixed (double* pValues = values)
                    {

                        //textBox2.BackColor = SystemColors.Window;
                        //textBox2.Text = "";

                        #region
                        //认为趋势泄漏的可能性大小对应的颜色从低到高为【蓝色->黄色->红色】


                        //if(条件...){蓝色}
                        //
                        //if(条件...){黄色}
                        //
                        //if(条件...){红色}


                        //判定条件1---LBTF

                        //判定条件2---LBTFBS


                        //判定条件3
                        //最大值、最小值、平均值，三者相差多不多
                        //最大值-平均值~=平均值-最小值，且此差值为最大值减去最小值的二分之一

                        //判定条件4
                        //
                        #endregion


                        double max = 0.0;
                        double min = 0.0;
                        analyzer.GetMaxMinValue(values, ref max, ref min);

                        double percent_1to1 = analyzer.GetPercentOfLatterBigThanFormer(pValues, values.Length);
                        double percent_10to1 = analyzer.GetPercentOfLatterBigThanFormerBySections(pValues, values.Length, values.Length / 10);
                        double percent_20to1 = analyzer.GetPercentOfLatterBigThanFormerBySections(pValues, values.Length, values.Length / 20);
                        double percent_50to1 = analyzer.GetPercentOfLatterBigThanFormerBySections(pValues, values.Length, values.Length / 50);
                        //double percent_100to1 = analyzer.GetPercentOfLatterBigThanFormerBySections(pValues, values.Length, values.Length / 100);

                        #region mean
                        double[] valuesFront = new double[values.Length / 10];
                        double[] valuesBetween = new double[values.Length / 10];
                        double[] valuesEnd = new double[values.Length / 10];

                        for (int i = 0; i < valuesFront.Length; i++)
                        {
                            valuesFront[i] = values[i];
                        }

                        for (int i = 0; i < valuesBetween.Length; i++)
                        {
                            valuesBetween[i] = values[values.Length / 2 - valuesBetween.Length / 2 + i];
                        }

                        for (int i = 0; i < valuesEnd.Length; i++)
                        {
                            valuesEnd[i] = values[values.Length - 1 - i];
                        }
                        #endregion
                        double frontMean = analyzer.GetMeanValue(valuesFront);
                        double betweenMean = analyzer.GetMeanValue(valuesBetween);
                        double endMean = analyzer.GetMeanValue(valuesEnd);

                     

                        double dif = max - min;
                        bool add = false;
                        bool condition1 = (endMean - betweenMean) > dif / 2
                                        && (betweenMean - frontMean) > dif / 2
                                        || (endMean - frontMean) > dif * 2 / 3;

                        bool condition2 = percent_1to1 > 0.8
                                        ||
                                        (percent_10to1 > 0.8
                                        && percent_10to1 <= percent_20to1  //LBTFBS逐渐增大
                                        && percent_20to1 <= percent_50to1
                                        //&& percent_50to1 <= percent_100to1
                                        && frontMean < betweenMean
                                        && betweenMean < endMean);

                        //或还是与 待定
                        add = condition1 || condition2;



                        //增加滤除条件
                        if (_columns[index].HeaderName.Contains("Elapsed"))
                        {
                            add=false;
                        }


                        if (add)
                        {
                            suspiciousItems.Add(_columns[index].HeaderName);                    
                        }

                    }
                }



            }

            hasBeenAnalyzed = true;
            allItemsBeSeen = false;
            listBox1.Items.Clear();
            listBox1.Items.AddRange(suspiciousItems.ToArray());


            MessageBox.Show("可疑曲线个数:"+listBox1.Items.Count.ToString());
        }


        /// <summary>
        /// 显示所有条目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAll_Click(object sender, EventArgs e)
        {
            allItemsBeSeen = true;
            if (allItems != null && allItems.Count > 0)
            {
                listBox1.Items.Clear();
                listBox1.Items.AddRange(allItems.ToArray());
            }

        }
    }


    public enum DataType
    {
        /// <summary>
        /// 四字节长
        /// </summary>
        UINT64,
        /// <summary>
        /// 八字节长
        /// </summary>
        DOUBLE
    }

    /// <summary>
    /// 此结构体表示CSV文件中的某一列(带标题)
    /// </summary>
    public struct ColumnItem
    {
        /// <summary>
        /// 列名
        /// </summary>
        public string HeaderName;

        /// <summary>
        /// 数据类型
        /// </summary>
        public DataType DataType;

        /// <summary>
        /// 此列中包含的所有数值，客户使用时需要按照数据类型进行转换,m目前统一按照double来处理
        /// </summary>
        public List<string> Values;

        /// <summary>
        /// 此行的背景色
        /// </summary>
        public Color bgColor;


    }

    public struct DoubleItem
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }

    }

    public struct UInt64Item
    {
        public DateTime Date { get; set; }
        public UInt64 Value { get; set; }

    }


}
