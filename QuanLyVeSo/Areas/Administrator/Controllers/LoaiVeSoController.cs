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
    public class LoaiVeSoController : Controller
    {
        // GET: Administrator/LoaiVeSo
        QLVSEntities db = new QLVSEntities();
        public ActionResult Index(int? _Page)
        {
            int PageNumber = (_Page ?? 1);
            int PageSize = 10;
            return View(db.LoaiVesoes.ToList().OrderBy(n => n.MaLoaiVeSo).ToPagedList(PageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult ThemMoi()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(LoaiVeso _LoaiVeso)
        {
            db.LoaiVesoes.Add(_LoaiVeso);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChinhSua(string _MaLoaiVeSo)
        {
            LoaiVeso lvs = db.LoaiVesoes.SingleOrDefault(n => n.MaLoaiVeSo == _MaLoaiVeSo);
            if (lvs == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lvs);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(LoaiVeso _LoaiVeso)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.Entry(_LoaiVeso).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Xoa(string _MaLoaiVeSo)
        {
            LoaiVeso lvs = db.LoaiVesoes.SingleOrDefault(n => n.MaLoaiVeSo == _MaLoaiVeSo);
            if (lvs == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(lvs);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateInput(false)]
        public ActionResult XacNhanXoa(string _MaLoaiVeSo)
        {
            LoaiVeso lvs = db.LoaiVesoes.SingleOrDefault(n => n.MaLoaiVeSo == _MaLoaiVeSo);
            
                if (lvs == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }                          
            db.LoaiVesoes.Remove(lvs);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}