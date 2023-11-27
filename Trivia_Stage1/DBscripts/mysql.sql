create database Trivia
go

CREATE TABLE [dbo].[Players] (
  [PlayerID] INT PRIMARY KEY,
  [Email] VARCHAR(255),
  [pName] VARCHAR(255),
  [Score] int,
  foreign key (RankId) references Ranks(RankId),
);

CREATE TABLE [dbo].[Topics] (
  TopicID INT PRIMARY KEY,
  TopicName VARCHAR(255)
);

CREATE TABLE [dbo].[Questions] (
  [QuestionID] INT PRIMARY KEY,
  [TopicID] INT,
  [Text] nvarchar(255),
  [CorrectAnswer] nvarchar(255),
  [Wrong1] nvarchar(255),
  [Wrong2] nvarchar(255),
  [Wrong3] nvarchar(255),
  [StatusID] INT,
  foreign key (TopicID) REFERENCES Topics(TopicID),
  foreign key (StatusID) REFERENCES [Status](StatusID),
  foreign key (PlayerId) references Players(PlayerId),
);

CREATE TABLE [dbo].[Status] (
  [StatusID] INT PRIMARY KEY,
  [StatusName] nvarchar(50)
);

-- Example statuses for questions
INSERT INTO [Status] (StatusID, StatusName) VALUES
  (1, 'Pending Approval'),
  (2, 'Approved'),
  (3, 'Rejected');

create table [dbo].[Ranks] (
	[RankID] int,
	[RankName] nvarchar(255),
);