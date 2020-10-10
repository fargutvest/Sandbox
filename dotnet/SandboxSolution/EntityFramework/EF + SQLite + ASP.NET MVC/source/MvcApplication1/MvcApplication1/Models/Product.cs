using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class Product
    {
        public int ID { get; set; }
        public string NameProduct { get; set; }
        public int CountProductUnit { get; set; }
        public string Barcode { get; set; }
    }
}
