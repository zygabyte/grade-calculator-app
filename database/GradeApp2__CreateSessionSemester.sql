ALTER TABLE [Courses] DROP CONSTRAINT [FK_Courses_Sessions_SessionId];

GO

ALTER TABLE [SessionCourses] DROP CONSTRAINT [FK_SessionCourses_Sessions_SessionId];

GO

DROP TABLE [Sessions];

GO

DROP INDEX [IX_SessionCourses_SessionId] ON [SessionCourses];

GO

EXEC sp_rename N'[Courses].[SessionId]', N'SessionSemesterId', N'COLUMN';

GO

EXEC sp_rename N'[Courses].[IX_Courses_SessionId]', N'IX_Courses_SessionSemesterId', N'INDEX';

GO

ALTER TABLE [SessionCourses] ADD [SessionSemesterId] bigint NULL;

GO

CREATE TABLE [SessionSemesters] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [SemesterId] bigint NOT NULL,
    [SemesterStartDate] datetime2 NOT NULL,
    [SemesterEndDate] datetime2 NOT NULL,
    [IsCurrent] bit NOT NULL,
    CONSTRAINT [PK_SessionSemesters] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SessionSemesters_Semesters_SemesterId] FOREIGN KEY ([SemesterId]) REFERENCES [Semesters] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_SessionCourses_SessionSemesterId] ON [SessionCourses] ([SessionSemesterId]);

GO

CREATE INDEX [IX_SessionSemesters_SemesterId] ON [SessionSemesters] ([SemesterId]);

GO

ALTER TABLE [Courses] ADD CONSTRAINT [FK_Courses_SessionSemesters_SessionSemesterId] FOREIGN KEY ([SessionSemesterId]) REFERENCES [SessionSemesters] ([Id]) ON DELETE NO ACTION;

GO

ALTER TABLE [SessionCourses] ADD CONSTRAINT [FK_SessionCourses_SessionSemesters_SessionSemesterId] FOREIGN KEY ([SessionSemesterId]) REFERENCES [SessionSemesters] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190606154133_ChangeSessionToSessions', N'2.2.4-servicing-10062');

GO

