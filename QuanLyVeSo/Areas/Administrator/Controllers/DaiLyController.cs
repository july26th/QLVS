using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using QuanLyVeSo.Areas.Administrator.Models;
using System.IO;

namespace QuanLyVeSo.Areas.Administrator.Controllers
{
    public class DaiLyController : Controller
    {
        // GET: Administrator/DaiLy
        QLVSEntities db = new QLVSEntities();
        public ActionResult Index(int? _Page)
        {
            int PageNumber = (_Page ?? 1);
            int PageSize = 10;
            return View(db.DaiLies.ToList().OrderBy(n => n.MaDaiLy).ToPagedList(PageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(DaiLy _DaiLy)
        {
            if (ModelState.IsValid)
            {
                db.DaiLies.Add(_DaiLy);
                db.SaveChanges();
            }
            //return RedirectToAction("Index");
            return View();
        }

        [HttpGet]
        public ActionResult ChinhSua(string _MaDaiLy)
        {
            DaiLy dl = db.DaiLies.SingleOrDefault(n => n.MaDaiLy == _MaDaiLy);
            if (dl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dl);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(DaiLy _DaiLy)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.Entry(_DaiLy).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Xoa(string _MaDaiLy)
        {
            DaiLy dl = db.DaiLies.SingleOrDefault(n => n.MaDaiLy == _MaDaiLy);
            if (dl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dl);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateInput(false)]
        public ActionResult XacNhanXoa(string _MaDaiLy)
        {
            DaiLy dl = db.DaiLies.SingleOrDefault(n => n.MaDaiLy == _MaDaiLy);

            if (dl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.DaiLies.Remove(dl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}