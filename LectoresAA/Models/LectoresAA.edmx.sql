
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/08/2023 14:52:10
-- Generated from EDMX file: C:\Users\solis\source\repos\LectoresAA\LectoresAA\Models\LectoresAA.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LectoresAABD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_TipoPublicacion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Publicaciones] DROP CONSTRAINT [FK_TipoPublicacion];
GO
IF OBJECT_ID(N'[dbo].[FK_PublicacionCapitulo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Capitulos] DROP CONSTRAINT [FK_PublicacionCapitulo];
GO
IF OBJECT_ID(N'[dbo].[FK_CapituloComentario]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Comentarios] DROP CONSTRAINT [FK_CapituloComentario];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Tipos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tipos];
GO
IF OBJECT_ID(N'[dbo].[Publicaciones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Publicaciones];
GO
IF OBJECT_ID(N'[dbo].[Comentarios]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comentarios];
GO
IF OBJECT_ID(N'[dbo].[Capitulos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Capitulos];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Tipos'
CREATE TABLE [dbo].[Tipos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Publicaciones'
CREATE TABLE [dbo].[Publicaciones] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Autor] nvarchar(max)  NOT NULL,
    [Titulo] nvarchar(max)  NOT NULL,
    [Descripcion] nvarchar(max)  NOT NULL,
    [Fecha] nvarchar(max)  NOT NULL,
    [User] nvarchar(max)  NOT NULL,
    [TipoId] int  NOT NULL
);
GO

-- Creating table 'Comentarios'
CREATE TABLE [dbo].[Comentarios] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [User] nvarchar(max)  NOT NULL,
    [Fecha] nvarchar(max)  NOT NULL,
    [Contenido] nvarchar(max)  NOT NULL,
    [CapituloId] int  NOT NULL
);
GO

-- Creating table 'Capitulos'
CREATE TABLE [dbo].[Capitulos] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Titulo] nvarchar(max)  NOT NULL,
    [Fecha] nvarchar(max)  NOT NULL,
    [Contenido] nvarchar(max)  NOT NULL,
    [PublicacionId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Tipos'
ALTER TABLE [dbo].[Tipos]
ADD CONSTRAINT [PK_Tipos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Publicaciones'
ALTER TABLE [dbo].[Publicaciones]
ADD CONSTRAINT [PK_Publicaciones]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Comentarios'
ALTER TABLE [dbo].[Comentarios]
ADD CONSTRAINT [PK_Comentarios]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Capitulos'
ALTER TABLE [dbo].[Capitulos]
ADD CONSTRAINT [PK_Capitulos]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TipoId] in table 'Publicaciones'
ALTER TABLE [dbo].[Publicaciones]
ADD CONSTRAINT [FK_TipoPublicacion]
    FOREIGN KEY ([TipoId])
    REFERENCES [dbo].[Tipos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TipoPublicacion'
CREATE INDEX [IX_FK_TipoPublicacion]
ON [dbo].[Publicaciones]
    ([TipoId]);
GO

-- Creating foreign key on [PublicacionId] in table 'Capitulos'
ALTER TABLE [dbo].[Capitulos]
ADD CONSTRAINT [FK_PublicacionCapitulo]
    FOREIGN KEY ([PublicacionId])
    REFERENCES [dbo].[Publicaciones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PublicacionCapitulo'
CREATE INDEX [IX_FK_PublicacionCapitulo]
ON [dbo].[Capitulos]
    ([PublicacionId]);
GO

-- Creating foreign key on [CapituloId] in table 'Comentarios'
ALTER TABLE [dbo].[Comentarios]
ADD CONSTRAINT [FK_CapituloComentario]
    FOREIGN KEY ([CapituloId])
    REFERENCES [dbo].[Capitulos]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CapituloComentario'
CREATE INDEX [IX_FK_CapituloComentario]
ON [dbo].[Comentarios]
    ([CapituloId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------