IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Schools] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
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

CREATE TABLE [Departments] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [SchoolId] bigint NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Departments_Schools_SchoolId] FOREIGN KEY ([SchoolId]) REFERENCES [Schools] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Sessions] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [SemesterId] bigint NOT NULL,
    [SemesterStartDate] datetime2 NOT NULL,
    [SemesterEndDate] datetime2 NOT NULL,
    CONSTRAINT [PK_Sessions] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Sessions_Semesters_SemesterId] FOREIGN KEY ([SemesterId]) REFERENCES [Semesters] ([Id]) ON DELETE CASCADE
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
    CONSTRAINT [FK_Lecturers_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Programmes] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Name] nvarchar(max) NULL,
    [DepartmentId] bigint NOT NULL,
    CONSTRAINT [PK_Programmes] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Programmes_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [SessionCourses] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [SessionId] bigint NOT NULL,
    CONSTRAINT [PK_SessionCourses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_SessionCourses_Sessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [Sessions] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [LecturerCourses] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [LecturerId] bigint NOT NULL,
    CONSTRAINT [PK_LecturerCourses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_LecturerCourses_Lecturers_LecturerId] FOREIGN KEY ([LecturerId]) REFERENCES [Lecturers] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [ProgrammeCourses] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [ProgrammeId] bigint NOT NULL,
    CONSTRAINT [PK_ProgrammeCourses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_ProgrammeCourses_Programmes_ProgrammeId] FOREIGN KEY ([ProgrammeId]) REFERENCES [Programmes] ([Id]) ON DELETE CASCADE
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
    CONSTRAINT [FK_Students_Programmes_ProgrammeId] FOREIGN KEY ([ProgrammeId]) REFERENCES [Programmes] ([Id]) ON DELETE CASCADE
);

GO

CREATE TABLE [Courses] (
    [Id] bigint NOT NULL IDENTITY,
    [Created] datetime2 NOT NULL,
    [Modified] datetime2 NULL,
    [IsDeleted] bit NOT NULL,
    [IsActive] bit NOT NULL,
    [Title] nvarchar(max) NULL,
    [Code] nvarchar(max) NULL,
    [CreditUnit] int NOT NULL,
    [LecturerCourseId] bigint NULL,
    [LecturerId] bigint NULL,
    [ProgrammeCourseId] bigint NULL,
    [ProgrammeId] bigint NULL,
    [SessionCourseId] bigint NULL,
    [SessionId] bigint NULL,
    CONSTRAINT [PK_Courses] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Courses_LecturerCourses_LecturerCourseId] FOREIGN KEY ([LecturerCourseId]) REFERENCES [LecturerCourses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Courses_Lecturers_LecturerId] FOREIGN KEY ([LecturerId]) REFERENCES [Lecturers] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Courses_ProgrammeCourses_ProgrammeCourseId] FOREIGN KEY ([ProgrammeCourseId]) REFERENCES [ProgrammeCourses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Courses_Programmes_ProgrammeId] FOREIGN KEY ([ProgrammeId]) REFERENCES [Programmes] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Courses_SessionCourses_SessionCourseId] FOREIGN KEY ([SessionCourseId]) REFERENCES [SessionCourses] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Courses_Sessions_SessionId] FOREIGN KEY ([SessionId]) REFERENCES [Sessions] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Courses_LecturerCourseId] ON [Courses] ([LecturerCourseId]);

GO

CREATE INDEX [IX_Courses_LecturerId] ON [Courses] ([LecturerId]);

GO

CREATE INDEX [IX_Courses_ProgrammeCourseId] ON [Courses] ([ProgrammeCourseId]);

GO

CREATE INDEX [IX_Courses_ProgrammeId] ON [Courses] ([ProgrammeId]);

GO

CREATE INDEX [IX_Courses_SessionCourseId] ON [Courses] ([SessionCourseId]);

GO

CREATE INDEX [IX_Courses_SessionId] ON [Courses] ([SessionId]);

GO

CREATE INDEX [IX_Departments_SchoolId] ON [Departments] ([SchoolId]);

GO

CREATE INDEX [IX_LecturerCourses_LecturerId] ON [LecturerCourses] ([LecturerId]);

GO

CREATE INDEX [IX_Lecturers_DepartmentId] ON [Lecturers] ([DepartmentId]);

GO

CREATE INDEX [IX_ProgrammeCourses_ProgrammeId] ON [ProgrammeCourses] ([ProgrammeId]);

GO

CREATE INDEX [IX_Programmes_DepartmentId] ON [Programmes] ([DepartmentId]);

GO

CREATE INDEX [IX_SessionCourses_SessionId] ON [SessionCourses] ([SessionId]);

GO

CREATE INDEX [IX_Sessions_SemesterId] ON [Sessions] ([SemesterId]);

GO

CREATE INDEX [IX_Students_ProgrammeId] ON [Students] ([ProgrammeId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20190529160944_InitialModel', N'2.2.4-servicing-10062');

GO

