using Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
namespace Models.DAO
{
    public class KhachHangDAO
    {
        QuanLyTourEntities db = null;
        public KhachHangDAO()
        {
            db = new QuanLyTourEntities();
        }
        public IEnumerable<KhachHang> DanhSachKhach_MaDoan(int madoan,int page = 1, int pageSize = 10)
        {
            var khachhang = from d in db.Doans
                            join ct in db.ChiTietDoans on d.MaDoan equals ct.MaDoan
                            join kh in db.KhachHangs on ct.MaKH equals kh.MaKH
                            where d.MaDoan == madoan
                            select kh;
            return khachhang.OrderByDescending(x=>x.MaKH).ToPagedList(page, pageSize); 
        }
        public List<KhachHang> DanhSachKH_MaDoan(int madoan)
        {
            var khachhang = from d in db.Doans
                            join ct in db.ChiTietDoans on d.MaDoan equals ct.MaDoan
                            join kh in db.KhachHangs on ct.MaKH equals kh.MaKH
                            where d.MaDoan == madoan
                            select kh;
            return khachhang.ToList();
        }

        public KhachHang KHTrung(KhachHang kh)
        {
            return db.KhachHangs.Find(kh);
            
        }

        public int Insert(KhachHang kh)
        {
            var model= db.KhachHangs.Where(x=>x.CMND==kh.CMND).FirstOrDefault();
            if (model != null)
                return model.MaKH;
            else
            { 
                db.KhachHangs.Add(kh);
                db.SaveChanges();
            }
            return 0;
        }
        public IEnumerable<KhachHang> DanhSachKH(string search,int page, int pageSize)
        {
            IQueryable<KhachHang> model = db.KhachHangs;
            if (!string.IsNullOrEmpty(search))
            {
                model = model.Where(x => x.TenKH.Contains(search));
            }
            return model.OrderByDescending(x => x.MaKH).ToPagedList(page, pageSize); 
        }
        public List<KhachHang> DanhSachKhach()
        {
            return db.KhachHangs.ToList();
        }

        public List<KhachHang> KH_Doan()
        {
            var model=(from d in db.Doans join ct in db.ChiTietDoans on d.MaDoan equals ct.MaDoan
                         join k in db.KhachHangs on ct.MaKH equals k.MaKH
                         where d.TinhTrang=="Kết thúc" 
                         select new
                         {
                             makh = k.MaKH,
                             tenkh = k.TenKH,
                             cmnd = k.CMND,
                             diachi = k.DiaChi,
                             gioitinh = k.GioiTinh,
                             sodt = k.SoDT
                         }).AsEnumerable().Select(x => new Models.KhachHang()
                         {
                             MaKH = x.makh,
                             TenKH = x.tenkh,
                             CMND = x.cmnd,
                             DiaChi = x.diachi,
                             GioiTinh = x.gioitinh,
                             SoDT = x.sodt
                         }).ToList();
            if (model.Count == 0)
                model = db.KhachHangs.ToList();
            return model;
        }


        public KhachHang LayKH_MaKH(int makh)
        {
            return db.KhachHangs.Find(makh);
        }

        public void CapNhatMaDoan(int maKH, int maDoan)
        {      
            ChiTietDoan ctdoan=new ChiTietDoan();
            ctdoan.MaKH=maKH;
            ctdoan.MaDoan=maDoan;
            db.ChiTietDoans.Add(ctdoan);
            db.SaveChanges();
        }

        public bool KTTrungKH(int makh, int madoan, int matour)
        {
            var kh = from t in db.Tours
                     join d in db.Doans on t.MaTour equals d.MaTour
                     join ct in db.ChiTietDoans on d.MaDoan equals ct.MaDoan
                     join k in db.KhachHangs on ct.MaKH equals k.MaKH
                     where d.MaDoan == madoan && t.MaTour == matour && k.MaKH == makh
                     select k.MaKH;
            
            if (kh.Count()>0)
                return true;
            else
                return false;
        }

        public void QuaTrinhDiTour(int maDoan)
        {

            var model = (from t in db.Tours
                         join d in db.Doans on t.MaTour equals d.MaTour
                         join ct in db.ChiTietDoans on d.MaDoan equals ct.MaDoan
                         join k in db.KhachHangs on ct.MaKH equals k.MaKH
                         where d.MaDoan == maDoan
                         select new
                         {
                             makh = k.MaKH,
                             ngaydi = d.NgayDi,
                             ngaykt = d.NgayKT,
                             tentour = t.TenTour,
                             thongtintour = t.ThongTinTour,
                             giatour = t.GiaTour
                         }).AsEnumerable().Select(x => new QTTour()
                         {
                             MaKH = x.makh,
                             TenTour = x.tentour,
                             ThongTinTour = x.thongtintour,
                             GiaTour = x.giatour,
                             NgayDi = x.ngaydi,
                             NgayKT = x.ngaykt
                         }).ToList();
            foreach(var m in model)
            {
                QuaTrinhTour qt = new QuaTrinhTour();
                qt.MaKH = m.MaKH;
                qt.TenTour = m.TenTour;
                qt.ThongTinTour = m.ThongTinTour;
                qt.GiaTour = m.GiaTour;
                qt.NgayDi = m.NgayDi;
                qt.NgayKT = m.NgayKT;
                db.QuaTrinhTours.Add(qt);
                db.SaveChanges();
            }
            
        }

        public List<QuaTrinhTour> QuaTrinhTour(int makh, DateTime ngay_di, DateTime ngay_kt)
        {

            var model = (from qt in db.QuaTrinhTours
                         where qt.MaKH == makh && ((ngay_di <= qt.NgayDi || ngay_di <= qt.NgayKT) && (ngay_kt >= qt.NgayDi || ngay_kt >= qt.NgayKT))
                         select new
                         {
                             makh = qt.MaKH,
                             ngaydi = qt.NgayDi,
                             ngaykt = qt.NgayKT,
                             tentour = qt.TenTour,
                             thongtintour = qt.ThongTinTour,
                             giatour = qt.GiaTour
                         }).AsEnumerable().Select(x => new Models.QuaTrinhTour()
                         {
                             MaKH = x.makh,
                             NgayDi = x.ngaydi,
                             NgayKT = x.ngaykt,
                             TenTour = x.tentour,
                             ThongTinTour = x.thongtintour,
                             GiaTour = x.giatour
                         });
            model.OrderByDescending(x => x.NgayDi);
            return model.ToList();
        }

        public void XoaKH(int makh)
        {
            var kh_ctdoan = db.ChiTietDoans.Where(x=>x.MaKH==makh);
            db.ChiTietDoans.RemoveRange(kh_ctdoan);
            var kh = db.KhachHangs.Find(makh);
            db.KhachHangs.Remove(kh);
            db.SaveChanges();
        }

        public void XoaKH_Doan(int makh, int madoan)
        {
            ChiTietDoan ctdoan = db.ChiTietDoans.Where(x => x.MaKH == makh && x.MaDoan==madoan).First();
            db.ChiTietDoans.Remove(ctdoan);
            db.SaveChanges();
        }

        public int KTTrungNgay_KH(int maKH, int maDoan, int maTour)
        {
            int kt=0;
            var doan = db.Doans.Find(maDoan);
            if (doan.TinhTrang == "Đang đi")
                kt = -1;
            if(doan.TinhTrang=="Chờ")
            {
                if (KTTrungKH(maKH, maDoan,maTour))
                    kt = 0;
                if (KTTrungNgayKH_Doan(maKH, doan.NgayDi, doan.NgayKT))
                    kt = 1;
                else
                    kt = 2;
            }
            return kt;
        }

        public bool KTTrungNgayKH_Doan(int maKH,DateTime? ngaydi, DateTime? ngaykt)
        {
            
            List<Doan> listDoan=new List<Doan>();
            var madoan_ctdoan = db.ChiTietDoans.Where(x=>x.MaKH==maKH).Select(x => x.MaDoan);
            if (madoan_ctdoan == null)
                return false;
            else
            {
                foreach(var madoan in madoan_ctdoan)
                {
                    var model = db.Doans.Find(madoan);
                    listDoan.Add(model);
                }
                foreach(var d in listDoan)
                {
                    if((ngaydi > d.NgayKT) || (ngaykt<d.NgayDi ))
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                        
                    }
                }
            }
            return false;
        }

        public List<KhachHang> TimTen(string searchstring)
        {
            return db.KhachHangs.Where(x => x.TenKH.Contains(searchstring)).ToList();
        }

    }
}
