create database Trivia
go

CREATE TABLE [dbo].[Players] (
  [PlayerID] INT PRIMARY KEY,
  [Email] nvarchar(100) unique,
  [pName] nvarchar(100),
  [Score] int,
  foreign key (RankId) references Ranks(RankId),
);

insert into Players (PlayerID, Email, pName, Score, RankId) values (1, "yaron.traitel@gmail.com", "Yaron", 0, 1)

CREATE TABLE [dbo].[Topics] (
  TopicID INT PRIMARY KEY,
  TopicName VARCHAR(100)
);

insert into Topics (TopicID, TopicName) values (1, 'Sports'), (2, 'Politics'), (3, 'History'), (4, 'Science'), (5, 'Ramon')

CREATE TABLE [dbo].[Questions] (
  [QuestionID] INT PRIMARY KEY,
  [TopicID] INT,
  [Text] nvarchar(255),
  [CorrectAnswer] nvarchar(100),
  [Wrong1] nvarchar(100),
  [Wrong2] nvarchar(100),
  [Wrong3] nvarchar(100),
  [StatusID] INT,
  foreign key (TopicID) REFERENCES Topics(TopicID),
  foreign key (StatusID) REFERENCES [Status](StatusID),
  foreign key (PlayerId) references Players(PlayerId),
);

CREATE TABLE [dbo].[Status] (
  [StatusID] INT PRIMARY KEY,
  [StatusName] nvarchar(50)
);

INSERT INTO [Status] (StatusID, StatusName) VALUES
  (1, 'Pending Approval'),
  (2, 'Approved'),
  (3, 'Rejected');

create table [dbo].[Ranks] (
	[RankID] int,
	[RankName] nvarchar(50),
);

insert into [Ranks] (RankID, RankName) values (1, 'Manager'), (2, 'Master'), (3, 'Rookie')