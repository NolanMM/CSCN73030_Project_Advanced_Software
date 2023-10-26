namespace Server_Side.Database_Services.Table_Interface
{
    public interface Output_Tables_Template
    {
        bool Test_Connection_To_Table();
        Task<bool> Create_Async(DateTime date, string request, string Session_ID);
        Task<List<object>?> Read_All_Async();
        Task Update_Async();
    }
}
