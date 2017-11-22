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
            var template = Db.DocumentDatas.Find(documentId);
            if (template?.Data == null)
            {
                throw new Exception("Could not find template");
            }
            var memoryStream = new MemoryStream(template.Data);
            Document document = new Document(memoryStream);
            var tags = document.GetChildNodes(NodeType.StructuredDocumentTag, true);
            foreach (var child in tags)
            {
                var tag = child as StructuredDocumentTag;
                if (tag != null)
                {
                    result.Add(tag.Tag);
                }
            }
            return result;
        }

        public void InsertText()
        {
            //var tags = doc.GetChildNodes(NodeType.StructuredDocumentTag, true);
            //foreach (var child in tags)
            //{
            //    var type = child as StructuredDocumentTag;
            //    if (type.SdtType == SdtType.RichText)
            //    {
            //        type.RemoveAllChildren();
            //        Paragraph para = (Paragraph)type.AppendChild(new Paragraph(app));
            //        Run run = new Run(app, "new text goes here");
            //        para.AppendChild(run);
            //    }
            //}
        }
    }
}
