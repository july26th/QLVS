using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuanLyVeSo.Areas.Administrator.Models
{
    public class SLGiao
    {
        QLVSEntities db = new QLVSEntities();
        public decimal TinhToanSLPhatTheoDaiLy(string MaDaiLy, string MaLoaiVeSo, System.DateTime NgayNhan)
        {

            decimal SLDK = db.SoLuongDKs.OrderByDescending(m => m.NgayDK).Where(m => m.MaDaiLy == MaDaiLy & System.DateTime.Compare(m.NgayDK, NgayNhan) <= 0).Select(m => m.SLDK).FirstOrDefault();
            System.DateTime NgayDK = db.SoLuongDKs.OrderByDescending(m => m.NgayDK).Where(m => m.MaDaiLy == MaDaiLy & System.DateTime.Compare(m.NgayDK, NgayNhan) <= 0).Select(m => m.NgayDK).FirstOrDefault();
            var listTop3 = db.PhatHanhs.Where(m => m.MaDaiLy == MaDaiLy & System.DateTime.Compare(m.NgayNhan, NgayNhan) <= 0 & m.SLBan != null).OrderByDescending(m => m.NgayNhan).Take(3);
            int count = listTop3.Count();
            if (count == 0)
            {
                return SLDK;
            }
            else
            {
                decimal? sum = 0;
                foreach (var item in listTop3)
                {
                    sum += item.SLBan / item.SoLuong;
                }
                decimal? getReturn = Math.Round((decimal)sum / count * SLDK);
                return getReturn ??default(decimal);
            }
        }
        public int TT()
        {
            int sldk = 100;
            return sldk * 2;
        }
        public int ID { get; set; }
        public string MaDaiLy { get; set; }
        public string MaLoaiVeSo { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public DateTime NgayNhan { get; set; }
        public Nullable<int> SLBan { get; set; }
        public Nullable<decimal> DoanhThuDPH { get; set; }
        public Nullable<decimal> HoaHong { get; set; }
        public Nullable<decimal> TienThanhToan { get; set; }

        public virtual DaiLy DaiLy { get; set; }
        public virtual LoaiVeso LoaiVeso { get; set; }
       
    }
}