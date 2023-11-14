using System.ComponentModel.DataAnnotations;

namespace Server_Side.DatabaseServices.Services.Models.Interfaces
{
    public class DateNotDefaultAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date != DateTime.MinValue;
            }
            return false;
        }
    }
}
