ALTER TABLE [dbo].[TILT_Tag_Note] DROP CONSTRAINT [FK_TILT_Tag_Note_TILT_Tag]
GO
ALTER TABLE [dbo].[TILT_Tag_Note] DROP CONSTRAINT [FK_TILT_Tag_Note_TILT_Note]
GO
ALTER TABLE [dbo].[TILT_Note] DROP CONSTRAINT [FK_TILT_Note_TILT_URL]
GO
/****** Object:  Table [dbo].[TILT_URL]    Script Date: 6/11/2023 10:27:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TILT_URL]') AND type in (N'U'))
DROP TABLE [dbo].[TILT_URL]
GO
/****** Object:  Table [dbo].[TILT_Tag_Note]    Script Date: 6/11/2023 10:27:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TILT_Tag_Note]') AND type in (N'U'))
DROP TABLE [dbo].[TILT_Tag_Note]
GO
/****** Object:  Table [dbo].[TILT_Tag]    Script Date: 6/11/2023 10:27:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TILT_Tag]') AND type in (N'U'))
DROP TABLE [dbo].[TILT_Tag]
GO
/****** Object:  Table [dbo].[TILT_Note]    Script Date: 6/11/2023 10:27:59 AM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TILT_Note]') AND type in (N'U'))
DROP TABLE [dbo].[TILT_Note]
GO
/****** Object:  Database [tiltdb]    Script Date: 6/11/2023 10:27:59 AM ******/
DROP DATABASE [tiltdb]
GO
/****** Object:  Database [tiltdb]    Script Date: 6/11/2023 10:27:59 AM ******/
CREATE DATABASE [tiltdb]  (EDITION = 'GeneralPurpose', SERVICE_OBJECTIVE = 'GP_S_Gen5_1', MAXSIZE = 32 GB) WITH CATALOG_COLLATION = SQL_Latin1_General_CP1_CI_AS, LEDGER = OFF;
GO
ALTER DATABASE [tiltdb] SET COMPATIBILITY_LEVEL = 150
GO
ALTER DATABASE [tiltdb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [tiltdb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [tiltdb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [tiltdb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [tiltdb] SET ARITHABORT OFF 
GO
ALTER DATABASE [tiltdb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [tiltdb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [tiltdb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [tiltdb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [tiltdb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [tiltdb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [tiltdb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [tiltdb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [tiltdb] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [tiltdb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [tiltdb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [tiltdb] SET  MULTI_USER 
GO
ALTER DATABASE [tiltdb] SET ENCRYPTION ON
GO
ALTER DATABASE [tiltdb] SET QUERY_STORE = ON
GO
ALTER DATABASE [tiltdb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
/*** The scripts of database scoped configurations in Azure should be executed inside the target database connection. ***/
GO
-- ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 8;
GO
/****** Object:  Table [dbo].[TILT_Note]    Script Date: 6/11/2023 10:28:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TILT_Note](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[IDURL] [bigint] NOT NULL,
	[Text] [nvarchar](150) NOT NULL,
	[Link] [nvarchar](500) NULL,
	[ForDate] [datetime] NULL,
	[TimeZoneString] [nvarchar](350) NULL,
 CONSTRAINT [PK_TILT_Note] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TILT_Tag]    Script Date: 6/11/2023 10:28:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TILT_Tag](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_TILT_Tag] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TILT_Tag_Note]    Script Date: 6/11/2023 10:28:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TILT_Tag_Note](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[IDTag] [bigint] NOT NULL,
	[IDNote] [bigint] NOT NULL,
 CONSTRAINT [PK_TILT_Tag_Note] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TILT_URL]    Script Date: 6/11/2023 10:28:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TILT_URL](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[URLPart] [nvarchar](20) NOT NULL,
	[Secret] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TILT_URL] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[TILT_Note] ON 
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (1, 1, N'my first tilt', N'', CAST(N'2022-04-27T21:56:56.307' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (2, 1, N'This is new text', N'', CAST(N'2022-04-28T03:45:29.537' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (3, 1, N'Another day', N'', CAST(N'2022-04-30T04:50:47.623' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (4, 2, N'Instrumentation with AppInsights', N'', CAST(N'2022-04-30T05:44:55.237' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (5, 2, N'Authentication or Authorization with JWT', N'http://tiltwebapp.azurewebsites.net/BlocklyAutomation/automation/loadexample/authenticate', CAST(N'2022-04-28T05:46:36.857' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (6, 2, N'Integrate Azure with Github Actions', N'', CAST(N'2022-04-27T05:47:37.310' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (7, 2, N'Adapt BlocklyAutomation for TILT', N'', CAST(N'2022-04-29T05:50:37.613' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (8, 9, N'A new TILT', N'', CAST(N'2022-04-30T20:42:20.817' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (9, 2, N'Do tests with LightBDD and Expectations', N'', CAST(N'2022-05-01T18:18:55.530' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (10, 2, N'For CodeCov with Github, go to settings and put main as the default branch', N'https://app.codecov.io/gh/ignatandrei/TILT', CAST(N'2022-05-02T13:18:12.037' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (11, 2, N'Always always fix versions in package json', N'', CAST(N'2022-05-03T17:00:07.837' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (14, 2, N'add a partial null constructor for TypeScript export class TILT {constructor(tilt: Partial<TILT>| null = null) { //object.Keys}}', N'', CAST(N'2022-05-04T06:54:25.100' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (17, 2, N'Always map the Observable from http to the initial class .pipe(       map(arr=>arr.map(it=>new TILT(it)))     )', N'', CAST(N'2022-05-05T06:54:25.100' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (18, 2, N'The problem is that you think that you have time (Buddha? )', N'', CAST(N'2022-05-06T03:24:39.723' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (19, 2, N'Incredibly easy to create a Pipe in Angular', N'https://angular.io/guide/pipes', CAST(N'2022-05-07T03:30:40.247' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (20, 1, N'This is new text', N'', CAST(N'2022-05-07T12:53:36.070' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (22, 2, N'WindowsTerminal wt new-tab --title webapi -d NetTilt.WebAPI PowerShell -noexit -c "dotnet watch run --no-hot-reload"', N'', CAST(N'2022-05-08T09:21:51.607' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (23, 1, N'This is new text', N'', CAST(N'2022-05-09T22:15:21.557' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (24, 9, N'Testing blocky', N'', CAST(N'2022-05-09T22:15:50.900' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (25, 2, N'even I want a boolean in TypeScript , it is still javascript - verify with typeof', NULL, CAST(N'2022-05-10T09:28:24.383' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (26, 2, N'sqlite can work on web: https://github.com/sql-js/sql.js/', NULL, CAST(N'2022-05-11T17:32:47.890' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (27, 2, N'to implement intro , https://github.com/shipshapecode/shepherd is very easy', NULL, CAST(N'2022-05-12T03:42:58.453' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (28, 2, N'Developer life is full of decisions: this framework ? Another ? Components ? ( Flutter or MAUI)?', NULL, CAST(N'2022-05-13T20:44:21.247' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (29, 2, N'Azure Functions -CORS is defined in C# , also in the Azure Portal for the functions. Must be both', NULL, CAST(N'2022-05-14T07:09:05.320' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (30, 2, N'If you do not write documentation about your open source project, it does not exists', NULL, CAST(N'2022-05-15T13:09:22.080' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (31, 2, N'change the angular default icon whenever you start a project', NULL, CAST(N'2022-05-16T10:36:11.853' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (32, 11, N'Azure cosmos: https://devblogs.microsoft.com/dotnet/the-azure-cosmos-db-journey-to-net-6/', NULL, CAST(N'2022-05-17T13:04:57.457' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (33, 2, N'Sometimes, you need just to rest ', NULL, CAST(N'2022-05-17T18:47:10.600' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (34, 2, N'Work hard for a year to see some results', NULL, CAST(N'2022-05-20T14:05:18.793' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (35, 2, N'Markdown is not complete. For example, table in table...', NULL, CAST(N'2022-05-22T12:56:04.820' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (36, 2, N'Visual Studio for Mac is impressive ', NULL, CAST(N'2022-05-24T17:57:00.570' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (37, 2, N'To use shepherd.js, ensure same links exists in every pagr', NULL, CAST(N'2022-05-26T03:25:23.703' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (38, 2, N'Object.assign is better for a copy constructor rather than Object.entries ', NULL, CAST(N'2022-05-27T02:57:19.837' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (39, 2, N'creating docker extension - well enough documented. Can be done in one day ... then refine', NULL, CAST(N'2022-05-28T17:03:04.660' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (40, 2, N'documentation of a product/feature can take the same amount of time as the code ', NULL, CAST(N'2022-05-29T07:20:19.653' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (41, 13, N'VueJS error handler configuration
', NULL, CAST(N'2022-06-01T06:28:57.443' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (42, 2, N'observability and metrics for http: https://developer.mozilla.org/en-US/docs/Web/API/User_Timing_API', NULL, CAST(N'2022-06-05T23:42:12.787' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (43, 1, N'Test TILT', NULL, CAST(N'2022-06-06T13:28:28.743' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (44, 2, N'almost every new product has example in react - not angular.Javascript beats typescript. NOOOOOOOO ', NULL, CAST(N'2022-06-06T20:12:41.777' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (45, 2, N'working with local dates in Javascript = not the best experience. But ... can be me , not language ', NULL, CAST(N'2022-06-07T18:42:02.257' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (46, 2, N'Most of data should come directly from backend.Do not calculate on frontend,just display and format', NULL, CAST(N'2022-06-08T18:10:17.143' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (47, 2, N'documentation - a necessary evil for each project . ', NULL, CAST(N'2022-06-09T17:14:15.517' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (48, 2, N'Learning ', NULL, CAST(N'2022-06-10T18:25:57.950' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (49, 2, N'Love Death and Robots from. Netflix is perfect for train ', NULL, CAST(N'2022-06-13T04:58:54.010' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (50, 2, N'analyze production of microservices: at least 1 h, even if you have grafana for aggregated errors', NULL, CAST(N'2022-06-15T06:13:33.167' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (51, 2, N'roslyn  -  a perfect way to inspect code. The possibilities - overwhelming!', NULL, CAST(N'2022-06-17T04:00:16.867' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (52, 2, N'I do like 
https://jonathanstark.com/daily/20220617-2257-selling-time-vs-selling-results ', NULL, CAST(N'2022-06-18T06:40:33.733' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (53, 2, N'I do like Roslyn - and how can write code that wrotes code', NULL, CAST(N'2022-06-20T18:37:36.833' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (54, 2, N'UAT and DEV MUST be 2 different systems , in 2 different networks , in order to avoid any problem', NULL, CAST(N'2022-06-22T06:31:48.013' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (55, 2, N'So simple to display time series / kpi with ELK stack', NULL, CAST(N'2022-06-28T17:42:30.440' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (56, 2, N'Birocracy solve problems in the short run. For long run use automation ', NULL, CAST(N'2022-06-30T04:59:11.990' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (57, 2, N'LightBDD is impressive on the reports', NULL, CAST(N'2022-07-01T18:22:16.560' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (58, 2, N'If you have alerts on weekend, you are always on duty ', NULL, CAST(N'2022-07-02T17:00:40.183' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (59, 2, N'Organizing is part of long term vision ', NULL, CAST(N'2022-07-04T19:51:36.497' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (60, 2, N'The difficult Roslyn - generating code from existing class (namespaces, full type of variables)', NULL, CAST(N'2022-07-07T03:59:14.623' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (61, 2, N'Do not have great expectations about movie continuation.', NULL, CAST(N'2022-07-09T19:42:09.237' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (62, 2, N' SPA vs MPA "https://nolanlawson.com/2022/06/27/spas-theory-versus-practice/"', NULL, CAST(N'2022-07-11T07:01:58.303' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (63, 2, N'Sample Sql Server Databases: https://www.sqlservercentral.com/articles/sql-server-sample-databases-2', NULL, CAST(N'2022-07-12T12:50:51.513' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (64, 2, N'Dall-E is awesome: https://labs.openai.com/e/qQ1MBygEiEfqV3xcJ0LkdkUa', NULL, CAST(N'2022-07-13T04:37:53.813' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (65, 2, N'do not work for free anymore. It has deeper problems than you can even immagine', N'', CAST(N'2022-07-14T20:29:51.567' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (66, 2, N'Webb vs Hubble', N'http://www.webbcompare.com/', CAST(N'2022-07-15T07:37:07.887' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (67, 2, N'Best of running CI/CD on local', N'https://github.com/xt0rted/dotnet-run-script', CAST(N'2022-07-27T18:13:37.093' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (68, 2, N'I am seeing as a world that is going backwards', N'https://www.theverge.com/2022/7/12/23204950/bmw-subscriptions-microtransactions-heated-seats-feature', CAST(N'2022-07-29T14:22:01.550' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (69, 2, N'A day off could recharge the batteries for the evening to program more....', N'', CAST(N'2022-07-30T17:39:20.597' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (70, 2, N'Want to try - at least in a dotnet tools', N'https://github.com/gui-cs/Terminal.Gui', CAST(N'2022-07-31T07:09:04.070' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (71, 2, N'A visual map of earth is always impressive', N'https://tjukanovt.github.io/notable-people', CAST(N'2022-08-01T04:20:00.870' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (72, 2, N'There MUST be a 10x diff between a junior and senior programmer. Otherwise, the senior is junior.', N'', CAST(N'2022-08-02T07:53:54.100' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (73, 2, N'A pizza team is sometimes better for motivation rather than just one team', N'', CAST(N'2022-08-03T20:21:46.157' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (74, 2, N'Adding AppInsights to an Azure app -without modifying code - is extremely easy . Just few clicks', N'', CAST(N'2022-08-04T05:41:08.210' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (75, 2, N'Jenkins jobs MUST be documented as any software project', N'', CAST(N'2022-08-05T04:18:52.263' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (76, 2, N'programming meetings of scrum - the worst', N'https://workchronicles.com/sprints/', CAST(N'2022-08-06T16:25:32.997' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (77, 2, N'Everything that you re-use in 2 projects should be added as a  (public ) library / package ', N'', CAST(N'2022-08-08T06:06:04.377' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (78, 2, N'easy to add commit id and date ( traceability) when CI occurs with Roslyn.
', N'https://github.com/ignatandrei/rsCG_ams', CAST(N'2022-08-09T16:00:16.640' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (79, 2, N'Nice and instructive the history of numpad', N'https://www.doc.cc/articles/a-brief-history-of-the-numeric-keypad', CAST(N'2022-08-10T07:36:17.943' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (80, 2, N'thniking about differences between novice and senior', N'https://twitter.com/dmofengineering/status/1556454109053480960', CAST(N'2022-08-11T05:56:52.537' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (81, 2, N'Dates are to be stored in UTC. To know the relation between user and day the user time zone ', N'', CAST(N'2022-08-16T14:21:37.843' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (84, 2, N'And each language has his own ideas - javascript and C#, frontend and backend... ', N'https://en.wikipedia.org/wiki/List_of_tz_database_time_zones', CAST(N'2022-08-16T21:46:53.717' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (85, 2, N'improving 1 step every day an application is better than do nothing  ', N'', CAST(N'2022-08-18T06:15:30.987' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (86, 2, N'How you read a page - I do conform', N'https://twitter.com/ShowwcaseHQ/status/1553686941106839553', CAST(N'2022-08-19T07:32:21.550' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (87, 2, N'If you want to make a desktop for windows / mac / android , there will be a hack of downloads....', N'', CAST(N'2022-08-20T16:45:26.520' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (88, 2, N'if you have docker integrate into your tests - awesome', N'', CAST(N'2022-08-22T16:34:17.493' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (92, 2, N'Sometime,  even if you try hard,  some components wont perform the work. Let it be another time', N'', CAST(N'2022-08-23T03:06:37.500' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (93, 2, N'learning about EnumeratorCancellation ', N'https://oleh-zheleznyak.blogspot.com/2020/07/enumeratorcancellation.html', CAST(N'2022-08-24T06:41:21.423' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (94, 2, N'as I study more Rx, more impressed I am', N'https://rxjs.dev/', CAST(N'2022-08-25T17:33:16.747' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (95, 2, N'mermaid js is very versatile', N'https://mermaid-js.github.io/mermaid', CAST(N'2022-08-27T14:14:08.363' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (96, 2, N'one week to do just body training  - it keeps you alive - with no more thoughts', N'', CAST(N'2022-09-04T19:15:30.637' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (97, 2, N'about stolen attention', N'https://www.theguardian.com/science/2022/jan/02/attention-span-focus-screens-apps-smartphones-social-media', CAST(N'2022-09-06T07:24:00.907' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (98, 2, N'Angular can have licenses exported', N'https://angular.io/cli/build', CAST(N'2022-09-07T20:06:57.513' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (99, 2, N'enum2combo,typescript:
export enum m {none= '''',t=''t''} 
public d= Object.values(m);
iterate in d', N'', CAST(N'2022-09-07T21:34:49.517' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (100, 2, N'TitleStrategy in Angular is invaluable to set descriptive titles in one place', N'', CAST(N'2022-09-09T05:28:56.107' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (101, 2, N'for 1 hour of presentation the work is 3 days', N'', CAST(N'2022-09-13T15:18:27.547' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (102, 2, N'you can make interactive rx marbles with milestones gannt charts  ( like mermaid)', N'https://mermaidjs.github.io/', CAST(N'2022-09-14T03:07:31.237' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (103, 2, N'to display json, use Angular pipe json and <pre> tag', N'', CAST(N'2022-09-15T04:52:24.237' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (104, 2, N'Sharing is caring - and teaching the same', N'', CAST(N'2022-09-16T19:52:19.340' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (105, 2, N'adding licences is simple for .NET Core and Angular', N'http://msprogrammer.serviciipeweb.ro/2022/09/20/licences-for-net-core-and-angular/', CAST(N'2022-09-18T05:27:07.297' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (106, 2, N'Learning about dev container VSCode features', N'https://code.visualstudio.com/blogs/2022/09/15/dev-container-features', CAST(N'2022-09-19T16:13:11.537' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (107, 2, N'free lessons for ML - to do every day', N'https://github.com/microsoft/ML-For-Beginners', CAST(N'2022-09-22T04:14:28.863' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (108, 2, N'learn about zod', N'https://timdeschryver.dev/blog/why-we-should-verify-http-response-bodies-and-why-we-should-use-zod-for-this', CAST(N'2022-09-24T06:41:21.863' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (109, 2, N'HTTP cheat:
1xx: hold on
2xx: here you go
3xx: go away
4xx: you fucked up
5xx: I fucked up', N'', CAST(N'2022-09-25T06:57:06.160' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (110, 2, N'Business does not require understanding Big O . Programming interviews, yes. ', N'', CAST(N'2022-09-28T09:12:35.360' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (111, 2, N'Pass from array to IAsyncEnumerable it is not so difficult', N'', CAST(N'2022-09-30T04:12:05.273' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (113, 2, N'If you do not understand some code when you refactor, please do not refactor ', N'', CAST(N'2022-10-01T08:51:07.560' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (114, 2, N'A night with old and new friends - talking about experiences - worth a lot! ', N'', CAST(N'2022-10-01T21:23:13.187' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (115, 2, N'every framework has his features to easy the use. E.g. ValidateOnStart() for ASP.NET Core', N'https://andrewlock.net/adding-validation-to-strongly-typed-configuration-objects-in-dotnet-6/', CAST(N'2022-10-06T09:45:07.397' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (116, 2, N'For Exceptions in C#:throw,throw new Exception(ex),ExceptionDispatchInfo.Capture,AggregateException', N'', CAST(N'2022-10-07T06:10:04.783' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (117, 2, N'Peak lens for mountains is awesome. I need in my glasses... ', N'https://play.google.com/store/apps/details?id=com.peaklens.ar', CAST(N'2022-10-08T07:22:14.297' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (118, 2, N'return string from http call :
 const opt = {
      responseType: ''text'' as const,
    };   ', N'https://angular.io/guide/http', CAST(N'2022-10-09T06:48:04.577' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (119, 2, N'you can inject everything in .net core- even an SqlClient', N'https://erikej.github.io/dotnet/sqlclient/2022/09/06/sqlclient-di-dotnet.html', CAST(N'2022-10-11T03:30:56.250' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (120, 2, N'7 terminals, 2k tests, Console.ReadKey', N'https://devblogs.microsoft.com/dotnet/console-readkey-improvements-in-net-7/', CAST(N'2022-10-12T06:48:03.253' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (121, 2, N'Letter to future me - bright idea', N'https://www.futureme.org/', CAST(N'2022-10-13T03:31:57.253' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (122, 2, N'Big Data is an umbrella - reporting, analysis, cloud ... and many more', N'', CAST(N'2022-10-13T21:01:08.050' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (123, 2, N'I should start learn ML', N'https://github.com/microsoft/ML-For-Beginners', CAST(N'2022-10-14T21:18:29.270' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (124, 2, N'learning about refactoring', N'https://martinfowler.com/bliki/OpportunisticRefactoring.html', CAST(N'2022-10-16T08:28:08.880' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (125, 2, N'new release means new incompatibilities ', N'', CAST(N'2022-10-17T10:28:57.907' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (126, 2, N'roadmap to become frontend or backend or anything. Pretty easy', N'https://roadmap.sh/', CAST(N'2022-10-18T13:11:56.797' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (127, 2, N'react is really a library, not a framework . But can become by adding data', N'https://reactjs.org/', CAST(N'2022-10-20T06:40:42.843' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (128, 2, N'I miss http client interceptors from Angular in React', N'', CAST(N'2022-10-21T12:18:01.090' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (129, 2, N'I miss navbar component from Angular. In React, even with MUI, is PIA', N'', CAST(N'2022-10-22T07:18:15.323' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (130, 2, N'react router- more examples will be better. However, documentation by example is good.', N'https://github.com/remix-run/react-router/tree/main/examples', CAST(N'2022-10-23T19:25:36.547' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (131, 2, N'A tsx function in react looks better ( if not too much GUI) rather than Angular separation. ', N'', CAST(N'2022-10-23T21:39:51.780' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (132, 2, N'the problem with multiple frameworks is that not all examples are the same', N'', CAST(N'2022-10-29T07:06:51.850' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (133, 2, N'Library vs Framework: it is simpler to decide with frameworks= you have it all', N'', CAST(N'2022-10-30T14:59:21.133' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (134, 2, N'From Stefan Judis: git check-ignore -v path/to/a/file.png', N'https://tinytip.co/tips/git-check-ignore-rule/', CAST(N'2022-10-31T09:57:02.410' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (135, 2, N'if you redo the same project in other framework, you can see tghe problems and improve', N'', CAST(N'2022-11-01T18:43:27.060' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (136, 2, N'In React MUI, the toolbar is called app bar', N'https://mui.com/material-ui/react-app-bar/', CAST(N'2022-11-02T17:52:37.767' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (137, 2, N'Ajax from rxjs is powerfull. Just have  return AjaxResponse<T> - and it is converted automatically', N'', CAST(N'2022-11-05T08:41:48.727' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (138, 2, N'rxjs Subject is formidable to transmit data between not related items in react', N'', CAST(N'2022-11-07T17:09:16.943' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (139, 2, N'in REACT just add <> to add dynamic children to a component', N'', CAST(N'2022-11-10T18:58:02.613' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (140, 2, N'for react routes, do not put a / in front:  <Route path="contacts/:id" element={<App />} />', N'', CAST(N'2022-11-12T07:23:06.563' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (141, 2, N'the programmer work these days: learn frameworks', N'', CAST(N'2022-11-13T18:30:40.993' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (142, 2, N'If the SC (git) is on a Linux , and you are in Windows, the case sensivity of folders ... great!', N'', CAST(N'2022-11-14T17:35:56.913' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (143, 2, N'For React the fact that in CI warnings are  errors - well , "it works on my PC"will be harsh', N'', CAST(N'2022-11-15T10:15:27.237' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (144, 2, N'OKR - really a good book', N'https://www.amazon.com/Measure-What-Matters-Google-Foundation/dp/0525536221', CAST(N'2022-11-22T22:29:28.297' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (145, 2, N'Oh, not again repository pattern....', N'https://blog.logrocket.com/exploring-repository-pattern-typescript-node/', CAST(N'2022-11-25T12:46:34.700' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (146, 2, N'it is difficult to learn something new every day. But I can try', N'', CAST(N'2022-11-27T07:51:39.317' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (147, 2, N'There are public free API : https://github.com/public-api-lists/public-api-lists', N'https://github.com/public-api-lists/public-api-lists', CAST(N'2022-11-28T12:58:37.363' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (148, 2, N'WSL for WIndows10 now can run GUI app ( install latest version for both Windows  and WSL) ', N'https://github.com/microsoft/wslg', CAST(N'2022-11-29T10:51:46.943' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (149, 2, N'Free tier for html is on the rage ', N'https://www.freecodecamp.org/news/deploy-react-app/', CAST(N'2022-12-01T12:25:19.877' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (150, 2, N'YUP and ZOD - validating data received from HTTP API', N'https://blog.logrocket.com/comparing-schema-validation-libraries-zod-vs-yup/', CAST(N'2022-12-02T13:09:11.580' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (151, 2, N'when you do not know TypeScript, you invent it :
const showCounter = useBoolean(true);', N'https://github.com/kitze/react-hanger', CAST(N'2022-12-03T09:32:33.537' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (152, 2, N'When you do not want to learn rxjs', N'https://ahooks.js.org/hooks/use-debounce', CAST(N'2022-12-04T07:51:46.457' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (153, 2, N'if xhr request to same site, Edge sends "Origin" header  - Chrome does not!', N'', CAST(N'2022-12-04T22:11:37.433' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (154, 2, N'"In teamwork, silence isn''t golden, it''s deadly." -Mark Sanborn', N'', CAST(N'2022-12-06T12:31:27.283' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (155, 2, N'50 times deploy per day- seems a lot', N'https://www.infoq.com/news/2014/03/etsy-deploy-50-times-a-day/', CAST(N'2022-12-07T13:18:57.933' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (156, 2, N'read always breaking changes for a framework', N'https://learn.microsoft.com/en-us/dotnet/core/compatibility/7.0', CAST(N'2022-12-10T17:11:48.367' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (157, 2, N'Github trends - interesting ', N'https://www.githubtrends.io/wrapped/ignatandrei', CAST(N'2022-12-11T09:32:32.227' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (158, 2, N'You can alias in csproj', N'https://twitter.com/SimonCropp/status/1602135637582544897', CAST(N'2022-12-13T13:52:40.740' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (159, 2, N'typescript utils https://github.com/sindresorhus/type-fest & https://github.com/millsp/ts-toolbelt', N'', CAST(N'2022-12-15T10:34:34.653' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (160, 2, N'ChatGPT is awespme for search - and generation', N'https://chat.openai.com/auth/login', CAST(N'2022-12-18T08:12:55.593' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (161, 2, N'a static code analyzer is worthy', N'https://pvs-studio.com/en/blog/posts/csharp/1015/', CAST(N'2022-12-19T04:55:21.030' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (162, 2, N'debugging T4 templates from EFCore= plain old Console.WriteLine', N'https://learn.microsoft.com/en-us/ef/core/managing-schemas/scaffolding/templates', CAST(N'2022-12-22T16:37:39.907' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (163, 2, N'For .NET CLI , Spectre is awesone', N'https://spectreconsole.net/', CAST(N'2022-12-23T19:26:34.597' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (164, 2, N'out-of-support IE 11 desktop  is scheduled to be permanently disabled on February 14, 2023', N'https://learn.microsoft.com/en-us/lifecycle/products/internet-explorer-11', CAST(N'2022-12-25T05:12:32.003' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (165, 2, N'GenerationEnvironment can be replaced in t4 file(with another stringbuilder) to generate more texts', N'', CAST(N'2022-12-26T15:26:19.637' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (166, 2, N'Meetings  with more 2 people are like a bug', N'https://twitter.com/CanadaKaz/status/1610274381267099650', CAST(N'2023-01-04T09:31:18.540' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (167, 2, N'So many things to do with typescript', N'https://www.learningtypescript.com/articles/extreme-explorations-of-typescripts-type-system', CAST(N'2023-01-07T22:01:40.083' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (168, 2, N'simple tree view wtih just css - can be done', N'https://iamkate.com/code/tree-views/', CAST(N'2023-01-08T23:22:23.247' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (169, 2, N'In React to autoexecute at start with a parameter , you can have a function with  useEffect ', N'', CAST(N'2023-01-10T10:57:15.953' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (170, 2, N'to add files to a nuget , just add in csproj 		<None Include="w/**" Pack="true" PackagePath="w" />
', N'', CAST(N'2023-01-11T13:06:35.190' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (171, 2, N'CI/CD is a job per se. It was taking some time to automatically deploy to nuget', N'', CAST(N'2023-01-26T15:17:13.260' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (172, 2, N'in React you can alter the style directly: style={{width: blocklyWidth}}', N'', CAST(N'2023-01-28T06:27:41.553' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (173, 2, N'Angular Material vs React Mui: dropdown menu vs button group: 1 line vs 110 lines....', N'https://mui.com/material-ui/react-button-group/', CAST(N'2023-01-29T08:39:18.687' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (174, 2, N'React has  for HTML
%PUBLIC_URL%
and for typescript
process.env.PUBLIC_URL
process.env.PUBLIC_URL ', N'https://create-react-app.dev/docs/using-the-public-folder/#adding-assets-outside-of-the-module-system', CAST(N'2023-01-30T08:21:46.157' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (175, 2, N'maybe we need nothing but good tested code', N'https://github.com/you-dont-need/You-Dont-Need-Lodash-Underscore#readme', CAST(N'2023-02-04T18:08:28.333' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (176, 2, N'HOC in JS vs HOC in TS : so much declarations!', N'https://javascript.plainenglish.io/5-react-design-patterns-you-should-know-629030e2e2c7', CAST(N'2023-02-06T05:58:21.873' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (177, 2, N'All about state', N'https://www.worldofbs.com/minimize-state/', CAST(N'2023-02-07T09:07:11.147' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (178, 2, N'Minimal API has WithOpenAPi', N'https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/openapi?view=aspnetcore-7.0', CAST(N'2023-02-10T17:38:53.943' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (179, 2, N'practical use of Sqlite in the browser: ', N'https://blog.kebab-ca.se/chapters/2023-02-12-sqlite-wasm/overview.html', CAST(N'2023-02-14T07:45:39.170' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (180, 2, N'Too many slides frameworks', N'https://github.com/astefanutti/decktape', CAST(N'2023-02-19T22:00:14.907' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (181, 2, N'Tried with npx depcruise --init 
and 
npx depcruise src --validate --output-type err-html -f r.html', N'https://github.com/sverweij/dependency-cruiser/', CAST(N'2023-02-21T16:18:14.363' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (182, 2, N'Nice return types', N'https://developer.mozilla.org/en-US/docs/Web/API/HTMLMediaElement/canPlayType', CAST(N'2023-02-23T08:43:20.673' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (183, 2, N'for django  to serve static files from a folder, there is something special : whitenoise', N'https://whitenoise.evans.io/en/stable/django.html', CAST(N'2023-02-25T06:31:07.417' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (184, 2, N'Should I learn more css? ', N'https://andy-bell.co.uk/my-favourite-3-lines-of-css/', CAST(N'2023-02-26T06:30:22.257' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (185, 2, N'for swagger in .NET core, you need ApiController and Route attribute', N'', CAST(N'2023-02-27T19:24:06.867' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (186, 2, N'react is a framework! 
VITE:import.meta.env.BASE_URL
CREATE-REACT-APP:process.env.PUBLIC_URL', N'', CAST(N'2023-03-03T10:07:39.507' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (187, 2, N'Javascript 2 undefined,1 exists
typeof process
typeof process.env 
typeof process.env.PUBLIC_URL 
', N'https://create-react-app.dev/docs/using-the-public-folder/', CAST(N'2023-03-04T06:48:31.873' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (188, 2, N'Powershell: find the difference: $x = $x -replace "..\a\\",''..\a\''', N'', CAST(N'2023-03-11T20:54:17.300' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (189, 2, N'Fun again with React const [isLoading, error, data]= useRxObs(()=>new DBAdmin().getDatabases());
  ', N'', CAST(N'2023-03-16T11:48:25.970' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (190, 2, N'my windows explorer shortcut is wt -d . instead of cmd ', N'', CAST(N'2023-03-19T10:20:25.833' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (191, 2, N'create react app is dead. Long Live React and the frameworks( Next is the first mentioned now )', N'https://react.dev/learn/start-a-new-react-project#framework-popular-alternatives', CAST(N'2023-03-20T06:44:00.187' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (192, 2, N'react router: useLocation vs UseMatches', N'', CAST(N'2023-03-21T06:01:11.207' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (193, 2, N'Sometimes you need just once. use-effect-once', N'https://usehooks-ts.com/react-hook/use-effect-once', CAST(N'2023-03-22T06:55:20.390' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (194, 2, N'backwards compatibility in node - a good joke( angular 12 with node 18 ....)', N'', CAST(N'2023-03-25T05:15:45.947' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (195, 2, N'utils for Test: assertion library and BDD. https://fluentassertions.com/', N'https://github.com/LightBDD/LightBDD', CAST(N'2023-03-26T00:34:18.100' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (196, 2, N'Stages :
1.Verbose A a = new A();
2.Compiler var a=new A();
3. Human: A a=new()', N'', CAST(N'2023-04-01T07:49:31.413' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (197, 2, N'I do like shorthand notations : opt ??=new ShortUrlOptions();', N'', CAST(N'2023-04-03T19:20:32.507' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (198, 2, N'Every time, very usefull', N'https://github.com/xt0rted/dotnet-run-script', CAST(N'2023-04-04T07:43:17.513' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (199, 2, N'XEEvents for any error: put >10 ', N'https://learn.microsoft.com/en-us/sql/relational-databases/extended-events/use-the-system-health-session', CAST(N'2023-04-05T13:00:27.870' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (200, 2, N'dotnet tool for test SMTP', N'https://www.nuget.org/packages/Rnwood.Smtp4dev/', CAST(N'2023-04-06T19:34:36.057' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (201, 2, N'for plugins in .NET Core, https://github.com/natemcmaster/DotNetCorePlugins', N'https://learn.microsoft.com/en-us/dotnet/core/tutorials/creating-app-with-plugin-support', CAST(N'2023-04-07T13:59:56.637' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (202, 2, N'Every software , after architecturing , has a pain point about writing more code ...', N'', CAST(N'2023-04-09T08:17:39.650' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (203, 2, N'ng generate environments vs ', N'https://dev.to/thisdotmedia/runtime-environment-configuration-with-angular-4f5j', CAST(N'2023-04-10T11:45:04.540' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (204, 2, N'DI INamedOptions in ASP.NET Core', N'https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-7.0', CAST(N'2023-04-12T06:00:01.873' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (205, 2, N'i do like if in React, if this way', N'https://github.com/romac/react-if', CAST(N'2023-04-13T05:58:45.453' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (206, 2, N'In angular child routes, do not define parnet. Define child with ''''', N'', CAST(N'2023-04-14T18:25:01.863' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (207, 2, N'dynamic loading in Angular is tough. In React, being JS, it is easy', N'', CAST(N'2023-04-18T05:19:52.247' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (208, 2, N'sometimes, StartInfo.UseShellExecute is what you need', N'', CAST(N'2023-04-19T15:28:08.253' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (209, 2, N'update from command line : winget upgrade -r', N'', CAST(N'2023-04-21T13:07:26.013' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (210, 2, N'psr is awesome for generating fast help', N'https://support.microsoft.com/en-us/windows/record-steps-to-reproduce-a-problem-46582a9b-620f-2e36-00c9-04e25d784e47', CAST(N'2023-04-23T18:52:08.407' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (211, 2, N'ADR is one step to architecture', N'https://www.hascode.com/2018/05/managing-architecture-decision-records-with-adr-tools/', CAST(N'2023-04-25T05:17:11.947' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (212, 2, N'podman + winget: winget install -e --id RedHat.Podman-Desktop', N'https://podman-desktop.io/downloads', CAST(N'2023-04-26T07:06:04.050' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (213, 2, N'ScriptDOM for Sql', N'https://techcommunity.microsoft.com/t5/azure-sql-blog/scriptdom-net-library-for-t-sql-parsing-is-now-open-source/ba-p/3804284', CAST(N'2023-04-29T07:09:35.050' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (214, 2, N'RSCG advanved . To be learned for performance', N'https://github.com/dotnet/roslyn/blob/main/docs/features/incremental-generators.md', CAST(N'2023-04-30T07:16:51.860' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (215, 2, N'split string after \r or \n or both. Not Environment.NewLine. And remove empty lines', N'', CAST(N'2023-05-02T17:07:01.220' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (216, 2, N'Instead of doing git commit in github actions', N'https://github.com/marketplace/actions/add-commit', CAST(N'2023-05-08T17:52:24.767' AS DateTime), N'Europe/Bucharest')
GO
INSERT [dbo].[TILT_Note] ([ID], [IDURL], [Text], [Link], [ForDate], [TimeZoneString]) VALUES (217, 2, N'See each day 50k+  NAMED errors in production - that''s DevOps investigation ;-)', N'', CAST(N'2023-05-17T05:58:23.690' AS DateTime), N'Europe/Bucharest')
GO
SET IDENTITY_INSERT [dbo].[TILT_Note] OFF
GO
SET IDENTITY_INSERT [dbo].[TILT_URL] ON 
GO
INSERT [dbo].[TILT_URL] ([ID], [URLPart], [Secret]) VALUES (1, N'test', N'test')
GO
INSERT [dbo].[TILT_URL] ([ID], [URLPart], [Secret]) VALUES (2, N'ignatandrei', N'qwe123')
GO
INSERT [dbo].[TILT_URL] ([ID], [URLPart], [Secret]) VALUES (9, N'bogdanBobe', N'test')
GO
INSERT [dbo].[TILT_URL] ([ID], [URLPart], [Secret]) VALUES (11, N'martino', N'marconi')
GO
INSERT [dbo].[TILT_URL] ([ID], [URLPart], [Secret]) VALUES (13, N'daniel', N'paroladetest@123')
GO
INSERT [dbo].[TILT_URL] ([ID], [URLPart], [Secret]) VALUES (14, N'ignatan', N'qwe123')
GO
SET IDENTITY_INSERT [dbo].[TILT_URL] OFF
GO
ALTER TABLE [dbo].[TILT_Note]  WITH CHECK ADD  CONSTRAINT [FK_TILT_Note_TILT_URL] FOREIGN KEY([IDURL])
REFERENCES [dbo].[TILT_URL] ([ID])
GO
ALTER TABLE [dbo].[TILT_Note] CHECK CONSTRAINT [FK_TILT_Note_TILT_URL]
GO
ALTER TABLE [dbo].[TILT_Tag_Note]  WITH CHECK ADD  CONSTRAINT [FK_TILT_Tag_Note_TILT_Note] FOREIGN KEY([IDNote])
REFERENCES [dbo].[TILT_Note] ([ID])
GO
ALTER TABLE [dbo].[TILT_Tag_Note] CHECK CONSTRAINT [FK_TILT_Tag_Note_TILT_Note]
GO
ALTER TABLE [dbo].[TILT_Tag_Note]  WITH CHECK ADD  CONSTRAINT [FK_TILT_Tag_Note_TILT_Tag] FOREIGN KEY([IDTag])
REFERENCES [dbo].[TILT_Tag] ([ID])
GO
ALTER TABLE [dbo].[TILT_Tag_Note] CHECK CONSTRAINT [FK_TILT_Tag_Note_TILT_Tag]
GO
ALTER DATABASE [tiltdb] SET  READ_WRITE 
GO
