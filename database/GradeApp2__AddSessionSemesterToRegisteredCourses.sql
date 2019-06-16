ALTER TABLE [RegisteredCourses] ADD [SessionSemesterId] bigint NOT NULL DEFAULT CAST(0 AS bigint);

GO

CREATE TABLE [RegisteredCourseGrades] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [RegisteredCourseId] bigint NOT NULL,
    [Attendance] int NOT NULL,
    [Quiz1] int NOT NULL,
    [Quiz2] int NOT NULL,
    [Assignment1] int NOT NULL,
    [Assignment2] int NOT NULL,
    [MidSemester] int NOT NULL,
    [Project] int NOT NULL,
    [Exam] int NOT NULL,
    [TotalCa] int NOT NULL,
    [FinalScore] int NOT NULL,
    [Grade] int NOT NULL,
    CONSTRAINT [PK_RegisteredCourseGrades] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RegisteredCourseGrades_RegisteredCourses_RegisteredCourseId] FOREIGN KEY ([RegisteredCourseId]) REFERENCES [RegisteredCourses] ([Id]) ON DELETE CASCADE
);

GO

CREATE INDEX [IX_RegisteredCourses_SessionSemesterId] ON [RegisteredCourses] ([SessionSemesterId]);

GO

CREATE INDEX [IX_RegisteredCourseGrades_RegisteredCourseId] ON [RegisteredCourseGrades] ([RegisteredCourseId]);

GO

ALTER TABLE [RegisteredCourses] ADD CONSTRAINT [FK_RegisteredCourses_SessionSemesters_SessionSemesterId] FOREIGN KEY ([SessionSemesterId]) REFERENCES [SessionSemesters] ([Id]) ON DELETE NO ACTION;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190616164608_AddSessionSemesterToRegisteredCourses', N'2.2.4-servicing-10062');

GO
