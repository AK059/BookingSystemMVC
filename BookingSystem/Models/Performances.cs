using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookingSystem.Models
{
    //add performance class here
    public class Performances
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ShowId { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    

}
    }

