IF db_id('DB_project2') IS NULL 
    CREATE DATABASE DB_project2
GO
USE DB_project2

CREATE TABLE Model (
	Id INT identity(1,1) NOT NULL,
	Name character varying(30) NOT NULL,
	CarBody character varying(30) NOT NULL,
	Year integer NOT NULL CHECK(Year > 0),
	BrandId integer NOT NULL,
	Price INT NOT NULL,
	Seats INT NOT NULL,
	DriveId INT NOT NULL,
	EngineType	character varying(30) NOT NULL,
	TopSpeed INT NOT NULL,
	Acceleration float NOT NULL,
	Image Image,
	CONSTRAINT Model_pk PRIMARY KEY (Id)
);

CREATE TABLE Drive (
	Id INT identity(1,1) NOT NULL,
	Name character varying(30) NOT NULL,
	CONSTRAINT Drive_pk PRIMARY KEY (Id)
);

CREATE TABLE Brand (
	Id INT identity(1,1) NOT NULL,
	BrandName character varying(30) NOT NULL UNIQUE,
	CountryId integer NOT NULL,
	FoundationYear integer NOT NULL CHECK(FoundationYear > 0),
	CompanyValue bigint NOT NULL CHECK(CompanyValue > 0),
	CONSTRAINT Brand_pk PRIMARY KEY (Id)
);

CREATE TABLE Country (
	Id INT identity(1,1) NOT NULL,
	Name character varying(30) NOT NULL UNIQUE,
	CONSTRAINT Country_pk PRIMARY KEY (Id)
);

CREATE TABLE Users (
	Id INT identity(1,1) NOT NULL,
	Username character varying(30) NOT NULL,
	PasswordHash CHAR(64) NOT NULL,
	UserStatus INT NOT NULL,
	Blocked bit NOT NULL DEFAULT 0

	CONSTRAINT Users_pk PRIMARY KEY (Id),
	CONSTRAINT Username_un UNIQUE(Username)   
)

CREATE TABLE UserRole (
	Id INT identity(1,1) NOT NULL,
	Name character varying(30) NOT NULL
	CONSTRAINT UserRole_pk PRIMARY KEY (Id),
)

CREATE TABLE AppPassword (
	Password character varying(30) NOT NULL
)

ALTER TABLE Users ADD CONSTRAINT "Users_fk0" FOREIGN KEY (UserStatus) REFERENCES UserRole(Id);
ALTER TABLE Model ADD CONSTRAINT "Model_fk0" FOREIGN KEY (BrandId) REFERENCES Brand(Id);
ALTER TABLE Brand ADD CONSTRAINT "Brand_fk0" FOREIGN KEY (CountryId) REFERENCES Country(Id);
ALTER TABLE Model ADD CONSTRAINT "Model_fk1" FOREIGN KEY (DriveId) REFERENCES Drive(Id);


CREATE FULLTEXT CATALOG FTS_Catalog WITH ACCENT_SENSITIVITY = ON
CREATE FULLTEXT INDEX ON Model(Name LANGUAGE 1033, CarBody LANGUAGE 1033) KEY INDEX Model_pk ON FTS_Catalog
CREATE FULLTEXT INDEX ON Brand(BrandName LANGUAGE 1033) KEY INDEX Brand_pk ON FTS_Catalog

CREATE TABLE Logs
(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	ChangeDate DATETIME DEFAULT GETDATE() NOT NULL,
	Command NCHAR(6) NOT NULL,
	ChangedTable NCHAR(100) NOT NULL,
	ChangedItemName NCHAR(100) NOT NULL,
	UserName NCHAR(100) NOT NULL
)

CREATE TRIGGER Users_change
ON Users
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
	DECLARE @operation CHAR(6)
		SET @operation = CASE
				WHEN EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
					THEN 'Update'
				WHEN EXISTS(SELECT * FROM inserted)
					THEN 'Insert'
				WHEN EXISTS(SELECT * FROM deleted)
					THEN 'Delete'
				ELSE NULL
		END
	IF @operation = 'Delete'
			INSERT INTO Logs (Command, ChangeDate,ChangedTable,ChangedItemName, UserName)
			SELECT @operation, GETDATE(),'Users', d.Username, USER_Name()
			FROM deleted d
 
	IF @operation = 'Insert'
			INSERT INTO Logs (Command, ChangeDate, ChangedTable,ChangedItemName, UserName)
			SELECT @operation, GETDATE(), 'Users', i.Username,  USER_Name()
			FROM inserted i
 
	IF @operation = 'Update'
			INSERT INTO Logs (Command, ChangeDate, ChangedTable,ChangedItemName, UserName)
			SELECT @operation, GETDATE(),'Users', i.Username, USER_Name()
			FROM deleted d, inserted i
END

CREATE TRIGGER Model_change
ON Model
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
	DECLARE @operation CHAR(6)
		SET @operation = CASE
				WHEN EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
					THEN 'Update'
				WHEN EXISTS(SELECT * FROM inserted)
					THEN 'Insert'
				WHEN EXISTS(SELECT * FROM deleted)
					THEN 'Delete'
				ELSE NULL
		END
	IF @operation = 'Delete'
			INSERT INTO Logs (Command, ChangeDate,ChangedTable,ChangedItemName, UserName)
			SELECT @operation, GETDATE(),'Model', d.Name, USER_Name()
			FROM deleted d
 
	IF @operation = 'Insert'
			INSERT INTO Logs (Command, ChangeDate, ChangedTable,ChangedItemName, UserName)
			SELECT @operation, GETDATE(), 'Model', i.Name,  USER_Name()
			FROM inserted i
 
	IF @operation = 'Update'
			INSERT INTO Logs (Command, ChangeDate, ChangedTable,ChangedItemName, UserName)
			SELECT @operation, GETDATE(),'Model', i.Name, USER_Name()
			FROM deleted d, inserted i
END

CREATE TRIGGER Brand_change
ON Brand
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
	DECLARE @operation CHAR(6)
		SET @operation = CASE
				WHEN EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
					THEN 'Update'
				WHEN EXISTS(SELECT * FROM inserted)
					THEN 'Insert'
				WHEN EXISTS(SELECT * FROM deleted)
					THEN 'Delete'
				ELSE NULL
		END
	IF @operation = 'Delete'
			INSERT INTO Logs (Command, ChangeDate,ChangedTable,ChangedItemName, UserName)
			SELECT @operation, GETDATE(),'Brand', d.BrandName, USER_Name()
			FROM deleted d
 
	IF @operation = 'Insert'
			INSERT INTO Logs (Command, ChangeDate, ChangedTable,ChangedItemName, UserName)
			SELECT @operation, GETDATE(), 'Brand', i.BrandName,  USER_Name()
			FROM inserted i
 
	IF @operation = 'Update'
			INSERT INTO Logs (Command, ChangeDate, ChangedTable,ChangedItemName, UserName)
			SELECT @operation, GETDATE(),'Brand', i.BrandName, USER_Name()
			FROM deleted d, inserted i
END

CREATE TRIGGER Country_change
ON Country
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
	DECLARE @operation CHAR(6)
		SET @operation = CASE
				WHEN EXISTS(SELECT * FROM inserted) AND EXISTS(SELECT * FROM deleted)
					THEN 'Update'
				WHEN EXISTS(SELECT * FROM inserted)
					THEN 'Insert'
				WHEN EXISTS(SELECT * FROM deleted)
					THEN 'Delete'
				ELSE NULL
		END
	IF @operation = 'Delete'
			INSERT INTO Logs (Command, ChangeDate,ChangedTable,ChangedItemName, UserName)
			SELECT @operation, GETDATE(),'Country', d.Name, USER_Name()
			FROM deleted d
 
	IF @operation = 'Insert'
			INSERT INTO Logs (Command, ChangeDate, ChangedTable,ChangedItemName, UserName)
			SELECT @operation, GETDATE(), 'Country', i.Name,  USER_Name()
			FROM inserted i
 
	IF @operation = 'Update'
			INSERT INTO Logs (Command, ChangeDate, ChangedTable,ChangedItemName, UserName)
			SELECT @operation, GETDATE(),'Country', i.Name, USER_Name()
			FROM deleted d, inserted i
END
