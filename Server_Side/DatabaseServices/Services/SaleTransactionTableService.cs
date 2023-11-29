using Server_Side.DatabaseServices.Services.Interface_Service;
using Server_Side.DatabaseServices.Services.Model;
using Server_Side.DatabaseServices.Services.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Server_Side.DatabaseServices.Services
{
    public class SaleTransactionTableService : IDatabaseServices
    {
        private readonly string apiUrl = "https://analysisreportingdatabasemodulegroup1.azurewebsites.net/Group1/DatabaseController/minhnguyen/Connhenbeo1/group1/2";

        public async Task<List<Group_1_Record_Abstraction>?> GetDataServiceAsync()
        {
            List<Group_1_Record_Abstraction>? SaleTransactionData = new List<Group_1_Record_Abstraction>();

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        string jsonContent = await response.Content.ReadAsStringAsync();

                        List<SaleTransaction>? saleTransactions = JsonConvert.DeserializeObject<List<SaleTransaction>?>(jsonContent);
                        if (saleTransactions != null)
                        {
                            foreach (SaleTransaction saleTransaction in saleTransactions)
                            {
                                if (ValidateDataAnnotations(saleTransaction))
                                {
                                    SaleTransactionData.Add(saleTransaction);
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine($"HTTP Error: Cannot Deserialize SaleTransaction Data Object");
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
            return SaleTransactionData;
        }
        public Dictionary<string, (string, string)>? ProcessSaleTransactionList(List<SaleTransaction>? saleTransactions,string UserID)
        {
            if (saleTransactions == null)
            {
                return null;
            }

            Dictionary<string, (string, string)> returnData = new Dictionary<string, (string, string)>(); // Product ID, (Total Quantity, Date)

            foreach (SaleTransaction saleTransaction in saleTransactions)
            {
                string transactionID = saleTransaction.Transaction_ID;
                string userID = saleTransaction.User_ID;
                string dateKey = saleTransaction.date.ToShortDateString();

                if (!string.IsNullOrEmpty(saleTransaction.Details_Products))
                {
                    // Parse the Details_Products field
                    List<ProductDetails> productDetailsList = ParseProductDetails(saleTransaction.Details_Products);

                    foreach (ProductDetails productDetails in productDetailsList)
                    {
                        string productId = productDetails.Product_ID;

                        // Check if the current product belongs to a different user
                        if (userID != UserID)
                        {
                            if (!returnData.ContainsKey(productId))
                            {
                                // Product ID is unique, add to the dictionary with total quantity and date
                                returnData.Add(productId, (productDetails.Product_Quantity.ToString(), dateKey));
                            }
                            else
                            {
                                // Product ID is duplicated, increment the total quantity
                                var (totalQuantity, existingDate) = returnData[productId];
                                int totalQuantityInt = int.Parse(totalQuantity) + productDetails.Product_Quantity;
                                returnData[productId] = (totalQuantityInt.ToString(), dateKey);
                            }
                        }
                    }
                }
            }

            return returnData;
        }

        private List<ProductDetails> ParseProductDetails(string detailsProducts)
        {
            // Replace single quotes with double quotes
            detailsProducts = detailsProducts.Replace("'", "\"");

            // Deserialize the modified JSON string
            return JsonConvert.DeserializeObject<List<ProductDetails>>(detailsProducts);
        }

        private class ProductDetails
        {
            public string Product_ID { get; set; }
            public decimal Product_Price { get; set; }
            public int Product_Quantity { get; set; }
        }

        public static bool ValidateDataAnnotations(SaleTransaction saleTransaction)
        {
            ValidationContext context = new ValidationContext(saleTransaction, serviceProvider: null, items: null);
            List<ValidationResult>? results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(saleTransaction, context, results, validateAllProperties: true);

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
