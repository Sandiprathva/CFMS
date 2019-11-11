using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CFMS.Models
{
    public class CartShow
    {
        [Key]
        public int id { get; set; }
        public Cart Ca { get; set; }
        public ItemMasert Items { get; set; }
    }
}