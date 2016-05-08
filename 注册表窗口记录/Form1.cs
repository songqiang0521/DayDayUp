using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.IO.IsolatedStorage;
using System.Xml;



namespace 注册表窗口记录
{
    public partial class Form1 : Form
    {

        //颜色对话框
        private ColorDialog chooseColorDialog = new ColorDialog();

        public Form1()
        {
            InitializeComponent();
            buttonChangeColor.Click += new EventHandler(buttonChangeColor_Click);
            try
            {

                if (ReadSetting() == false)
                {
                    this.listBoxMessage.Items.Add("还没有注册表记录配置项目！");
                }
                else
                {
                    this.listBoxMessage.Items.Add("正在读取注册表项目");
                }
                StartPosition = FormStartPosition.Manual;


            }
            catch (Exception ex)
            {
                listBoxMessage.Items.Add("一个问题出现了！读取信息失败!");
                listBoxMessage.Items.Add(ex.Message);
            }
        }
        //
        private bool ReadSetting()
        {

            #region 注册表
            ////throw new NotImplementedException();
            //RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("software",true);
            //RegistryKey SQpress = softwareKey.OpenSubKey("SQpress", true);
            //if (SQpress==null)
            //{
            //    return false;

            //}
            //RegistryKey selfWindowsKey = SQpress.OpenSubKey("selfWindowsKey", true);

            //if (selfWindowsKey==null)
            //{
            //    return false;

            //}
            //else
            //{
            //    listBoxMessage.Items.Add("添加注册表项成功！！！！！！！！！！！！");
            //    listBoxMessage.Items.Add(selfWindowsKey.ToString());
            //}

            //int redComponent = (int)selfWindowsKey.GetValue("Red");
            //int greenComponent = (int)selfWindowsKey.GetValue("Green");
            //int blueComponent = (int)selfWindowsKey.GetValue("Blue");
            //BackColor = Color.FromArgb(redComponent, greenComponent, blueComponent);
            //listBoxMessage.Items.Add("背景色" + BackColor.Name);

            //int X = (int)selfWindowsKey.GetValue("X");
            //int Y = (int)selfWindowsKey.GetValue("Y");
            //DesktopLocation = new Point(X, Y);

            //listBoxMessage.Items.Add("窗口位置" + DesktopLocation);
            //this.Height = (int)selfWindowsKey.GetValue("Height");
            //this.Width = (int)selfWindowsKey.GetValue("Width");
            //listBoxMessage.Items.Add("窗口大小" + new Size(X, Y));

            //string initialWindowStates = (string)selfWindowsKey.GetValue("WindowState");

            //listBoxMessage.Items.Add("窗口扎状态" + initialWindowStates);
            //this.WindowState = (FormWindowState)FormWindowState.Parse(WindowState.GetType(), initialWindowStates);

            //return true;

            #endregion

            #region ISO
            IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForDomain();
            string[] files = iso.GetFileNames("SelfPlace.xml");
            foreach (string userFile in files)
            {
                if (userFile == "SelfPlace.xml")
                {
                    listBoxMessage.Items.Add("成功打开文件" + userFile);

                    StreamReader sr = new StreamReader(new IsolatedStorageFileStream("SelfPlace.xml", FileMode.OpenOrCreate, FileAccess.Read));

                    System.Xml.XmlTextReader reader = new System.Xml.XmlTextReader(sr);

                    int x = 0;
                    int y = 0;

                    while (reader.Read())
                    {
                        switch (reader.Name)
                        {
                            case "X":
                                x = int.Parse(reader.ReadString());
                                break;
                            case "Y":
                                y = int.Parse(reader.ReadString());
                                break;
                            case "Width":
                                this.Width = int.Parse(reader.ReadString());
                                break;
                            case "Height":
                                this.Height = int.Parse(reader.ReadString());
                                break;

                            default:
                                break;
                        }
                    }

                    this.DesktopLocation = new Point(x, y);
                    iso.Close();
                    sr.Close();


                }

            }

            return true;

            #endregion

        }

        private void SaveSetting()
        {
            //throw new System.NotImplementedException();

            #region zhucebiao
            //RegistryKey softwareKey = Registry.LocalMachine.OpenSubKey("SoftWare", true);
            //RegistryKey SQKey = softwareKey.CreateSubKey("SQPress");
            //RegistryKey selfWindowsKey = SQKey.CreateSubKey("selfWindowsKey");
            //selfWindowsKey.SetValue("BackColor", (object)BackColor.ToKnownColor());
            //selfWindowsKey.SetValue("Red", (object)(int)BackColor.R);
            //selfWindowsKey.SetValue("Green", (object)(int)BackColor.G);
            //selfWindowsKey.SetValue("Blue", (object)(int)BackColor.B);
            //selfWindowsKey.SetValue("Width", (object)Width);
            //selfWindowsKey.SetValue("Height", (object)Height);
            //selfWindowsKey.SetValue("X", (object)DesktopLocation.X);
            //selfWindowsKey.SetValue("Y", (object)DesktopLocation.Y);
            //selfWindowsKey.SetValue("WindowState", (object)WindowState.ToString());

            //softwareKey.Close();
            //SQKey.Close();
            //selfWindowsKey.Close();
            #endregion

            #region ISO
            IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForDomain();
            var isoStream = new IsolatedStorageFileStream("SelfPlace.xml", FileMode.Create, FileAccess.ReadWrite);
            System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(isoStream, Encoding.Default);
            writer.Formatting = System.Xml.Formatting.Indented;


            writer.WriteStartDocument();
            writer.WriteStartElement("Settings");

            writer.WriteStartElement("Width");
            writer.WriteValue(this.Width);
            writer.WriteEndElement();

            writer.WriteStartElement("Height");
            writer.WriteValue(this.Height);
            writer.WriteEndElement();

            writer.WriteStartElement("X");
            writer.WriteValue(this.DesktopLocation.X);
            writer.WriteEndElement();

            writer.WriteStartElement("Y");
            writer.WriteValue(this.DesktopLocation.Y);
            writer.WriteEndElement();

            writer.WriteEndElement();

            writer.Flush();
            writer.Close();

            isoStream.Close();
            iso.Close();
            #endregion



        }

        void buttonChangeColor_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            if (chooseColorDialog.ShowDialog() == DialogResult.OK)
            {
                BackColor = chooseColorDialog.Color;

            }


        }

        private void listBoxMessage_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void buttonChangeColor_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StringBuilder output = new StringBuilder();

            string xmlString = @"<?xml version='1.0'?>
          <!-- This is a sample XML document -->
          <Items>
            <Item>test with a child element <more/> stuff</Item>
          </Items>";

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlString)))
            {
                XmlWriterSettings ws = new XmlWriterSettings();
                ws.Indent = true;
                using (XmlWriter writer = XmlWriter.Create(output, ws))
                {
                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                                writer.WriteStartElement(reader.Name);
                                break;
                            case XmlNodeType.Text:
                                writer.WriteString(reader.Value);
                                break;
                            case XmlNodeType.XmlDeclaration:
                            case XmlNodeType.ProcessingInstruction:
                                writer.WriteProcessingInstruction(reader.Name, reader.Value);
                                break;
                            case XmlNodeType.Comment:
                                writer.WriteComment(reader.Value);
                                break;
                            case XmlNodeType.EndElement:
                                writer.WriteFullEndElement();
                                break;
                        }
                    }

                }

            }

           MessageBox.Show(output.ToString());



        }
    }
}
