using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service;
using WebApplication1.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        private ApplicationDbContext context;
        private static bool UpdateDatabase = false;
        private ISession _session;

        public ISession Session { get { return _session; } }

        public HomeController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            //trả về data NguoiDung từ CSDL chuyển vào table view Index
            var nguoiDung = context.NguoiDung.ToList();
            return View(nguoiDung);
        }
        [HttpPost]
        public IActionResult Index(NguoiDungDTO nguoiDungDTO)
        {

            //kiểm tra lỗi
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            bool isError = false;
            // thêm NguoiDung vào CSDL
            NguoiDung nguoiDung = new NguoiDung()
            {
                UserID = nguoiDungDTO.UserID.Trim().ToUpper(),
                UserName = nguoiDungDTO.UserName,
                Email = nguoiDungDTO.Email,
                Tel = nguoiDungDTO.Tel,
                Password = nguoiDungDTO.Password,
                Disable = 1
            };
            // Kiểm tra mã người dùng tồn tại hay chưa
            using (context)
            {
                // trả số lượng người dùng được tìm thấy
                var existingUser = context.NguoiDung.FirstOrDefault(u => u.UserID == nguoiDungDTO.UserID);

                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Mã người dùng đã tồn tại");
                    return RedirectToAction("Index");
                }
                else
                {
                    
                    //thêm data NguoiDung vào CSDL
                    context.NguoiDung.Add(nguoiDung);
                    try
                    {
                        // Lưu thay đổi vào cơ sở dữ liệu
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    catch (DbUpdateException ex)
                    {
                        context.NguoiDung.Update(nguoiDung);
                        context.SaveChanges();
                        return RedirectToAction("Index");
                    }

                }
            }
            
        }
        
        public IActionResult Detail(string userid)
        {
            if (userid == null)
            {
                return RedirectToAction("Index");
            }
            //trả về data NguoiDung từ CSDL chuyển vào table view Index
            var nguoiDung = context.NguoiDung.Find(userid);
            if (nguoiDung == null)
            {
                return RedirectToAction("Index");
            }

            var nguoiDungDTO = new NguoiDungDTO()
            {
                UserID = nguoiDung.UserID,
                UserName = nguoiDung.UserName,
                Email = nguoiDung.Email,
                Tel = nguoiDung.Tel,
                Password = nguoiDung.Password,
                Disable = 1
            };

            return Json(nguoiDungDTO);
        }

        public IActionResult Delete(string userid)
        {
            if (userid == null)
            {
                return RedirectToAction("Index");
            }
            //trả về data NguoiDung từ CSDL chuyển vào table view Index
            var nguoiDung = context.NguoiDung.Find(userid);
            if (nguoiDung !=null)
            {
                context.NguoiDung.Remove(nguoiDung);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }

        public JsonResult Grid_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(context.NguoiDung.ToList().ToDataSourceResult(request));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, NguoiDung nguoiDungDTO)
        {
            if (nguoiDungDTO != null && ModelState.IsValid)
            {
                // thêm NguoiDung vào CSDL
                NguoiDung nguoiDung = new NguoiDung()
                {
                    UserID = nguoiDungDTO.UserID,
                    UserName = nguoiDungDTO.UserName,
                    Email = nguoiDungDTO.Email,
                    Tel = nguoiDungDTO.Tel,
                    Password = nguoiDungDTO.Password,
                    Disable = 1
                };

             
                    context.NguoiDung.Update(nguoiDung);
                    context.SaveChanges();
                    return Json(new[] { nguoiDungDTO }.ToDataSourceResult(request, ModelState));

            }

            return Json(new[] { nguoiDungDTO }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("Post")]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, NguoiDung nguoiDung)
        {
            using (context)
            {
                var entity = new NguoiDung();

                entity.UserID = nguoiDung.UserID;

                context.NguoiDung.Attach(entity);

                context.NguoiDung.Remove(entity);

                var orderDetails = context.NguoiDung.Where(pd => pd.UserID == entity.UserID);

                foreach (var orderDetail in orderDetails)
                {
                    context.NguoiDung.Remove(orderDetail);
                }

                context.SaveChanges();
            }
            return Json(new[] { nguoiDung }.ToDataSourceResult(request, ModelState));
        }


        /// <summary>
        /// Đang edit ở đây
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Validation(NguoiDung model)
        {
          

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                TempData["View"] = "Validation";
                return View("Success");
            }
        }


    }
}