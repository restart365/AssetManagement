using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AssetManagement.DataAccessObject;
using AssetManagement.Models;
using PagedList;

namespace AssetManagement.Controllers
{
    public class AssetController : Controller
    {
        // GET: Asset
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;

            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.GroupSortParm = sortOrder == "group_asc" ? "group_desc" : "group_asc";
            ViewBag.PriceSortParm = sortOrder == "price_asc" ? "price_desc" : "price_asc";
            ViewBag.CountSortParm = sortOrder == "count_asc" ? "count_desc" : "count_asc";
            ViewBag.TotalPriceSortParm = sortOrder == "total_asc" ? "total_desc" : "total_asc";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var list = AssetDao.ReadFromFile();

            // Search box filtering
            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper())
                                       || s.Group.ToString().ToUpper().Contains(searchString.ToUpper())
                                       || s.Id.ToUpper().Contains(searchString));
            }

            // Sorting
            switch (sortOrder)
            {
                case "price_asc":
                    list = list.OrderBy(s => s.UnitPrice);
                    break;
                case "price_desc":
                    list = list.OrderByDescending(s => s.UnitPrice);
                    break;

                case "count_asc":
                    list = list.OrderBy(s => s.Count);
                    break;
                case "count_desc":
                    list = list.OrderByDescending(s => s.Count);
                    break;

                case "total_asc":
                    list = list.OrderBy(s => s.TotalValue);
                    break;
                case "total_desc":
                    list = list.OrderByDescending(s => s.TotalValue);
                    break;

                case "group_asc":
                    list = list.OrderBy(s => s.Group);
                    break;
                case "group_desc":
                    list = list.OrderByDescending(s => s.Group);
                    break;

                case "name_desc":
                    list = list.OrderByDescending(s => s.Name);
                    break;
                default:
                    list = list.OrderBy(s => s.Name);
                    break;

            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(list.ToPagedList(pageNumber, pageSize));
        }
    }
}