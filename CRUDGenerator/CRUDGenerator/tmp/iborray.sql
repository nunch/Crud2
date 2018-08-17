
go
CREATE TABLE [dbo].[Utilisateur](
    [Id_Utilisateur] INT IDENTITY(1, 1) NOT NULL,
    [IsSuppr] BIT NOT NULL
);

ALTER TABLE [Utilisateur] ADD CONSTRAINT pk_Utilisateur PRIMARY KEY ([Id_Utilisateur]);
go




go
CREATE TABLE [dbo].[Rapport](
    [Id_Rapport] INT IDENTITY(1, 1) NOT NULL,
    [IsSuppr] BIT NOT NULL,
    [Nom] VARCHAR(MAX) NOT NULL,
    [Commentaire] VARCHAR(MAX) NOT NULL,
    [Id_Utilisateur] INT NOT NULL,
    [Type] INT NOT NULL,
    [EnvoiMail] BIT NOT NULL,
    [PeriodiciteMail] INT NOT NULL
);

ALTER TABLE [Rapport] ADD CONSTRAINT pk_Rapport PRIMARY KEY ([Id_Rapport]);
go




go
CREATE TABLE [dbo].[Colonne](
    [Id_Colonne] INT IDENTITY(1, 1) NOT NULL,
    [IsSuppr] BIT NOT NULL,
    [Encart] INT NOT NULL,
    [NomColonne] VARCHAR(MAX) NOT NULL,
    [Id_Rapport] INT NOT NULL
);

ALTER TABLE [Colonne] ADD CONSTRAINT pk_Colonne PRIMARY KEY ([Id_Colonne]);
go




go
CREATE TABLE [dbo].[Condition](
    [Id_Condition] INT IDENTITY(1, 1) NOT NULL,
    [IsSuppr] BIT NOT NULL,
    [Scope] INT NOT NULL,
    [Encart] INT ,
    [NomColonne] VARCHAR(MAX) ,
    [Operateur] INT NOT NULL,
    [Valeur] VARCHAR(MAX) NOT NULL,
    [IsAnd] BIT NOT NULL,
    [Id_Rapport] INT NOT NULL
);

ALTER TABLE [Condition] ADD CONSTRAINT pk_Condition PRIMARY KEY ([Id_Condition]);
go







ALTER TABLE [Rapport]
    ADD CONSTRAINT fk_Rapport_0
    FOREIGN KEY ([Id_Utilisateur])
    REFERENCES [Utilisateur]([Id_Utilisateur]);



ALTER TABLE [Colonne]
    ADD CONSTRAINT fk_Colonne_0
    FOREIGN KEY ([Id_Rapport])
    REFERENCES [Rapport]([Id_Rapport]);



ALTER TABLE [Condition]
    ADD CONSTRAINT fk_Condition_0
    FOREIGN KEY ([Id_Rapport])
    REFERENCES [Rapport]([Id_Rapport]);