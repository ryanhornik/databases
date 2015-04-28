using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace SiliconShores
{
    public class Helper
    {
        public static MemoryStream MergePdf(List<MemoryStream> inFiles)
        {
            var ms = new MemoryStream();
            var doc = new Document();
            var pdf = new PdfCopy(doc, ms);

            doc.Open();
            PdfReader reader;
            PdfImportedPage page;
            inFiles.ForEach(file =>
            {
                reader = new PdfReader(file);

                for (var i = 0; i < reader.NumberOfPages; i++)
                {
                    page = pdf.GetImportedPage(reader, i + 1);
                    pdf.AddPage(page);
                }

                pdf.FreeReader(reader);
            });

            
            pdf.CloseStream = false;
            doc.Close();
            ms.Position = 0;
            return ms;
        }
    }
}