using System.ComponentModel.DataAnnotations;

public class ProductDTO
{

    public string ProductName { get; set; }

    public int QuantityAvailable { get; set; }

    public int PricePerQuantity { get; set; }
    public string ImgUrl {get;set;}
}