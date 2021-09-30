using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProceedToBuyModule.Model
{
    public class CustomerWishList
    {
        public int Id { get; set; }
        public int ProductId { get; set; }


        public DateTime DateAddedToWishList { get; set; }
    }
}
