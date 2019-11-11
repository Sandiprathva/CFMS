using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CFMS.Models
{
    [Table("CFMS_User")]
    public class Registration 
    {
        [Key]
        public int rid { get; set; }
        public string rtype { get; set; }

        public string name { get; set; }
        public string gname { get; set; }
        
        public string addr { get; set; }
        public string email { get; set; }
        public string monum { get; set; }
        public string pass { get; set; }

        public int gamemsatus { get; set; }
        public int gpnamestust { get; set; }
        public int adgroupsatus { get; set; }
        public int adcompnystatus { get; set; }
        public int stusdele { get; set; }

    }
}