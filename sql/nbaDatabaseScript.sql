USE [nba]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Team]') AND type in (N'U'))
ALTER TABLE [dbo].[Team] DROP CONSTRAINT IF EXISTS [FK_Team_Conference]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Player]') AND type in (N'U'))
ALTER TABLE [dbo].[Player] DROP CONSTRAINT IF EXISTS [FK_Player_Team]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 2020-12-18 10:22:44 PM ******/
DROP TABLE IF EXISTS [dbo].[Team]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 2020-12-18 10:22:44 PM ******/
DROP TABLE IF EXISTS [dbo].[Player]
GO
/****** Object:  Table [dbo].[Conference]    Script Date: 2020-12-18 10:22:44 PM ******/
DROP TABLE IF EXISTS [dbo].[Conference]
GO
USE [master]
GO
/****** Object:  Database [nba]    Script Date: 2020-12-18 10:22:44 PM ******/
DROP DATABASE IF EXISTS [nba]
GO
/****** Object:  Database [nba]    Script Date: 2020-12-18 10:22:44 PM ******/
CREATE DATABASE [nba]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'nba', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER_2019\MSSQL\DATA\nba.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'nba_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER_2019\MSSQL\DATA\nba_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [nba] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [nba].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [nba] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [nba] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [nba] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [nba] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [nba] SET ARITHABORT OFF 
GO
ALTER DATABASE [nba] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [nba] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [nba] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [nba] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [nba] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [nba] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [nba] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [nba] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [nba] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [nba] SET  ENABLE_BROKER 
GO
ALTER DATABASE [nba] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [nba] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [nba] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [nba] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [nba] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [nba] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [nba] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [nba] SET RECOVERY FULL 
GO
ALTER DATABASE [nba] SET  MULTI_USER 
GO
ALTER DATABASE [nba] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [nba] SET DB_CHAINING OFF 
GO
ALTER DATABASE [nba] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [nba] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [nba] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'nba', N'ON'
GO
ALTER DATABASE [nba] SET QUERY_STORE = OFF
GO
USE [nba]
GO
/****** Object:  Table [dbo].[Conference]    Script Date: 2020-12-18 10:22:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conference](
	[ConferenceId] [int] IDENTITY(1,1) NOT NULL,
	[ConfName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Conference] PRIMARY KEY CLUSTERED 
(
	[ConferenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Player]    Script Date: 2020-12-18 10:22:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Player](
	[PlayerId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[LastName] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[BirthDate] [datetime2](7) NOT NULL,
	[Salary] [decimal](19, 0) NOT NULL,
	[TeamId] [int] NULL,
 CONSTRAINT [PK_Player] PRIMARY KEY CLUSTERED 
(
	[PlayerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Team]    Script Date: 2020-12-18 10:22:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Team](
	[TeamId] [int] IDENTITY(1,1) NOT NULL,
	[TeamName] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[Arena] [varchar](50) NOT NULL,
	[Division] [int] NOT NULL,
	[ConferenceId] [int] NOT NULL,
 CONSTRAINT [PK_Team] PRIMARY KEY CLUSTERED 
(
	[TeamId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Conference] ON 
GO
INSERT [dbo].[Conference] ([ConferenceId], [ConfName]) VALUES (1, N'Eastern')
GO
INSERT [dbo].[Conference] ([ConferenceId], [ConfName]) VALUES (2, N'Western')
GO
SET IDENTITY_INSERT [dbo].[Conference] OFF
GO
SET IDENTITY_INSERT [dbo].[Player] ON 
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (1, N'Rick', N'Barry', 0, CAST(N'1944-03-28T00:00:00.0000000' AS DateTime2), CAST(3010000 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (2, N'Elvin', N'Hayes', 0, CAST(N'1945-11-17T00:00:00.0000000' AS DateTime2), CAST(7463983 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (3, N'Walt', N'Frazier', 0, CAST(N'1945-04-29T00:00:00.0000000' AS DateTime2), CAST(8800000 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (4, N'Isiah', N'Thomas', 0, CAST(N'1961-04-30T00:00:00.0000000' AS DateTime2), CAST(4500060 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (5, N'Pau', N'Gasol', 1, CAST(N'1980-07-06T00:00:00.0000000' AS DateTime2), CAST(2654100 AS Decimal(19, 0)), 19)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (6, N'George', N'Gervin', 0, CAST(N'1952-04-27T00:00:00.0000000' AS DateTime2), CAST(11000544 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (7, N'Ray', N'Allen', 0, CAST(N'1975-07-20T00:00:00.0000000' AS DateTime2), CAST(6255333 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (8, N'Reggie', N'Miller', 0, CAST(N'1965-08-24T00:00:00.0000000' AS DateTime2), CAST(1223600 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (9, N'Kawhi', N'Leonard', 1, CAST(N'1991-06-29T00:00:00.0000000' AS DateTime2), CAST(6002966 AS Decimal(19, 0)), 22)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (10, N'Paul', N'Pierce', 0, CAST(N'1977-10-13T00:00:00.0000000' AS DateTime2), CAST(7225008 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (11, N'Adrian', N'Dantley', 0, CAST(N'1955-02-28T00:00:00.0000000' AS DateTime2), CAST(9677800 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (12, N'Bill', N'Walton', 0, CAST(N'1952-11-05T00:00:00.0000000' AS DateTime2), CAST(7552200 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (13, N'Giannis', N'Antetokounmpo', 1, CAST(N'1994-12-06T00:00:00.0000000' AS DateTime2), CAST(3555100 AS Decimal(19, 0)), 10)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (14, N'Bill', N'Russell', 0, CAST(N'1934-02-12T00:00:00.0000000' AS DateTime2), CAST(7000500 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (15, N'Bob', N'McAdoo', 0, CAST(N'1951-09-25T00:00:00.0000000' AS DateTime2), CAST(5002666 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (16, N'Stephen', N'Curry', 1, CAST(N'1988-03-14T00:00:00.0000000' AS DateTime2), CAST(2000440 AS Decimal(19, 0)), 21)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (17, N'Steve', N'Nash', 0, CAST(N'1974-02-07T00:00:00.0000000' AS DateTime2), CAST(6357100 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (18, N'Magic', N'Johnson', 0, CAST(N'1959-08-14T00:00:00.0000000' AS DateTime2), CAST(4022110 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (19, N'Moses', N'Malone', 0, CAST(N'1955-03-23T00:00:00.0000000' AS DateTime2), CAST(7553200 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (20, N'James', N'Harden', 0, CAST(N'1900-01-31T00:00:00.0000000' AS DateTime2), CAST(6005966 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (21, N'Patrick', N'Ewing', 1, CAST(N'1989-08-26T00:00:00.0000000' AS DateTime2), CAST(5032366 AS Decimal(19, 0)), 27)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (22, N'Manu', N'Ginobili', 0, CAST(N'1977-07-28T00:00:00.0000000' AS DateTime2), CAST(5888441 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (23, N'Russell', N'Westbrook', 1, CAST(N'1988-11-12T00:00:00.0000000' AS DateTime2), CAST(7699900 AS Decimal(19, 0)), 15)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (24, N'Talen', N'Horton-Tucker', 1, CAST(N'2000-11-25T00:00:00.0000000' AS DateTime2), CAST(4555222 AS Decimal(19, 0)), 23)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (25, N'Kevin', N'McHale', 0, CAST(N'1957-12-19T00:00:00.0000000' AS DateTime2), CAST(7000500 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (26, N'Jason', N'Kidd', 0, CAST(N'1973-03-23T00:00:00.0000000' AS DateTime2), CAST(6333222 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (27, N'Clyde', N'Drexler', 0, CAST(N'1962-06-22T00:00:00.0000000' AS DateTime2), CAST(7889658 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (28, N'Dolph', N'Schayes', 0, CAST(N'1928-12-10T00:00:00.0000000' AS DateTime2), CAST(1000255 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (29, N'Bob', N'Pettit', 0, CAST(N'1932-12-12T00:00:00.0000000' AS DateTime2), CAST(12222055 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (30, N'Michael', N'Jordan', 0, CAST(N'1963-02-17T00:00:00.0000000' AS DateTime2), CAST(33140000 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (31, N'John', N'Stockton', 0, CAST(N'1962-03-26T00:00:00.0000000' AS DateTime2), CAST(4322210 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (32, N'Chris', N'Paul', 1, CAST(N'1985-05-06T00:00:00.0000000' AS DateTime2), CAST(9555233 AS Decimal(19, 0)), 24)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (33, N'Eric', N'Gordon', 1, CAST(N'1988-12-25T00:00:00.0000000' AS DateTime2), CAST(4000522 AS Decimal(19, 0)), 27)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (34, N'John', N'Havlicek', 0, CAST(N'1940-04-08T00:00:00.0000000' AS DateTime2), CAST(7899699 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (35, N'Scottie', N'Pippen', 0, CAST(N'1965-09-25T00:00:00.0000000' AS DateTime2), CAST(4200266 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (36, N'Elgin', N'Baylor', 0, CAST(N'1934-09-16T00:00:00.0000000' AS DateTime2), CAST(2630210 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (37, N'Moses', N'Malone', 0, CAST(N'1955-09-13T00:00:00.0000000' AS DateTime2), CAST(1559488 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (38, N'Julius', N'Erving', 0, CAST(N'1900-01-31T00:00:00.0000000' AS DateTime2), CAST(7554123 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (39, N'Dwyane', N'Wade', 0, CAST(N'1950-02-22T00:00:00.0000000' AS DateTime2), CAST(3654774 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (40, N'Karl', N'Malone', 0, CAST(N'1963-07-24T00:00:00.0000000' AS DateTime2), CAST(1002369 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (41, N'Charles', N'Barkley', 0, CAST(N'1963-02-20T00:00:00.0000000' AS DateTime2), CAST(9555322 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (42, N'Jerry', N'West', 0, CAST(N'1938-05-28T00:00:00.0000000' AS DateTime2), CAST(11000550 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (43, N'Dirk', N'Nowitzki', 0, CAST(N'1978-06-19T00:00:00.0000000' AS DateTime2), CAST(2001455 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (44, N'Kevin', N'Garnett', 0, CAST(N'1979-05-19T00:00:00.0000000' AS DateTime2), CAST(6220144 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (45, N'Kevin', N'Durant', 1, CAST(N'1988-09-29T00:00:00.0000000' AS DateTime2), CAST(2666333 AS Decimal(19, 0)), 21)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (46, N'Kobe', N'Bryant', 0, CAST(N'1978-08-23T00:00:00.0000000' AS DateTime2), CAST(9555144 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (47, N'Hakeem', N'Olajuwon', 0, CAST(N'1963-01-21T00:00:00.0000000' AS DateTime2), CAST(8552010 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (48, N'David', N'Robinson', 0, CAST(N'1900-01-31T00:00:00.0000000' AS DateTime2), CAST(2010022 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (49, N'Oscar', N'Robertson', 0, CAST(N'1965-08-06T00:00:00.0000000' AS DateTime2), CAST(9552266 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (50, N'Wilt', N'Chamberlain', 0, CAST(N'1936-08-21T00:00:00.0000000' AS DateTime2), CAST(2005099 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (51, N'Derrick', N'Rose', 1, CAST(N'1988-10-04T00:00:00.0000000' AS DateTime2), CAST(6995010 AS Decimal(19, 0)), 8)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (52, N'Tim', N'Duncan', 0, CAST(N'1976-04-25T00:00:00.0000000' AS DateTime2), CAST(2100300 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (53, N'Shaquille', N'O''Neal', 0, CAST(N'1900-01-31T00:00:00.0000000' AS DateTime2), CAST(21002665 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (54, N'Larry', N'Bird', 0, CAST(N'1972-03-06T00:00:00.0000000' AS DateTime2), CAST(7070500 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (55, N'Kareem', N'Abdul-Jabbar', 0, CAST(N'1947-04-16T00:00:00.0000000' AS DateTime2), CAST(9500055 AS Decimal(19, 0)), NULL)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (56, N'LeBron', N'James', 1, CAST(N'1984-12-30T00:00:00.0000000' AS DateTime2), CAST(39255699 AS Decimal(19, 0)), 23)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (57, N'Kyle', N'Kuzma', 1, CAST(N'1994-07-24T00:00:00.0000000' AS DateTime2), CAST(5996333 AS Decimal(19, 0)), 23)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (58, N'Robert', N'Covington', 1, CAST(N'1990-12-14T00:00:00.0000000' AS DateTime2), CAST(3011899 AS Decimal(19, 0)), 19)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (59, N'Serge', N'Ibaka', 1, CAST(N'1989-09-18T00:00:00.0000000' AS DateTime2), CAST(2558966 AS Decimal(19, 0)), 22)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (60, N'Pascal', N'Siakam', 1, CAST(N'1994-04-02T00:00:00.0000000' AS DateTime2), CAST(3554010 AS Decimal(19, 0)), 5)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (61, N'Terence', N'Davis', 1, CAST(N'1987-05-16T00:00:00.0000000' AS DateTime2), CAST(3500899 AS Decimal(19, 0)), 5)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (62, N'Kyle', N'Lowry', 1, CAST(N'1986-03-25T00:00:00.0000000' AS DateTime2), CAST(3666200 AS Decimal(19, 0)), 5)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (63, N'Fred', N'VanVleet', 1, CAST(N'1994-02-25T00:00:00.0000000' AS DateTime2), CAST(8995210 AS Decimal(19, 0)), 5)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (64, N'Andrew', N'Wiggins', 1, CAST(N'1995-02-23T00:00:00.0000000' AS DateTime2), CAST(5884199 AS Decimal(19, 0)), 21)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (65, N'Eric', N'Paschall', 1, CAST(N'1996-11-04T00:00:00.0000000' AS DateTime2), CAST(8999652 AS Decimal(19, 0)), 21)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (66, N'Damion', N'Lee', 1, CAST(N'1992-10-21T00:00:00.0000000' AS DateTime2), CAST(3557965 AS Decimal(19, 0)), 21)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (67, N'Jayson', N'Tatum', 1, CAST(N'1998-03-03T00:00:00.0000000' AS DateTime2), CAST(2588633 AS Decimal(19, 0)), 1)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (68, N'Kemba', N'Walker', 1, CAST(N'1990-05-08T00:00:00.0000000' AS DateTime2), CAST(8444123 AS Decimal(19, 0)), 1)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (69, N'Jaylen', N'Brown', 1, CAST(N'1996-10-31T00:00:00.0000000' AS DateTime2), CAST(3222555 AS Decimal(19, 0)), 1)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (70, N'Gordon', N'Hayward', 1, CAST(N'1990-03-23T00:00:00.0000000' AS DateTime2), CAST(3666541 AS Decimal(19, 0)), 1)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (71, N'Marcus', N'Smart', 1, CAST(N'1994-06-03T00:00:00.0000000' AS DateTime2), CAST(2555414 AS Decimal(19, 0)), 1)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (72, N'Paul', N'George', 1, CAST(N'1990-05-02T00:00:00.0000000' AS DateTime2), CAST(2558699 AS Decimal(19, 0)), 22)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (73, N'Montrezl', N'Harrell', 1, CAST(N'1994-01-26T00:00:00.0000000' AS DateTime2), CAST(3664478 AS Decimal(19, 0)), 23)
GO
INSERT [dbo].[Player] ([PlayerId], [FirstName], [LastName], [Active], [BirthDate], [Salary], [TeamId]) VALUES (74, N'Lou', N'Williams', 1, CAST(N'1986-10-27T00:00:00.0000000' AS DateTime2), CAST(2556995 AS Decimal(19, 0)), 22)
GO
SET IDENTITY_INSERT [dbo].[Player] OFF
GO
SET IDENTITY_INSERT [dbo].[Team] ON 
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (1, N'Boston Celtics', N'Boston', N'Massachusetts', N'TD Garden', 0, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (2, N'Brooklyn Nets', N'New York City', N'New York', N'Barclays Center', 0, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (3, N'New York Knicks', N'New York City', N'New York', N'Madison Square Garden', 0, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (4, N'Philadelphia 76ers', N'Philadelphia', N'Pennsylvania', N'Wells Fargo Center', 0, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (5, N'Toronto Raptors', N'Toronto', N'Ontario', N'Scotiabank Arena', 0, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (6, N'Chicago Bulls', N'Chicago', N'Illinois', N'United Center', 1, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (7, N'Cleveland Cavaliers', N'Cleveland', N'Ohio', N'Rocket Mortgage FieldHouse', 1, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (8, N'Detroit Pistons', N'Detroit', N'Michigan', N'Little Caesars Arena', 1, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (9, N'Indiana Pacers', N'Indianapolis', N'Indiana', N'Bankers Life Fieldhouse', 1, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (10, N'Milwaukee Bucks', N'Milwaukee', N'Wisconsin', N'Fiserv Forum', 1, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (11, N'Atlanta Hawks', N'Atlanta', N'Georgia', N'State Farm Arena', 2, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (12, N'Charlotte Hornets', N'Charlotte', N'North Carolina', N'Spectrum Center', 2, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (13, N'Miami Heat', N'Miami', N'Florida', N'American Airlines Arena', 2, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (14, N'Orlando Magic', N'Orlando', N'Florida', N'Amway Center', 2, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (15, N'Washington Wizards', N'Washington', N'D.C.', N'Capital One Arena', 2, 1)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (16, N'Denver Nuggets', N'Denver', N'Colorado', N'Ball Arena', 3, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (17, N'Minnesota Timberwolves', N'Minneapolis', N'Minnesota', N'Target Center', 3, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (18, N'Oklahoma City Thunder', N'Oklahoma City', N'Oklahoma', N'Chesapeake Energy Arena', 3, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (19, N'Portland Trail Blazers', N'Portland', N'Oregon', N'Moda Center', 3, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (20, N'Utah Jazz', N'Salt Lake City', N'Utah', N'Vivint Smart Home Arena', 3, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (21, N'Golden State Warriors', N'San Francisco', N'California', N'Chase Center', 4, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (22, N'Los Angeles Clippers', N'Los Angeles', N'California', N'Staples Center', 4, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (23, N'Los Angeles Lakers', N'Los Angeles', N'California', N'Staples Center', 4, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (24, N'Phoenix Suns', N'Phoenix', N'Arizona', N'PHX Arena', 4, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (25, N'Sacramento Kings', N'Sacramento', N'California', N'Golden 1 Center', 4, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (26, N'Dallas Mavericks', N'Dallas', N'Texas', N'American Airlines Center', 5, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (27, N'Houston Rockets', N'Houston', N'Texas', N'Toyota Center', 5, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (28, N'Memphis Grizzlies', N'Memphis', N'Tennessee', N'FedExForum', 5, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (29, N'New Orleans Pelicans', N'New Orleans', N'Louisiana', N'Smoothie King Center', 5, 2)
GO
INSERT [dbo].[Team] ([TeamId], [TeamName], [City], [State], [Arena], [Division], [ConferenceId]) VALUES (30, N'San Antonio Spurs', N'San Antonio', N'Texas', N'AT&T Center', 5, 2)
GO
SET IDENTITY_INSERT [dbo].[Team] OFF
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [FK_Player_Team] FOREIGN KEY([TeamId])
REFERENCES [dbo].[Team] ([TeamId])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_Player_Team]
GO
ALTER TABLE [dbo].[Team]  WITH CHECK ADD  CONSTRAINT [FK_Team_Conference] FOREIGN KEY([ConferenceId])
REFERENCES [dbo].[Conference] ([ConferenceId])
GO
ALTER TABLE [dbo].[Team] CHECK CONSTRAINT [FK_Team_Conference]
GO
USE [master]
GO
ALTER DATABASE [nba] SET  READ_WRITE 
GO
