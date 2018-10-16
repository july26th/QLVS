using PagedList;
using QuanLyVeSo.Areas.Administrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyVeSo.Areas.Administrator.Controllers
{
    public class PhieuThuController : Controller
    {
        // GET: Administrator/PhieuThu
        QLVSEntities db = new QLVSEntities();
        public ActionResult Index(int? _Page)
        {
            int PageNumber = (_Page ?? 1);
            int PageSize = 10;
            return View(db.PhieuThus.ToList().OrderBy(n => n.MaPhieuThu).ToPagedList(PageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLies.ToList().OrderBy(n => n.TenDaiLy), "MaDaiLy", "TenDaiLy");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(PhieuThu _PhieuThu)
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLies.ToList().OrderBy(n => n.TenDaiLy), "MaDaiLy", "TenDaiLy");
            db.PhieuThus.Add(_PhieuThu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChinhSua(string _MaPhieuThu)
        {
            PhieuThu ph = db.PhieuThus.SingleOrDefault(n => n.MaPhieuThu == _MaPhieuThu);
            if (ph == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLies.ToList().OrderBy(n => n.TenDaiLy), "MaDaiLy", "TenDaiLy", ph.MaDaiLy);
            //return View(db.PhieuThus.SingleOrDefault(n=>n.ID.Equals(_ID)));
            return View(ph);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(PhieuThu _PhieuThu)
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLies.ToList().OrderBy(n => n.TenDaiLy), "MaDaiLy", "TenDaiLy", _PhieuThu.MaDaiLy);
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.Entry(_PhieuThu).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Xoa(string _MaPhieuThu)
        {
            PhieuThu ph = db.PhieuThus.SingleOrDefault(n => n.MaPhieuThu == _MaPhieuThu);
            if (ph == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ph);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateInput(false)]
        public ActionResult XacNhanXoa(string _MaPhieuThu)
        {
            PhieuThu dl = db.PhieuThus.SingleOrDefault(n => n.MaPhieuThu == _MaPhieuThu);

            if (dl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.PhieuThus.Remove(dl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}