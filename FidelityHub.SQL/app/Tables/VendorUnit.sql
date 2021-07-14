CREATE TABLE [app].[VendorUnit] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Email]        VARCHAR (MAX) NULL,
    [AddressId]    INT           NOT NULL,
    [Phone]        VARCHAR (30)  NOT NULL,
    [Mobile]       VARCHAR (30)  NULL,
    [Description]  VARCHAR (MAX) NULL,
    [Website]      VARCHAR (MAX) NULL,
    [OpeningHours] INT           NULL,
    [ClosingHours] INT           NULL,
    [VendorId]     INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([AddressId]) REFERENCES [usr].[Address] ([Id]),
    FOREIGN KEY ([VendorId]) REFERENCES [usr].[Vendor] ([Id])
);

