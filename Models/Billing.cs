using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hotel_Reservation.Models
{
    public class Billing
    {
        [Key]
        public int BillingID { get; set; }

        [Required]
        public int ReservationID { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        [MaxLength(50)]
        public string PaymentMethod { get; set; }

        [ForeignKey("ReservationID")]
        public Reservation? Reservation { get; set; }


    }
}
