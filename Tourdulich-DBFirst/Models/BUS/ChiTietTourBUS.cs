using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.DAO;
using Models.DTO;

namespace Models.BUS
{
    public class ChiTietTourBUS
    {
        public int themChiTiet(ChiTietTourDTO ctt)
        {
            ChiTietTourDAO cttour = new ChiTietTourDAO();
            return cttour.ThemChiTiet(ctt);
        }

        public int xoaChiTiet(int matour)
        {
            ChiTietTourDAO cttour = new ChiTietTourDAO();
            return cttour.XoaChiTiet(matour);
        }
    }
}
