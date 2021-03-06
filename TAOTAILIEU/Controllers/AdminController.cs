﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Amaris.Aspose;
using DAL;

namespace TAOTAILIEU.Controllers
{
    public class AdminController : Controller
    {
        private TAOTAILIEUEntities Db { get; } = new TAOTAILIEUEntities();
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult ImportTemplate(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength < 0)
            {
                return Content("File is empty.");
            }
            var memoryStream = new MemoryStream();
            file.InputStream.CopyTo(memoryStream);
            var document = new DocumentData()
            {
                Data = memoryStream.ToArray()
            };
            Db.DocumentDatas.Add(document);
            Db.SaveChanges();
            var urls = new List<string>();
            var factory = AmarisAsposeFactory.Get(memoryStream, DocumentType.Doc, 72);
            var count = factory.PageCount();
            for (int i = 0; i < count; i++)
            {
                urls.Add(Url.Action("PreviewImage", "Admin",
                    new { documentId = document.DocumentId, page = i, date = DateTime.Now.Ticks }));
            }
            return PartialView("PreviewTemplate", urls);
        }
        public ActionResult PreviewImage(int documentId, int page = 1)
        {
            var doc = Db.DocumentDatas.Find(documentId);
            var data = doc.Data;
            if (data != null)
            {
                using (var ms = new MemoryStream(data))
                {
                    var factory = AmarisAsposeFactory.Get(ms, DocumentType.Doc, 72);
                    var image = factory.ToImage(page);
                    image.Position = 0;
                    var bytes = new BinaryReader(image).ReadBytes((int)image.Length);
                    return File(bytes, "image/jpeg");
                }
            }
            return null;
        }
    }
}