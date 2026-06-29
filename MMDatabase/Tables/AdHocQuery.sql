CREATE TABLE [dbo].[AdhocQuery]
(
	AdhocQueryId INT NOT NULL IDENTITY (1, 1),
	NaturalLanguageQuery NVARCHAR(MAX) NOT NULL,
	MessageOrSqlReturned NVARCHAR(MAX) NOT NULL,
	WhenRun DATETIME NOT NULL,
	IsSuccessful BIT NOT NULL,
	AiProvider NVARCHAR(50) NOT NULL DEFAULT 'Offline', 
    CONSTRAINT PK_AdhocQuery PRIMARY KEY (AdhocQueryId)
)
