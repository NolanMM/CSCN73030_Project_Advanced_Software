# CSCN73030_Project_Advanced_Software

 ![Main Branch Workflow](https://github.com/NolanMM/CSCN73030_Project_Advanced_Software/actions/workflows/Main_Branch_Workflow.yml/badge.svg)

Explore key metrics and analysis to drive data-informed decisions for your business.

The functional overview of our analytics dashboard module provides a comprehensive insight into various key performance metrics for our website. This dashboard is a powerful tool to monitor and analyze the performance and success of our online presence. By leveraging the dashboard's data and insights, we can make informed decisions to drive our business's growth and success.

### I. Analytics and Report Module Services

><details><summary><strong> 1. Unique Visitors </strong></summary>
>
>  This metric represents the total number of distinct individuals who visited the site within a specified period. By tracking this, we can gauge the reach and popularity of our website among different segments of users. Understanding the behavior and  preferences of our unique visitors allows us to tailor our strategies and content to engage better and retain our audience.

</details>

><details><summary><strong> 2. Page Views </strong></summary>
>
> Understanding which pages receive the most attention is crucial for optimizing our website's content and layout. We can identify our website's most engaging and valuable sections by analyzing page views. This information helps us prioritize our efforts in creating compelling and relevant content that resonates with our audience, ultimately driving higher engagement and conversion rates.

</details>

><details><summary><strong> 3. Total Sales </strong></summary>
>
> This metric gives us a snapshot of gross revenue over a specific timeframe. By monitoring total sales, we can assess the overall financial health of our business and identify periods of high or low sales activity. This information is invaluable for making informed decisions about pricing strategies, inventory management, and marketing campaigns to maximize revenue and profitability.

</details>

><details><summary><strong> 4. Average Order Value (AOV) </strong></summary>
>
> Knowing how much customers spend per order is essential for strategic pricing decisions. By analyzing the average order value, we can identify opportunities to increase revenue by encouraging customers to spend more. This could involve offering bundled products, upselling or cross-selling, or implementing loyalty programs to incentivize higher-order values.

</details>

><details><summary><strong> 5. Best Category Analysis </strong></summary>
>
> To maximize profits, it is essential to identify the most popular and profitable product categories. We can focus on promoting and expanding these categories by conducting a best-category analysis. This may involve optimizing marketing campaigns, improving product assortment, or enhancing the overall customer experience within these categories to drive sales and revenue further.

</details>

><details><summary><strong> 6. Time Analysis </strong></summary>
>
> Sales trends can vary throughout the year, and understanding these patterns is crucial for effective planning and resource allocation. We can identify seasonal trends and adjust our strategies by conducting time analysis. This allows us to allocate resources more efficiently, optimize inventory levels, and tailor marketing efforts to capitalize on peak seasons or mitigate dips in sales during slower periods.

</details>

><details><summary><strong> 7. Feedback/Stars Analysis </strong></summary>
>
> Customer feedback is invaluable for making data-driven decisions on product recommendations. By analyzing feedback and star ratings, we can identify areas for improvement and make informed decisions on product development and marketing. This information helps us enhance product quality, address customer concerns, and build stronger relationships with our audience.

</details>

><details><summary><strong> 8. Conversion Rate by Product </strong></summary>
>
> Understanding which products have the highest conversion rate is essential for optimizing our product offerings. We can identify top-performing products by analyzing the conversion rate by product and prioritize resources accordingly. This allows us to allocate marketing budgets, inventory space, and other resources to products with higher conversion rates, driving overall sales and profitability.

</details>

><details><summary><strong> 9. Price Analysis </strong></summary>
>
> Comparing product price ranges is essential for ensuring competitive pricing and maximizing profitability. We can identify opportunities to adjust prices and improve our pricing strategy by conducting a price analysis. This could involve analyzing price elasticity, benchmarking against competitors, or implementing dynamic pricing strategies to optimize revenue and maintain a competitive edge.

</details>

><details><summary><strong> 10. Sales Analysis </strong></summary>
>
> Prioritizing products based on sales and customer feedback can help us optimize our portfolio. We can identify top-selling products and allocate resources by conducting a sales analysis. This allows us to focus on higher-demand products, optimize inventory levels, and streamline operations to meet customer expectations and preferences.

</details>

><details><summary><strong> 11. Profit and Loss </strong></summary>
>
> To comprehensively understand our business's financial health, analyzing the detailed breakdown of revenues and expenses is essential. By conducting a profit and loss analysis, we can identify areas of improvement and optimize our business operations. This includes identifying cost-saving opportunities, optimizing pricing and discount strategies, and refining our business model to drive profitability and sustainable growth.

</details>

---

### I, Folder Hierarchy
> <details><summary>CSCN73030_Project_Advanced_Software Repos</summary>
> 
>  - `CSCN73030_Project_Advanced_Software/`
>    - `.github/workflows/`
>    - `.vs/`
>    - `AnalysisServiceTests/`
>      - `Analysis_Report_Services_Test.cs`
>    - `Server_Side/`
>      - `.vs/Server_Side/`
>      - `Controllers/`
>        - `AnalyticsController.cs`
>      - `Database_Services/`
>        - `Input_Schema/Raw_Data_Tables_Class/`
>        - `Output_Schema/Log_Database_Schema/`
>        - `Notebooks_Minh/`
>        - `Table_Interface/`
>        - `DataWarehouse_Analysis_Reports_Services.cs`
>      - `GraphQL/`
>        - `GraphQL_Schemas.cs`
>      - `Services/`
>        - `Analysis_Report_Services.cs`
>      - `wwwroot/`
>        - `css/`
>        - `image/`
>        - `js/`
>      - `obj/`
>      - `Views/Analytics/`
>      - `Souce.cs/`
>      - `Server_Side.sln`
>    - `DatabaseAnalysisModuleTests/`
>      - `Database_Tests/`
>      - `Test_Database_Services.cs`
>    - `Client_Side/`
>      - `css/`
>      - `html/`
>      - `images/`
>      - `js/`
>      - `.DS_Store/`
>    - `README.md`
></details>
[Back To Top](#CSCN73030_Project_Advanced_Software)

---

### II. Table of Contents

- [CSCN73030\_Project\_Advanced\_Software](#cscn73030_project_advanced_software)
    - [I. Analytics and Report Module Services](#i-analytics-and-report-module-services)
    - [I, Folder Hierarchy](#i-folder-hierarchy)
    - [II. Table of Contents](#ii-table-of-contents)
      - [1. Status Main Branch Deploy](#1-status-main-branch-deploy)
      - [2. Tech Stack:](#2-tech-stack)
      - [3. High Level Module Design](#3-high-level-module-design)
      - [4. Endpoints](#4-endpoints)
      - [5. Analysis Report Services Data Structures](#5-analysis-report-services-data-structures)
        - [5.1 List of Valid User Views](#51-list-of-valid-user-views)
        - [5.2 List of Website logs](#52-list-of-website-logs)
        - [5.3 List of Sales Transactions](#53-list-of-sales-transactions)
        - [5.4 List of Feedback](#54-list-of-feedback)
      - [6. MVP Designs](#6-mvp-designs)
        - [6.1 EndPoint 1: .root/](#61-endpoint-1-root)
        - [6.2 EndPoint 2: .root/analytics/charts/](#62-endpoint-2-rootanalyticscharts)
      - [7. Nuget Packages Install:](#7-nuget-packages-install)

---

#### 1. Status Main Branch Deploy
 ![Main Work Flow](https://github.com/NolanMM/CSCN73030_Project_Advanced_Software/actions/workflows/Main_Branch_Workflow.yml/badge.svg)

---

#### 2. Tech Stack:

- `Server:` [ASP.NET]() 6.0 Web API 
- `Front-end:` JavaScript, CsHTML, CSS
- `Database:` Azure MySQL

[Back To Top](#CSCN73030_Project_Advanced_Software)

---

#### 3. High Level Module Design

<p align="center">
  <img src="Documents/Project_Images/High_Level_Design.png" alt="High Level Module Design Image" width="80%">
</p>

[Back To Top](#CSCN73030_Project_Advanced_Software)

---

#### 4. Endpoints
<strong>.root:</strong> https://localhost:<\port>
<details>
  <summary><strong>EndPoint 1: .root/</strong></summary>
  <ul>
    <li>1. <strong>GET</strong> -> .root/js/script.js</li>
    <li>2. <strong>GET</strong> -> .root/js/script.js</li>
    <li>3. <strong>GET</strong> -> .root/css/style.css</li>
    <li>4. <strong>GET</strong> -> .root/image/logo.png</li>
    <li>
        5. <strong>GET</strong> -> .root/analytic/salesData
        <ul>
            <li>-> GetTotalSales() -> <strong>SaleTransaction</strong> class </li>
            <li>-> GetPageViews() -> <strong>PageView</strong> class </li>
            <li>-> GetSalesAnalysis() -> <strong>Dictionary<\string, int></strong></li>
            <li>-> GetFeedbackAnalysis() -> <strong>Feedback</strong> class </li>
        </ul>
    </li>
    <li>6. <strong>GET</strong> -> .root/analytic/tableData</li>
  </ul>
</details>

<details>
  <summary><strong>EndPoint 2: .root/analytics/charts/</strong></summary>
  <ul>
    <li>1. <strong>GET</strong> -> .root/js/chart.js</li>
    <li>
        2. <strong>GET</strong> -> .root/charts/productInfoData
        <ul>
            <li>-> GetConversionRate() -> <strong>PageView & SaleTransaction</strong> classes</li>
        </ul>
    </li>
    <li>
        3. <strong>GET</strong> -> .root/charts/monthlySalesData
        <ul>
            <li>->GetTimeAnalysis() -> <strong>SaleTransaction</strong> class</li>
        </ul>
    </li>
    <li>4. <strong>GET</strong> -> .root/charts/monthlyViewsData</li>
    <li>5. <strong>GET</strong> -> .root/charts/monthlySatisfactionData</li>
  </ul>
</details>

---

#### 5. Analysis Report Services Data Structures

##### 5.1 List of Valid User Views

```CSharp
        public class UserView
        {
            public string UserId { get; set; }
            public DateTime Timestamp { get; set; }
        }
```

##### 5.2 List of Website logs

```CSharp
        public class PageView
        {
            public string SessionId { get; set; }
            public string UserId { get; set; }
            public string PageUrl { get; set; }
            public string PageInfo { get; set; }
            public string ProductId { get; set; }
            public DateTime DateTime { get; set; }
        }   
```

##### 5.3 List of Sales Transactions

```CSharp
        public class SaleTransaction
        {
            public string TransactionId { get; set; }
            public string UserId { get; set; }
            public decimal TransactionValue { get; set; }
            public DateTime Date { get; set; }
        }
```

##### 5.4 List of Feedback

```CSharp
        public class Feedback
        {
            public string UserId { get; set; }
            public string ProductId { get; set; }
            public int StarRating { get; set; }
        }
```

[Back To Top](#CSCN73030_Project_Advanced_Software)

---

#### 6. MVP Designs

##### 6.1 <strong>EndPoint 1: .root/</strong>
<div style="display: flex;">
  <div style="flex: 50%; padding: 10px;">
    <strong>Design</strong>
    <img src="Documents/Project_Images/MVP_Design_1.png" alt="MVP Designs 1">
  </div>
  <div style="flex: 50%; padding: 10px;">
    <strong>Preview</strong>
    <img src="Documents/Project_Images/MVP_Design_1_UI.png" alt="MVP Preview UI 1">
  </div>
</div>

##### 6.2 <strong>EndPoint 2: .root/analytics/charts/</strong>
<div style="display: flex; flex-direction: column;">
  <div style="padding: 10px;">
    <strong>Design</strong>
    <img src="Documents/Project_Images/MVP_Design_2.png" alt="MVP Designs 2" style="width: 100%;">
  </div>
  <div style="display: flex;">
    <div style="flex: 50%; padding: 10px;">
      <strong>Preview UI 1</strong>
      <img src="Documents/Project_Images/MVP_Design_2_UI_2.png" alt="MVP Preview UI 2 1" style="width: 100%;">
    </div>
    <div style="flex: 50%; padding: 10px;">
      <strong>Preview UI 2</strong>
      <img src="Documents/Project_Images/MVP_Design_2_UI_1.png" alt="MVP Preview UI 2 2" style="width: 100%;">
    </div>
  </div>
</div>


---

#### 7. Nuget Packages Install:

- `GraphQL: 7.6.1`
- `GraphQL.Authorization: 7.0.0`
- `GraphQL.Server.Transports.AspNetCore: 7.6.0`
- `GraphQL.Server.Ui.Voyager: 7.6.0`

[Back To Top](#CSCN73030_Project_Advanced_Software)

---
