ALTER TABLE [Courses] DROP CONSTRAINT [FK_Courses_Lecturers_LecturerId];

GO

ALTER TABLE [Courses] DROP CONSTRAINT [FK_Courses_Programmes_ProgrammeId];

GO

ALTER TABLE [Courses] DROP CONSTRAINT [FK_Courses_SessionSemesters_SessionSemesterId];

GO

DROP INDEX [IX_Courses_LecturerId] ON [Courses];

GO

DROP INDEX [IX_Courses_ProgrammeId] ON [Courses];

GO

DROP INDEX [IX_Courses_SessionSemesterId] ON [Courses];

GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Courses]') AND [c].[name] = N'LecturerId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Courses] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Courses] DROP COLUMN [LecturerId];

GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Courses]') AND [c].[name] = N'ProgrammeId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Courses] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Courses] DROP COLUMN [ProgrammeId];

GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Courses]') AND [c].[name] = N'SessionSemesterId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Courses] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Courses] DROP COLUMN [SessionSemesterId];

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190609162440_RemoveUnecessaryCourses', N'2.2.4-servicing-10062');

GO
