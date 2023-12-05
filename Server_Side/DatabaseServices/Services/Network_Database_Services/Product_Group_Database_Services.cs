using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Server_Side.DatabaseServices.Services.Network_Database_Services
{
    public class ResponseData
    {
        [JsonPropertyName("pid")]
        public string pid { get; set; } = string.Empty;             // Need Product ID

        [JsonPropertyName("sid")]
        public string sid { get; set; } = string.Empty;            // Need - UserID

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;             // Need

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("image")]
        public string Image { get; set; } = string.Empty;

        [JsonPropertyName("category")]
        public string Category { get; set; } = string.Empty;

        [JsonPropertyName("price")]                 // need Product Prices
        public double Price { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }

        [JsonPropertyName("sales")]
        public int Sales { get; set; }

        [JsonPropertyName("rating")]
        public double Rating { get; set; }

        [JsonPropertyName("clicks")]
        public int Clicks { get; set; }

        public override string ToString()
        {
            return $"ProductId: {pid}, SupplierId: {sid}, Name: {Name}, " +
                   $"Description: {Description}, Image: {Image}, Category: {Category}, " +
                   $"Price: {Price}, Stock: {Stock}, Sales: {Sales}, Rating: {Rating}, Clicks: {Clicks}";
        }
    }
    public abstract class Product_Group_Database_Services
    {
        private static readonly string apiUrl = "http://172.105.25.146:8080/api/product?category=&search=";

        public static async Task<List<ResponseData>?> GetDataServiceAsync()
        {
            List<ResponseData>? responseDatas = new List<ResponseData>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        List<ResponseData>? ResponseList = JsonConvert.DeserializeObject<List<ResponseData>?>(jsonContent);

                        if (ResponseList != null)
                        {
                            foreach (ResponseData reponse in ResponseList)
                            {
                                if (ValidateDataAnnotations(reponse))
                                {
                                    responseDatas.Add(reponse);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"HTTP Error: Cannot Deserialize reponse Data Object");
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
            return responseDatas;
        }

        public static async Task<Dictionary<string, (string, string, string, string)>?> ProcessGetTableRequestByUserIDAsync(string UserID)
        {
            Dictionary<string, (string, string, string, string)>? processedData = await ProcessGetTableRequestByUserID_All_List();

            if (processedData == null)
            {
                return null;
            }

            // Filter the processed data by UserID
            var filteredData = processedData.Where(kv => kv.Value.Item1 == UserID)
                                            .ToDictionary(kv => kv.Key, kv => kv.Value);

            return filteredData;
        }

        public static async Task<Dictionary<string, (string, string, string, string)>?> ProcessGetTableRequestByUserID_All_List()
        {
            List<ResponseData>? ResponseData_list = await GetDataServiceAsync();

            if (ResponseData_list == null)
            {
                return null;
            }

            Dictionary<string, (string, string, string, string)> return_Data = new Dictionary<string, (string, string, string, string)>(); // (pid, (sid, Name, Price, Date))

            foreach (ResponseData feedback in ResponseData_list)
            {
                string productId = feedback.pid;
                string supplierId = feedback.sid;
                string name = feedback.Name;
                double price = feedback.Price;
                string dateKey = DateTime.Now.ToShortDateString();

                if (!return_Data.ContainsKey(productId))
                {
                    // Product ID is unique, add to the dictionary
                    return_Data.Add(productId, (supplierId, name, price.ToString(), dateKey));
                }
                else
                {
                    // Product ID is duplicated, calculate the average star rating within the date range 1 day
                    var (existingSupplierId, existingName, existingPrice, existingDateKey) = return_Data[productId];

                    if (existingDateKey == dateKey)
                    {
                        // Same date, calculate average prices
                        double existingPriceDouble = double.Parse(existingPrice);
                        double averagePrice = (existingPriceDouble + price) / 2;
                        return_Data[productId] = (existingSupplierId, existingName, averagePrice.ToString(), dateKey);
                    }
                    else
                    {
                        // Different date, add a new entry
                        return_Data.Add($"{productId}_{dateKey}", (supplierId, name, price.ToString(), dateKey));
                    }
                }
            }
            return return_Data;
        }

        public static bool ValidateDataAnnotations<T>(T data)
        {
            if (data == null) {  return false; }
            ValidationContext context = new ValidationContext(data, serviceProvider: null, items: null);
            List<ValidationResult> results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(data, context, results, validateAllProperties: true);

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
