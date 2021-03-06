﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyVeSo.Areas.Administrator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class LoaiVeso
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LoaiVeso()
        {
            this.KetQuaSoXoes = new HashSet<KetQuaSoXo>();
            this.PhatHanhs = new HashSet<PhatHanh>();
        }
        [Display(Name = "Mã Loại Vé Số")] //Thuộc tính Display đặt lại tên cho cột
        public string MaLoaiVeSo { get; set; }
        [Display(Name = "Tên Tỉnh")] //Thuộc tính Display đặt lại tên cho cột
        public string Tinh { get; set; }
        [Display(Name = "Giá Vé")] //Thuộc tính Display đặt lại tên cho cột
        [Required(ErrorMessage = "{0} không được để trống")] //Kiểm tra rỗng
        public Nullable<decimal> GiaVe { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KetQuaSoXo> KetQuaSoXoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PhatHanh> PhatHanhs { get; set; }
    }
}
