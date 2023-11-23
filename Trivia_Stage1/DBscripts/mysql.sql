create database Trivia
go

CREATE TABLE [dbo].[Players] (
  PlayerID INT PRIMARY KEY,
  Email VARCHAR(255),
  Playername VARCHAR(255),
  [Password VARCHAR(255),
  [Rank] VARCHAR(50),
  [NumOfQuestions] INT
);

CREATE TABLE [dbo].[Topics] (
  TopicID INT PRIMARY KEY,
  TopicName VARCHAR(255)
);

CREATE TABLE [dbo].[Questions] (
  QuestionID INT PRIMARY KEY,
  TopicID INT,
  Text TEXT,
  CorrectAnswer VARCHAR(255),
  StatusID INT,
  FOREIGN KEY (TopicID) REFERENCES Topics(TopicID),
  FOREIGN KEY (StatusID) REFERENCES Status(StatusID)
);

CREATE TABLE [dbo].[PlayerPoints] (
  [PlayerId] INT,
  [TopicID] INT,
  [Points] INT,
  PRIMARY KEY (PlayerId, TopicID),
  FOREIGN KEY (PlayerId) REFERENCES Users(PlayerId),
  FOREIGN KEY (TopicID) REFERENCES Topics(TopicID)
);

CREATE TABLE [dbo].[Status] (
  [StatusID] INT PRIMARY KEY,
  [StatusName] VARCHAR(50) UNIQUE
);

-- Example statuses for questions
INSERT INTO Status (StatusID, StatusName) VALUES
  (1, 'Pending Approval'),
  (2, 'Approved'),
  (3, 'Rejected');

-- Example statuses for user accounts
INSERT INTO Status (StatusID, StatusName) VALUES
  (4, 'Active'),
  (5, 'Suspended'),
  (6, 'Banned');