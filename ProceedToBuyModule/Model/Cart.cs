using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuyModule
{
    
    public class Cart
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ZipCode { get; set; }
        public DateTime DeliveryDate { get; set; }
        [NotMapped]
        public Vendor Vendor { get; set; }
        
    }
}
