using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceApp3.Models
{
    public class Inventory
    {
        [PrimaryKey]
        public int InventoryId { get; set; }

        public int ProductId { get; set; }

        [ManyToOne]
        public Product Product { get; set; }

        public int WarehouseId { get; set; }

        public string WarehouseName { get; set; }

        public double Stock { get; set; }

        public override int GetHashCode()
        {
            return InventoryId;
        }
    }
}
