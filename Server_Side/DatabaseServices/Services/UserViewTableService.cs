using Server_Side.DatabaseServices.Services.Interface_Service;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server_Side.DatabaseServices.Services
{
    public class UserViewTableService : IDatabaseServices
    {
        private readonly string apiUrl = "https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/minhnguyen/Connhenbeo1/group1/0";

        public async Task<List<Group_1_Record_Abstraction>?> GetDataServiceAsync()
        {
            List<Group_1_Record_Abstraction>? UserViewData = new List<Group_1_Record_Abstraction>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        List<UserView>? userViews = JsonConvert.DeserializeObject<List<UserView>?>(jsonContent);
                        if (userViews != null)
                        {
                            foreach (UserView userView in userViews)
                            {
                                if ((bool)ValidateDataAnnotations(userView))
                                {
                                    UserViewData.Add(userView);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"HTTP Error: Cannot Deserialize UserView Data Object");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
            return UserViewData;
        }

        private static bool ValidateDataAnnotations(UserView userView)
        {
            ValidationContext context = new ValidationContext(userView, serviceProvider: null, items: null);
            List<ValidationResult>? results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(userView, context, results, validateAllProperties: true);

            if (!isValid)
            {
                foreach (ValidationResult validationResult in results)
                {
                    Console.WriteLine(validationResult.ErrorMessage);
                }
            }

            return isValid;
        }
    }
}
