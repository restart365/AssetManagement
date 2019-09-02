using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssetManagement.Models;
using AssetManagement.DataAccessObject;

namespace AssetManagement.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Author()
        {
            return View();
        }

        public ActionResult TestAdd()
        {
            /*
            IEnumerable<Asset> list = new List<Asset>
            {
                new Asset { Id = 0, Name = "T-Shirt", Group = Group.GROUP_A, UnitPrice = 20.00f, Count = 10},
                new Asset { Id = 1, Name = "Jeans", Group = Group.GROUP_B, UnitPrice = 50.99f, Count = 3},
                new Asset { Id = 2, Name = "Sweater", Group = Group.GROUP_A, UnitPrice = 30.00f, Count = 6},
                new Asset { Id = 3, Name = "Hoody", Group = Group.GROUP_A, UnitPrice = 49.99f, Count = 1},
                new Asset { Id = 4, Name = "Sweat Pants", Group = Group.GROUP_B, UnitPrice = 30.99f, Count = 0},
                new Asset { Id = 5, Name = "Skirt", Group = Group.GROUP_B, UnitPrice = 28.00f, Count = 13},
            };

            AssetDao.WriteToFile(list);*/
            return RedirectToAction("Index");
        }

    }
}