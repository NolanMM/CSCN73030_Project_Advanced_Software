namespace Server_Side.DatabaseServices.Services.Models.Interfaces
{
    public abstract class Group_1_Record_Abstraction : Group_1_Record_Interfaces
    {
        public override string ToString()
        {
            var properties = GetType().GetProperties();

            string result = string.Join(", ", properties.Select(prop => $"{prop.Name}: {prop.GetValue(this)}"));

            return result;
        }
    }
}
