using Server_Side.DatabaseServices.Services.Model;

namespace Server_Side.DatabaseServices.Services.DatabaseDictionary
{
    public class DatabaseServices_Group_1_Dictionary
    {
        public int Index { get; set; }
        public string ServiceName { get; set; }
        public Type DataType { get; set; }

        public DatabaseServices_Group_1_Dictionary(int index, string serviceName, Type dataType)
        {
            Index = index;
            ServiceName = serviceName;
            DataType = dataType;
        }
        public static List<DatabaseServices_Group_1_Dictionary> Tablesname_List_with_Data_Type = new List<DatabaseServices_Group_1_Dictionary>
        {
            new DatabaseServices_Group_1_Dictionary(0, "userview", typeof(UserView)),
            new DatabaseServices_Group_1_Dictionary(1, "pageview", typeof(PageView)),
            new DatabaseServices_Group_1_Dictionary(2, "sales_transaction_table", typeof(SaleTransaction)),
            new DatabaseServices_Group_1_Dictionary(3, "feedback_table", typeof(Feedback))
        };
    }

}
