CREATE TABLE [Administrators] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [PasswordHash] nvarchar(max) NULL,
    [UserRole] int NOT NULL,
    CONSTRAINT [PK_Administrators] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190620204930_AddAdministrators', N'2.2.4-servicing-10062');

GO
