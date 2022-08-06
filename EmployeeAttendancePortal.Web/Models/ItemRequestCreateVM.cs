using InventoryManagement.Web.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryManagement.Web.Models
{
    public class ItemRequestCreateVM : IValidatableObject
    {
        public ProductVM? Product { get; set; }

        [Required]
        [Display(Name ="Item Name")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Quantity Requested")]
        [Range(1, 30, ErrorMessage = "Please enter a valid number, greater than 0.")]
        public int Quantity_Requested { get; set; }

        public SelectList? Products { get; set; }

        public DateTime DateRequested { get; set; }
        [Display(Name ="Request Comments")]

        public string? RequstComments { get; set; }

      
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Quantity_Requested <= 0)
            {
                yield return new ValidationResult("Please Input a value greater than 0 .", new[] { nameof(Quantity_Requested) });
            }
            if(RequstComments?.Length > 50)
            {
                yield return new ValidationResult("Comments are Too Long. Please input less characters.", new[] { nameof(RequstComments) });

            }
        }
    }
}
