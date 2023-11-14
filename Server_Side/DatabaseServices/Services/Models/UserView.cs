using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Server_Side.DatabaseServices.Services.Model
{
    public class UserView : Group_1_Record_Abstraction
    {
        public int UserView_ID { get; set; }
        [Required]
        public string User_ID { get; set; }
        [Required]
        public string Product_ID { get; set; }
        [Required]
        public decimal Time_Count { get; set; }
        [Required]
        public DateTime Date_Access { get; set; }

        public override string ToString()
        {
            return $"UserView_ID: {UserView_ID}, User_ID: {User_ID}, Product_ID: {Product_ID}, Time_Count: {Time_Count}, Date_Access: {Date_Access}";
        }
    }
}
