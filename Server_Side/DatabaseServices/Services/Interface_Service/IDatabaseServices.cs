using Server_Side.DatabaseServices.Services.Models.Interfaces;

namespace Server_Side.DatabaseServices.Services.Interface_Service
{
    public interface IDatabaseServices
    {
        Task<List<Group_1_Record_Abstraction>?> GetDataServiceAsync();
    }
}
