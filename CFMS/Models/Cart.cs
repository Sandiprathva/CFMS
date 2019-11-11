using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CFMS.Models
{
    [Table("CFMS_Cart")]
    public class Cart
    {
        [Key]
        public int cid { get;set; }
        public int rid { get;set; }
        public int Imid { get;set; }
        public int varidCart { get;set; }
    
        public int qt { get;set; }
        public int pric { get;set; }
        public int amtot { get;set; }
        public int stucart { get;set; }
    }
}