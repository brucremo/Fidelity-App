CREATE TABLE [app].[PromotionSale] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [PromotionId] INT            NOT NULL,
    [UserId]      NVARCHAR (450) NOT NULL,
    [Timestamp]   DATETIME       NOT NULL,
    [Amount]      INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([PromotionId]) REFERENCES [app].[Promotion] ([Id]),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id])
);

