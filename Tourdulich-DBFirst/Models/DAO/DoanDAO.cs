using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
namespace Models.DAO
{
    public class DoanDAO
    {
        QuanLyTourEntities db = null;
        public DoanDAO()
        {
            db = new QuanLyTourEntities();
        }
        public IEnumerable<Doan> DanhSachDoan_MaTour(int? matour, string search,int page , int pageSize)
        {
            if(matour!=null)
                return db.Doans.Where(x => x.MaTour == matour).OrderByDescending(x=>x.MaDoan).ToPagedList(page, pageSize);
            else
            {
                IQueryable<Doan> model = db.Doans;
                if (!string.IsNullOrEmpty(search))
                {
                    model = model.Where(x => x.TenDoan.Contains(search));
                }
                return model.OrderByDescending(x=>x.MaDoan).ToPagedList(page, pageSize);
            }
        }

       

        public string TenDoan(int madoan)
        {
            var doan = db.Doans.Find(madoan);
            string tendoan = doan.TenDoan;
            return tendoan;
        }

        public IEnumerable<Doan> DSDoan_Ngay(DateTime ngaydi, DateTime ngaykt)
        {
            IQueryable<Doan> model=db.Doans;
            model= db.Doans.Where(x=>x.NgayDi>=ngaydi && x.NgayKT<=ngaykt);
            return model;
        }

        public int ThemDoan(Doan doan)
        {
            doan.TinhTrang = "Chờ";
            db.Doans.Add(doan);
            db.SaveChanges();            
            return doan.MaDoan;
        }

        public bool KTTrungNgay(Doan doan)
        {
            var model = db.Doans.Where(x => x.NgayDi == doan.NgayDi && x.NgayKT == doan.NgayKT);
            if (model.Count()==0)
                return false;
            else
                return true;

        }

        public IEnumerable<Doan> LayDoanTheoNgay(Doan doan)
        {
            return db.Doans.Where(x => x.NgayDi == doan.NgayDi && x.NgayKT == doan.NgayKT).Take(1).ToList();
            
        }

        public Doan LayDoan_MaDoan(int madoan)
        {
            return db.Doans.Find(madoan);
        }

        public void SuaDoan(Doan doan)
        {
            Doan d = db.Doans.Find(doan.MaDoan);
            d.TenDoan = doan.TenDoan;
            d.NoiDung = doan.NoiDung;
            d.NgayDi = doan.NgayDi;
            d.NgayKT = doan.NgayKT;
            d.TinhTrang = doan.TinhTrang;
            db.SaveChanges();
        }

        public bool KTTrungTen(Doan doan)
        {
            var d = db.Doans.Where(x => x.TenDoan == doan.TenDoan);
            if (d.Count() == 0)
                return false;
            else
                return true;
        }

        
    }
}
