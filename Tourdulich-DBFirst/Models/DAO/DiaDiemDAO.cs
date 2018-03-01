using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Models.BUS;
using Models.DTO;
using System.Diagnostics;

namespace Models.DAO
{
    public class DiaDiemDAO
    {
        QuanLyTourEntities db = null;
        public DiaDiemDAO()
        {
            db = new QuanLyTourEntities();
        }
        public List<DiaDiem> DanhSachDiaDiem()
        {
            return db.DiaDiems.OrderBy(x => x.MaDiaDiem).ToList();
        }

        public List<DiaDiem> DSDiaDiem_Tour(int matour)
        {
            var diadiem = from d in db.Tours
                          join ct in db.ChiTietTours on d.MaTour equals ct.MaTour
                          join dd in db.DiaDiems on ct.MaDiaDiem equals dd.MaDiaDiem
                          where d.MaTour == matour
                          select dd;
            return diadiem.ToList();

        }

        public int Search_madd(string namedd)
        {
            var t = db.DiaDiems.Where(x => x.TenDiaDiem == namedd).FirstOrDefault();
            if (t != null)
            {
                int maDiaDiem = t.MaDiaDiem;
                return maDiaDiem;
            }
            else
                return 0;
        }
    }
}
