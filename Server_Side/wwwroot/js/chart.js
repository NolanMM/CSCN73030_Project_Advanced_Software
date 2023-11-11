// Function to request data from c# berkley socket server restful

// Function to receive data from c# berkley socket server restful

// Example data for the flex containers/ array to hold data for flex containers
const values = ["Error: Fetching", "Error: Fetching"];
function fetchFlexDataFromServer() {
    fetch("https://sprint1deploymentgroup1.azurewebsites.net/charts/productInfoData")
    .then((response) => response.json())
    .then((data) => {
      values[0] = data.salesRate;
      values[1] = data.placeHolder;
      // Call the updateFlexContainer function to update the containers
      updateFlexContainer();
    })
    .catch((error) => {
      console.error("Error fetching data:", error);
    });
}
// Call the function to fetch data when the page loads
window.addEventListener("load", fetchFlexDataFromServer);
// Function to update the flex containers
function updateFlexContainer() {
  const valueElements = document.querySelectorAll(".flex-value");
  // Loop through the values and update the text content of the corresponding value elements
  for (let i = 0; i < values.length; i++) {
    valueElements[i].textContent = values[i];
  }
}

// Sample data for monthly sales
var salesData = {
  labels: ["January", "February", "March", "April", "May"],
  datasets: [
    {
      label: "Monthly Sales",
      data: [0, 0, 0, 0, 0],
      backgroundColor: "rgba(75, 192, 192, 0.2)",
      borderColor: "rgba(75, 192, 192, 1)",
      borderWidth: 2,
      yAxisID: "y-axis-sales", // Assign this dataset to the 'y-axis-sales'
    },
  ],
};
// Create a chart for monthly sales
var ctxSales = document.getElementById("MonthlySales").getContext("2d");
var myChartSales = new Chart(ctxSales, {
  type: "line",
  data: salesData,
  options: {
    maintainAspectRatio: false, // Disable maintaining aspect ratio
    responsive: true, // Allow the chart to be responsive

    // You can also set the width and height explicitly
    // width: 400, // Set the width of the chart (pixels)
    // height: 200, // Set the height of the chart (pixels)
  },
});

// Sample data for monthly views
var viewsData = {
  labels: ["January", "February", "March", "April", "May"],
  datasets: [
    {
      label: "Page Views",
      data: [0, 0, 0, 0, 0],
      backgroundColor: "rgba(54, 162, 235, 0.2)",
      borderColor: "rgba(54, 162, 235, 1)",
      borderWidth: 2,
      yAxisID: "y-axis-views", // Assign this dataset to the 'y-axis-views'
    },
  ],
};
// Create a chart for monthly views
var ctxViews = document.getElementById("MonthlyViews").getContext("2d");
var myChartViews = new Chart(ctxViews, {
  type: "line",
  data: viewsData,
  options: {
    maintainAspectRatio: false, // Disable maintaining aspect ratio
    responsive: true, // Allow the chart to be responsive
  },
});

// Sample data for monthly average satisfaction
var satisfactionData = {
  labels: [
    "January",
    "February",
    "March",
    "April",
    "May",
    "June",
    "July",
    "August",
    "September",
    "October",
    "November",
    "December",
  ],
  datasets: [
    {
      label: "Average Satisfaction (1-5)",
      data: [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0],
      backgroundColor: "rgba(75, 192, 192, 0.2)",
      borderColor: "rgba(75, 192, 192, 1)",
      borderWidth: 2,
    },
  ],
};
// Create a chart for monthly average satisfaction
var ctxSatisfaction = document
  .getElementById("MonthlySatisfaction")
  .getContext("2d");
var myChartSatisfaction = new Chart(ctxSatisfaction, {
  type: "bar",
  data: satisfactionData,
  options: {
    maintainAspectRatio: false, // Disable maintaining aspect ratio
    responsive: true, // Allow the chart to be responsive
  },
});

// Create a chart that combines sales and views data
var ctxSalesAndViews = document
  .getElementById("MonthlySalesAndViews")
  .getContext("2d");
var myChartSalesAndViews = new Chart(ctxSalesAndViews, {
  type: "line", // Use a line chart for sales and views
  data: {
    labels: ["January", "February", "March", "April", "May"],
    datasets: [salesData.datasets[0], viewsData.datasets[0]], // Include both sales and views datasets
  },
  options: {
    maintainAspectRatio: false,
    responsive: true,
    scales: {
      y: {
        position: "left",
        beginAtZero: true,
      },
      "y-axis-sales": {
        position: "right",
        beginAtZero: true,
      },
      "y-axis-views": {
        position: "right",
        beginAtZero: true,
      },
    },
  },
});


//Fetch and update data for monthly sales and monthly viesw and sales charts 
function fetchMonthlySalesFromServer() {
    fetch("https://sprint1deploymentgroup1.azurewebsites.net/charts/monthlySalesData")
    .then((response) => response.json())
    .then((data) => {
      salesData.datasets[0].data = data.monthlySales;
      myChartSales.update();
      myChartSalesAndViews.update();
    })
    .catch((error) => {
      console.error("Error fetching monthly sales data:", error);
    });
}
window.addEventListener("load", fetchMonthlySalesFromServer);

//Fetch and update data for monthly views chart and monthly viesw and sales charts 
function fetchMonthlyViewsFromServer() {
    fetch("https://sprint1deploymentgroup1.azurewebsites.net/charts/monthlyViewsData")
    .then((response) => response.json())
    .then((data) => {
      viewsData.datasets[0].data = data.monthlyViews;
      myChartViews.update();
      myChartSalesAndViews.update();
    })
    .catch((error) => {
      console.error("Error fetching monthly views data:", error);
    });
}
window.addEventListener("load", fetchMonthlyViewsFromServer);

//Fetch data for monthly satisfaction
function fetchMonthlySatisfactionFromServer() {
    fetch("https://sprint1deploymentgroup1.azurewebsites.net/charts/monthlySatisfactionData")
    .then((response) => response.json())
    .then((data) => {
      satisfactionData.datasets[0].data = data.monthlySatisfaction;
      myChartSatisfaction.update();
    })
    .catch((error) => {
      console.error("Error fetching monthly satisfaction data:", error);
    });
}
window.addEventListener("load", fetchMonthlySatisfactionFromServer);

// Check if chart3 is the only child
if (document.querySelectorAll(".chart-container").length === 1) {
  // Add a class for full-width layout
  document.querySelector(".chart-container").classList.add("full-width-chart");
}

