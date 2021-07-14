CREATE TABLE [usr].[Subscription] (
    [Id]             INT           NOT NULL,
    [Description]    VARCHAR (MAX) NOT NULL,
    [RecurrenceDays] INT           NOT NULL,
    [Price]          MONEY         NULL,
    [Title]          VARCHAR (60)  NULL,
    [Active]         BIT           NULL,
    CONSTRAINT [PK_Subscription] PRIMARY KEY CLUSTERED ([Id] ASC)
);

