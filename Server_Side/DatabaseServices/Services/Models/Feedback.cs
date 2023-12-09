using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Server_Side.DatabaseServices.Services.Model
{
    public class Feedback : Group_1_Record_Abstraction
    {
        [Required]
        public int Feedback_ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string User_ID { get; set; } = string.Empty;
        [Required]
        [MaxLength(50)]
        public string Product_ID { get; set; } = string.Empty;
        [Required(ErrorMessage = "Stars Rating is required")]
        public decimal Stars_Rating { get; set; }
        [Required(ErrorMessage = "Date is required")]
        [DateNotDefault(ErrorMessage = "Date must be filled")]
        public DateTime Date_Updated { get; set; }

        public override string ToString()
        {
            return $"Feedback_ID: {Feedback_ID}, User_ID: {User_ID}, Product_ID: {Product_ID}, Stars_Rating: {Stars_Rating}, Date_Updated: {Date_Updated}";
        }
    }
}
