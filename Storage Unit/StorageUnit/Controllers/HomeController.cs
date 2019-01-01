using StorageUnit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StorageUnit.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            if (Request.IsAuthenticated == true)
            {
                return RedirectToAction("ItemsTable", "Home");
            }
            return View();
        }
        //Прави проверка по зададените параметри за да покаже изисканата информация
        public ActionResult ItemsTable(string SearchString,string searchBy,string category)
        {
            var db = new ApplicationDbContext();
            var list = from m in db.StorageItems
                         select m;
            if (!String.IsNullOrEmpty(category))
            {
                list = list.Where(s => s.Type.Contains(category));
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                if (searchBy == "Code")
                {

                    int integerTerm;
                    Int32.TryParse(SearchString, out integerTerm);
                    list = list.Where(s => s.Code == integerTerm);
                }
                else
                {
                    list = list.Where(s => s.ItemName.Contains(SearchString));
                }
            }


                return View(list);
        }

        public ActionResult DeleteItem(int id)
        {

            using (var db = new ApplicationDbContext())
            {
                var v = db.StorageItems.Where(a => a.Id == id).FirstOrDefault();
                if (v != null)
                {
                    db.StorageItems.Remove(v);
                    db.SaveChanges();

                }
            }
            return RedirectToAction("ItemsTable", "Home");
        }
        //GET заявка която ни изкарва информацията в Add формата
        [HttpGet]
        public ActionResult UpdateItem(int Id)
        {
            using (var db = new ApplicationDbContext())
            {
                var item = db.StorageItems.Where(a => a.Id == Id).FirstOrDefault();
                return View(item);
            }
        }
        // Post заявка която задава новата информация в базата данни
        [HttpPost]
        public ActionResult UpdateItem(AddItemsViewModel e)
        {

            using (var db = new ApplicationDbContext())
            {
                var item = db.StorageItems.Where(a => a.Id == e.Id).FirstOrDefault();
                 if (item != null)
                 {
                     item.ItemName = e.ItemName;
                     item.Description = e.Description;
                     item.Bought = e.Bought;
                     item.Selling = e.Selling;
                     item.Quantity = e.Quantity;
                     item.Type = e.Type;
                     item.Code = e.Code;
                 }
                db.SaveChanges();
                return RedirectToAction("ItemsTable", "Home");
            }
        }

    }
}