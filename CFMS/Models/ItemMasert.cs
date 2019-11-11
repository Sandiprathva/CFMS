using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CFMS.Models
{
    [Table("CFMS_ItemMaster")]
    public class ItemMasert
    {
        [Key]
        public int Imid { get; set; }
        public int ItypeId { get; set; }
        public int rid { get; set; }
        public int cpid { get; set; }
        public string img { get; set; }
        public DateTime date { get; set; }
        public int qty { get; set; }
        public int varkeyItem { get; set; }
        public int price { get; set; }
        public int stusshow { get; set; }
        public int uid { get; set; }
      //  public int stucart { get; set; }

        public string stussubcrop { get; set; }
        public string stuspay { get; set; }
        public int stuscomp { get; set; }
        public int stusdele { get; set; }
        public int stusclu { get; set; }
    }
}