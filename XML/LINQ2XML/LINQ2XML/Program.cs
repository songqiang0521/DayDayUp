using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LINQ2XML
{
    class Program
    {
        static void Main(string[] args)
        {
            //在某些情况下，需要在Element的参数中表明xmlns才能成功地获取元素
            {
                string path = "";
                XDocument doc = XDocument.Load(@"..\..\Newtonsoft.Json.nuspec.xml");
                var ns = doc.Root.GetDefaultNamespace();
                var metadata = doc.Root.Element(ns + "metadata");
                var licenseUrl = metadata.Element(ns + "licenseUrl");

                path = licenseUrl.Value;
                WebClient wc = new WebClient();

                //@"https://raw.github.com/JamesNK/Newtonsoft.Json/master/LICENSE.md"
                string content = wc.DownloadString(path);
                Console.WriteLine(content);
            }

            {
                string path = "";
                XDocument doc = XDocument.Load(@"..\..\Newtonsoft.Json.nuspec-no-xmlns.xml");
                var metadata = doc.Root.Element( "metadata");
                var licenseUrl = metadata.Element("licenseUrl");

                path = licenseUrl.Value;
                WebClient wc = new WebClient();

                //@"https://raw.github.com/JamesNK/Newtonsoft.Json/master/LICENSE.md"
                string content = wc.DownloadString(path);
                Console.WriteLine(content);
            }
        }
    }
}
