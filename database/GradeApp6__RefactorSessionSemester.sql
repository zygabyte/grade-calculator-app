ALTER TABLE [Courses] DROP CONSTRAINT [FK_Courses_SessionCourses_SessionCourseId];

GO

ALTER TABLE [SessionCourses] DROP CONSTRAINT [FK_SessionCourses_SessionSemesters_SessionSemesterId];

GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SessionCourses]') AND [c].[name] = N'SessionId');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [SessionCourses] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [SessionCourses] DROP COLUMN [SessionId];

GO

EXEC sp_rename N'[Courses].[SessionCourseId]', N'SessionSemesterCourseId', N'COLUMN';

GO

EXEC sp_rename N'[Courses].[IX_Courses_SessionCourseId]', N'IX_Courses_SessionSemesterCourseId', N'INDEX';

GO

DROP INDEX [IX_SessionCourses_SessionSemesterId] ON [SessionCourses];
DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SessionCourses]') AND [c].[name] = N'SessionSemesterId');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [SessionCourses] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [SessionCourses] ALTER COLUMN [SessionSemesterId] bigint NOT NULL;
CREATE INDEX [IX_SessionCourses_SessionSemesterId] ON [SessionCourses] ([SessionSemesterId]);

GO

ALTER TABLE [Courses] ADD CONSTRAINT [FK_Courses_SessionCourses_SessionSemesterCourseId] FOREIGN KEY ([SessionSemesterCourseId]) REFERENCES [SessionCourses] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [SessionCourses] ADD CONSTRAINT [FK_SessionCourses_SessionSemesters_SessionSemesterId] FOREIGN KEY ([SessionSemesterId]) REFERENCES [SessionSemesters] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190610205144_RefactorSessionSemester', N'2.2.4-servicing-10062');

GO

ALTER TABLE [Courses] DROP CONSTRAINT [FK_Courses_SessionCourses_SessionSemesterCourseId];

GO

ALTER TABLE [SessionCourses] DROP CONSTRAINT [FK_SessionCourses_SessionSemesters_SessionSemesterId];

GO

ALTER TABLE [SessionCourses] DROP CONSTRAINT [PK_SessionCourses];

GO

EXEC sp_rename N'[SessionCourses]', N'SessionSemesterCourses';

GO

EXEC sp_rename N'[SessionSemesterCourses].[IX_SessionCourses_SessionSemesterId]', N'IX_SessionSemesterCourses_SessionSemesterId', N'INDEX';

GO

ALTER TABLE [SessionSemesterCourses] ADD CONSTRAINT [PK_SessionSemesterCourses] PRIMARY KEY ([Id]);

GO

ALTER TABLE [Courses] ADD CONSTRAINT [FK_Courses_SessionSemesterCourses_SessionSemesterCourseId] FOREIGN KEY ([SessionSemesterCourseId]) REFERENCES [SessionSemesterCourses] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [SessionSemesterCourses] ADD CONSTRAINT [FK_SessionSemesterCourses_SessionSemesters_SessionSemesterId] FOREIGN KEY ([SessionSemesterId]) REFERENCES [SessionSemesters] ([Id]) ON DELETE CASCADE;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190610213538_RefactorSessionSemesterContext', N'2.2.4-servicing-10062');

GO
