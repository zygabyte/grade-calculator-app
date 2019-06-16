IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Courses] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [Code] nvarchar(max) NULL,
    [CreditUnit] int NOT NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Schools] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [Code] nvarchar(max) NULL,
    CONSTRAINT [PK_Schools] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Semesters] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [Code] nvarchar(max) NULL,
    CONSTRAINT [PK_Semesters] PRIMARY KEY ([Id])
);

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

CREATE TABLE [Departments] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [Code] nvarchar(max) NULL,
    [SchoolId] bigint NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Departments_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [SessionSemesters] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [SemesterId] bigint NOT NULL,
    [SessionId] bigint NOT NULL,
    [SemesterStartDate] datetime2 NOT NULL,
    [SemesterEndDate] datetime2 NOT NULL,
    [IsCurrent] bit NOT NULL,
    CONSTRAINT [PK_SessionSemesters] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SessionSemesters_Semesters_SemesterId] FOREIGN KEY ([SemesterId]) REFERENCES [Semesters] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SessionSemesters_Sessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [Sessions] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Lecturers] (
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
    [DepartmentId] bigint NOT NULL,
    CONSTRAINT [PK_Lecturers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Lecturers_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Programmes] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [Code] nvarchar(max) NULL,
    [DepartmentId] bigint NOT NULL,
    CONSTRAINT [PK_Programmes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Programmes_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [SessionSemesterCourses] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [SessionSemesterId] bigint NOT NULL,
    [CourseId] bigint NOT NULL,
    CONSTRAINT [PK_SessionSemesterCourses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SessionSemesterCourses_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_SessionSemesterCourses_SessionSemesters_SessionSemesterId] FOREIGN KEY ([SessionSemesterId]) REFERENCES [SessionSemesters] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [LecturerCourses] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [LecturerId] bigint NOT NULL,
    [CourseId] bigint NOT NULL,
    CONSTRAINT [PK_LecturerCourses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LecturerCourses_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_LecturerCourses_Lecturers_LecturerId] FOREIGN KEY ([LecturerId]) REFERENCES [Lecturers] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [ProgrammeCourses] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [ProgrammeId] bigint NOT NULL,
    [CourseId] bigint NOT NULL,
    CONSTRAINT [PK_ProgrammeCourses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProgrammeCourses_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProgrammeCourses_Programmes_ProgrammeId] FOREIGN KEY ([ProgrammeId]) REFERENCES [Programmes] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [Students] (
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
    [MatricNumber] nvarchar(max) NULL,
    [ProgrammeId] bigint NOT NULL,
    CONSTRAINT [PK_Students] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Students_Programmes_ProgrammeId] FOREIGN KEY ([ProgrammeId]) REFERENCES [Programmes] ([Id]) ON DELETE NO ACTION
);

GO

CREATE TABLE [RegisteredCourses] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [StudentId] bigint NOT NULL,
    [LecturerId] bigint NOT NULL,
    [CourseId] bigint NOT NULL,
    CONSTRAINT [PK_RegisteredCourses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_RegisteredCourses_Courses_CourseId] FOREIGN KEY ([CourseId]) REFERENCES [Courses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RegisteredCourses_Lecturers_LecturerId] FOREIGN KEY ([LecturerId]) REFERENCES [Lecturers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_RegisteredCourses_Students_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Students] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Departments_SchoolId] ON [Departments] ([SchoolId]);

GO

CREATE INDEX [IX_LecturerCourses_CourseId] ON [LecturerCourses] ([CourseId]);

GO

CREATE INDEX [IX_LecturerCourses_LecturerId] ON [LecturerCourses] ([LecturerId]);

GO

CREATE INDEX [IX_Lecturers_DepartmentId] ON [Lecturers] ([DepartmentId]);

GO

CREATE INDEX [IX_ProgrammeCourses_CourseId] ON [ProgrammeCourses] ([CourseId]);

GO

CREATE INDEX [IX_ProgrammeCourses_ProgrammeId] ON [ProgrammeCourses] ([ProgrammeId]);

GO

CREATE INDEX [IX_Programmes_DepartmentId] ON [Programmes] ([DepartmentId]);

GO

CREATE INDEX [IX_RegisteredCourses_CourseId] ON [RegisteredCourses] ([CourseId]);

GO

CREATE INDEX [IX_RegisteredCourses_LecturerId] ON [RegisteredCourses] ([LecturerId]);

GO

CREATE INDEX [IX_RegisteredCourses_StudentId] ON [RegisteredCourses] ([StudentId]);

GO

CREATE INDEX [IX_SessionSemesterCourses_CourseId] ON [SessionSemesterCourses] ([CourseId]);

GO

CREATE INDEX [IX_SessionSemesterCourses_SessionSemesterId] ON [SessionSemesterCourses] ([SessionSemesterId]);

GO

CREATE INDEX [IX_SessionSemesters_SemesterId] ON [SessionSemesters] ([SemesterId]);

GO

CREATE INDEX [IX_SessionSemesters_SessionId] ON [SessionSemesters] ([SessionId]);

GO

CREATE INDEX [IX_Students_ProgrammeId] ON [Students] ([ProgrammeId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190616030832_InitialModel', N'2.2.4-servicing-10062');

GO
