CREATE TABLE [dbo].[Persons] (
    [PersonID]    INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (100) NULL,
    [Surname]     NVARCHAR (100) NOT NULL,
    [BirthNumber] BIGINT         NULL,
    [Address]     NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([PersonID] ASC)
);

