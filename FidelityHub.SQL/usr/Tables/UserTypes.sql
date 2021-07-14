CREATE TABLE [usr].[UserTypes] (
    [UserTypeId]      INT         NOT NULL,
    [TypeDescription] NCHAR (256) NOT NULL,
    CONSTRAINT [PK_UserTypes] PRIMARY KEY CLUSTERED ([UserTypeId] ASC)
);

