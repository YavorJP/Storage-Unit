using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StorageUnit.Models
{
    //Клас който ни създава таблицата с продукти в склада
    public class AddItemsViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "* Item Name")]
        public string ItemName { get; set; }

        [StringLength(2000)]
        [Display(Name = "Description")]
        public string Description { get; set; }    

        //Използваме регулярен израз за да позволим добавянето на цифри с позитивен знак
        [Required]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Must be a positive value.")]
        [Display(Name = "* Bought For")]
        public double Bought { get; set; }

        [Required]
        [RegularExpression(@"-?\d+(?:\.\d+)?", ErrorMessage = "Must be a positive value.")]
        [Display(Name = "* Selling For")]
        public double Selling { get; set; }

        [Required]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Must be a positive value.")]
        [Display(Name = "* Quantity available")]
        public int Quantity { get; set; } = 0;

        [Display(Name = "Category")]
        public string Type { get; set; }

        //Използваме Remote атрибута и classa IsCodeExist който се намира в StorageItemsController за да проверим уникалността на кода
        [Remote("IsCodeExist", "StorageItems",
                ErrorMessage = "Unique code already exists in database.")]
        [Display(Name = "Unique Code")]
        public int Code { get; set; }
        
    }
    
}