CREATE TABLE [dbo].[Users] (
    [Id]               INT           IDENTITY (1, 1) NOT NULL,
    [UserName]         VARCHAR (50)  NOT NULL,
    [Password]         VARCHAR (50)  NOT NULL,
    [ConnectionString] VARCHAR (MAX) NOT NULL,
    [Salt] NCHAR(10) NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

