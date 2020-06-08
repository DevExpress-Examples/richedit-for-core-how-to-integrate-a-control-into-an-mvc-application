using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RichMVC.Controllers
{
    public class HomeController : Controller
    {
        private const string documentFolderPath = "~/Docs/";
        public ActionResult Index()
        {
            ViewBag.Document = Convert.ToBase64String(System.IO.File.ReadAllBytes(Server.MapPath($"{documentFolderPath}template.docx")));
            return View();
        }

        [HttpPost]
        public ActionResult ExportDocument(string base64, string fileName, int format, string reason)
        {
            byte[] fileContent = Convert.FromBase64String(base64);
            System.IO.File.WriteAllBytes(Server.MapPath($"{documentFolderPath}{fileName}.{GetExtension(format)}"), fileContent);
            return new EmptyResult();
        }

        [HttpGet]
        public JsonResult GetDataSource()
        {
            int id = 0;
            var jsondata = new[] { "John", "Piter", "Mark" }.Select(name => new Hashtable() { { "FirstName", name }, { "Id", id++ } }).ToList();
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

        private static string GetExtension(int format)
        {
            switch (format)
            {
                case 4: return "docx";
                case 3: return "rtf";
                case 1: return "txt";
            }
            return "docx";
        }
    }
}