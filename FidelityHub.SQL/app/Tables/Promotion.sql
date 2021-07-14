CREATE TABLE [app].[Promotion] (
    [Id]                 INT           IDENTITY (1, 1) NOT NULL,
    [VendorUnitId]       INT           NOT NULL,
    [StartDate]          DATETIME      NOT NULL,
    [EndDate]            DATETIME      NOT NULL,
    [PromotionTypeId]    INT           NOT NULL,
    [PromotionHourStart] INT           NULL,
    [PromotionHourEnd]   INT           NULL,
    [Description]        VARCHAR (MAX) NULL,
    [Active]             BIT           NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([PromotionTypeId]) REFERENCES [app].[PromotionType] ([Id]),
    FOREIGN KEY ([VendorUnitId]) REFERENCES [app].[VendorUnit] ([Id])
);

