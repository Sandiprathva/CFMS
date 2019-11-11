using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CFMS.Models
{
    [Table("CFMS_Crop")]
    public class AddCrop
    {
        [Key]

        public int cpid { get; set; }
        public string cpname { get; set; }
        public int ItypeId { get; set; }
        public int deletst { get; set; }
    }
}