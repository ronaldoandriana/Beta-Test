using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Auctions.Models
{
    public class ListingVM
    {
        public int Id { get; set; }
        [Display(Name = "Produit")]
        public string Title { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Prix")]
        public double Price { get; set; }
        [Display(Name = "Image")]
        public IFormFile Image { get; set; }
        [Display(Name = "Est vendu")]
        public bool IsSold { get; set; }

        [Required]
        public string? IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public IdentityUser? User { get; set; }
    }
}
