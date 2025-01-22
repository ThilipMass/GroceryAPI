using System.ComponentModel.DataAnnotations;

namespace practice.models;
public class BookingDetail
{
    [Key]
    public int BookingID { get; set; }

    [Required]
    public int CustomerID { get; set; }

    [Required]
    public double TotalPrice { get; set; }

    [Required]
    public DateTime DateOfBooking { get; set; }

    [Required]
    public string BookingStatus { get; set; }

}