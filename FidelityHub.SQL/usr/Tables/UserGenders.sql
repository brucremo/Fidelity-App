CREATE TABLE [usr].[UserGenders] (
    [UserGenderId]      INT         NOT NULL,
    [GenderDescription] NCHAR (256) NOT NULL,
    CONSTRAINT [PK_UserGenders] PRIMARY KEY CLUSTERED ([UserGenderId] ASC)
);

