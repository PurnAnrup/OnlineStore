using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuyModule.Model
{
    
    public class VendorsList
    {
        public int ProductId { get; set; }

        public Vendor Vendor { get; set; }

        public int VendorId { get; set; }

        public int StockInHand { get; set; }

        
        public DateTime ExpectedStockReplinshmentDate { get; set; }
    }
}
