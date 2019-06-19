CREATE TABLE [TokenUserMaps] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Token] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [UserRole] int NOT NULL,
    CONSTRAINT [PK_TokenUserMaps] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190619062642_AddTokenUserMap', N'2.2.4-servicing-10062');

GO