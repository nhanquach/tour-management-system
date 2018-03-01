using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.BUS;
using Models.DAO;

namespace Models.DTO
{
    public class DiaDiemDTO
    {
        public int MaDiaDiem { get; set; }
        public string TenDiaDiem { get; set; }
        public string ThongTinDD { get; set; }
        public string Tinh { get; set; }
        public string Nuoc { get; set; }
    }
}
