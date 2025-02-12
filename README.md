![🖥️CSMS(1)](https://github.com/user-attachments/assets/dfaefc27-9a31-4a8a-b17e-831bf272c242)
___
<p align="center">
Computer Service Management System is an application that allows you to manage computer service on various fields. App made as semestral project from Business <b>Desktop Applications Development</b>.

<p align="center">
  <img src="https://github.com/VortexOoN/ComputerRepairService/blob/main/Readme/1.PNG">
</p>

## Features

Computer Service is a desktop application developed using WPF technology, following the MVVM design pattern and utilizing Entity Framework. The application allows managing a computer repair service, including:
- Adding, Modyfying, Deleting, Displaying of
  - Employees
  - Roles (like Technician etc)
  - Services
  - Parts
  - Part Categories
  - Ordering Parts
  - Suppliers
  - Repairs
  - Invoices
  - Schedules
  - Job Statuses
  - Feedbacks
- Generating Raports in card form from given period of time
- Sorting and Ordering on database in displaying views
- Input Data Validation
- Analyzing how service is performing in Dashboard like component

## Technologies
- 👨‍💻 C# main programming language
- 🖥️ WPF (Windows Presentation Foundation) user interface
- 🧱 MVVM (Model-View-ViewModel) application architecture
- 🛠️ Entity Framework Core  data access layer
- 🛢️ SQL database

## Screenshots

<p align="center">
  <img src="https://github.com/VortexOoN/ComputerRepairService/blob/main/Readme/2.PNG">
</p>

<p align="center">
  <img src="https://github.com/VortexOoN/ComputerRepairService/blob/main/Readme/3.PNG">
</p>

## Project Structure

| Directories          |   Content Description                   |
|------------------|--------------------------------|
| Helpers            | Contains utility classes like BaseCommand.cs  |
| Models           | Contains services, dto's, db context |
| Models/Services           | Contains services for centralized logic |
| Models/Dtos           | Contains data transfer objects |
| Models/Contexts           | Contains database context |
| Readme           | files used in readme.md                    |
| ViewModels           | All viewmodels for application                    |
| Views           | All views for application                    |
| ViewModels           | Contains images used in application                     |


## Installation and Setup
1. Clone Repo
````github
git clone https://github.com/VortexOoN/ComputerRepairService.git
````
2. Database Configuration

    - Make sure you have installed [SSMS](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16) and [SQL Server](https://www.microsoft.com/pl-pl/sql-server/sql-server-downloads)

    - Create Database from file : [ComputerService.sql](https://github.com/VortexOoN/ComputerRepairService/blob/main/ComputerService.sql)

3. Database Context code changes

    - Change database data in connection string in [DatabaseContext](https://github.com/VortexOoN/ComputerRepairService/blob/main/Models/Contexts/DatabaseContext.cs)
    <p align="center">
      <img src="https://github.com/VortexOoN/ComputerRepairService/blob/main/Readme/connection_string.png">
    </p>
4. Build Project in Visual Studio and Run !
   - Now, you are all set up 😎

  
## Contributing

1. Fork the repository
2. Create a branch for your new feature
3. Commit changes with clear description of changes you've made
