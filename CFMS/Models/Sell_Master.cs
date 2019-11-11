using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CFMS.Models
{
    [Table("CFMS_Sell_Master")]
    public class Sell_Master
    {
        [Key]
        public int sid { get; set; }
        public int rid { get; set; }
        public int cid { get; set; }
        public int Imid { get; set; }
        public int varidsell { get; set; }
        public DateTime date { get; set; }
        public int Quty { get; set; }
        public int price { get; set; }
        public int amut { get; set; }
        public int stusdeli { get; set; }
        public int stussmdele { get; set; }
    }
}