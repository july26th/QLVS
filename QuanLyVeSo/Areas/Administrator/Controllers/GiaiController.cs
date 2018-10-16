using PagedList;
using QuanLyVeSo.Areas.Administrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyVeSo.Areas.Administrator.Controllers
{
    public class GiaiController : Controller
    {
        // GET: Administrator/Giai
        QLVSEntities db = new QLVSEntities();
        public ActionResult Index(int? _Page)
        {
            int PageNumber = (_Page ?? 1);
            int PageSize = 10;
            return View(db.Giais.ToList().OrderBy(n => n.MaGiai).ToPagedList(PageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(Giai _Giai)
        {
            db.Giais.Add(_Giai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChinhSua(string _MaGiai)
        {
            Giai giai = db.Giais.SingleOrDefault(n => n.MaGiai == _MaGiai);
            if (giai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(giai);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(Giai _Giai)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.Entry(_Giai).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Xoa(string _MaGiai)
        {
            Giai giai = db.Giais.SingleOrDefault(n => n.MaGiai == _MaGiai);
            if (giai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(giai);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateInput(false)]
        public ActionResult XacNhanXoa(string _Magiai)
        {
            Giai giai = db.Giais.SingleOrDefault(n => n.MaGiai == _Magiai);

            if (giai == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.Giais.Remove(giai);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}