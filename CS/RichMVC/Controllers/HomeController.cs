using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RichMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Document = Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath("~/Docs/template.docx")));
            return View();
        }

        [HttpPost]
        public ActionResult ExportDocument(string base64, string fileName, int format, string reason)
        {
            byte[] fileContent = Convert.FromBase64String(base64);
            System.IO.File.WriteAllBytes(Server.MapPath($"~/Docs/{fileName}.docx"), fileContent);
            return new EmptyResult();
        }
    }
}