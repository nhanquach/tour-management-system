using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models;
using PagedList;
using System.Web.Script.Serialization;
using System.Threading.Tasks;
namespace TourWeb.Controllers
{
    public class DoanController : Controller
    {
        //
        // GET: /Doan/
        [HttpGet]
        public ActionResult Them(string MaTour)
        {
            if (MaTour == null)
                return RedirectToAction("Danhsach", "Tour");
            TempData["MaTour"] = MaTour;
            var model = new KhachHangDAO().KH_Doan();
            TempData["KH"] = model;
            return View();
        }

        [HttpPost]
        public ActionResult Them(Doan doan)
        {
            var model = new KhachHangDAO().KH_Doan();
            TempData["KH"] = model;
            List<KhachHang> khachhang = Session["KH_DaChon"] as List<KhachHang>;
            DoanDAO doandao = new DoanDAO();
            if (doandao.KTTrungNgay(doan))
            {
                if (khachhang != null && khachhang.Count > 0)
                    TempData["CoKH"] = "1";
                IEnumerable<Doan> doanMoi = doandao.LayDoanTheoNgay(doan);
                foreach (var d in doanMoi)
                {
                    TempData["TenDoanMoi"] = d.TenDoan;
                    TempData["MaDoanMoi"] = d.MaDoan;
                }
                return RedirectToAction("Them", "Doan", new { MaTour = doan.MaTour });
            }
            else
            {
                int MaDoan = doandao.ThemDoan(doan);
                if (khachhang != null && khachhang.Count > 0)
                {
                    foreach (var kh in khachhang)
                        new KhachHangDAO().CapNhatMaDoan(kh.MaKH, MaDoan);
                    Session.Remove("KH_DaChon");
                }
                return RedirectToAction("Danhsach", "Doan", new { MaTour = doan.MaTour });
            }

        }

        public JsonResult ChonKH_Doan(string madoan)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var MaDoan = serializer.Deserialize<string>(madoan);
            int MaTour = new TourDAO().MaTour_MaDoan(Int16.Parse(MaDoan));
            var model = new KhachHangDAO().DanhSachKhach();
            ViewBag.KH = model;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult ChonKH(string arrayid)
        {
            List<KhachHang> kh;
            kh = new List<KhachHang>();
            KhachHangDAO dao = new KhachHangDAO();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            var arrayID = serializer.Deserialize<List<string>>(arrayid);
            foreach (var id in arrayID)
                kh.Add(dao.LayKH_MaKH(Int16.Parse(id)));
            try
            {
                if (Session["KH_DaChon"] != null)
                {
                    Session.Remove("KH_DaChon");
                    Session["KH_DaChon"] = kh;
                }
                else
                    Session["KH_DaChon"] = kh;
                return Json(new
                {
                    status = true
                });
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public JsonResult ThemKH_Doan(string arrayid, string madoan, string matour)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int madoanmoi = Int16.Parse(serializer.Deserialize<string>(madoan));
            int matourmoi = Int16.Parse(serializer.Deserialize<string>(matour));
            List<KhachHang> kh = new List<KhachHang>();
            var arrayID = serializer.Deserialize<List<string>>(arrayid);
            int kttrung = 0;
            foreach (var id in arrayID)
            {
                int KTTrung = new KhachHangDAO().KTTrungNgay_KH(Int16.Parse(id), madoanmoi, matourmoi);
                if (KTTrung == -1)
                {
                    kttrung = -1;
                    break;
                }
                if (KTTrung == 0)
                {
                    kttrung = 0;
                    break;
                }
                if (KTTrung == 1)
                {
                    kttrung = 1;
                    break;
                }
                if (KTTrung == 2)
                {
                    new KhachHangDAO().CapNhatMaDoan(Int16.Parse(id), madoanmoi);
                    kttrung = 2;
                }
            }
            if (kttrung == -1)
            {
                return Json(new
                {
                    status = -1
                });
            }
            if (kttrung == 0)
            {
                return Json(new
                {
                    status = 0
                });
            }
            if (kttrung == 1)
            {
                return Json(new
                {
                    status = 1
                });
            }
            else
            {
                return Json(new
                {
                    status = 2
                });
            }
        }

        public JsonResult ThemDoanMoi(string madoan, string matour)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int madoanmoi = Int16.Parse(serializer.Deserialize<string>(madoan));
            int matourmoi = Int16.Parse(serializer.Deserialize<string>(matour));
            List<KhachHang> khachhang = Session["KH_DaChon"] as List<KhachHang>;
            int kttrung = 0;
            foreach (var kh in khachhang)
            {
                bool KTTrung = new KhachHangDAO().KTTrungKH(kh.MaKH, madoanmoi, matourmoi);
                if (KTTrung)
                {
                    kttrung = -1;
                    break;
                }
                else
                {
                    new KhachHangDAO().CapNhatMaDoan(kh.MaKH, madoanmoi);
                    kttrung = 1;
                }
            }
            if (kttrung == -1)
            {
                return Json(new
                {
                    status = 0
                });
            }
            else
            {
                Session.Remove("KH_DaChon");
                return Json(new
                {
                    status = 1
                });
            }
        }

        [HttpGet]
        public ActionResult Danhsach(int? MaTour, string searchString, int page = 1, int pageSize = 10)
        {
            var model = new DoanDAO().DanhSachDoan_MaTour(MaTour, searchString, page, pageSize);
            TempData["TenTour"] = new TourDAO().TenTour(MaTour);
            TempData["MaTour"] = MaTour;
            var kh = new KhachHangDAO().DanhSachKhach();
            TempData["KH"] = kh;
            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult TimTour(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new TourDAO();
            var model = dao.TimTour(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return RedirectToAction("Them", model);
        }

        public JsonResult XoaKH(string makh)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            int maKH = Int16.Parse(serializer.Deserialize<string>(makh));
            List<KhachHang> khachhang = Session["KH_DaChon"] as List<KhachHang>;
            khachhang.RemoveAll(x => x.MaKH == maKH);
            Session.Remove("KH_DaChon");
            try
            {
                Session["KH_DaChon"] = khachhang;
                return Json(new
                {
                    status = true
                });
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        [HttpGet]
        public ActionResult Sua(int MaDoan)
        {
            var model = new DoanDAO().LayDoan_MaDoan(MaDoan);
            if (model.TinhTrang == "Kết thúc")
            {
                return RedirectToAction("Danhsach", "Doan");
            }
            ViewBag.ListKH_Doan = new KhachHangDAO().DanhSachKH_MaDoan(MaDoan);
            return View(model);
        }

        [HttpPost]
        public ActionResult Sua(Doan doan)
        {           
            new DoanDAO().SuaDoan(doan);
            if (doan.TinhTrang == "Đang đi" || doan.TinhTrang == "Kết thúc")
                new KhachHangDAO().QuaTrinhDiTour(doan.MaDoan);
            return RedirectToAction("Danhsach", "Doan", new { MaTour = doan.MaTour });
        }

        public ActionResult XoaKH_Doan(int MaKH, int MaDoan)
        {
            new KhachHangDAO().XoaKH_Doan(MaKH, MaDoan);
            return RedirectToAction("Sua", "Doan", new { MaDoan = MaDoan });
        }

        [HttpPost]
        public ActionResult DSKhach(string searchString)
        {
            var model = new KhachHangDAO().KH_Doan();
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrEmpty(searchString) && model != null && model.Count > 0)
                {
                    var searchedlist = (from list in model
                                        where list.TenKH.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                                        select list
                                            ).ToList();
                    return PartialView("DSKhach", searchedlist);
                }
                else
                {
                    return PartialView("DSKhach", model);
                }
            }
            else
            {
                return PartialView("DSKhach", model);
            }
        }

        [HttpPost]
        public ActionResult DSKhach1(string searchString)
        {
            var model = new KhachHangDAO().DanhSachKhach();
            if (Request.IsAjaxRequest())
            {
                if (!string.IsNullOrEmpty(searchString) && model != null && model.Count > 0)
                {
                    var searchedlist = (from list in model
                                        where list.TenKH.IndexOf(searchString, StringComparison.OrdinalIgnoreCase) >= 0
                                        select list
                                            ).ToList();
                    return PartialView("DSKhach", searchedlist);
                }
                else
                {
                    return PartialView("DSKhach", model);
                }
            }
            else
            {
                return PartialView("DSKhach", model);
            }
        }
    }
}

