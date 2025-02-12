CREATE DATABASE ComputerRepairShopDB
GO
USE ComputerRepairShopDB;

--1. Addreses table
CREATE TABLE Addresses (
    Id INT PRIMARY KEY IDENTITY,
	Place NVARCHAR(128),
    Street NVARCHAR(128),
	HouseNumber nvarchar(16),
    PostalCity NVARCHAR(100) NOT NULL,
    PostalCode NVARCHAR(32) NOT NULL,
    Country NVARCHAR(100) NOT NULL,
    IsActive BIT,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);

--2. Customers table
CREATE TABLE Customers (
    Id INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(100) NOT NULL,
	Surname NVARCHAR(100) NOT NULL,
	NIP NVARCHAR(20),
	Title nvarchar(128),
    PhoneNumber NVARCHAR(15),
    Email NVARCHAR(100),
    AddressId INT FOREIGN KEY REFERENCES Addresses(Id) NOT NULL,
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);
--3. Roles for Employees
CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY,
	RoleName NVARCHAR(100) NOT NULL,    
	RoleDescription NVARCHAR(255),
	IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL, 
    DateEdited DATETIME,
    DateDeleted DATETIME  
);

--4. Employees table
CREATE TABLE Employees (
    Id INT PRIMARY KEY IDENTITY,
    FirstName NVARCHAR(100) NOT NULL,
	Surname NVARCHAR(100) NOT NULL,
    RoleId INT FOREIGN KEY REFERENCES Roles(Id) ,
    PhoneNumber NVARCHAR(15),
    Email NVARCHAR(100),
    HireDate DATE NOT NULL,
    Salary DECIMAL(10, 2) NOT NULL,
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);
--5. Categories table for categories of parts
CREATE TABLE PartCategories (
    Id INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(100) NOT NULL,
    CategoryDescription NVARCHAR(255),
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);
--6. Parts table for stroing individual parts
CREATE TABLE Parts (
    Id INT PRIMARY KEY IDENTITY,
    PartName NVARCHAR(100) NOT NULL,
	PartCategoryId INT FOREIGN KEY REFERENCES PartCategories(Id) NOT NULL,
    PartDescription NVARCHAR(255),
    QuantityInStock INT NOT NULL,
    UnitPrice DECIMAL(10, 2) NOT NULL,
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);
--7. Suppliers table 
CREATE TABLE Suppliers (
    Id INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(15) ,
	NIP NVARCHAR(20),
    Email NVARCHAR(100),
    AddressId INT FOREIGN KEY REFERENCES Addresses(Id) NOT NULL,
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);
--8. PartOrders table for stroring ordered parts details
CREATE TABLE PartOrders (
    Id INT PRIMARY KEY IDENTITY,
    SupplierId INT FOREIGN KEY REFERENCES Suppliers(Id) NOT NULL,
    PartId INT FOREIGN KEY REFERENCES Parts(Id) NOT NULL,
    QuantityOrdered INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    DeliveryDate DATE ,
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);
--9. Job statuses to monitor what is happening with repair jobs
CREATE TABLE JobStatuses (
    Id INT PRIMARY KEY IDENTITY,
    StatusName NVARCHAR(100) NOT NULL,
    StatusDescription NVARCHAR(255),
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,                 
    DateDeleted DATETIME                   
);
--10. Repair jobs for handling, adding new repairs
CREATE TABLE RepairJobs (
    Id INT PRIMARY KEY IDENTITY,
    CustomerId INT FOREIGN KEY REFERENCES Customers(Id) NOT NULL,
    IssueDescription NVARCHAR(255),
    JobStatus INT FOREIGN KEY REFERENCES JobStatuses(Id) NOT NULL,
    TotalCost DECIMAL(10, 2),
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);

-- 11. JobParts Table (Many-to-Many between RepairJobs and Parts)
CREATE TABLE JobParts (
    Id INT PRIMARY KEY IDENTITY,
    JobId INT FOREIGN KEY REFERENCES RepairJobs(Id) NOT NULL,
    PartId INT FOREIGN KEY REFERENCES Parts(Id) NOT NULL,
    QuantityUsed INT NOT NULL,
    Cost DECIMAL(10, 2),
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);

-- 12. Services Table
CREATE TABLE Services (
    Id INT PRIMARY KEY IDENTITY,
    ServiceName NVARCHAR(100) NOT NULL,
    ServiceDescription NVARCHAR(255),
    BasePrice DECIMAL(10, 2) NOT NULL,
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);

-- 13 JobServices Table (Many-to-Many between RepairJobs and Services)
--cost is a price after negotiations or discounts. Not the same as field BasePrice in Serivces!
CREATE TABLE JobServices (
    Id INT PRIMARY KEY IDENTITY,
    JobId INT FOREIGN KEY REFERENCES RepairJobs(Id)NOT NULL,
    ServiceId INT FOREIGN KEY REFERENCES Services(Id)NOT NULL,
    Cost DECIMAL(10, 2) NOT NULL,
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);

-- 14. Invoices Table
CREATE TABLE Invoices (
    Id INT PRIMARY KEY IDENTITY,
    JobId INT FOREIGN KEY REFERENCES RepairJobs(Id)NOT NULL,
    DateOfIssue DATETIME NOT NULL,
    TotalCost DECIMAL(10, 2) NOT NULL,
    PaymentStatus NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);
-- 15. PaymentMethods (not used in project)
CREATE TABLE PaymentMethods (
    Id INT PRIMARY KEY IDENTITY,
	Title nvarchar(128) NOT NULL,
	TransactionFee DECIMAL(10, 2) NOT NULL,
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);
-- 16. Payments Table (not used in project)
CREATE TABLE Payments (
    Id INT PRIMARY KEY IDENTITY,
    InvoiceId INT FOREIGN KEY REFERENCES Invoices(Id) NOT NULL,
    PaymentDate DATE NOT NULL,
	Currency VARCHAR(10) NOT NULL,
    PaymentMethod INT FOREIGN KEY REFERENCES PaymentMethods(Id) NOT NULL,
    Amount DECIMAL(10, 2) NOT NULL,
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);

-- 17. Feedback Table
CREATE TABLE Feedback (
    Id INT PRIMARY KEY IDENTITY,
    JobId INT FOREIGN KEY REFERENCES RepairJobs(Id)NOT NULL,
    FeedbackText NVARCHAR(255) NOT NULL,
    Rating float NOT NULL,
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);

-- 18. Schedules Table stores schedulge for repair jobs
CREATE TABLE Schedules (
    Id INT PRIMARY KEY IDENTITY,
    EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
    JobId INT FOREIGN KEY REFERENCES RepairJobs(Id) NOT NULL,
    DateAssigned DATETIME NOT NULL,
    StartDate DATETIME NOT NULL,
    EndDate DATETIME,
    IsActive BIT NOT NULL,
    DateCreated DATETIME NOT NULL,
    DateEdited DATETIME,
    DateDeleted DATETIME
);


