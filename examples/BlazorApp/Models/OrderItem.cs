using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorApp.Models
{
    [Table("OrderItems")]
    public class OrderItem : IBaseEntity, IValidatableObject
    {
        [Required, Range(0, int.MaxValue)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderItemId { get; set; }

        [Range(0, int.MaxValue)]
        //[ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        [Required, MinLength(3)]
        public string Name { get; set; } = string.Empty;

        [Required, Range(0, int.MaxValue)]
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public decimal Total() => Price * Quantity;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            return Enumerable.Empty<ValidationResult>();
        }

    }
}
