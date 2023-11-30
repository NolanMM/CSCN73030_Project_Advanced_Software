using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Server_Side.DatabaseServices.Services.Model
{
    public class SaleTransaction : Group_1_Record_Abstraction
    {
        [Required]
        public string Transaction_ID { get; set; }
        [Required]
        public string User_ID { get; set; }
        [Required(ErrorMessage = "Order Value is required")]
        [Range(0.00001, double.MaxValue, ErrorMessage = "Required Order value > 0")]
        public decimal Order_Value { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [DateNotDefault(ErrorMessage = "Date must be filled")]
        public DateTime date { get; set; }

        [Required(ErrorMessage = "The item list cannot be empty")]
        [MaxLength(10000)]
        public string Details_Products { get; set; }

        public override string ToString()
        {
            return $"Transaction_ID: {Transaction_ID}, User_ID: {User_ID}, Order_Value: {Order_Value}, date: {date}, Product List: {Details_Products}";
        }
    }
}
