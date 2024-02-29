using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatGeek.Data.Models
{
    public class Order
    {
        public int OrderID { get; set; }

        //foreign key
        [Required]
        [ForeignKey("Event")]
        public int EventID { get; set; }


        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int NumberTickets {  get; set; }

        [Required]
        public float OrderTotal { get; set; }

        public ApplicationUser? user { get; set; }

        public virtual Event Event { get; set; } = null!;
    }
}
