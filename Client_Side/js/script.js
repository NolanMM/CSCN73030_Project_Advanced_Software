// Example data for the flex containers/ array to hold data for flex containers
const values = [
  "Error: Fetching",
  "Error: Fetching",
  "Error: Fetching",
  "Error: Fetching",
];
// Function to receive/request flex container data from c# berkley socket server restful
function fetchFlexDataFromServer() {
  //fetch('http://localhost:8080/api/salesData')
  fetch("/api/salesData") // Replace with your actual API endpoint
    .then((response) => response.json())
    .then((data) => {
      // Update the 'values' array with the received data
      values[0] = data.salesTotal;
      values[1] = data.viewTotal;
      values[2] = data.lifetimeSales;
      values[3] = data.averageSatisfaction;
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
// Call the updateFlexContainer function on page load
window.addEventListener("load", updateFlexContainer);

// Example data for the product table
const data = [
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
// Function to update the table
function updateTable() {
  const tableBody = document.getElementById("table-body");

  data.forEach((rowData, index) => {
    const row = document.createElement("tr");
    for (const key in rowData) {
      const cell = document.createElement("td");
      if (key === "col5") {
        const detailsLink = document.createElement("a");
        detailsLink.textContent = "Details";
        detailsLink.href = "/Client_Side/html/Charts.html"; // Specify the chat.html URL here
        cell.appendChild(detailsLink);
      } else {
        cell.textContent = rowData[key];
      }
      row.appendChild(cell);
    }
    tableBody.appendChild(row);
  });
}
// Call the updateTable function on page load
window.addEventListener("load", updateTable);
