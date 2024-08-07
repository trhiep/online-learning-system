USE [master]
GO
/****** Object:  Database [OLS_DB]    Script Date: 7/22/2024 12:33:16 PM ******/
CREATE DATABASE [OLS_DB]
GO
USE [OLS_DB]
GO
ALTER DATABASE [OLS_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OLS_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OLS_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OLS_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OLS_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [OLS_DB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [OLS_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OLS_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OLS_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OLS_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OLS_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OLS_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OLS_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OLS_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OLS_DB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [OLS_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OLS_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OLS_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OLS_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OLS_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OLS_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OLS_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OLS_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OLS_DB] SET  MULTI_USER 
GO
ALTER DATABASE [OLS_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OLS_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OLS_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OLS_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OLS_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OLS_DB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OLS_DB] SET QUERY_STORE = OFF
GO
USE [OLS_DB]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Password] [varchar](255) NOT NULL,
	[Fullname] [nvarchar](255) NOT NULL,
	[Email] [varchar](255) NOT NULL,
	[Role] [varchar](15) NOT NULL,
	[Status] [bit] NOT NULL,
 CONSTRAINT [Account_pk] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Answer]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Answer](
	[QuestionAnswerID] [int] IDENTITY(1,1) NOT NULL,
	[QuestionID] [int] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
	[IsCorrectAnswer] [bit] NOT NULL,
 CONSTRAINT [Answer_pk] PRIMARY KEY CLUSTERED 
(
	[QuestionAnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classroom]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classroom](
	[ClassID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](100) NOT NULL,
	[CreateBy] [int] NOT NULL,
	[FormTeacherID] [int] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [Classroom_pk] PRIMARY KEY CLUSTERED 
(
	[ClassID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassStudent]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassStudent](
	[ClassStudentID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NOT NULL,
	[ClassID] [int] NOT NULL,
 CONSTRAINT [ClassStudent_pk] PRIMARY KEY CLUSTERED 
(
	[ClassStudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassSubject]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassSubject](
	[ClassSubjectID] [int] IDENTITY(1,1) NOT NULL,
	[SubjectID] [int] NOT NULL,
	[ClassID] [int] NOT NULL,
	[SubjectTeacher] [int] NOT NULL,
 CONSTRAINT [ClassSubject_pk] PRIMARY KEY CLUSTERED 
(
	[ClassSubjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassSubjectPost]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassSubjectPost](
	[ClassSubjectID] [int] NOT NULL,
	[PostID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[PostedDate] [datetime] NOT NULL,
 CONSTRAINT [ClassSubjectPost_pk] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ClassSubjectTest]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ClassSubjectTest](
	[TestID] [int] IDENTITY(1,1) NOT NULL,
	[CLassSubjectID] [int] NOT NULL,
	[TestName] [nvarchar](100) NOT NULL,
	[TestDescription] [nvarchar](max) NULL,
	[Attempts] [int] NOT NULL,
	[Duration] [real] NOT NULL,
	[PassScore] [real] NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
 CONSTRAINT [ClassSubjectTest_pk] PRIMARY KEY CLUSTERED 
(
	[TestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ConversationMember]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ConversationMember](
	[ConversationMemberID] [int] IDENTITY(1,1) NOT NULL,
	[AccountID] [int] NOT NULL,
	[ConversationID] [int] NOT NULL,
 CONSTRAINT [ConversationMember_pk] PRIMARY KEY CLUSTERED 
(
	[ConversationMemberID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CoversationMessage]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoversationMessage](
	[MessageID] [int] IDENTITY(1,1) NOT NULL,
	[ConversationID] [int] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[SendTime] [datetime] NOT NULL,
	[SendByID] [int] NOT NULL,
 CONSTRAINT [CoversationMessage_pk] PRIMARY KEY CLUSTERED 
(
	[MessageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notification]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[NotificationID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [Notification_pk] PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NotificationOfAccount]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotificationOfAccount](
	[NotificationID] [int] NOT NULL,
	[To] [int] NOT NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [NotificationOfAccount_pk] PRIMARY KEY CLUSTERED 
(
	[NotificationID] ASC,
	[To] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostAttachment]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostAttachment](
	[PostAttachmentID] [int] NOT NULL,
	[PostID] [int] NOT NULL,
	[AttachmentLink] [nvarchar](max) NOT NULL,
 CONSTRAINT [PostAttachment_pk] PRIMARY KEY CLUSTERED 
(
	[PostAttachmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PostComment]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostComment](
	[PostID] [int] NOT NULL,
	[CommentID] [bigint] IDENTITY(1,1) NOT NULL,
	[PostedBy] [int] NOT NULL,
	[CommentContent] [nvarchar](max) NOT NULL,
	[FatherID] [int] NULL,
 CONSTRAINT [PostComment_pk] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentTestAnswer]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTestAnswer](
	[StudentTestAnswerID] [int] IDENTITY(1,1) NOT NULL,
	[StudentID] [int] NOT NULL,
	[TestID] [int] NOT NULL,
	[TestQuestionID] [int] NOT NULL,
	[SelectedAnswerID] [int] NOT NULL,
	[AnswerTime] [datetime] NOT NULL,
	[AttemptNo] [int] NOT NULL,
 CONSTRAINT [StudentTestAnswer_pk] PRIMARY KEY CLUSTERED 
(
	[StudentTestAnswerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[SubjectID] [int] IDENTITY(1,1) NOT NULL,
	[SubjectName] [nvarchar](100) NOT NULL,
 CONSTRAINT [Subject_pk] PRIMARY KEY CLUSTERED 
(
	[SubjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubjectConversation]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubjectConversation](
	[ConversationID] [int] IDENTITY(1,1) NOT NULL,
	[ClassSubjectID] [int] NOT NULL,
	[GroupChatName] [nvarchar](100) NOT NULL,
 CONSTRAINT [SubjectConversation_pk] PRIMARY KEY CLUSTERED 
(
	[ConversationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TestQuestion]    Script Date: 7/22/2024 12:33:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TestQuestion](
	[TestID] [int] NOT NULL,
	[TestQuestionID] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[LastModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [TestQuestion_pk] PRIMARY KEY CLUSTERED 
(
	[TestQuestionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (1, N'tuanballboo', N'tuananh235', N'Hoàng Tuấn Anh', N'tuanballboo@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (4, N'admin', N'admin', N'Hoàng Tuấn Anh', N'tuanballboo1@gmail.com', N'Admin', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (5, N'teacher', N'teacher', N'Hoàng Tuấn Anh', N'tuanballbooo@gmail.com', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (6, N'hieptv', N'1', N'Trần Hoàng Hiệp', N'hieptvhe173252@fpt.edu.vn', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (7, N'hieptv2', N'1', N'Trần Văn Hiệp', N'hieptvhe173252@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (8, N'lamdt', N'lamdt', N'Doãn Tùng Lâm', N'dtlam122003@gmail.com', N'Admin', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (9, N'kinlala', N'kinlala', N'Kin Lalaman', N'kinlalaman123@gmail.com', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (10, N'kinjackson', N'kinjackson', N'Kin Jackson', N'kinjack321@gmail.com', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (11, N'johnwick', N'johnwick', N'John Wick', N'johnwick345@gmail.com', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (12, N'gordonramsdey', N'gordonramsdey', N'Gordon Ramsdey', N'gordonramsdey101@gmail.com', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (13, N'pepguardiola', N'pepguardiola', N'Pep Guardiola', N'pepguardiola222@gmail.com', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (14, N'huongnt7', N'huongnt7', N'Nguyễn Thuý Hường', N'HuongNT135@fe.edu.vn', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (15, N'sangnv', N'sangnv', N'Nguyễn Văn Sang', N'sangnv@fe.edu.vn', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (16, N'khuongpd', N'khuongpd', N'Phùng Duy Khương', N'KhuongPD5@fe.edu.vn', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (17, N'HuongDTT37', N'HuongDTT37', N'Đoàn Thị Thanh Hương', N'HuongDTT37@fe.edu.vn
', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (18, N'kiennt', N'kiennt', N'Nguyễn Trung Kiên', N'KienNT@fe.edu.vn', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (19, N'anhttv20', N'anhttv20', N'Trịnh Thị Vân Anh', N'AnhTTV20@fe.edu.vn', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (20, N'hoaptt8', N'hoaptt8', N'Phạm Thị Thanh Hoa', N'hoaptt8@fe.edu.vn', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (21, N'thulx', N'thulx', N'Lại Xuân Thu', N'hoaptt8@fe.edu.vn', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (22, N'hadtt', N'hadtt', N'Đỗ Thị Thu Hà', N'HaDTT13@fe.edu.vn', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (23, N'AnhKD', N'AnhKD', N'Khuất Đức Anh', N'AnhKD3@fe.edu.vn', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (24, N'trungnt', N'trungnt', N'Nguyễn Tất Trung', N'TrungNT77@fe.edu.vn', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (25, N'ThangNT44', N'ThangNT44', N'Nguyễn Tất Thắng', N'ThangNT44@fe.edu.vn', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (26, N'thunv', N'1234', N'Nguyễn Việt Thu', N'thunvhe176252@fpt.edu.vn', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (27, N'hieu', N'1', N'HIeu', N'hieu', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (28, N'hieuTeacher', N'1', N'hieuTeacher', N'hieuTeacher', N'Teacher', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (29, N'hieuAdmin', N'1', N'hieuAdmin', N'hieuAdmin', N'Admin', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (30, N'hieptv3', N'1', N'Tran Hiep', N'hieptran.pa@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (32, N'test', N'abcd1234', N'Trịnh Minh Anh', N'tuanbgala2wf@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (46, N'test', N'abcd1234', N'Hoàng Tuann Anh', N'tuangag@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (48, N'anhha', N'abcd1234', N'Hoàng aTuấn Anh', N'tua3f@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (49, N'anhht', N'abcd1234', N'Hoàng Tuann Anh', N'tuangag@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (50, N'anhtm', N'abcd1234', N'Trịnh Minh Anh', N'tuanwf@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (51, N'anhha', N'abcd1234', N'Hoàng aTuấn Anh', N'tua3f@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (52, N'anhht', N'abcd1234', N'Hoàng Tuann Anh', N'tuangag@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (53, N'anhtm', N'abcd1234', N'Trịnh Minh Anh', N'tuanwf@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (54, N'anhha4909240093973321593', N'abcd1234', N'Hoàng aTuấn Anh', N'tua3f@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (55, N'anhht6799089399223773359', N'abcd1234', N'Hoàng Tuann Anh', N'tuangag@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (56, N'anhtm2880741610683266274', N'abcd1234', N'Trịnh Minh Anh', N'tuanwf@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (57, N'anhha3', N'abcd1234', N'Hoàng aTuấn Anh', N'tua3f@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (58, N'anhht2', N'abcd1234', N'Hoàng Tuann Anh', N'tuangag@gmail.com', N'Student', 1)
INSERT [dbo].[Account] ([AccountID], [Username], [Password], [Fullname], [Email], [Role], [Status]) VALUES (59, N'anhtm1', N'abcd1234', N'Trịnh Minh Anh', N'tuanwf@gmail.com', N'Student', 1)
SET IDENTITY_INSERT [dbo].[Account] OFF
GO
SET IDENTITY_INSERT [dbo].[Answer] ON 

INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (1, 1, N'TL1', CAST(N'2024-07-21T16:34:00.253' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (2, 1, N'TL2', CAST(N'2024-07-21T16:34:00.480' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (3, 1, N'TL3', CAST(N'2024-07-21T16:34:00.693' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (4, 2, N'TL1', CAST(N'2024-07-21T16:34:01.400' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (5, 2, N'TL2', CAST(N'2024-07-21T16:34:01.900' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (6, 2, N'TL3', CAST(N'2024-07-21T16:34:02.110' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (7, 3, N'TL1', CAST(N'2024-07-21T16:34:02.817' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (8, 3, N'TL2', CAST(N'2024-07-21T16:34:03.027' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (9, 3, N'TL3', CAST(N'2024-07-21T16:34:03.517' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (10, 4, N'Trả lời 1', CAST(N'2024-07-21T16:34:03.937' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (11, 4, N'Trả lời 2', CAST(N'2024-07-21T16:34:04.147' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (12, 4, N'Trả lời 3', CAST(N'2024-07-21T16:34:04.353' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (13, 5, N'Trả lời 1', CAST(N'2024-07-21T16:34:04.773' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (14, 5, N'Trả lời 2', CAST(N'2024-07-21T16:34:05.257' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (15, 5, N'Trả lời 3', CAST(N'2024-07-21T16:34:05.463' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (16, 6, N'CH51', CAST(N'2024-07-21T16:34:05.880' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (17, 6, N'CH52', CAST(N'2024-07-21T16:34:06.097' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (18, 6, N'CH53', CAST(N'2024-07-21T16:34:06.307' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (19, 7, N'CH61', CAST(N'2024-07-21T16:34:07.287' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (20, 7, N'CH62', CAST(N'2024-07-21T16:34:07.500' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (21, 8, N'CH71', CAST(N'2024-07-21T16:34:07.920' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (22, 8, N'CH72', CAST(N'2024-07-21T16:34:08.140' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (23, 9, N'CH81', CAST(N'2024-07-21T16:34:08.837' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (24, 9, N'CH82', CAST(N'2024-07-21T16:34:09.047' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (25, 10, N'Câu trả lời 1', CAST(N'2024-07-21T16:34:09.733' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (26, 10, N'Câu trả lời 2', CAST(N'2024-07-21T16:34:09.943' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (27, 10, N'Câu trả lời 3', CAST(N'2024-07-21T16:34:10.157' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (28, 11, N'CH101', CAST(N'2024-07-21T16:34:10.583' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (29, 11, N'CH102', CAST(N'2024-07-21T16:34:11.060' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (30, 11, N'CH103', CAST(N'2024-07-21T16:34:11.270' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (31, 11, N'CH104', CAST(N'2024-07-21T16:34:11.480' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (32, 12, N'CH111', CAST(N'2024-07-21T16:34:11.900' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (33, 12, N'CH112', CAST(N'2024-07-21T16:34:12.393' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (34, 12, N'CH113', CAST(N'2024-07-21T16:34:12.877' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (35, 12, N'CH114', CAST(N'2024-07-21T16:34:13.087' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (36, 12, N'CH115', CAST(N'2024-07-21T16:34:13.870' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (37, 13, N'CH121', CAST(N'2024-07-21T16:34:14.290' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (38, 13, N'CH122', CAST(N'2024-07-21T16:34:14.777' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (39, 13, N'CH123', CAST(N'2024-07-21T16:34:15.263' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (40, 13, N'CH124', CAST(N'2024-07-21T16:34:15.470' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (41, 14, N'CH131', CAST(N'2024-07-21T16:34:16.167' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (42, 14, N'CH132', CAST(N'2024-07-21T16:34:16.377' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (43, 14, N'CH133', CAST(N'2024-07-21T16:34:16.863' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (44, 14, N'CH134', CAST(N'2024-07-21T16:34:17.073' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (45, 15, N'Paris', CAST(N'2024-07-21T16:34:17.490' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (46, 15, N'Berlin', CAST(N'2024-07-21T16:34:17.700' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (47, 15, N'London', CAST(N'2024-07-21T16:34:17.910' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (48, 16, N'3', CAST(N'2024-07-21T16:34:19.157' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (49, 16, N'4', CAST(N'2024-07-21T16:34:19.363' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (50, 16, N'5', CAST(N'2024-07-21T16:34:19.903' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (51, 17, N'TL1', CAST(N'2024-07-21T20:36:54.197' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (52, 17, N'TL2', CAST(N'2024-07-21T20:36:54.413' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (53, 17, N'TL3', CAST(N'2024-07-21T20:36:54.617' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (54, 18, N'TL1', CAST(N'2024-07-21T20:36:55.037' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (55, 18, N'TL2', CAST(N'2024-07-21T20:36:55.527' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (56, 18, N'TL3', CAST(N'2024-07-21T20:36:55.743' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (57, 19, N'TL1', CAST(N'2024-07-21T20:36:56.157' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (58, 19, N'TL2', CAST(N'2024-07-21T20:36:56.363' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (59, 19, N'TL3', CAST(N'2024-07-21T20:36:56.563' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (60, 20, N'Trả lời 1', CAST(N'2024-07-21T20:36:57.247' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (61, 20, N'Trả lời 2', CAST(N'2024-07-21T20:36:57.450' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (62, 20, N'Trả lời 3', CAST(N'2024-07-21T20:36:57.657' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (63, 21, N'Trả lời 1', CAST(N'2024-07-21T20:36:58.340' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (64, 21, N'Trả lời 2', CAST(N'2024-07-21T20:36:58.547' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (65, 21, N'Trả lời 3', CAST(N'2024-07-21T20:36:58.760' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (66, 22, N'CH51', CAST(N'2024-07-21T20:36:59.450' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (67, 22, N'CH52', CAST(N'2024-07-21T20:36:59.923' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (68, 22, N'CH53', CAST(N'2024-07-21T20:37:00.733' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (69, 23, N'CH61', CAST(N'2024-07-21T20:37:01.150' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (70, 23, N'CH62', CAST(N'2024-07-21T20:37:01.623' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (71, 24, N'CH71', CAST(N'2024-07-21T20:37:02.583' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (72, 24, N'CH72', CAST(N'2024-07-21T20:37:02.790' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (73, 25, N'CH81', CAST(N'2024-07-21T20:37:03.480' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (74, 25, N'CH82', CAST(N'2024-07-21T20:37:04.253' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (75, 26, N'Câu trả lời 1', CAST(N'2024-07-21T20:37:05.917' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (76, 26, N'Câu trả lời 2', CAST(N'2024-07-21T20:37:06.403' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (77, 26, N'Câu trả lời 3', CAST(N'2024-07-21T20:37:06.623' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (78, 27, N'CH101', CAST(N'2024-07-21T20:37:07.023' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (79, 27, N'CH102', CAST(N'2024-07-21T20:37:07.233' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (80, 27, N'CH103', CAST(N'2024-07-21T20:37:07.453' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (81, 27, N'CH104', CAST(N'2024-07-21T20:37:07.657' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (82, 28, N'CH111', CAST(N'2024-07-21T20:37:08.350' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (83, 28, N'CH112', CAST(N'2024-07-21T20:37:08.820' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (84, 28, N'CH113', CAST(N'2024-07-21T20:37:09.297' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (85, 28, N'CH114', CAST(N'2024-07-21T20:37:09.780' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (86, 28, N'CH115', CAST(N'2024-07-21T20:37:10.257' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (87, 29, N'CH121', CAST(N'2024-07-21T20:37:10.937' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (88, 29, N'CH122', CAST(N'2024-07-21T20:37:11.687' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (89, 29, N'CH123', CAST(N'2024-07-21T20:37:11.897' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (90, 29, N'CH124', CAST(N'2024-07-21T20:37:12.107' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (91, 30, N'CH131', CAST(N'2024-07-21T20:37:12.843' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (92, 30, N'CH132', CAST(N'2024-07-21T20:37:13.317' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (93, 30, N'CH133', CAST(N'2024-07-21T20:37:13.530' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (94, 30, N'CH134', CAST(N'2024-07-21T20:37:13.737' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (95, 31, N'Paris', CAST(N'2024-07-21T20:37:14.690' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (96, 31, N'Berlin', CAST(N'2024-07-21T20:37:15.177' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (97, 31, N'London', CAST(N'2024-07-21T20:37:15.657' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (98, 32, N'3', CAST(N'2024-07-21T20:37:16.357' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (99, 32, N'4', CAST(N'2024-07-21T20:37:16.837' AS DateTime), 1)
GO
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (100, 32, N'5', CAST(N'2024-07-21T20:37:17.627' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (101, 33, N'Paris', CAST(N'2024-07-21T22:18:54.240' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (102, 33, N'Berlin', CAST(N'2024-07-21T22:18:54.460' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (103, 33, N'London', CAST(N'2024-07-21T22:18:54.680' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (104, 34, N'3', CAST(N'2024-07-21T22:18:55.107' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (105, 34, N'4', CAST(N'2024-07-21T22:18:55.323' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (106, 34, N'5', CAST(N'2024-07-21T22:18:55.540' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (107, 35, N'Paris', CAST(N'2024-07-21T22:48:11.557' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (108, 35, N'Berlin', CAST(N'2024-07-21T22:48:11.793' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (109, 35, N'London', CAST(N'2024-07-21T22:48:12.017' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (110, 36, N'3', CAST(N'2024-07-21T22:48:12.477' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (111, 36, N'4', CAST(N'2024-07-21T22:48:12.707' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (112, 36, N'5', CAST(N'2024-07-21T22:48:13.220' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (113, 37, N'Paris', CAST(N'2024-07-22T02:20:19.220' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (114, 37, N'Berlin', CAST(N'2024-07-22T02:20:19.537' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (115, 37, N'London', CAST(N'2024-07-22T02:20:19.797' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (116, 38, N'3', CAST(N'2024-07-22T02:20:20.503' AS DateTime), 0)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (117, 38, N'4', CAST(N'2024-07-22T02:20:20.707' AS DateTime), 1)
INSERT [dbo].[Answer] ([QuestionAnswerID], [QuestionID], [Content], [LastModifiedDate], [IsCorrectAnswer]) VALUES (118, 38, N'5', CAST(N'2024-07-22T02:20:20.970' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Answer] OFF
GO
SET IDENTITY_INSERT [dbo].[Classroom] ON 

INSERT [dbo].[Classroom] ([ClassID], [ClassName], [CreateBy], [FormTeacherID], [IsActive]) VALUES (1, N'12A12', 4, 6, 0)
INSERT [dbo].[Classroom] ([ClassID], [ClassName], [CreateBy], [FormTeacherID], [IsActive]) VALUES (2, N'12A2', 8, 13, 1)
INSERT [dbo].[Classroom] ([ClassID], [ClassName], [CreateBy], [FormTeacherID], [IsActive]) VALUES (3, N'11B1', 8, 10, 1)
INSERT [dbo].[Classroom] ([ClassID], [ClassName], [CreateBy], [FormTeacherID], [IsActive]) VALUES (4, N'11B2', 8, 9, 1)
INSERT [dbo].[Classroom] ([ClassID], [ClassName], [CreateBy], [FormTeacherID], [IsActive]) VALUES (5, N'10A1', 8, 12, 0)
INSERT [dbo].[Classroom] ([ClassID], [ClassName], [CreateBy], [FormTeacherID], [IsActive]) VALUES (6, N'10A2', 8, 24, 1)
INSERT [dbo].[Classroom] ([ClassID], [ClassName], [CreateBy], [FormTeacherID], [IsActive]) VALUES (40, N'11A1', 4, 16, 1)
INSERT [dbo].[Classroom] ([ClassID], [ClassName], [CreateBy], [FormTeacherID], [IsActive]) VALUES (41, N'10D2', 4, 28, 1)
SET IDENTITY_INSERT [dbo].[Classroom] OFF
GO
SET IDENTITY_INSERT [dbo].[ClassStudent] ON 

INSERT [dbo].[ClassStudent] ([ClassStudentID], [StudentID], [ClassID]) VALUES (1, 1, 1)
INSERT [dbo].[ClassStudent] ([ClassStudentID], [StudentID], [ClassID]) VALUES (2, 6, 1)
INSERT [dbo].[ClassStudent] ([ClassStudentID], [StudentID], [ClassID]) VALUES (3, 9, 2)
INSERT [dbo].[ClassStudent] ([ClassStudentID], [StudentID], [ClassID]) VALUES (4, 7, 3)
INSERT [dbo].[ClassStudent] ([ClassStudentID], [StudentID], [ClassID]) VALUES (5, 26, 1)
INSERT [dbo].[ClassStudent] ([ClassStudentID], [StudentID], [ClassID]) VALUES (6, 7, 1)
INSERT [dbo].[ClassStudent] ([ClassStudentID], [StudentID], [ClassID]) VALUES (7, 30, 1)
SET IDENTITY_INSERT [dbo].[ClassStudent] OFF
GO
SET IDENTITY_INSERT [dbo].[ClassSubject] ON 

INSERT [dbo].[ClassSubject] ([ClassSubjectID], [SubjectID], [ClassID], [SubjectTeacher]) VALUES (1, 1, 1, 6)
INSERT [dbo].[ClassSubject] ([ClassSubjectID], [SubjectID], [ClassID], [SubjectTeacher]) VALUES (5, 1, 2, 5)
INSERT [dbo].[ClassSubject] ([ClassSubjectID], [SubjectID], [ClassID], [SubjectTeacher]) VALUES (6, 3, 1, 28)
INSERT [dbo].[ClassSubject] ([ClassSubjectID], [SubjectID], [ClassID], [SubjectTeacher]) VALUES (7, 4, 1, 21)
SET IDENTITY_INSERT [dbo].[ClassSubject] OFF
GO
SET IDENTITY_INSERT [dbo].[ClassSubjectTest] ON 

INSERT [dbo].[ClassSubjectTest] ([TestID], [CLassSubjectID], [TestName], [TestDescription], [Attempts], [Duration], [PassScore], [StartDate], [EndDate]) VALUES (1, 1, N'Giữa kỳ Toán 12', N'Test', 1, 60, 5, CAST(N'2024-07-18T12:00:00.000' AS DateTime), CAST(N'2024-07-19T14:00:00.000' AS DateTime))
INSERT [dbo].[ClassSubjectTest] ([TestID], [CLassSubjectID], [TestName], [TestDescription], [Attempts], [Duration], [PassScore], [StartDate], [EndDate]) VALUES (2, 1, N'Bài kiểm tra cho trẻ con', N'', 2, 10, 5, CAST(N'2024-07-20T12:00:00.000' AS DateTime), CAST(N'2024-07-24T12:00:00.000' AS DateTime))
INSERT [dbo].[ClassSubjectTest] ([TestID], [CLassSubjectID], [TestName], [TestDescription], [Attempts], [Duration], [PassScore], [StartDate], [EndDate]) VALUES (3, 1, N'Ba con moè', NULL, 5, 30, 4, CAST(N'2024-07-23T00:00:00.000' AS DateTime), CAST(N'2024-07-25T12:00:00.000' AS DateTime))
INSERT [dbo].[ClassSubjectTest] ([TestID], [CLassSubjectID], [TestName], [TestDescription], [Attempts], [Duration], [PassScore], [StartDate], [EndDate]) VALUES (23, 1, N'Một con moè', N'', 5, 20, 3, CAST(N'2024-07-19T12:00:00.000' AS DateTime), CAST(N'2024-07-19T22:00:00.000' AS DateTime))
INSERT [dbo].[ClassSubjectTest] ([TestID], [CLassSubjectID], [TestName], [TestDescription], [Attempts], [Duration], [PassScore], [StartDate], [EndDate]) VALUES (24, 1, N'Hai con moè', NULL, 5, 60, 5, CAST(N'2024-07-21T12:00:00.000' AS DateTime), CAST(N'2024-07-23T08:00:00.000' AS DateTime))
INSERT [dbo].[ClassSubjectTest] ([TestID], [CLassSubjectID], [TestName], [TestDescription], [Attempts], [Duration], [PassScore], [StartDate], [EndDate]) VALUES (25, 1, N'Bài Thi Tình Yêu', NULL, 1, 50, 10, CAST(N'2024-07-11T12:00:00.000' AS DateTime), CAST(N'2024-07-26T12:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[ClassSubjectTest] OFF
GO
SET IDENTITY_INSERT [dbo].[Notification] ON 

INSERT [dbo].[Notification] ([NotificationID], [Title], [Content], [CreatedDate], [CreatedBy]) VALUES (2, N'test', N'test', CAST(N'2024-07-21T13:48:02.620' AS DateTime), 4)
INSERT [dbo].[Notification] ([NotificationID], [Title], [Content], [CreatedDate], [CreatedBy]) VALUES (3, N'test', N'signalR', CAST(N'2024-07-21T15:16:07.723' AS DateTime), 4)
INSERT [dbo].[Notification] ([NotificationID], [Title], [Content], [CreatedDate], [CreatedBy]) VALUES (4, N'test', N'signalR', CAST(N'2024-07-21T15:26:28.377' AS DateTime), 4)
INSERT [dbo].[Notification] ([NotificationID], [Title], [Content], [CreatedDate], [CreatedBy]) VALUES (5, N'test', N'a', CAST(N'2024-07-21T20:08:43.060' AS DateTime), 4)
INSERT [dbo].[Notification] ([NotificationID], [Title], [Content], [CreatedDate], [CreatedBy]) VALUES (6, N'test', N'ab', CAST(N'2024-07-21T20:10:26.100' AS DateTime), 4)
INSERT [dbo].[Notification] ([NotificationID], [Title], [Content], [CreatedDate], [CreatedBy]) VALUES (7, N'save', N'savechange', CAST(N'2024-07-21T20:11:47.500' AS DateTime), 4)
INSERT [dbo].[Notification] ([NotificationID], [Title], [Content], [CreatedDate], [CreatedBy]) VALUES (8, N'test send email', N'new notification send by email', CAST(N'2024-07-21T20:32:49.430' AS DateTime), 8)
INSERT [dbo].[Notification] ([NotificationID], [Title], [Content], [CreatedDate], [CreatedBy]) VALUES (9, N'231123', N'12312312', CAST(N'2024-07-22T12:04:43.573' AS DateTime), 4)
INSERT [dbo].[Notification] ([NotificationID], [Title], [Content], [CreatedDate], [CreatedBy]) VALUES (10, N'231123', N'12312312', CAST(N'2024-07-22T12:04:57.440' AS DateTime), 4)
INSERT [dbo].[Notification] ([NotificationID], [Title], [Content], [CreatedDate], [CreatedBy]) VALUES (11, N'231123', N'12312312', CAST(N'2024-07-22T12:05:05.423' AS DateTime), 4)
SET IDENTITY_INSERT [dbo].[Notification] OFF
GO
INSERT [dbo].[NotificationOfAccount] ([NotificationID], [To], [IsRead]) VALUES (7, 26, 0)
INSERT [dbo].[NotificationOfAccount] ([NotificationID], [To], [IsRead]) VALUES (8, 7, 0)
INSERT [dbo].[NotificationOfAccount] ([NotificationID], [To], [IsRead]) VALUES (8, 26, 0)
INSERT [dbo].[NotificationOfAccount] ([NotificationID], [To], [IsRead]) VALUES (11, 1, 0)
INSERT [dbo].[NotificationOfAccount] ([NotificationID], [To], [IsRead]) VALUES (11, 6, 0)
INSERT [dbo].[NotificationOfAccount] ([NotificationID], [To], [IsRead]) VALUES (11, 7, 0)
INSERT [dbo].[NotificationOfAccount] ([NotificationID], [To], [IsRead]) VALUES (11, 9, 0)
INSERT [dbo].[NotificationOfAccount] ([NotificationID], [To], [IsRead]) VALUES (11, 26, 0)
INSERT [dbo].[NotificationOfAccount] ([NotificationID], [To], [IsRead]) VALUES (11, 30, 0)
GO
SET IDENTITY_INSERT [dbo].[StudentTestAnswer] ON 

INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (221, 7, 2, 1, 3, CAST(N'2024-07-21T21:10:08.847' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (222, 7, 2, 2, 6, CAST(N'2024-07-21T21:10:09.470' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (223, 7, 2, 3, 7, CAST(N'2024-07-21T21:10:09.770' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (224, 7, 2, 4, 11, CAST(N'2024-07-21T21:10:10.077' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (240, 7, 2, 1, 1, CAST(N'2024-07-21T23:56:44.477' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (241, 7, 2, 2, 4, CAST(N'2024-07-21T23:56:44.707' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (242, 7, 2, 2, 6, CAST(N'2024-07-21T23:56:44.943' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (243, 7, 2, 3, 7, CAST(N'2024-07-21T23:56:45.197' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (244, 7, 2, 3, 9, CAST(N'2024-07-21T23:56:45.437' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (245, 7, 2, 4, 11, CAST(N'2024-07-21T23:56:45.677' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (246, 7, 2, 5, 14, CAST(N'2024-07-21T23:56:45.913' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (247, 7, 2, 6, 16, CAST(N'2024-07-21T23:56:46.157' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (248, 7, 2, 7, 19, CAST(N'2024-07-21T23:56:46.397' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (249, 7, 2, 8, 22, CAST(N'2024-07-21T23:56:46.627' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (250, 7, 2, 9, 23, CAST(N'2024-07-21T23:56:46.853' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (251, 7, 2, 10, 26, CAST(N'2024-07-21T23:56:47.080' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (252, 7, 2, 11, 29, CAST(N'2024-07-21T23:56:47.310' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (253, 7, 2, 12, 34, CAST(N'2024-07-21T23:56:47.540' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (254, 7, 2, 13, 37, CAST(N'2024-07-21T23:56:47.777' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (255, 7, 2, 13, 40, CAST(N'2024-07-21T23:56:48.003' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (256, 7, 2, 14, 44, CAST(N'2024-07-21T23:56:48.233' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (257, 7, 2, 15, 45, CAST(N'2024-07-21T23:56:48.463' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (258, 7, 2, 16, 49, CAST(N'2024-07-21T23:56:48.697' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (259, 7, 2, 16, 50, CAST(N'2024-07-21T23:56:48.927' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (260, 7, 23, 35, 107, CAST(N'2024-07-21T23:57:45.780' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (261, 7, 23, 35, 108, CAST(N'2024-07-21T23:57:46.010' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (262, 7, 23, 35, 109, CAST(N'2024-07-21T23:57:46.253' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (263, 7, 23, 36, 110, CAST(N'2024-07-21T23:57:46.487' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (264, 7, 23, 36, 111, CAST(N'2024-07-21T23:57:46.713' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (265, 7, 23, 36, 112, CAST(N'2024-07-21T23:57:46.950' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (266, 7, 23, 35, 107, CAST(N'2024-07-22T00:01:21.277' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (267, 7, 23, 36, 110, CAST(N'2024-07-22T00:01:21.697' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (268, 7, 23, 36, 111, CAST(N'2024-07-22T00:01:21.993' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (269, 7, 23, 35, 107, CAST(N'2024-07-22T00:01:41.423' AS DateTime), 3)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (270, 7, 23, 35, 108, CAST(N'2024-07-22T00:01:41.653' AS DateTime), 3)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (271, 7, 23, 35, 109, CAST(N'2024-07-22T00:01:41.930' AS DateTime), 3)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (272, 7, 23, 36, 110, CAST(N'2024-07-22T00:01:42.247' AS DateTime), 3)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (273, 7, 23, 36, 111, CAST(N'2024-07-22T00:01:42.563' AS DateTime), 3)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (274, 7, 23, 36, 112, CAST(N'2024-07-22T00:01:42.823' AS DateTime), 3)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (275, 7, 23, 35, 108, CAST(N'2024-07-22T00:02:24.890' AS DateTime), 4)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (276, 7, 23, 36, 111, CAST(N'2024-07-22T00:02:25.187' AS DateTime), 4)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (277, 7, 23, 35, 107, CAST(N'2024-07-22T00:02:42.453' AS DateTime), 5)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (278, 7, 23, 36, 111, CAST(N'2024-07-22T00:02:42.733' AS DateTime), 5)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (279, 7, 23, 36, 112, CAST(N'2024-07-22T00:02:42.950' AS DateTime), 5)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (293, 7, 24, 37, 114, CAST(N'2024-07-22T09:59:20.243' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (294, 7, 24, 37, 115, CAST(N'2024-07-22T09:59:20.463' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (295, 7, 24, 38, 117, CAST(N'2024-07-22T09:59:20.680' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (296, 7, 24, 38, 118, CAST(N'2024-07-22T09:59:21.183' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (300, 30, 24, 37, 113, CAST(N'2024-07-22T10:05:04.063' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (301, 30, 24, 38, 116, CAST(N'2024-07-22T10:05:04.267' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (302, 30, 24, 38, 118, CAST(N'2024-07-22T10:05:04.473' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (303, 7, 24, 37, 114, CAST(N'2024-07-22T10:05:56.863' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (304, 7, 24, 38, 116, CAST(N'2024-07-22T10:05:57.087' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (305, 7, 24, 38, 117, CAST(N'2024-07-22T10:05:57.600' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (306, 7, 24, 38, 118, CAST(N'2024-07-22T10:05:57.817' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (307, 30, 2, 1, 1, CAST(N'2024-07-22T10:14:01.280' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (308, 30, 2, 1, 3, CAST(N'2024-07-22T10:14:01.897' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (309, 30, 2, 2, 5, CAST(N'2024-07-22T10:14:02.110' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (310, 30, 2, 3, 8, CAST(N'2024-07-22T10:14:02.320' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (311, 30, 2, 4, 12, CAST(N'2024-07-22T10:14:02.533' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (315, 30, 2, 1, 2, CAST(N'2024-07-22T10:15:43.317' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (316, 30, 2, 2, 5, CAST(N'2024-07-22T10:15:44.023' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (317, 30, 2, 3, 9, CAST(N'2024-07-22T10:15:44.250' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (348, 1, 2, 15, 46, CAST(N'2024-07-22T11:46:52.360' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (349, 1, 2, 16, 48, CAST(N'2024-07-22T11:46:52.620' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (350, 1, 2, 1, 1, CAST(N'2024-07-22T11:46:58.360' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (351, 1, 2, 2, 4, CAST(N'2024-07-22T11:46:58.603' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (352, 1, 2, 3, 8, CAST(N'2024-07-22T11:46:58.850' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (353, 1, 2, 4, 11, CAST(N'2024-07-22T11:46:59.093' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (354, 1, 2, 5, 14, CAST(N'2024-07-22T11:46:59.340' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (355, 1, 2, 6, 17, CAST(N'2024-07-22T11:46:59.587' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (356, 1, 2, 7, 19, CAST(N'2024-07-22T11:46:59.830' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (357, 1, 2, 8, 21, CAST(N'2024-07-22T11:47:00.077' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (358, 1, 2, 9, 23, CAST(N'2024-07-22T11:47:00.320' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (359, 1, 2, 10, 25, CAST(N'2024-07-22T11:47:00.563' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (360, 1, 2, 11, 29, CAST(N'2024-07-22T11:47:00.807' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (361, 1, 2, 12, 32, CAST(N'2024-07-22T11:47:01.053' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (362, 1, 2, 13, 38, CAST(N'2024-07-22T11:47:01.303' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (363, 1, 2, 14, 41, CAST(N'2024-07-22T11:47:01.550' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (364, 1, 2, 15, 46, CAST(N'2024-07-22T11:47:01.797' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (365, 1, 2, 16, 48, CAST(N'2024-07-22T11:47:02.043' AS DateTime), 1)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (366, 30, 24, 37, 113, CAST(N'2024-07-22T12:25:37.063' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (367, 30, 24, 37, 114, CAST(N'2024-07-22T12:25:37.760' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (368, 30, 24, 37, 115, CAST(N'2024-07-22T12:25:37.977' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (369, 30, 24, 38, 116, CAST(N'2024-07-22T12:25:38.487' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (370, 30, 24, 38, 117, CAST(N'2024-07-22T12:25:38.997' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (371, 30, 24, 38, 118, CAST(N'2024-07-22T12:25:39.217' AS DateTime), 2)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (372, 30, 24, 37, 113, CAST(N'2024-07-22T12:25:50.270' AS DateTime), 3)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (373, 30, 24, 37, 114, CAST(N'2024-07-22T12:25:50.593' AS DateTime), 3)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (374, 30, 24, 37, 115, CAST(N'2024-07-22T12:25:50.990' AS DateTime), 3)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (375, 30, 24, 38, 116, CAST(N'2024-07-22T12:25:51.300' AS DateTime), 3)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (376, 30, 24, 38, 117, CAST(N'2024-07-22T12:25:51.547' AS DateTime), 3)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (377, 30, 24, 38, 118, CAST(N'2024-07-22T12:25:52.373' AS DateTime), 3)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (378, 30, 24, 37, 113, CAST(N'2024-07-22T12:26:28.127' AS DateTime), 4)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (379, 30, 24, 37, 114, CAST(N'2024-07-22T12:26:28.630' AS DateTime), 4)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (380, 30, 24, 37, 115, CAST(N'2024-07-22T12:26:28.847' AS DateTime), 4)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (381, 30, 24, 38, 116, CAST(N'2024-07-22T12:26:29.350' AS DateTime), 4)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (382, 30, 24, 38, 117, CAST(N'2024-07-22T12:26:29.567' AS DateTime), 4)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (383, 30, 24, 38, 118, CAST(N'2024-07-22T12:26:30.063' AS DateTime), 4)
GO
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (384, 30, 24, 37, 113, CAST(N'2024-07-22T12:26:37.973' AS DateTime), 5)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (385, 30, 24, 37, 114, CAST(N'2024-07-22T12:26:38.187' AS DateTime), 5)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (386, 30, 24, 37, 115, CAST(N'2024-07-22T12:26:38.953' AS DateTime), 5)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (387, 30, 24, 38, 116, CAST(N'2024-07-22T12:26:39.163' AS DateTime), 5)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (388, 30, 24, 38, 117, CAST(N'2024-07-22T12:26:39.390' AS DateTime), 5)
INSERT [dbo].[StudentTestAnswer] ([StudentTestAnswerID], [StudentID], [TestID], [TestQuestionID], [SelectedAnswerID], [AnswerTime], [AttemptNo]) VALUES (389, 30, 24, 38, 118, CAST(N'2024-07-22T12:26:39.887' AS DateTime), 5)
SET IDENTITY_INSERT [dbo].[StudentTestAnswer] OFF
GO
SET IDENTITY_INSERT [dbo].[Subject] ON 

INSERT [dbo].[Subject] ([SubjectID], [SubjectName]) VALUES (1, N'Văn')
INSERT [dbo].[Subject] ([SubjectID], [SubjectName]) VALUES (2, N'Toán')
INSERT [dbo].[Subject] ([SubjectID], [SubjectName]) VALUES (3, N'Lý')
INSERT [dbo].[Subject] ([SubjectID], [SubjectName]) VALUES (4, N'Hóa')
SET IDENTITY_INSERT [dbo].[Subject] OFF
GO
SET IDENTITY_INSERT [dbo].[TestQuestion] ON 

INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 1, N'CH1', CAST(N'2024-07-21T16:33:59.947' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 2, N'CH2', CAST(N'2024-07-21T16:34:00.907' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 3, N'CH2', CAST(N'2024-07-21T16:34:02.323' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 4, N'CH3', CAST(N'2024-07-21T16:34:03.727' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 5, N'CH3', CAST(N'2024-07-21T16:34:04.563' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 6, N'CH5', CAST(N'2024-07-21T16:34:05.670' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 7, N'CH6', CAST(N'2024-07-21T16:34:06.793' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 8, N'CH7', CAST(N'2024-07-21T16:34:07.710' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 9, N'CH8', CAST(N'2024-07-21T16:34:08.350' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 10, N'CH9', CAST(N'2024-07-21T16:34:09.527' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 11, N'CH10', CAST(N'2024-07-21T16:34:10.367' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 12, N'CH11', CAST(N'2024-07-21T16:34:11.690' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 13, N'CH12', CAST(N'2024-07-21T16:34:14.083' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 14, N'CH13', CAST(N'2024-07-21T16:34:15.683' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 15, N'What is the capital of France?', CAST(N'2024-07-21T16:34:17.283' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (2, 16, N'2 + 2 = ?', CAST(N'2024-07-21T16:34:18.670' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 17, N'CH1', CAST(N'2024-07-21T20:36:53.847' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 18, N'CH2', CAST(N'2024-07-21T20:36:54.830' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 19, N'CH2', CAST(N'2024-07-21T20:36:55.953' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 20, N'CH3', CAST(N'2024-07-21T20:36:57.040' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 21, N'CH3', CAST(N'2024-07-21T20:36:57.863' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 22, N'CH5', CAST(N'2024-07-21T20:36:59.240' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 23, N'CH6', CAST(N'2024-07-21T20:37:00.940' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 24, N'CH7', CAST(N'2024-07-21T20:37:02.107' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 25, N'CH8', CAST(N'2024-07-21T20:37:02.993' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 26, N'CH9', CAST(N'2024-07-21T20:37:04.897' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 27, N'CH10', CAST(N'2024-07-21T20:37:06.823' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 28, N'CH11', CAST(N'2024-07-21T20:37:08.133' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 29, N'CH12', CAST(N'2024-07-21T20:37:10.730' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 30, N'CH13', CAST(N'2024-07-21T20:37:12.317' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 31, N'What is the capital of France?', CAST(N'2024-07-21T20:37:14.220' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (3, 32, N'2 + 2 = ?', CAST(N'2024-07-21T20:37:15.863' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (1, 33, N'What is the capital of France?', CAST(N'2024-07-21T22:18:53.900' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (1, 34, N'2 + 2 = ?', CAST(N'2024-07-21T22:18:54.897' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (23, 35, N'What is the capital of France?', CAST(N'2024-07-21T22:48:09.633' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (23, 36, N'2 + 2 = ?', CAST(N'2024-07-21T22:48:12.257' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (24, 37, N'What is the capital of France?', CAST(N'2024-07-22T02:20:18.780' AS DateTime))
INSERT [dbo].[TestQuestion] ([TestID], [TestQuestionID], [Content], [LastModifiedDate]) VALUES (24, 38, N'2 + 2 = ?', CAST(N'2024-07-22T02:20:20.303' AS DateTime))
SET IDENTITY_INSERT [dbo].[TestQuestion] OFF
GO
ALTER TABLE [dbo].[Answer]  WITH CHECK ADD  CONSTRAINT [Answer_TestQuestion] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[TestQuestion] ([TestQuestionID])
GO
ALTER TABLE [dbo].[Answer] CHECK CONSTRAINT [Answer_TestQuestion]
GO
ALTER TABLE [dbo].[Classroom]  WITH CHECK ADD  CONSTRAINT [Classroom_Account_CreateBy] FOREIGN KEY([CreateBy])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[Classroom] CHECK CONSTRAINT [Classroom_Account_CreateBy]
GO
ALTER TABLE [dbo].[Classroom]  WITH CHECK ADD  CONSTRAINT [Classroom_Account_FormTeacher] FOREIGN KEY([FormTeacherID])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[Classroom] CHECK CONSTRAINT [Classroom_Account_FormTeacher]
GO
ALTER TABLE [dbo].[ClassStudent]  WITH CHECK ADD  CONSTRAINT [ClassStudent_Account] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[ClassStudent] CHECK CONSTRAINT [ClassStudent_Account]
GO
ALTER TABLE [dbo].[ClassStudent]  WITH CHECK ADD  CONSTRAINT [ClassStudent_Classroom] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Classroom] ([ClassID])
GO
ALTER TABLE [dbo].[ClassStudent] CHECK CONSTRAINT [ClassStudent_Classroom]
GO
ALTER TABLE [dbo].[ClassSubject]  WITH CHECK ADD  CONSTRAINT [ClassSubject_Account] FOREIGN KEY([SubjectTeacher])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[ClassSubject] CHECK CONSTRAINT [ClassSubject_Account]
GO
ALTER TABLE [dbo].[ClassSubject]  WITH CHECK ADD  CONSTRAINT [ClassSubject_Classroom] FOREIGN KEY([ClassID])
REFERENCES [dbo].[Classroom] ([ClassID])
GO
ALTER TABLE [dbo].[ClassSubject] CHECK CONSTRAINT [ClassSubject_Classroom]
GO
ALTER TABLE [dbo].[ClassSubject]  WITH CHECK ADD  CONSTRAINT [ClassSubject_Subject] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[Subject] ([SubjectID])
GO
ALTER TABLE [dbo].[ClassSubject] CHECK CONSTRAINT [ClassSubject_Subject]
GO
ALTER TABLE [dbo].[ClassSubjectPost]  WITH CHECK ADD  CONSTRAINT [ClassPost_ClassSubject] FOREIGN KEY([ClassSubjectID])
REFERENCES [dbo].[ClassSubject] ([ClassSubjectID])
GO
ALTER TABLE [dbo].[ClassSubjectPost] CHECK CONSTRAINT [ClassPost_ClassSubject]
GO
ALTER TABLE [dbo].[ClassSubjectTest]  WITH CHECK ADD  CONSTRAINT [ClassTest_ClassSubject] FOREIGN KEY([CLassSubjectID])
REFERENCES [dbo].[ClassSubject] ([ClassSubjectID])
GO
ALTER TABLE [dbo].[ClassSubjectTest] CHECK CONSTRAINT [ClassTest_ClassSubject]
GO
ALTER TABLE [dbo].[ConversationMember]  WITH CHECK ADD  CONSTRAINT [ConversationMember_Account] FOREIGN KEY([AccountID])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[ConversationMember] CHECK CONSTRAINT [ConversationMember_Account]
GO
ALTER TABLE [dbo].[ConversationMember]  WITH CHECK ADD  CONSTRAINT [ConversationMember_SubjectConversation] FOREIGN KEY([ConversationID])
REFERENCES [dbo].[SubjectConversation] ([ConversationID])
GO
ALTER TABLE [dbo].[ConversationMember] CHECK CONSTRAINT [ConversationMember_SubjectConversation]
GO
ALTER TABLE [dbo].[CoversationMessage]  WITH CHECK ADD  CONSTRAINT [CoversationMessage_Account] FOREIGN KEY([SendByID])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[CoversationMessage] CHECK CONSTRAINT [CoversationMessage_Account]
GO
ALTER TABLE [dbo].[CoversationMessage]  WITH CHECK ADD  CONSTRAINT [CoversationMessage_SubjectConversation] FOREIGN KEY([ConversationID])
REFERENCES [dbo].[SubjectConversation] ([ConversationID])
GO
ALTER TABLE [dbo].[CoversationMessage] CHECK CONSTRAINT [CoversationMessage_SubjectConversation]
GO
ALTER TABLE [dbo].[Notification]  WITH CHECK ADD  CONSTRAINT [Notification_Account] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[Notification] CHECK CONSTRAINT [Notification_Account]
GO
ALTER TABLE [dbo].[NotificationOfAccount]  WITH CHECK ADD  CONSTRAINT [NotificationOfAccount_Account_To] FOREIGN KEY([To])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[NotificationOfAccount] CHECK CONSTRAINT [NotificationOfAccount_Account_To]
GO
ALTER TABLE [dbo].[NotificationOfAccount]  WITH CHECK ADD  CONSTRAINT [NotificationOfAccount_Notification] FOREIGN KEY([NotificationID])
REFERENCES [dbo].[Notification] ([NotificationID])
GO
ALTER TABLE [dbo].[NotificationOfAccount] CHECK CONSTRAINT [NotificationOfAccount_Notification]
GO
ALTER TABLE [dbo].[PostAttachment]  WITH CHECK ADD  CONSTRAINT [PostAttachment_ClassPost] FOREIGN KEY([PostID])
REFERENCES [dbo].[ClassSubjectPost] ([PostID])
GO
ALTER TABLE [dbo].[PostAttachment] CHECK CONSTRAINT [PostAttachment_ClassPost]
GO
ALTER TABLE [dbo].[PostComment]  WITH CHECK ADD  CONSTRAINT [PostComment_Account] FOREIGN KEY([PostedBy])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[PostComment] CHECK CONSTRAINT [PostComment_Account]
GO
ALTER TABLE [dbo].[PostComment]  WITH CHECK ADD  CONSTRAINT [PostComment_ClassPost] FOREIGN KEY([PostID])
REFERENCES [dbo].[ClassSubjectPost] ([PostID])
GO
ALTER TABLE [dbo].[PostComment] CHECK CONSTRAINT [PostComment_ClassPost]
GO
ALTER TABLE [dbo].[StudentTestAnswer]  WITH CHECK ADD  CONSTRAINT [StudentTestAnswer_Account] FOREIGN KEY([StudentID])
REFERENCES [dbo].[Account] ([AccountID])
GO
ALTER TABLE [dbo].[StudentTestAnswer] CHECK CONSTRAINT [StudentTestAnswer_Account]
GO
ALTER TABLE [dbo].[StudentTestAnswer]  WITH CHECK ADD  CONSTRAINT [StudentTestAnswer_Answer] FOREIGN KEY([SelectedAnswerID])
REFERENCES [dbo].[Answer] ([QuestionAnswerID])
GO
ALTER TABLE [dbo].[StudentTestAnswer] CHECK CONSTRAINT [StudentTestAnswer_Answer]
GO
ALTER TABLE [dbo].[StudentTestAnswer]  WITH CHECK ADD  CONSTRAINT [StudentTestAnswer_ClassTest] FOREIGN KEY([TestID])
REFERENCES [dbo].[ClassSubjectTest] ([TestID])
GO
ALTER TABLE [dbo].[StudentTestAnswer] CHECK CONSTRAINT [StudentTestAnswer_ClassTest]
GO
ALTER TABLE [dbo].[StudentTestAnswer]  WITH CHECK ADD  CONSTRAINT [StudentTestAnswer_TestQuestion] FOREIGN KEY([TestQuestionID])
REFERENCES [dbo].[TestQuestion] ([TestQuestionID])
GO
ALTER TABLE [dbo].[StudentTestAnswer] CHECK CONSTRAINT [StudentTestAnswer_TestQuestion]
GO
ALTER TABLE [dbo].[SubjectConversation]  WITH CHECK ADD  CONSTRAINT [SubjectConversation_ClassSubject] FOREIGN KEY([ClassSubjectID])
REFERENCES [dbo].[ClassSubject] ([ClassSubjectID])
GO
ALTER TABLE [dbo].[SubjectConversation] CHECK CONSTRAINT [SubjectConversation_ClassSubject]
GO
ALTER TABLE [dbo].[TestQuestion]  WITH CHECK ADD  CONSTRAINT [TestQuestion_ClassTest] FOREIGN KEY([TestID])
REFERENCES [dbo].[ClassSubjectTest] ([TestID])
GO
ALTER TABLE [dbo].[TestQuestion] CHECK CONSTRAINT [TestQuestion_ClassTest]
GO
USE [master]
GO
ALTER DATABASE [OLS_DB] SET  READ_WRITE 
GO
