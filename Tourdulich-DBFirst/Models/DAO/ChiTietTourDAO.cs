using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.BUS;
using Models.DTO;

namespace Models.DAO
{
    public class ChiTietTourDAO
    {
        QuanLyTourEntities db = null;
        public ChiTietTourDAO()
        {
            db = new QuanLyTourEntities();
        }

        public int ThemChiTiet(ChiTietTourDTO ctt)
        {
            ChiTietTour ct = new ChiTietTour();
            ct.MaTour = ctt.MaTour;
            ct.MaDiaDiem = ctt.MaDiaDiem;
            db.ChiTietTours.Add(ct);
            db.SaveChanges();
            return 1;
        }

        public int XoaChiTiet(int matour)
        {
            IEnumerable<ChiTietTour> chitiet;
            chitiet = db.ChiTietTours.OrderBy(x => x.MaCTTour).ToList();
            foreach (var t1 in chitiet)
            {
                if (t1.MaTour == matour)
                    db.ChiTietTours.Remove(t1);
            }
            db.SaveChanges();
            return 1;
        }
    }
}
