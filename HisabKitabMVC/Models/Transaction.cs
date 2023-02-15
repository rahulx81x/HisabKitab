using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HisabKitabMVC.Models
{
    public class Transaction
    {
        [ScaffoldColumn(false)]
        public int UserId { get; set; }

        [ScaffoldColumn(false)]
        public int TranId { get; set; }

        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Amount is mandatory.")]
        public decimal Amount { get; set; }
        [Required(ErrorMessage = "Type is mandatory.")]
        public string Type { get; set; } = null!;

        [StringLength(maximumLength: 100)]
        [DisplayName("Transaction Remarks")]
        public string? Remarks { get; set; }

        [ScaffoldColumn(false)]
        public List<SelectListItem>? TypeOptions { get; set; }

    }
}
