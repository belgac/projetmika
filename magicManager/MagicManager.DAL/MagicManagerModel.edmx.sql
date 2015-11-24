
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/19/2015 10:38:05
-- Generated from EDMX file: C:\Users\paris10\Desktop\MagicManagerData\MagicManager.DAL\MagicManagerModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [MagicManagerEntities];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[fk_articleDailyprice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[DailyPrice] DROP CONSTRAINT [fk_articleDailyprice];
GO
IF OBJECT_ID(N'[dbo].[FK_expansionProduct]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK_expansionProduct];
GO
IF OBJECT_ID(N'[dbo].[fk_gameExpansion]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Expansion] DROP CONSTRAINT [fk_gameExpansion];
GO
IF OBJECT_ID(N'[dbo].[FK_languageArticle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article] DROP CONSTRAINT [FK_languageArticle];
GO
IF OBJECT_ID(N'[dbo].[FK_productArticle]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Article] DROP CONSTRAINT [FK_productArticle];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Article]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Article];
GO
IF OBJECT_ID(N'[dbo].[DailyPrice]', 'U') IS NOT NULL
    DROP TABLE [dbo].[DailyPrice];
GO
IF OBJECT_ID(N'[dbo].[Expansion]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Expansion];
GO
IF OBJECT_ID(N'[dbo].[Game]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Game];
GO
IF OBJECT_ID(N'[dbo].[Lang]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Lang];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[WorkerAction]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkerAction];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Article'
CREATE TABLE [dbo].[Article] (
    [ArticleId] int NOT NULL,
    [LanguageId] int  NOT NULL,
    [isFoil] bit  NOT NULL,
    [isSigned] bit  NOT NULL,
    [isAltered] bit  NOT NULL,
    [isPlayset] bit  NOT NULL,
    [isFirstEd] bit  NOT NULL,
	[Price] float NULL,
	[Count] int NULL,
    [WorkerEditTime] datetime  NULL,
    [ProductId] int  NOT NULL
);
GO

-- Creating table 'DailyPrice'
CREATE TABLE [dbo].[DailyPrice] (
    [DailyPriceId] int IDENTITY(1, 1) NOT NULL,
    [Sell] float  NULL,
    [Low] float  NULL,
    [Average] float  NULL,
	[CountArticles] Int Null,
	[CountFoils] Int null,
    [LastEdited] datetime  NULL,
    [WorkerEditTime] datetime  NULL,
    [Productid] int  NOT NULL
);
GO

-- Creating table 'Expansion'
CREATE TABLE [dbo].[Expansion] (
    [ExpansionId] int NOT NULL,
    [Name] nvarchar(250)  NULL,
    [Icon] int  NULL,
    [WorkerEditTime] datetime  NULL,
    [GameId] int  NOT NULL
);
GO

-- Creating table 'Game'
CREATE TABLE [dbo].[Game] (
    [GameId] int NOT NULL,
    [Name] nvarchar(250)  NULL,
    [WorkerEditTime] datetime  NULL
);
GO

-- Creating table 'Lang'
CREATE TABLE [dbo].[Lang] (
    [LanguageId] int NOT NULL,
    [Name] nvarchar(250) NOT NULL,
    [WorkerEditTime] datetime  NULL
);
GO

-- Creating table 'Product'
CREATE TABLE [dbo].[Product] (
    [ProductId] int NOT NULL,
    [ProductName] nvarchar(250)  NULL,
    [ProductUrl] nvarchar(250)  NULL,
    [ImageUrl] nvarchar(250)  NULL,
    [Rarity] nvarchar(250)  NULL,
    [WorkerEditTime] datetime  NULL,
    [ExpansionId] int  NOT NULL
);
GO

-- Creating table 'WorkerAction'
CREATE TABLE [dbo].[WorkerAction] (
    [WorkerActionId] int NOT NULL,
    [Type] nvarchar(250)  NOT NULL,
    [Comment] nvarchar(250)  NULL,
    [Date] datetime  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ArticleId] in table 'Article'
ALTER TABLE [dbo].[Article]
ADD CONSTRAINT [PK_Article]
    PRIMARY KEY CLUSTERED ([ArticleId] ASC);
GO

-- Creating primary key on [DailyPriceId] in table 'DailyPrice'
ALTER TABLE [dbo].[DailyPrice]
ADD CONSTRAINT [PK_DailyPrice]
    PRIMARY KEY CLUSTERED ([DailyPriceId] ASC);
GO

-- Creating primary key on [ExpansionId] in table 'Expansion'
ALTER TABLE [dbo].[Expansion]
ADD CONSTRAINT [PK_Expansion]
    PRIMARY KEY CLUSTERED ([ExpansionId] ASC);
GO

-- Creating primary key on [GameId] in table 'Game'
ALTER TABLE [dbo].[Game]
ADD CONSTRAINT [PK_Game]
    PRIMARY KEY CLUSTERED ([GameId] ASC);
GO

-- Creating primary key on [LanguageId] in table 'Lang'
ALTER TABLE [dbo].[Lang]
ADD CONSTRAINT [PK_Lang]
    PRIMARY KEY CLUSTERED ([LanguageId] ASC);
GO

-- Creating primary key on [ProductId] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [PK_Product]
    PRIMARY KEY CLUSTERED ([ProductId] ASC);
GO

-- Creating primary key on [WorkerActionId] in table 'WorkerAction'
ALTER TABLE [dbo].[WorkerAction]
ADD CONSTRAINT [PK_WorkerAction]
    PRIMARY KEY CLUSTERED ([WorkerActionId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Articleid] in table 'DailyPrice'
ALTER TABLE [dbo].[DailyPrice]
ADD CONSTRAINT [fk_productDailyprice]
    FOREIGN KEY ([Productid])
    REFERENCES [dbo].[Product]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_articleDailyprice'
CREATE INDEX [IX_fk_productDailyprice]
ON [dbo].[DailyPrice]
    ([Productid]);
GO

-- Creating foreign key on [LanguageId] in table 'Article'
ALTER TABLE [dbo].[Article]
ADD CONSTRAINT [FK_languageArticle]
    FOREIGN KEY ([LanguageId])
    REFERENCES [dbo].[Lang]
        ([LanguageId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_languageArticle'
CREATE INDEX [IX_FK_languageArticle]
ON [dbo].[Article]
    ([LanguageId]);
GO

-- Creating foreign key on [ProductId] in table 'Article'
ALTER TABLE [dbo].[Article]
ADD CONSTRAINT [FK_productArticle]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Product]
        ([ProductId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_productArticle'
CREATE INDEX [IX_FK_productArticle]
ON [dbo].[Article]
    ([ProductId]);
GO

-- Creating foreign key on [ExpansionId] in table 'Product'
ALTER TABLE [dbo].[Product]
ADD CONSTRAINT [FK_expansionProduct]
    FOREIGN KEY ([ExpansionId])
    REFERENCES [dbo].[Expansion]
        ([ExpansionId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_expansionProduct'
CREATE INDEX [IX_FK_expansionProduct]
ON [dbo].[Product]
    ([ExpansionId]);
GO

-- Creating foreign key on [GameId] in table 'Expansion'
ALTER TABLE [dbo].[Expansion]
ADD CONSTRAINT [fk_gameExpansion]
    FOREIGN KEY ([GameId])
    REFERENCES [dbo].[Game]
        ([GameId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'fk_gameExpansion'
CREATE INDEX [IX_fk_gameExpansion]
ON [dbo].[Expansion]
    ([GameId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------