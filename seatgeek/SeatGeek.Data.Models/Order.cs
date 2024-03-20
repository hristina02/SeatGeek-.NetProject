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
        public int Id { get; set; }

        //foreign key
        [Required]
        [ForeignKey("Event")]
        public int EventId{ get; set; }


        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public int NumberTickets {  get; set; }

        [Required]
        public decimal OrderTotal { get; set; }

        public Guid UserId { get; set; } // Assuming UserId is a string in your ApplicationUser class

        [ForeignKey("UserId")]
        public virtual ApplicationUser? User { get; set; }

        public virtual Event Event { get; set; } = null!;
    }
}
