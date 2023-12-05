using Server_Side.DatabaseServices.Services.Interface_Service;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server_Side.DatabaseServices.Services
{
    public class PageViewTableService : IDatabaseServices
    {
        private readonly string apiUrl = "https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/minhnguyen/Connhenbeo1/group1/1";

        public async Task<List<Group_1_Record_Abstraction>?> GetDataServiceAsync()
        {
            List<Group_1_Record_Abstraction>? PageViewData = new List<Group_1_Record_Abstraction>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        List<PageView>? pageViews = JsonConvert.DeserializeObject<List<PageView>?>(jsonContent);

                        if (pageViews != null)
                        {
                            foreach (PageView pageView in pageViews)
                            {
                                if (ValidateDataAnnotations(pageView))
                                {
                                    PageViewData.Add(pageView);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"HTTP Error: Cannot Deserialize PageView Data Object");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode}");
                    }
                }
                catch (Exception)
                {
                    //Console.WriteLine($"An error occurred: {ex.Message}");
                    return null;
                }
            }
            return PageViewData;
        }
        public Dictionary<(string, string), string>? ProcessPageViewList(List<PageView>? PageView_Lists, string UserID)
        {
            if (PageView_Lists == null)
            {
                return null;
            }

            Dictionary<(string, string), string> return_Data = new Dictionary<(string, string), string>(); // (Product ID, Date), Count

            foreach (PageView pageView in PageView_Lists)
            {
                string productId = pageView.Product_ID;

                // Check if the UserID is not equal to the specified UserID
                if (pageView.UserID != UserID)
                {
                    string dateKey = pageView.Start_Time.ToShortDateString();

                    if (!return_Data.ContainsKey((productId, dateKey)))
                    {
                        // Product ID and date combination is unique, add to the dictionary with count 1
                        return_Data.Add((productId, dateKey), "1");
                    }
                    else
                    {
                        // Product ID and date combination is duplicated, increment the count
                        int countInt = int.Parse(return_Data[(productId, dateKey)]);
                        countInt++;
                        return_Data[(productId, dateKey)] = countInt.ToString();
                    }
                }
            }

            return return_Data;
        }

        public static bool ValidateDataAnnotations(PageView pageView)
        {
            ValidationContext context = new ValidationContext(pageView, serviceProvider: null, items: null);
            List<ValidationResult>? results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(pageView, context, results, validateAllProperties: true);

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
