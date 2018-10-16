using PagedList;
using QuanLyVeSo.Areas.Administrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyVeSo.Areas.Administrator.Controllers
{
    public class KetQuaSoXoController : Controller
    {
        // GET: Administrator/KetQuaSoXo
        QLVSEntities db = new QLVSEntities();
        public ActionResult Index(int? _Page)
        {
            int PageNumber = (_Page ?? 1);
            int PageSize = 10;
            return View(db.KetQuaSoXoes.ToList().OrderBy(n => n.ID).ToPagedList(PageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaGiai = new SelectList(db.Giais.ToList().OrderBy(n => n.TenGiai), "MaGiai", "TenGiai");
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes.ToList().OrderBy(n => n.Tinh), "MaLoaiVeSo", "Tinh");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(KetQuaSoXo _KetQuaSoXo)
        {
            ViewBag.MaGiai = new SelectList(db.Giais.ToList().OrderBy(n => n.TenGiai), "MaGiai", "TenGiai");
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes.ToList().OrderBy(n => n.Tinh), "MaLoaiVeSo", "Tinh");
            db.KetQuaSoXoes.Add(_KetQuaSoXo);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult ChinhSua(int _ID)
        {
            KetQuaSoXo kqxs = db.KetQuaSoXoes.SingleOrDefault(n => n.ID == _ID);
            if (kqxs == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaGiai = new SelectList(db.Giais.ToList().OrderBy(n => n.TenGiai), "MaGiai", "TenGiai", kqxs.MaGiai);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes.ToList().OrderBy(n => n.Tinh), "MaLoaiVeSo", "Tinh", kqxs.MaLoaiVeSo);
            return View(kqxs);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(KetQuaSoXo _KetQuaSoXo)
        {
            ViewBag.MaGiai = new SelectList(db.Giais.ToList().OrderBy(n => n.TenGiai), "MaGiai", "TenGiai", _KetQuaSoXo.MaGiai);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes.ToList().OrderBy(n => n.Tinh), "MaLoaiVeSo", "Tinh", _KetQuaSoXo.MaLoaiVeSo);
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.Entry(_KetQuaSoXo).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Xoa(int _ID)
        {
            KetQuaSoXo kqxs = db.KetQuaSoXoes.SingleOrDefault(n => n.ID == _ID);
            if (kqxs == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(kqxs);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateInput(false)]
        public ActionResult XacNhanXoa(int _ID)
        {
            KetQuaSoXo kqxs = db.KetQuaSoXoes.SingleOrDefault(n => n.ID == _ID);

            if (kqxs == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.KetQuaSoXoes.Remove(kqxs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}