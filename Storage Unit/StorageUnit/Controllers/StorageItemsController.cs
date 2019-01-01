using StorageUnit.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StorageUnit.Controllers
{
    public class StorageItemsController : Controller
    {
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }
        
        //Метод който използваме за проверка на уникалността на Code
        public JsonResult IsCodeExist(int code)
        {
            var db = new ApplicationDbContext();
            var validateCode = db.StorageItems.FirstOrDefault
                                 (x => x.Code == code);
            if (validateCode != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        
        [HttpPost]
        public ActionResult Add (AddItemsViewModel addModel)
        {
           
               
                var db = new ApplicationDbContext();
                var addItems = new AddItemsViewModel
                {
                    ItemName = addModel.ItemName,
                    Description = addModel.Description,                   
                    Bought = addModel.Bought,
                    Selling = addModel.Selling,
                    Quantity = addModel.Quantity,
                    Type = addModel.Type,
                    Code = addModel.Code
                };
                db.StorageItems.Add(addItems);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");

            
        }       
    }

}