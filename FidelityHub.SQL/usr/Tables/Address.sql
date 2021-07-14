CREATE TABLE [usr].[Address] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [PostalCode]   VARCHAR (20)  NOT NULL,
    [StreetNumber] INT           NOT NULL,
    [Street]       VARCHAR (90)  NOT NULL,
    [Region]       VARCHAR (90)  NULL,
    [City]         VARCHAR (90)  NOT NULL,
    [State]        VARCHAR (90)  NOT NULL,
    [Country]      VARCHAR (90)  NOT NULL,
    [Complement]   VARCHAR (120) NULL,
    CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED ([Id] ASC)
);

