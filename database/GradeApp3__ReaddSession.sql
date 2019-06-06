DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SessionSemesters]') AND [c].[name] = N'Name');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [SessionSemesters] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [SessionSemesters] DROP COLUMN [Name];

GO

ALTER TABLE [SessionSemesters] ADD [SessionId] bigint NOT NULL DEFAULT CAST(0 AS bigint);

GO

CREATE TABLE [Sessions] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [Code] nvarchar(max) NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY ([Id])
);

GO

CREATE INDEX [IX_SessionSemesters_SessionId] ON [SessionSemesters] ([SessionId]);

GO

ALTER TABLE [SessionSemesters] ADD CONSTRAINT [FK_SessionSemesters_Sessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [Sessions] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190606161809_ReaddSession', N'2.2.4-servicing-10062');

GO
