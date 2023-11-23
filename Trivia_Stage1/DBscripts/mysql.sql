create database Trivia
go

create table [dbo].[Players] (
	[Mail] nvarchar(20),
	[Name] int,
	[Level] nvarchar(20),
	[NumOfQues] int,
);