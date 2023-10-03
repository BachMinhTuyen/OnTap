using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnTapKiemTraLan1.Models;

namespace OnTapKiemTraLan1.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            ConnectSongs connect = new ConnectSongs();
            List<Songs> list = connect.getData();
            int soDongHienThi = 5;
            list = list.Take(soDongHienThi).ToList();
            return View(list);
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                ConnectUserAccount connectUserAccount = new ConnectUserAccount();
                int result = connectUserAccount.insertAccount(userAccount.UserName, userAccount.PassWord);
                if (result == 0)
                {
                    ViewBag.Message = "DK không thành công";
                    return View();
                }
                return RedirectToAction("Login", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                ConnectUserAccount connectUserAccount = new ConnectUserAccount();
                int result = connectUserAccount.confirmAccount(userAccount.UserName, userAccount.PassWord);
                if (result == 0)
                {
                    ViewBag.Message = "Dang Nhap không thành công (Sai Thông Tin)";
                    return View();
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
        public ActionResult ShowAll()
        {
            ConnectSongs connect = new ConnectSongs();
            List<Songs> list = connect.getData();
            return View(list);
        }
        public ActionResult Edit(string id)
        {
            ConnectSongs connect = new ConnectSongs();
            Songs songs = connect.getDetail(id);
            return View(songs);
        }
        [HttpPost]
        public ActionResult Edit(string SongID, string SongName, string TheLoaiID, string NhacSiID)
        {
            ConnectSongs connect = new ConnectSongs();
            connect.EditData(SongID, SongName, TheLoaiID, NhacSiID);
            return RedirectToAction("ShowAll", "Home");
        }
    }
}