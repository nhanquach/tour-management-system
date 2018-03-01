using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models;
using System.Web.Script.Serialization;
using Models.DTO;
namespace TourWeb.Controllers
{
    public class KhachHangController : Controller
    {
        //
        // GET: /KhachHang/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Danhsach(string MaDoan, string searchString, int page = 1, int pageSize = 10)
        {
            if(MaDoan==null)
            {
                var model = new KhachHangDAO().DanhSachKH(searchString,page, pageSize);
                ViewBag.SearchString = searchString;
                return View(model);
            }
            else
            {
                var model = new KhachHangDAO().DanhSachKhach_MaDoan(Int16.Parse(MaDoan), page , pageSize);
                TempData["TenDoan"] = new DoanDAO().TenDoan(Int16.Parse(MaDoan));
                return View(model);
            }
            
        }
        //
        // GET: /KhachHang/Create
        [HttpGet]
        public ActionResult Them()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Them(KhachHang kh)
        {
            if (ModelState.IsValid)
            {
                var dao = new KhachHangDAO();
                if(dao.Insert(kh)==0)
                    return RedirectToAction("Danhsach");
                else
                {
                    ViewBag.Message = "Đã tồn tại khách hàng này. Xin hãy đặt tour.";
                    return View(kh);
                }
            }
            
            return View(kh);
        }

        [HttpGet]
        public ActionResult QuaTrinh(string MaKH)
        {
            if (MaKH == null)
                return RedirectToAction("Danhsach", "KhachHang");
            var kh = new KhachHangDAO().LayKH_MaKH(Int16.Parse(MaKH));
            TempData["TenKH"] = kh.TenKH;
            TempData["MaKH"] = kh.MaKH;
            return View();
        }
        
        [HttpPost]
        public ActionResult QuaTrinh()
        {
            string makh = Request.Form["makh"];
            string ngay_di = Request.Form["ngay_di"];
            string ngay_kt = Request.Form["ngay_kt"];
            var kh = new KhachHangDAO().LayKH_MaKH(Int16.Parse(makh));
            TempData["TenKH"] = kh.TenKH;
            TempData["MaKH"] = kh.MaKH;
            var qttour = new KhachHangDAO().QuaTrinhTour(Int16.Parse(makh), DateTime.Parse(ngay_di), DateTime.Parse(ngay_kt));
            return View("QuaTrinh",qttour);
        }

        [HttpGet]
        public ActionResult XoaKH(int MaKH)
        {

            new KhachHangDAO().XoaKH(MaKH);
            return RedirectToAction("Danhsach","KhachHang");
        }
    }
}
