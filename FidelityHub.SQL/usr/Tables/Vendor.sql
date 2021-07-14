CREATE TABLE [usr].[Vendor] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [LegalName]      VARCHAR (MAX)  NOT NULL,
    [GovernmentId]   VARCHAR (MAX)  NULL,
    [Email]          VARCHAR (MAX)  NOT NULL,
    [Phone]          VARCHAR (30)   NOT NULL,
    [Description]    VARCHAR (MAX)  NULL,
    [LOBId]          INT            NOT NULL,
    [SubscriptionId] INT            NOT NULL,
    [Mobile]         VARCHAR (30)   NULL,
    [AddressId]      INT            NOT NULL,
    [UserTypeId]     INT            NOT NULL,
    [UserName]       NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AddressId]) REFERENCES [usr].[Address] ([Id]),
    FOREIGN KEY ([SubscriptionId]) REFERENCES [usr].[Subscription] ([Id]),
    FOREIGN KEY ([UserTypeId]) REFERENCES [usr].[UserTypes] ([UserTypeId])
);

