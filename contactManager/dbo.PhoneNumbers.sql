CREATE TABLE [dbo].[PhoneNumbers] (
    [PhoneNumberID] INT IDENTITY (1, 1) NOT NULL,
    [PNumber]       INT NULL,
    [PersonID]      INT NULL,
    PRIMARY KEY CLUSTERED ([PhoneNumberID] ASC),
    FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Persons] ([PersonID])
);

