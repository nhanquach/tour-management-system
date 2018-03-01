using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTO
{
    public class ThongKeDTO
    {
        public int MaTour { get; set; }
        public string TenTour { get; set; }
        public Nullable<System.DateTime> NgayDi { get; set; }
        public Nullable<System.DateTime> NgayKT { get; set; }
        public Nullable<decimal> GiaTour { get; set; }
        public Nullable<decimal> SoNguoi { get; set; }
    }
}
