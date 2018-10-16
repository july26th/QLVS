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
    public class SoLuongDKController : Controller
    {
        // GET: Administrator/SoLuongDK
        QLVSEntities db = new QLVSEntities();
        public ActionResult Index(int? _Page)
        {
            int PageNumber = (_Page ?? 1);
            int PageSize = 10;
            return View(db.SoLuongDKs.ToList().OrderBy(n => n.ID).ToPagedList(PageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLies.ToList().OrderBy(n => n.TenDaiLy), "MaDaiLy", "TenDaiLy");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(SoLuongDK _SoLuongDK)
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLies.ToList().OrderBy(n => n.TenDaiLy), "MaDaiLy", "TenDaiLy");
            db.SoLuongDKs.Add(_SoLuongDK);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult ChinhSua(string _ID)
        {
            SoLuongDK dk = db.SoLuongDKs.SingleOrDefault(n => n.ID == _ID);
            if (dk == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLies.ToList().OrderBy(n => n.TenDaiLy), "MaDaiLy", "TenDaiLy", dk.MaDaiLy);
            //return View(db.SoLuongDKs.SingleOrDefault(n=>n.ID.Equals(_ID)));
            return View(dk);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(SoLuongDK _SoLuongDK)
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLies.ToList().OrderBy(n => n.TenDaiLy), "MaDaiLy", "TenDaiLy", _SoLuongDK.MaDaiLy);
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.Entry(_SoLuongDK).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Xoa(string _ID)
        {
            SoLuongDK dl = db.SoLuongDKs.SingleOrDefault(n => n.ID == _ID);
            if (dl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(dl);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateInput(false)]
        public ActionResult XacNhanXoa(string _ID)
        {
            SoLuongDK dl = db.SoLuongDKs.SingleOrDefault(n => n.ID == _ID);

            if (dl == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.SoLuongDKs.Remove(dl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}