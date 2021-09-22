using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuyModule.Model
{
    [Keyless]
    public class VendorsList
    {
        [ForeignKey("Vendor")]
        public int VendorId{ get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Vendor Vendor { get; set; }
    }
}
