
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace BlazorApp.Models
{
    [Table("Orders")]
    public class Order : IBaseEntity, IValidatableObject
    {
        [Required, Range(0, int.MaxValue)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }

        [EmailAddress, MaxLength(50)]
        public string? BuyerEmail { get; set; }
        [Phone, MaxLength(20)]
        public string? BuyerPhone { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string LiqPayPaymentStatus { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public DateTime ModifyAt { get; set; }

        [NotMapped]
        public IEnumerable<OrderItem>? Items { get; set; }
        public decimal Amount() => Items?.Select(p => p.Total()).Sum() ?? 0;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (BuyerEmail != null)
            {
                if (BuyerEmail == string.Empty)
                    BuyerEmail = null;
            }
            if (BuyerPhone != null)
            {
                if (BuyerPhone == string.Empty)
                    BuyerPhone = null;
            }
            return Enumerable.Empty<ValidationResult>();
        }
    }
}
