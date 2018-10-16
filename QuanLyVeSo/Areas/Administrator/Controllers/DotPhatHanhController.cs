using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using QuanLyVeSo.Areas.Administrator.Models;
using System.IO;
using System.Data.SqlClient;

namespace QuanLyVeSo.Areas.Administrator.Controllers
{
    public class DotPhatHanhController : Controller
    {
        // GET: Administrator/DotPhatHanh
        QLVSEntities db = new QLVSEntities();
        public ActionResult Index(int? _Page)
        {
            int PageNumber = (_Page ?? 1);
            int PageSize = 10;
            return View(db.PhatHanhs.ToList().OrderBy(n => n.ID).ToPagedList(PageNumber, PageSize));
        }

        [HttpGet]
        public ActionResult ThemMoi()
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLies.ToList().OrderBy(n => n.TenDaiLy), "MaDaiLy", "TenDaiLy");
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes.ToList().OrderBy(n => n.Tinh), "MaLoaiVeSo", "Tinh");

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemMoi(string MaDaiLy, string MaLoaiVeSo, System.DateTime NgayNhan, PhatHanh _PhatHanh, LoaiVeso _LVS)
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLies.ToList().OrderBy(n => n.TenDaiLy), "MaDaiLy", "TenDaiLy");
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes.ToList().OrderBy(n => n.Tinh), "MaLoaiVeSo", "Tinh");
            ////try
            //{
            //    SqlParameter param1 = new SqlParameter("@MADAILY", _PhatHanh.MaDaiLy);
            //    SqlParameter param2 = new SqlParameter("@MALOAIVESO", _PhatHanh.MaLoaiVeSo);
            //    SqlParameter param3 = new SqlParameter("@SLG", _PhatHanh.SoLuong);
            //    SqlParameter param4 = new SqlParameter("@NgayNhan", _PhatHanh.NgayNhan);
            //    SqlParameter param5 = new SqlParameter("@SLBan", _PhatHanh.SLBan);
            //    SqlParameter param6 = new SqlParameter("@DoanhThuDPH", _PhatHanh.DoanhThuDPH);
            //    SqlParameter param7 = new SqlParameter("@HoaHong", _PhatHanh.HoaHong);
            //    SqlParameter param8 = new SqlParameter("@TienThanhToan", _PhatHanh.TienThanhToan);
            //    var db = new QLVSEntities();
            //    var data = db.Database.ExecuteSqlCommand("them @MADAILY, @MALOAIVESO, @SLG, @NgayNhan, @SLBan, @DoanhThuDPH, @HoaHong, @TienThanhToan", param1, param2, param3, param4, param5, param6, param7, param8);
            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
            MaDaiLy = _PhatHanh.MaDaiLy;
            MaLoaiVeSo = _PhatHanh.MaLoaiVeSo;
            NgayNhan = _PhatHanh.NgayNhan;
            _PhatHanh.DoanhThuDPH = _PhatHanh.SLBan * _LVS.GiaVe;
            db.PhatHanhs.Add(_PhatHanh);
            SLGiao slg = new SLGiao();
            _PhatHanh.SoLuong = slg.TinhToanSLPhatTheoDaiLy(MaDaiLy, MaLoaiVeSo, NgayNhan);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        [HttpGet]
        public ActionResult ChinhSua(int _ID)
        {
            PhatHanh ph = db.PhatHanhs.SingleOrDefault(n => n.ID == _ID);
            if (ph == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaDaiLy = new SelectList(db.DaiLies.ToList().OrderBy(n => n.TenDaiLy), "MaDaiLy", "TenDaiLy", ph.MaDaiLy);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes.ToList().OrderBy(n => n.Tinh), "MaLoaiVeSo", "Tinh", ph.MaLoaiVeSo);
            return View(ph);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ChinhSua(PhatHanh _PhatHanh)
        {
            ViewBag.MaDaiLy = new SelectList(db.DaiLies.ToList().OrderBy(n => n.TenDaiLy), "MaDaiLy", "TenDaiLy", _PhatHanh.MaDaiLy);
            ViewBag.MaLoaiVeSo = new SelectList(db.LoaiVesoes.ToList().OrderBy(n => n.Tinh), "MaLoaiVeSo", "Tinh", _PhatHanh.MaLoaiVeSo);
            if (!ModelState.IsValid)
            {
                return View();
            }
            db.Entry(_PhatHanh).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Xoa(int _ID)
        {
            PhatHanh ph = db.PhatHanhs.SingleOrDefault(n => n.ID == _ID);
            if (ph == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(ph);
        }

        [HttpPost, ActionName("Xoa")]
        [ValidateInput(false)]
        public ActionResult XacNhanXoa(int _ID)
        {
            PhatHanh ph = db.PhatHanhs.SingleOrDefault(n => n.ID == _ID);

            if (ph == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.PhatHanhs.Remove(ph);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}