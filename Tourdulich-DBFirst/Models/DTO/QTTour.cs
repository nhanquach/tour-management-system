using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Models.DTO
{
    public class QTTour
    {
        public int MaKH { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> NgayDi { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public Nullable<System.DateTime> NgayKT { get; set; }

        public string TenTour { get; set; }
        public string ThongTinTour { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> GiaTour { get; set; }

        [DisplayFormat(DataFormatString = "{0:n0}", ApplyFormatInEditMode = true)]
        public Nullable<decimal> TongTien { get; set; }
        public string SoNguoi { get; set; }
    }
}
