CREATE TABLE [usr].[LOB] (
    [Id]          INT           IDENTITY (1, 1) NOT NULL,
    [Description] VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_LOB] PRIMARY KEY CLUSTERED ([Id] ASC)
);

