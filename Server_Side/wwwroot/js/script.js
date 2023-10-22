// Example data for the flex containers/ array to hold data for flex containers
const values = [
  "Error: Fetching",
  "Error: Fetching",
  "Error: Fetching",
  "Error: Fetching",
];
function fetchFlexDataFromServer() {
  fetch("https://localhost:5001/analytic/salesData")
    .then((response) => response.json())
    .then((data) => {
      values[0] = data.salesTotal;
      values[1] = data.viewTotal;
      values[2] = data.lifetimeSales;
      values[3] = data.averageSatisfaction;

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
  fetch("https://localhost:5001/analytic/tableData") // Replace with the actual endpoint
    .then((response) => response.json())
    .then((tableData) => {
      // Call the updateTable function to update the table with the fetched data
      updateTable(Data);
    })
    .catch((error) => {
      console.error("Error fetching table data:", error);
    });
}

// Call the function to fetch table data when the page loads
window.addEventListener("load", fetchTableDataFromServer);

// Function to update the table with fetched data
function updateTable(tableData) {
  const tableBody = document.getElementById("table-body");
  tableData.forEach((rowData, index) => {
    const row = document.createElement("tr");
    for (const key in rowData) {
      const cell = document.createElement("td");
      if (key === "col5") {
        const detailsLink = document.createElement("a");
        detailsLink.textContent = "Details";
        detailsLink.href = "html/Charts.html"; // Specify the correct URL here
        cell.appendChild(detailsLink);
      } else {
        cell.textContent = rowData[key];
      }
      row.appendChild(cell);
    }
    tableBody.appendChild(row);
  });
}
