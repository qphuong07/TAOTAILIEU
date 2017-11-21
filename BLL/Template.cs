using System;
using System.Collections.Generic;
using System.IO;
using Amaris.Aspose.Word;
using Aspose.Words;
using Aspose.Words.Markup;
using DAL;

namespace BLL
{ 
    public class Template:BaseBLL
    {
        public int SaveTemplate(MemoryStream input)
        {
            var document = new DocumentData()
            {
                Data = input.ToArray()
            };
            Db.DocumentDatas.Add(document);
            Db.SaveChanges();
            return document.DocumentId;
        }
        public List<string> GetTemplateFields(int documentId)
        {
            List<string> result = new List<string>();
            var document = Db.DocumentDatas.Find(documentId);
            var tmpFile = Path.GetTempFileName();
            var memoryStream = new MemoryStream(document.Data);
            Document app = new Document(memoryStream);
            var tags = app.GetChildNodes(NodeType.StructuredDocumentTag, true);
            foreach (var child in tags)
            {
                var type = child as StructuredDocumentTag;
                if (type.SdtType == SdtType.RichText)
                {
                    type.RemoveAllChildren();
                    Paragraph para = (Paragraph)type.AppendChild(new Paragraph(app));
                    Run run = new Run(app, "new text goes here");
                    para.AppendChild(run);
                }
            }
            app.Save(@"C:\\Temp.docx");

            return result;
        }

        public void XXX(int documentId)
        {
            var document = Db.DocumentDatas.Find(documentId);
            var memoryStream = new MemoryStream(document.Data);
            Amaris.Aspose.Word.DocumentWord xxx = new DocumentWord(memoryStream);
        }
    }
}
