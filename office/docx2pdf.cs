using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Office.Interop.Word;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var wordAPP = new Application();
            var veersion=wordAPP.Version;

            var files = Directory.GetFiles(@"Z:\", "*.docx");
            foreach (string filename in files)
            {
                var pdfName = Path.ChangeExtension(filename, ".pdf");
                var doc = wordAPP.Documents.Open(filename);
                doc.ExportAsFixedFormat(pdfName,WdExportFormat.wdExportFormatPDF);
            }

            ((Microsoft.Office.Interop.Word._Application)wordAPP).Quit();
            
        }
    }
}
