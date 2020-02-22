--codelists
CREATE TABLE NoteType(
	[Id] [uniqueidentifier] NOT NULL,
	[TypeName] [nvarchar](255) NOT NULL,
	PRIMARY KEY (Id)
);
GO

--seed the two codelists
INSERT INTO NoteType (Id, TypeName) values ('27A6378A-D5F1-437A-9324-FB42E7A0E1EF','Regular text'); 
INSERT INTO NoteType (Id, TypeName) values ('05514712-1775-47CE-A411-9281F30AE59C','Itemized Text');
GO

--users table
CREATE TABLE Users(
	[Id] [uniqueidentifier] NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[Username] [nvarchar](255) NOT NULL,
	PRIMARY KEY (Id)
);
GO

--folders table
CREATE TABLE Folders(
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	PRIMARY KEY (Id)	
);
GO

ALTER TABLE Folders  WITH CHECK ADD CONSTRAINT [FK_Folders_Users_UserId] FOREIGN KEY([UserId])
REFERENCES Users([Id]);
GO

--notes table
CREATE TABLE Notes(
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[FolderId] [uniqueidentifier] NULL,
	[IsShared] [bit] NOT NULL,
	[NoteTypeId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	PRIMARY KEY(Id)
);
GO

ALTER TABLE Notes  WITH CHECK ADD  CONSTRAINT [FK_Notes_Folders_FolderId] FOREIGN KEY([FolderId])
REFERENCES Folders([Id]);
GO

ALTER TABLE Notes  WITH CHECK ADD  CONSTRAINT [FK_Notes_Users_UserId] FOREIGN KEY([UserId])
REFERENCES Users([Id]);
GO

ALTER TABLE Notes ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsShared];
GO