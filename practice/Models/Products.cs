using System.ComponentModel.DataAnnotations;
namespace practice.models;
public class Products
{
    [Key]
    public int ProductID { get;set; } 

    [Required]
    [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Invalid Name. Only alphabetic characters and spaces are allowed.")]
    public string ProductName { get; set; }

    [Required]
    public int QuantityAvailable { get; set; }

    [Required]
    public int PricePerQuantity { get; set; }

      [Required]
      public string ImgUrl {get;set;}
}