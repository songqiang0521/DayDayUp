using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace fonts
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        AutoResetEvent autoEvent = new AutoResetEvent(false);//初始状态--非终止
        public Form1()
        {

            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //InstalledFontCollection ifc = new InstalledFontCollection();
            //float x = 0f;
            //float y = 0f;
            //for (int i = 0; i < ifc.Families.Length; i++)
            //{
            //    if (ifc.Families[i].IsStyleAvailable(FontStyle.Regular))
            //    {
            //        e.Graphics.DrawString(ifc.Families[i].Name, new Font(ifc.Families[i], 12), Brushes.Black,x,y);
            //        y += 20;
            //        if (y%700==0)
            //        {
            //            y = 0;
            //            x += 140;
            //        }
            //    }
            //}
        }




        private string getQiushi(string URI)
        {
            WebRequest request = WebRequest.Create(URI);
            WebResponse result = null;
            result = request.GetResponse();



            Stream ReceiveStream = result.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream);
            string resultstring = sr.ReadToEnd();

            //webBrowser1.DocumentText=resultstring;

            StringBuilder responseString = new StringBuilder();




            //Regex regContent = new Regex("<div class=\"qiushi\">(?<content>[\\s\\S]+?)</div>");   //匹配糗事内容
            //Regex regComment = new Regex("<p class=\"vote\">(?<content>[\\s\\S]+?)</p>");         //匹配评论
            //Regex regUserInfo = new Regex("<p class=\"user\">(?<content>[\\s\\S]+?)</p>");        //匹配发布者信息
            //Regex regLinks = new Regex("(href=\")(/[^\\s]*)(\")");                                //匹配链接
            //Regex regPrevPage = new Regex("<a href=\".*?\">上一页</a>");                          //匹配换页
            //Regex regNextPage = new Regex("<a href=\".*?\">下一页</a>");
            //Regex regBlankLine = new Regex(@"[\n|\r|\r\n]");                                      //匹配换行
            //MatchCollection mcContent = regContent.Matches(resultstring);
            //Match mcPrevPage = regPrevPage.Match(resultstring);
            //Match mcNextPage = regNextPage.Match(resultstring);
            //string prevPage = "<a href=\"?param=" + mcPrevPage.ToString().Replace("<a href=\"", "").Replace("\">上一页</a>", "") + "\">上一页</a>&nbsp;&nbsp;";
            //string nextPage = "<a href=\"?param=" + mcNextPage.ToString().Replace("<a href=\"", "").Replace("\">下一页</a>", "") + "\">下一页</a>";

            //for (int i = 0; i < mcContent.Count; i++)
            //{
            //    string content = mcContent[i].ToString();
            //    content = Regex.Replace(content, regComment.ToString(), "", RegexOptions.IgnoreCase);
            //    content = Regex.Replace(content, regUserInfo.ToString(), "", RegexOptions.IgnoreCase);
            //    content = Regex.Replace(content, regLinks.ToString(), "href=\"?param=$2\"", RegexOptions.IgnoreCase);
            //    content = Regex.Replace(content, regBlankLine.ToString(), "", RegexOptions.IgnoreCase);

            //    responseString.Append(content);
            //}

            //responseString.Append("<div style=\"text-align:center\">" + prevPage);
            //responseString.Append(nextPage + "</div>");

            //return responseString.ToString();
            return resultstring;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string URI = "http://wap3.qiushibaike.com";
            //       WebRequest req = WebRequest.Create("param");
            //string pageInfo = Request.QueryString["param"] == null ? string.Empty : Request.QueryString["param"].ToString().Trim();
            // URI = URI + pageInfo;

            webBrowser1.DocumentText = getQiushi(URI);


        }

        private void Method()
        {

            //Thread.Sleep(10000);
            listBox1.Invoke(new Action(() => { listBox1.Items.Add("Thread等待通知"); }));
            autoEvent.WaitOne();
            //listBox1.BeginInvoke(new Action(() => {  listBox1.Items.Add("等待5000ms后"); }));
            listBox1.Invoke(new Action(() => { listBox1.Items.Add("Thread接到通知,开始计算"); }));
            //Thread.Sleep(5000);
            listBox1.Invoke(new Action(() => { listBox1.Items.Add("Thread计算完毕"); }));


            //listBox1.Items.Add("keyibu????");
            //Thread.Sleep(5000);

            //this.Invoke(new Action(() => {  listBox1.Items.Add("等待5000ms后"); }));

        }

        private void button2_Click(object sender, EventArgs e)
        {

            //listBox1.Items.Add("开始");

            listBox1.Items.Add("主线程开始");
            //autoEvent.WaitOne();
            //listBox1.Items.Add("接到通知，开始计算");

            //new Thread(Method).Start();
            var thread = new Thread(Method);
            thread.IsBackground = true;
            thread.Start();


            var t = Task.Factory.StartNew(new Action(() =>
            {
                //Thread.Sleep(5000); listBox1.Invoke(new Action(() => { listBox1.Items.Add("等待通知"); }));
                autoEvent.WaitOne();
                //listBox1.BeginInvoke(new Action(() => {  listBox1.Items.Add("等待5000ms后"); }));
                listBox1.Invoke(new Action(() => { listBox1.Items.Add("Task接到通知,开始计算"); }));
                //autoEvent.WaitOne();
                listBox1.BeginInvoke(new Action(() => { listBox1.Items.Add("TASK等待5000ms后"); }));
            })
                //, cts.Token
            );
            //t.Wait();
            //listBox1.Items.Add(t.Result);


            //listBox1.Invoke(new Action(() => { Thread.Sleep(5000); listBox1.Items.Add("等待5000ms后"); }));
            listBox1.Items.Add("最后");








        }

        private void button4_Click(object sender, EventArgs e)
        {
            //    string URI = "http://wap3.qiushibaike.com";

            //    var req = WebRequest.Create(URI);
            //    var rspStream = ((HttpWebRequest)req).GetResponse().GetResponseStream();





        }

        private void button3_Click(object sender, EventArgs e)
        {
            autoEvent.Set();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            cts.Cancel();
        }
    }
}
