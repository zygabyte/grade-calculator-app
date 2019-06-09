EXEC sp_rename N'[Courses].[Title]', N'Name', N'COLUMN';

GO

ALTER TABLE [Schools] ADD [Code] nvarchar(max) NULL;

GO

ALTER TABLE [Programmes] ADD [Code] nvarchar(max) NULL;

GO

ALTER TABLE [Departments] ADD [Code] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190609165520_NameCodeUnit', N'2.2.4-servicing-10062');

GO
