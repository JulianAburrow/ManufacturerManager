CREATE TABLE Language (
    LanguageId INT IDENTITY(1,1),
    EnglishName NVARCHAR(200) NOT NULL,
    OriginalName NVARCHAR(200) NOT NULL,
    TransliteratedName NVARCHAR(200) NOT NULL,
    Code CHAR(2) NOT NULL,
    UseInHelpPage BIT NOT NULL DEFAULT 0,
    CONSTRAINT PK_Language PRIMARY KEY (LanguageId)
);