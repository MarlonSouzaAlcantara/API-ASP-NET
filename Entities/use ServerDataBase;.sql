use ServerDataBase;
go

select * from Jogos;



-- Create the table in the specified schema
CREATE TABLE Jogos
(
    Id UNIQUEIDENTIFIER NOT NULL PRIMARY KEY default NEWID(), -- primary key column
    Nome [NVARCHAR](50) NOT NULL,
    Produtora [NVARCHAR](50) NOT NULL,
    Preco FLOAT NOT NULL 
);
GO

