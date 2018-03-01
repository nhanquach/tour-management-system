using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DAO;
using Models.BUS;

namespace Models.DTO
{
    public class ChiTietTourDTO
    {
        public int MaCTTour { get; set; }
        public int MaTour { get; set; }
        public int MaDiaDiem { get; set; }
    }
}
