CREATE TABLE [ref].[SentContactRequests] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Timestamp] DATETIME      NOT NULL,
    [SentTo]    VARCHAR (MAX) NOT NULL,
    [Status]    VARCHAR (MAX) NULL,
    [Success]   BIT           NULL,
    CONSTRAINT [PK_SentContactRequests] PRIMARY KEY CLUSTERED ([Id] ASC)
);

