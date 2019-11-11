using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CFMS.Models
{
    [Table("CFMS_Cropvariety")]
    public class VarityCrop
    {
        [Key]

        public int varid { get; set; }
        public string verity { get; set; }
        public int cpid { get; set; }
        public int ItypeId { get; set; }
        public int deletst { get; set; }
    }
}