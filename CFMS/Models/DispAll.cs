using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CFMS.Models
{
    public class DispAll
    {
        public List<ItemMasert> itemDis {get;set;}
        public List<AddCrop> CropDis { get; set; }
        //public List<ItemType> typeDis { get; set; }

        public List<VarityCrop> varityDis { get; set; }
    }
}