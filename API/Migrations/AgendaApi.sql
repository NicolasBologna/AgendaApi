IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250603134426_init'
)
BEGIN
    CREATE TABLE [Users] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [State] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250603134426_init'
)
BEGIN
    CREATE TABLE [Groups] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NULL,
        [OwnerId] int NOT NULL,
        CONSTRAINT [PK_Groups] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Groups_Users_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250603134426_init'
)
BEGIN
    CREATE TABLE [Contacts] (
        [Id] int NOT NULL IDENTITY,
        [FirstName] nvarchar(max) NOT NULL,
        [LastName] nvarchar(max) NOT NULL,
        [Address] nvarchar(max) NULL,
        [Number] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Image] nvarchar(max) NULL,
        [Company] nvarchar(max) NULL,
        [Description] nvarchar(max) NOT NULL,
        [UserId] int NOT NULL,
        [IsFavorite] bit NOT NULL,
        [GroupId] int NULL,
        CONSTRAINT [PK_Contacts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Contacts_Groups_GroupId] FOREIGN KEY ([GroupId]) REFERENCES [Groups] ([Id]),
        CONSTRAINT [FK_Contacts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250603134426_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'FirstName', N'LastName', N'Password', N'State') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] ON;
    EXEC(N'INSERT INTO [Users] ([Id], [Email], [FirstName], [LastName], [Password], [State])
    VALUES (1, N''karenbailapiola@gmail.com'', N''Karen'', N''Lasot'', N''Pa$$w0rd'', 0),
    (2, N''elluismidetotoras@gmail.com'', N''Luis Gonzalez'', N''Gonzales'', N''lamismadesiempre'', 0)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Email', N'FirstName', N'LastName', N'Password', N'State') AND [object_id] = OBJECT_ID(N'[Users]'))
        SET IDENTITY_INSERT [Users] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250603134426_init'
)
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Company', N'Description', N'Email', N'FirstName', N'GroupId', N'Image', N'IsFavorite', N'LastName', N'Number', N'UserId') AND [object_id] = OBJECT_ID(N'[Contacts]'))
        SET IDENTITY_INSERT [Contacts] ON;
    EXEC(N'INSERT INTO [Contacts] ([Id], [Address], [Company], [Description], [Email], [FirstName], [GroupId], [Image], [IsFavorite], [LastName], [Number], [UserId])
    VALUES (1, NULL, N''PwC'', N''Plomero'', N''jpreze@pwc.com'', N''Jaimito'', NULL, NULL, CAST(0 AS bit), N''Perez'', N''341457896'', 1),
    (2, NULL, N''Austral'', N''Papa'', N''pramirez@austral.com'', N''Pepe'', NULL, NULL, CAST(0 AS bit), N''Ramirez'', N''34156978'', 2),
    (3, NULL, N''google'', N''Jefa'', N''mpaez@google.com'', N''Maria'', NULL, NULL, CAST(0 AS bit), N''paez'', N''341457896'', 1)');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Address', N'Company', N'Description', N'Email', N'FirstName', N'GroupId', N'Image', N'IsFavorite', N'LastName', N'Number', N'UserId') AND [object_id] = OBJECT_ID(N'[Contacts]'))
        SET IDENTITY_INSERT [Contacts] OFF;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250603134426_init'
)
BEGIN
    CREATE INDEX [IX_Contacts_GroupId] ON [Contacts] ([GroupId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250603134426_init'
)
BEGIN
    CREATE INDEX [IX_Contacts_UserId] ON [Contacts] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250603134426_init'
)
BEGIN
    CREATE INDEX [IX_Groups_OwnerId] ON [Groups] ([OwnerId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20250603134426_init'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20250603134426_init', N'8.0.11');
END;
GO

COMMIT;
GO

