using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CFMS.Models
{
    public class DataCon :DbContext
    {
        public DbSet<Registration> Reg { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<ItemMasert> ItemMaserts { get; set; }
        public DbSet<Cart> carts { get; set; }
        public DbSet<CartShow> CartShow { get; set; }
        public DbSet<Sell_Master> sell { get; set; }
        public DbSet<AddCrop> crop { get; set; }
        public DbSet<VarityCrop> Varity { get; set; }
        //public DbSet<DispAll> Display { get; set; }
    }
}