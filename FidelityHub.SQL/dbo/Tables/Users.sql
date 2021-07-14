CREATE TABLE [dbo].[Users] (
    [Id]         INT            IDENTITY (1, 1) NOT NULL,
    [Created]    DATETIME2 (7)  NOT NULL,
    [Modified]   DATETIME2 (7)  NOT NULL,
    [FirstName]  NVARCHAR (MAX) NULL,
    [LastName]   NVARCHAR (MAX) NULL,
    [IdentityId] NVARCHAR (MAX) NULL,
    [UserName]   NVARCHAR (MAX) NULL,
    [UserTypeId] INT            NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);

