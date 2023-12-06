create database Trivia
go

create table [dbo].[Ranks] (
	[RankID] int primary key,
	[RankName] nvarchar(50),
);

insert into [Ranks] (RankID, RankName) values (1, 'Manager'), (2, 'Master'), (3, 'Rookie')

CREATE TABLE [dbo].[Players] (
  [PlayerID] INT PRIMARY KEY,
  [Email] nvarchar(100) unique,
  [pName] nvarchar(100),
  [Score] int,
  RankID int foreign key references Ranks(RankID)
);

insert into Players (PlayerID, Email, pName, Score, RankId) values 
					(1, 'yaron.traitel@gmail.com', 'Yaron', 0, 1), 
					(2, 'josef@gmail.com', 'Josef', 0, 3)

CREATE TABLE [dbo].[Topics] (
  TopicID INT PRIMARY KEY not null,
  TopicName VARCHAR(100) not null
);

insert into Topics (TopicID, TopicName) values (1, 'Sports'), (2, 'Politics'), (3, 'History'), (4, 'Science'), (5, 'Ramon')

CREATE TABLE [dbo].[Status] (
  [StatusID] INT PRIMARY KEY not null,
  [StatusName] nvarchar(50) not null
);

INSERT INTO [Status] (StatusID, StatusName) VALUES
  (1, 'Pending Approval'),
  (2, 'Approved'),
  (3, 'Rejected');

CREATE TABLE [dbo].[Questions] (
  [QuestionID] INT PRIMARY KEY not null,
  [Text] nvarchar(255),
  [CorrectAnswer] nvarchar(100) not null,
  [Wrong1] nvarchar(100) not null,
  [Wrong2] nvarchar(100) not null,
  [Wrong3] nvarchar(100) not null,
  TopicID int foreign key references Topics(TopicID),
  StatusID int foreign key references [Status](StatusID),
  PlayerID int foreign key references Players(PlayerID)
);

insert into Questions (QuestionID, [Text], CorrectAnswer, Wrong1, Wrong2, Wrong3, TopicID, StatusID, PlayerID) values 
	(1, 'How old is Benjamin Netanyahu?', '74', '73', '63', '94', 2, 2, 1),
	(2, 'What is the hardest natural substance on Earth?', 'Diamond', 'Corundum', 'Titanium', 'Granite', 4, 2, 1),
	(3, 'Who was the first President of the United States?', 'George Washington', 'John Adams', 'James Madison', 'Thomas Jefferson', 3, 2, 1),
	(4, 'How many students are there in Ramon High Shcool?', '550', '551', '490', '1000', 5, 2, 1),
	(5, 'Which country hosted the 2016 Summer Olympics?', 'Brazil', 'United States', 'Russia', 'China', 1, 2, 1)

select * from Questions