CREATE TABLE [dbo].[RefreshTokens] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [Created]         DATETIME2 (7)  NOT NULL,
    [Modified]        DATETIME2 (7)  NOT NULL,
    [Token]           NVARCHAR (MAX) NULL,
    [Expires]         DATETIME2 (7)  NOT NULL,
    [UserId]          INT            NOT NULL,
    [RemoteIpAddress] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id])
);

