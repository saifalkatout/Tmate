using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MateAPI.Models
{
    public class Service
    {
        public int ServiceID { get; set; }
        public int ShopID { get; set; }
        public string ServiceName { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string CategoryID { get; set; }
        public Boolean IsCustom { get; set; }
        public byte[] Picture { get; set; }

    }
}
