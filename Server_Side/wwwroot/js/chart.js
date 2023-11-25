var productId = '@ViewBag.ProductId'; // Retrieve the userId from ViewBag or wherever it's set


// Function to fetch data for the flex container from the server based on the productId
function fetchFlexDataFromServer(productId) {
    //fetch(`http://localhost:8080/charts/productInfoData/${productId}`) //Debugging url
    fetch(`https://sprint1deploymentgroup1.azurewebsites.net/charts/productInfoData/${productId}`) // Release URL
        .then((response) => response.json())
        .then((data) => {
            updateFlexContainer(data);
        })
        .catch((error) => {
            console.error("Error fetching data:", error);
        });
}
// Function to update the flex container with fetched data
function updateFlexContainer(data) {
    const valueElements = document.querySelectorAll(".flex-value");
    if (valueElements.length !== Object.keys(data).length) {
        console.error("Data length doesn't match the number of elements.");
        return;
    }

    valueElements.forEach((element, index) => {
        element.textContent = data[Object.keys(data)[index]];
    });
}

// Sample data for monthly sales
var salesData = {
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
      label: "Monthly Sales",
      data: [],
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

//Fetch and update data for monthly sales and monthly viesw and sales charts 
function fetchMonthlySalesFromServer(productId) {
    //fetch(`http://localhost:8080/charts/monthlySalesData/${productId}`) //Debugging url
    fetch(`https://sprint1deploymentgroup1.azurewebsites.net/charts/monthlySalesData/${productId}`) // Release URL
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

// Sample data for monthly views
var viewsData = {
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
      label: "Page Views",
      data: [],
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
//Fetch and update data for monthly views chart and monthly viesw and sales charts 
function fetchMonthlyViewsFromServer(productId) {
   // fetch(`http://localhost:8080/charts/monthlyViewsData/${productId}`) //Debugging url
    fetch(`https://sprint1deploymentgroup1.azurewebsites.net/charts/monthlyViewsData/${productId}`) // Release URL
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
      data: [],
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
//Fetch data for monthly satisfaction
function fetchMonthlySatisfactionFromServer(productId) {
    //fetch(`http://localhost:8080/charts/monthlySatisfactionData/${productId}`) //Debugging url
    fetch(`https://sprint1deploymentgroup1.azurewebsites.net/charts/monthlySatisfactionData/${productId}`) // Release URL
        .then((response) => response.json())
        .then((data) => {
            satisfactionData.datasets[0].data = data.monthlySatisfaction;
            myChartSatisfaction.update();
        })
        .catch((error) => {
            console.error("Error fetching monthly satisfaction data:", error);
        });
}


// Create a chart that combines sales and views data
var ctxSalesAndViews = document
  .getElementById("MonthlySalesAndViews")
  .getContext("2d");
var myChartSalesAndViews = new Chart(ctxSalesAndViews, {
  type: "line", // Use a line chart for sales and views
  data: {
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


// Check if chart3 is the only child
if (document.querySelectorAll(".chart-container").length === 1) {
  // Add a class for full-width layout
  document.querySelector(".chart-container").classList.add("full-width-chart");
}


// Function to be executed on window load
window.addEventListener("load", function () {
    if (productId) {
        fetchFlexDataFromServer(productId);
        fetchMonthlySalesFromServer(productId);
        fetchMonthlyViewsFromServer(productId);
        fetchMonthlySatisfactionFromServer(productId);
    } else {
        console.error("Product ID not found!");
    }
});