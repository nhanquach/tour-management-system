using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using Models.DTO;
namespace Models.DAO
{
    public class TourDAO
    {
        QuanLyTourEntities db = null;
        public TourDAO()
        {
            db = new QuanLyTourEntities();
        }
        public List<Tour> DanhSachTour()
        {
            return db.Tours.OrderBy(x=>x.MaTour).ToList();
        }
        
        public IEnumerable<Tour> TimTour(string searchString, int page , int pageSize)
        {
            IQueryable<Tour> model = db.Tours;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.TenTour.Contains(searchString));
            }

            return model.OrderBy(x=>x.MaTour).ToPagedList(page,pageSize);
        }

        public List<Tour> TimTour_Ten(string tentour)
        {

            return db.Tours.Where(x => x.TenTour.Contains(tentour)).OrderBy(x => x.MaTour).ToList();

        }

        public List<Tour> TimTour_Thongtin(string thongtintour)
        {

            return db.Tours.Where(x => x.ThongTinTour.Contains(thongtintour)).OrderBy(x => x.MaTour).ToList();

        }

        public List<Tour> TimTour_Gia(int gia1, int gia2)
        {

            return db.Tours.Where(x => x.GiaTour>=gia1 && x.GiaTour<=gia2).OrderBy(x => x.MaTour).ToList();

        }

        public string TenTour(int? maTour)
        {
            string tentour=null;
            var tour = db.Tours.Find(maTour);
            if(tour!=null)
                tentour=tour.TenTour;
            return tentour;
        }

        public int ThemTour(TourDTO t)
        {
            if (db.Tours.Where(x => x.MaTour == t.MaTour).FirstOrDefault() == null)
            {
                Tour tour = new Tour();
                tour.TenTour = t.TenTour;
                tour.ThongTinTour = t.ThongTinTour;
                tour.GiaTour = t.GiaTour;
                db.Tours.Add(tour);
                db.SaveChanges();
                return tour.MaTour;
            }
            return 0;

        }

        public int SuaTour(TourDTO tour)
        {
            var t = db.Tours.Where(x => x.MaTour == tour.MaTour).FirstOrDefault();
            if (tour != null)
            {
                t.MaTour = tour.MaTour;
                t.TenTour = tour.TenTour;
                t.ThongTinTour = tour.ThongTinTour;
                t.GiaTour = tour.GiaTour;
                db.SaveChanges();
                return 1;
            }
            return 0;
        }

        public int MaTour_MaDoan(int MaDoan)
        {
            var doan = db.Doans.Find(MaDoan);
            var tour = db.Tours.Find(doan.MaTour);
            return tour.MaTour;
        }

        public List<QTTour> TKDT(DateTime ngay_bd, DateTime ngay_kt)
        {
            var model = (from qt in db.QuaTrinhTours
                         where (ngay_bd <= qt.NgayDi || ngay_bd <= qt.NgayKT) && (ngay_kt >= qt.NgayDi || ngay_kt >= qt.NgayKT)
                         group qt by new { qt.TenTour, qt.NgayDi, qt.NgayKT } into g
                         select new
                         {
                             SoNguoi = g.Count(),
                             TongTien = g.Sum(i => i.GiaTour)
                         }).ToList();
            var model1 = (from qt in db.QuaTrinhTours
                          where (ngay_bd <= qt.NgayDi || ngay_bd <= qt.NgayKT) && (ngay_kt >= qt.NgayDi || ngay_kt >= qt.NgayKT)
                          select new
                          {
                              ngaydi = qt.NgayDi,
                              ngaykt = qt.NgayKT,
                              tentour = qt.TenTour,
                              thongtintour = qt.ThongTinTour,
                              giatour = qt.GiaTour
                          }).Distinct().AsEnumerable().Select(x => new QTTour()
                          {
                              NgayDi = x.ngaydi,
                              NgayKT = x.ngaykt,
                              TenTour = x.tentour,
                              ThongTinTour = x.thongtintour,
                              GiaTour = x.giatour
                          }).ToList();
                int i1 = 0;
                foreach(var m1 in model1)
                { 

                    m1.SoNguoi = model.ElementAt(i1).SoNguoi.ToString();
                    m1.TongTien = model.ElementAt(i1).TongTien;
                    i1++;
                }
            
            return model1;
        }

      
        public decimal? TongTien(DateTime ngay_bd, DateTime ngay_kt)
        {
            decimal? tongtien=0;
            var gia = db.QuaTrinhTours.Where(x => (ngay_bd <= x.NgayDi || ngay_bd <= x.NgayKT) && (ngay_kt >= x.NgayDi || ngay_kt >= x.NgayKT)).Select(x => x.GiaTour);
            foreach(var t in gia)
            {
                tongtien += t;
            }
            return tongtien;
        }
    }
}
