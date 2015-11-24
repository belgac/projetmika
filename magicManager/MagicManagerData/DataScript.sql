CREATE TABLE [dbo].[Game]
(
	[GameId]			INT			primary key IDENTITY (1, 1) NOT NULL,
	[Name]				Nvarchar	null,
	[WorkerEditTime]	DateTime	null,
)

GO

CREATE TABLE [dbo].[Expansion]
(
	[ExpansionId]		INT			primary key Identity (1, 1) NOT NULL,
	[Name]				Nvarchar	null,
	[Icon]				INT			null,
	[WorkerEditTime]	DateTime	null,
	[GameId]			INT			Not null,	
	constraint fk_gameExpansion foreign key ([GameId]) references [dbo].[Game]([GameId])
)

GO

CREATE TABLE [dbo].[Product]
(
	[ProductId]			INT			Identity (1, 1) Not Null Primary KEY,
	[ProductName]		NVARCHAR	NULL ,
	[ProductUrl]		NVARCHAR	NULL,
	[ImageUrl]			NVARCHAR	Null,
	[Rarity]			NVARCHAR	NUll,
	[WorkerEditTime]	DateTime	NULL,
	[ExpansionId]		INT			NOT NULL,
	 
	CONSTRAINT FK_expansionProduct FOREIGN KEY (ExpansionId)
	 REFERENCES [dbo].[Expansion]([ExpansionId])

)

GO


CREATE TABLE [dbo].[Lang]
(
	[LanguageId]		INT			IDENTITY (1, 1) NOT NULL PRIMARY KEY,
	[Name]				Nvarchar	not null,
	[WorkerEditTime]	DateTime	null,
)

GO

CREATE TABLE [dbo].[Article]
(
	[ArticleId]			INT			IDENTITY (1, 1) Not Null PRIMARY KEY,
	[LanguageId]		INT			Not null,
	[isFoil]			bit			Not null,
	[isSigned]			bit			Not null,
	[isAltered]		    bit			Not null,
	[isPlayset]			bit			Not null,
	[isFirstEd]			bit			Not null,
	[SiteWideCount]		Int			null,
	[WorkerEditTime]	DateTime	null,
	[ProductId]			INT			Not null,
	CONSTRAINT FK_productArticle FOREIGN KEY (ProductId) REFERENCES [dbo].[Product](ProductId),
	CONSTRAINT FK_languageArticle FOREIGN KEY (LanguageId) REFERENCES [dbo].[Lang](LanguageId)
  
)

GO

CREATE TABLE [dbo].[DailyPrice]
(
	[DailyPriceId]		INT			primary key Identity (1, 1) NOT NULL,
	[Sell]				float	    null,
	[Low]				float 		null,
	[Average]			float		null,
	[Count]				INT			null,
	[Price]				float		null,
	[LastEdited]		DateTime	null,
	[WorkerEditTime]	DateTime	null,
	[Articleid]			INT			NOT NULL,
	constraint fk_articleDailyprice foreign key ([Articleid]) references [dbo].[Article]([ArticleId])
	
)

GO

CREATE TABLE [dbo].[WorkerAction]
(
	[WorkerActionId]	INT			Primary key Identity (1, 1) NOT NULL,
	[Type]				NVARCHAR	Not null,
	[Comment]			Nvarchar	null,
	[Date]				DateTime	null,

)