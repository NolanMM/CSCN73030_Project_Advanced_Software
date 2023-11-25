var userId = '@ViewBag.UserId'; // Retrieve the userId from ViewBag or wherever it's set
function fetchFlexDataFromServer() {
    fetch(`http://localhost:8080/analytics/salesData/Profile/${userId}`)
        .then((response) => response.json())
        .then((data) => {
            updateFlexContainer(data);
        })
        .catch((error) => {
            console.error("Error fetching data:", error);
        });
}
function updateFlexContainer(data) {
    const valueElements = document.querySelectorAll(".flex-value");
    if (valueElements.length !== data.length) {
        console.error("Data length doesn't match the number of elements.");
        return;
    }

    valueElements.forEach((element, index) => {
        element.textContent = data[index];
    });
}

// Function to fetch table data from the server
function fetchTableDataFromServer(userId) {
    fetch(`http://localhost:8080/analytics/tableData/Profile/${userId}`) //Debugging url
    //fetch("https://sprint1deploymentgroup1.azurewebsites.net/analytics/tableData") // Replace with the actual endpoint
    .then((response) => response.json())
    .then((tableData) => {
      // Call the updateTable function to update the table with the fetched data
      updateTable(tableData);
    })
    .catch((error) => {
        console.error("Error fetching table data:", error);
        clearTable();
    });
}


// Function to be executed on window load
window.addEventListener("load", function () {
    if (userId) {
        fetchTableDataFromServer(userId);
        fetchFlexDataFromServer(userId); 
    } else {
        console.error("User ID not found!");
    }
});


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