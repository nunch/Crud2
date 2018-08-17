
CREATE TABLE [ProgrammeMoteur](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [Nom] VARCHAR(50) NOT NULL,
    [IsVisible] BIT NOT NULL,
    [IsSuppr] BIT NOT NULL
);
ALTER TABLE [ProgrammeMoteur] ADD CONSTRAINT pk_ProgrammeMoteur PRIMARY KEY ([Id]);




CREATE TABLE [Module](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [Nom] VARCHAR(50) NOT NULL,
    [IdProgrammeMoteur] INT NOT NULL,
    [IsVisible] BIT NOT NULL,
    [IsSuppr] BIT NOT NULL
);
ALTER TABLE [Module] ADD CONSTRAINT pk_Module PRIMARY KEY ([Id]);
ALTER TABLE [Module]
    ADD CONSTRAINT fk_Module_0
    FOREIGN KEY ([IdProgrammeMoteur])
    REFERENCES [ProgrammeMoteur]([Id]);




CREATE TABLE [Moteur](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [Nom] VARCHAR(50) NOT NULL,
    [NumOF] VARCHAR(10) NOT NULL,
    [Etat] INT NOT NULL,
    [DateCreation] DATETIME NOT NULL,
    [Charge] INT NOT NULL,
    [IdProgrammeMoteur] INT NOT NULL,
    [IsSuppr] BIT NOT NULL,
    [IsVisible] BIT NOT NULL
);
ALTER TABLE [Moteur] ADD CONSTRAINT pk_Moteur PRIMARY KEY ([Id]);
ALTER TABLE [Moteur]
    ADD CONSTRAINT fk_Moteur_0
    FOREIGN KEY ([IdProgrammeMoteur])
    REFERENCES [ProgrammeMoteur]([Id]);




CREATE TABLE [Utilisateur](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [Matricule] VARCHAR(10) NOT NULL,
    [Nom] VARCHAR(50) NOT NULL,
    [Prenom] VARCHAR(50) NOT NULL,
    [Mail] VARCHAR(100) NOT NULL,
    [Departement] VARCHAR(10) NOT NULL,
    [Role] INT NOT NULL
);
ALTER TABLE [Utilisateur] ADD CONSTRAINT pk_Utilisateur PRIMARY KEY ([Id]);




CREATE TABLE [Activite](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [IdMoteur] INT NOT NULL,
    [IdModule] INT NOT NULL,
    [TypeActivite] SMALLINT NOT NULL,
    [Priorite] SMALLINT NOT NULL,
    [Etat] SMALLINT NOT NULL,
    [DateCreation] DATETIME NOT NULL,
    [UTTotalPlanifie] DECIMAL(6, 2) NOT NULL,
    [UTTotalConsomme] DECIMAL(6, 2) NOT NULL,
    [DateSolde] DATETIME NOT NULL,
    [IsSuppr] BIT NOT NULL,
    [IsVisible] BIT NOT NULL
);
ALTER TABLE [Activite] ADD CONSTRAINT pk_Activite PRIMARY KEY ([Id]);
ALTER TABLE [Activite]
    ADD CONSTRAINT fk_Activite_0
    FOREIGN KEY ([IdModule])
    REFERENCES [Module]([Id]);
ALTER TABLE [Activite]
    ADD CONSTRAINT fk_Activite_1
    FOREIGN KEY ([IdMoteur])
    REFERENCES [Moteur]([Id]);




CREATE TABLE [PeriodeActivite](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [IdActivite] INT NOT NULL,
    [DateDebut] DATE NOT NULL,
    [DateFin] DATE NOT NULL,
    [UTConsomme] DECIMAL(6, 2) NOT NULL,
    [Etat] INT NOT NULL,
    [UTPlanifie] DECIMAL(6, 2) NOT NULL
);
ALTER TABLE [PeriodeActivite] ADD CONSTRAINT pk_PeriodeActivite PRIMARY KEY ([Id]);
ALTER TABLE [PeriodeActivite]
    ADD CONSTRAINT fk_PeriodeActivite_0
    FOREIGN KEY ([IdActivite])
    REFERENCES [Activite]([Id]);




CREATE TABLE [Consigne](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [IdActivite] INT NOT NULL,
    [NomConsigne] VARCHAR(MAX) NOT NULL,
    [DateCreation] DATETIME NOT NULL
);
ALTER TABLE [Consigne] ADD CONSTRAINT pk_Consigne PRIMARY KEY ([Id]);
ALTER TABLE [Consigne]
    ADD CONSTRAINT fk_Consigne_0
    FOREIGN KEY ([IdActivite])
    REFERENCES [Activite]([Id]);




CREATE TABLE [Operateur](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [Matricule] VARCHAR(10) NOT NULL,
    [Nom] VARCHAR(30) NOT NULL,
    [Prenom] VARCHAR(30) NOT NULL,
    [Departement] VARCHAR(10) NOT NULL,
    [Equipe] SMALLINT NOT NULL,
    [TypeActivite] SMALLINT NOT NULL
);
ALTER TABLE [Operateur] ADD CONSTRAINT pk_Operateur PRIMARY KEY ([Id]);




CREATE TABLE [RHP](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [WeekNumber] SMALLINT NOT NULL,
    [Date] DATE NOT NULL,
    [Annee] INT NOT NULL
);
ALTER TABLE [RHP] ADD CONSTRAINT pk_RHP PRIMARY KEY ([Id]);




CREATE TABLE [ResponsableMoteur](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [IdMoteur] INT NOT NULL,
    [IdUtilisateur] INT NOT NULL,
    [TypeResponsable] INT NOT NULL
);
ALTER TABLE [ResponsableMoteur] ADD CONSTRAINT pk_ResponsableMoteur PRIMARY KEY ([Id]);
ALTER TABLE [ResponsableMoteur]
    ADD CONSTRAINT fk_ResponsableMoteur_0
    FOREIGN KEY ([IdMoteur])
    REFERENCES [Moteur]([Id]);
ALTER TABLE [ResponsableMoteur]
    ADD CONSTRAINT fk_ResponsableMoteur_1
    FOREIGN KEY ([IdUtilisateur])
    REFERENCES [Utilisateur]([Id]);




CREATE TABLE [Calendrier](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [TypeCalendrier] SMALLINT NOT NULL,
    [IdOperateur] INT NOT NULL,
    [Date] DATE NOT NULL
);
ALTER TABLE [Calendrier] ADD CONSTRAINT pk_Calendrier PRIMARY KEY ([Id]);
ALTER TABLE [Calendrier]
    ADD CONSTRAINT fk_Calendrier_0
    FOREIGN KEY ([IdOperateur])
    REFERENCES [Operateur]([Id]);




CREATE TABLE [Affectation](
    [Id] INT IDENTITY(1, 1) NOT NULL,
    [IdPeriodeActivite] INT NOT NULL,
    [IdOperateur] INT NOT NULL,
    [DateJourAffecte] DATE NOT NULL,
    [UTAffectation] DECIMAL(6, 2) NOT NULL
);
ALTER TABLE [Affectation] ADD CONSTRAINT pk_Affectation PRIMARY KEY ([Id]);
ALTER TABLE [Affectation]
    ADD CONSTRAINT fk_Affectation_0
    FOREIGN KEY ([IdPeriodeActivite])
    REFERENCES [PeriodeActivite]([Id]);
ALTER TABLE [Affectation]
    ADD CONSTRAINT fk_Affectation_1
    FOREIGN KEY ([IdOperateur])
    REFERENCES [Operateur]([Id]);
