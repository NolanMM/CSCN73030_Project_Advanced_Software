using Server_Side.DatabaseServices.Services.Interface_Service;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server_Side.DatabaseServices.Services
{
    public class FeedbackTableService : IDatabaseServices
    {
        private readonly string apiUrl = "https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/minhnguyen/Connhenbeo1/group1/3";

        public async Task<List<Group_1_Record_Abstraction>?> GetDataServiceAsync()
        {
            List<Group_1_Record_Abstraction>? feedbackData = new List<Group_1_Record_Abstraction>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        List<Feedback>? feedbackList = JsonConvert.DeserializeObject<List<Feedback>?>(jsonContent);

                        if (feedbackList != null)
                        {
                            foreach (Feedback feedback in feedbackList)
                            {
                                if (ValidateDataAnnotations(feedback))
                                {
                                    feedbackData.Add(feedback);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"HTTP Error: Cannot Deserialize Feedback Data Object");
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
            return feedbackData;
        }

        private static bool ValidateDataAnnotations(Feedback feedback)
        {
            ValidationContext context = new ValidationContext(feedback, serviceProvider: null, items: null);
            List<ValidationResult>? results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(feedback, context, results, validateAllProperties: true);

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
