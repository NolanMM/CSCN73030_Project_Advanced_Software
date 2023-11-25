
//set the userID
var userId = '@ViewBag.UserId';
window.addEventListener("load", function () {
    if (userId) {
        fetchTableDataFromServer(userId);
        fetchFlexDataFromServer();
    } else {
        console.error("User ID not found!");
    }
});


function fetchFlexDataFromServer() {
    fetch(`http://localhost:8080/analytics/salesData/Profile/${userId}`)
        .then((response) => response.json())
        .then((data) => {
            const values = [
                data.salesTotal,
                data.viewTotal,
                data.lifetimeSales,
                data.averageSatisfaction
            ];
            updateFlexContainer(values);
        })
        .catch((error) => {
            console.error("Error fetching data:", error);
        });
}
function updateFlexContainer(values) {
    const valueElements = document.querySelectorAll(".flex-value");
    valueElements.forEach((element, index) => {
        element.textContent = values[index];
    });
}

// Example data for the product table
const Data = [
  {
    col1: "Row 1, Col 1",
    col2: "Row 1, Col 2",
    col3: "Row 1, Col 3",
    col4: "Row 1, Col 4",
    col5: "Details",
  },
  {
    col1: "Row 2, Col 1",
    col2: "Row 2, Col 2",
    col3: "Row 2, Col 3",
    col4: "Row 2, Col 4",
    col5: "Details",
  },
  {
    col1: "Row 3, Col 1",
    col2: "Row 3, Col 2",
    col3: "Row 3, Col 3",
    col4: "Row 3, Col 4",
    col5: "Details",
  },
];

// Function to fetch table data from the server
function fetchTableDataFromServer() {
    fetch(`http://localhost:8080/analytics/tableData/Profile/${userId}`)
    //fetch("https://sprint1deploymentgroup1.azurewebsites.net/analytics/tableData") // Replace with the actual endpoint
    .then((response) => response.json())
    .then((tableData) => {
      // Call the updateTable function to update the table with the fetched data
      updateTable(Data);
    })
    .catch((error) => {
        console.error("Error fetching table data:", error);
        clearTable();
    });
}

// Call the function to fetch table data when the page loads
window.addEventListener("load", fetchTableDataFromServer);

// Function to update the table with fetched data
function updateTable(tableData) {
  const tableBody = document.getElementById("table-body");
  tableBody.innerHTML = ""; // Clear previous data before populating
  tableData.forEach((rowData, index) => {
    const row = document.createElement("tr");
    for (const key in rowData) {
      const cell = document.createElement("td");
      if (key === "col5") {
        const detailsLink = document.createElement("a");
        detailsLink.textContent = "Details";
        detailsLink.href = "/analytics/charts"; // Specify the correct URL here
        cell.appendChild(detailsLink);
      } else {
        cell.textContent = rowData[key];
      }
      row.appendChild(cell);
    }
    tableBody.appendChild(row);
  });
}
