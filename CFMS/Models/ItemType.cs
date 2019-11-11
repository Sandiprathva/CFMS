using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CFMS.Models
{
   [Table("CFMS_ItemType")]
    public class ItemType
    {
        [Key]
        public int ItypeId { get; set; }
        public string type { get; set; }
        public int deletst { get; set; }
    }
}