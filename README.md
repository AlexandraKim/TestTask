# TestTask

This is a sample test task for [https://bodylease.dev/vacancy/aspnet-developer]. 

## Table of Contents

1. [Requirements](#requirements)
2. [Installation](#installation)
3. [Running the Application](#running-the-application)
4. [Testing](#testing)
5. [Deployment](#deployment)
6. [Usage](#usage)

## Requirements

1. Create login form with fields "Username" and "Password"
  a. There must be an error notification if any of the parameters are incorrect
  b. There must be 2 users with different roles - admin, user
  c. Users can be added to the database using a registration functionality or database seeding scripts
  d. If the user has submitted a valid username and password then the system must redirect to the view from point 2.
2. Product management:
  a. A HTML table with columns "Item name", "Quantiy", "Price" and data loaded from database table "product" (see point 3)
  b. The user with "admin" role must have full CRUD functionality. The input fields in forms must have validation.
  c. The user with "user" role must only be able to view the records
  d. A extra column "Total price with VAT" which is calculated in backend using formula: (Item amount * Price per item) * (1+VAT), where VAT value is retrieved from configuration file
  e. A unit test must be created for method which does point 2.d calculation;
3. A Table called "product" must be created in the database using migration scripts and data must be inserted using seeding scripts. For
example:
# title quantiy price
1 HDD 1TB 55 74.09
2 HDD SSD 512GB 102 190.99
3 RAM DDR4 16GB 47 80.32
... ... ... ..
4. Database requires the storage of data history, for example table "product_audit" which stores data about the changes - what was changed
by which user and when the data was changed.
  a. Data is not required to be available in a UI but "admin" user must have access to them via HTTP API (JSON) with the ability to filter the
data based on the data modification date (i.e. with &from and &to GET parametrs by the specified date)
5. The source code and deployment instruction (i.e. readme.md how to deploy and run program) should be posted in public code repository,
for example github.com or anywhere else

## Installation

1. **Clone the repository:**
    ```bash
    git clone https://github.com/AlexandraKim/TestTask.git
    cd TestTask
    ```

2. **Restore dependencies:**
    ```bash
    dotnet restore
    ```

3. **Set up the database:**
    - Update the `appsettings.json` file with your database connection string.
    - Apply migrations:
        ```bash
        dotnet ef database update
        ```

## Running the Application

1. **Run the application:**
    ```bash
    dotnet run
    ```

2. The application will start on `http://localhost:5162/` by default. You can change the port in the `launchSettings.json` file if needed.

## Testing

1. **Run unit tests:**
    ```bash
    dotnet test
    ```

## Usage

1. **Access the application:**
    Open your browser and navigate to `http://localhost:5162` (or the appropriate URL if deployed).

2. **API Endpoints:**
    - **GET** `/api/ProductChangesApi`: Retrieve a list of changes.

3. **Swagger Documentation:**
    Navigate to `http://localhost:5162/api` to view the API documentation and test endpoints interactively.

