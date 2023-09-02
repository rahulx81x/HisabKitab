Create DATABASE HisabKitabDB
USE HisabKitabDB
go

CREATE TABLE Users
( UserID INT IDENTITY CONSTRAINT pk_userId PRIMARY KEY,
UserName VARCHAR(30) NOT NULL UNIQUE,
UserPassword VARCHAR(15) NOT NULL
)
go

CREATE TABLE Transactions
( UserId INT NOT NULL FOREIGN KEY REFERENCES Users(UserId),
TranId INT IDENTITY CONSTRAINT pk_tranId PRIMARY KEY,
[Date] DATETIME NOT NULL,
Amount DECIMAL(12,2) NOT NULL,
[Type] CHAR NOT NULL CONSTRAINT chk_type check(type in ('C','D')), --C for credit and D for Debit
[Remarks] VARCHAR(100)
)
go
