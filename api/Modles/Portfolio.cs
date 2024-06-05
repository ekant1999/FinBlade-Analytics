using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.Modles
{
    [Table("Portfolios")]
    public class Portfolio
    {
        public int StockId {get; set;}
        public string AppUserId {get; set;}
        public AppUser? AppUser {get; set;}
        public Stock? Stock {get; set;}
    }
}