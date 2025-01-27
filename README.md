# MCComputers

MCComputers is a simple .NET 8 Web API designed to generate invoices. This API allows users to create, manage, and process invoices, leveraging a **Code First** approach with **Entity Framework** for database interactions.

## Features

- **Invoice Generation**: Generate invoices through a simple API.
- **Database Integration**: Uses SQL Server as the database backend, configured via Entity Framework.

## Technologies Used

- **.NET 8**: The backend is built using the latest .NET 8 framework.
- **Entity Framework Core**: Utilized for database access with a Code First approach.
- **SQL Server**: The application interacts with a SQL Server database for persistent storage.
- **xUnit**: Unit tests are written using **xUnit** for test automation.

## Prerequisites

Before you begin, ensure you have the following:

- **Visual Studio 2022** (or a compatible IDE)
- **SQL Server** (or an equivalent database server)
- **.NET 8 SDK**

## Installation

Follow these steps to set up the project:

1. **Create the SQL Database**:  
   Create a SQL Server database named `MCComputers`.

2. **Configure the Database Connection**:  
   Open the `appsettings.json` file and update the connection string with your own SQL Server credentials:
   ```
   json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=your_server_name;Database=MCComputers;User Id=your_username;Password=your_password;"
     }
   }
3. **Open the Project in Visual Studio**:  
   Open the solution in **Visual Studio 2022**.

4. **Install Dependencies**:  
   Ensure all necessary dependencies are installed:

- In Visual Studio, go to the **Package Manager Console** and run:  
   ```  
   dotnet restore  
   ```
5. **Create the Migration**:  
   To generate the migration file, open the **Package Manager Console** in Visual Studio and run the following command:  
   ```  
   add-migration  
   ```
6. **Update the Database**:  
   After the migration file is created, apply it to the database to create the necessary tables. Run the following command:  
   ```
   update-database  
   ```
7. **Start the API**:  
   Run the project from Visual Studio. The Web API should be up and running.

## Usage

Once the API is running, you can interact with it via HTTP requests.

### Example Endpoints:

1. **POST /api/invoice**: Create a new invoice.

   **Request Body**:
   ```
   json

   [
      {
         ProductId : 2,
         Quantity : 5,
         Price : 100.00,
         Discount : 15
      },
      {
         ProductId : 4,
         Quantity : 10,
         Price : 150.00,
         Discount : 5
      }
   ]
   ```
   **Response**:  
   **201 Created**: If the invoice was successfully created.

2. **GET /api/invoice**: Retrieve a list of all invoices.  
   **Response**  
   ```
   [
    {
      "id": 1,
      "customerName": "John Doe",
      "invoiceDate": "2025-01-08",
      "totalAmount": 150.75
    },
    {
      "id": 2,
      "customerName": "Jane Smith",
      "invoiceDate": "2025-02-15",
      "totalAmount": 200.00
    }
  ]
  
3. **GET /api/invoice/{id}**: Retrieve a specific invoice by ID.  
   **URL Parameters**:  
   `id:` The unique identifier of the invoice.  
   **Response**  
   ```  
   {
     "id": 1,
     "customerName": "John Doe",
     "invoiceDate": "2025-01-08",
     "totalAmount": 150.75
   }

## License
This project is licensed under the MIT License – see the LICENSE file for details.
