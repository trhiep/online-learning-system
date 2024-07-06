-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2024-07-06 09:52:25.343


-- DATABASE NAME: OLS

-- tables
-- Table: Account
CREATE TABLE Account (
    AccountID int  NOT NULL IDENTITY(1, 1),
    Username varchar(50)  NOT NULL,
    Password varchar(255)  NOT NULL,
    Fullname nvarchar(255)  NOT NULL,
    Email varchar(255)  NOT NULL,
    RoleID int  NOT NULL,
    StatusID int  NOT NULL,
    CONSTRAINT Account_pk PRIMARY KEY  (AccountID)
);

-- Table: Answer
CREATE TABLE Answer (
    QuestionAnswerID int  NOT NULL IDENTITY(1, 1),
    QuestionID int  NOT NULL,
    Content nvarchar(max)  NOT NULL,
    ImageLink nvarchar(max)  NOT NULL,
    LastModifiedDate datetime  NOT NULL,
    IsCorrectAnswer bit  NOT NULL,
    CONSTRAINT Answer_pk PRIMARY KEY  (QuestionAnswerID)
);

-- Table: ClassStudent
CREATE TABLE ClassStudent (
    ClassStudentID int  NOT NULL IDENTITY(1, 1),
    StudentID int  NOT NULL,
    ClassID int  NOT NULL,
    CONSTRAINT ClassStudent_pk PRIMARY KEY  (ClassStudentID)
);

-- Table: ClassSubject
CREATE TABLE ClassSubject (
    CLassSubjectID int  NOT NULL IDENTITY(1, 1),
    SubjectID int  NOT NULL,
    ClassID int  NOT NULL,
    SubjectTeacher int  NOT NULL,
    CONSTRAINT ClassSubject_pk PRIMARY KEY  (CLassSubjectID)
);

-- Table: ClassSubjectPost
CREATE TABLE ClassSubjectPost (
    CLassSubjectID int  NOT NULL,
    PostID int  NOT NULL IDENTITY(1, 1),
    Title nvarchar(100)  NOT NULL,
    Content nvarchar(max)  NOT NULL,
    PostedDate datetime  NOT NULL,
    CONSTRAINT ClassSubjectPost_pk PRIMARY KEY  (PostID)
);

-- Table: ClassSubjectTest
CREATE TABLE ClassSubjectTest (
    TestID int  NOT NULL IDENTITY(1, 1),
    CLassSubjectID int  NOT NULL,
    TestName nvarchar(100)  NOT NULL,
    TestDescription nvarchar(max)  NOT NULL,
    TestImage nvarchar(max)  NOT NULL,
    Attempts int  NOT NULL,
    Duration float(2)  NOT NULL,
    PassScore float(2)  NOT NULL,
    StartDate datetime  NOT NULL,
    EndDate datetime  NOT NULL,
    TypeID int  NOT NULL,
    CONSTRAINT ClassSubjectTest_pk PRIMARY KEY  (TestID)
);

-- Table: ClassSubjectTestAttachments
CREATE TABLE ClassSubjectTestAttachments (
    TestAttachmentID int  NOT NULL IDENTITY(1, 1),
    TestID int  NOT NULL,
    AttachmentLink nvarchar(max)  NOT NULL,
    CONSTRAINT ClassSubjectTestAttachments_pk PRIMARY KEY  (TestAttachmentID)
);

-- Table: ClassSubjectTestType
CREATE TABLE ClassSubjectTestType (
    TypeID int  NOT NULL,
    TypeName nvarchar(100)  NOT NULL,
    CONSTRAINT ClassSubjectTestType_pk PRIMARY KEY  (TypeID)
);

-- Table: Classroom
CREATE TABLE Classroom (
    ClassID int  NOT NULL IDENTITY(1, 1),
    ClassName nvarchar(100)  NOT NULL,
    CreateBy int  NOT NULL,
    FormTeacherID int  NOT NULL,
    IsActive bit  NOT NULL,
    CONSTRAINT Classroom_pk PRIMARY KEY  (ClassID)
);

-- Table: Notification
CREATE TABLE Notification (
    NotificationID int  NOT NULL IDENTITY(1, 1),
    Title nvarchar(100)  NOT NULL,
    Content nvarchar(max)  NOT NULL,
    CreatedDate datetime  NOT NULL,
    CreatedBy int  NOT NULL,
    CONSTRAINT Notification_pk PRIMARY KEY  (NotificationID)
);

-- Table: NotificationOfAccount
CREATE TABLE NotificationOfAccount (
    NotificationID int  NOT NULL,
    "To" int  NOT NULL,
    IsRead bit  NOT NULL,
    CONSTRAINT NotificationOfAccount_pk PRIMARY KEY  (NotificationID,"To")
);

-- Table: PostAttachment
CREATE TABLE PostAttachment (
    PostID int  NOT NULL,
    AttachmentLink nvarchar(max)  NOT NULL,
    CONSTRAINT PostAttachment_pk PRIMARY KEY  (PostID)
);

-- Table: PostComment
CREATE TABLE PostComment (
    PostID int  NOT NULL,
    CommentID bigint  NOT NULL IDENTITY(1, 1),
    PostedBy int  NOT NULL,
    CommentContent nvarchar(max)  NOT NULL,
    FatherID int  NULL,
    CONSTRAINT PostComment_pk PRIMARY KEY  (CommentID)
);

-- Table: StudentTestAnswer
CREATE TABLE StudentTestAnswer (
    StudentTestAnswerID int  NOT NULL IDENTITY(1, 1),
    StudentID int  NOT NULL,
    TestID int  NOT NULL,
    QuestionAnswerID int  NOT NULL,
    TestQuestionID int  NOT NULL,
    CONSTRAINT StudentTestAnswer_pk PRIMARY KEY  (StudentTestAnswerID)
);

-- Table: Subject
CREATE TABLE Subject (
    SubjectID int  NOT NULL IDENTITY(1, 1),
    SubjectName nvarchar(100)  NOT NULL,
    CONSTRAINT Subject_pk PRIMARY KEY  (SubjectID)
);

-- Table: TestQuestion
CREATE TABLE TestQuestion (
    TestID int  NOT NULL,
    TestQuestionID int  NOT NULL IDENTITY(1, 1),
    Content nvarchar(max)  NOT NULL,
    ImageLink nvarchar(max)  NOT NULL,
    LastModifiedDate datetime  NOT NULL,
    CONSTRAINT TestQuestion_pk PRIMARY KEY  (TestQuestionID)
);

-- Table: UserRole
CREATE TABLE UserRole (
    RoleID int  NOT NULL IDENTITY(1, 1),
    RoleName nvarchar(50)  NOT NULL,
    CONSTRAINT UserRole_pk PRIMARY KEY  (RoleID)
);

-- Table: UserStatus
CREATE TABLE UserStatus (
    StatusID int  NOT NULL IDENTITY(1, 1),
    StatusName nvarchar(50)  NOT NULL,
    CONSTRAINT UserStatus_pk PRIMARY KEY  (StatusID)
);

-- foreign keys
-- Reference: Account_UserRole (table: Account)
ALTER TABLE Account ADD CONSTRAINT Account_UserRole
    FOREIGN KEY (RoleID)
    REFERENCES UserRole (RoleID);

-- Reference: Account_UserStatus (table: Account)
ALTER TABLE Account ADD CONSTRAINT Account_UserStatus
    FOREIGN KEY (StatusID)
    REFERENCES UserStatus (StatusID);

-- Reference: Answer_TestQuestion (table: Answer)
ALTER TABLE Answer ADD CONSTRAINT Answer_TestQuestion
    FOREIGN KEY (QuestionID)
    REFERENCES TestQuestion (TestQuestionID);

-- Reference: ClassPost_ClassSubject (table: ClassSubjectPost)
ALTER TABLE ClassSubjectPost ADD CONSTRAINT ClassPost_ClassSubject
    FOREIGN KEY (CLassSubjectID)
    REFERENCES ClassSubject (CLassSubjectID);

-- Reference: ClassStudent_Account (table: ClassStudent)
ALTER TABLE ClassStudent ADD CONSTRAINT ClassStudent_Account
    FOREIGN KEY (StudentID)
    REFERENCES Account (AccountID);

-- Reference: ClassStudent_Classroom (table: ClassStudent)
ALTER TABLE ClassStudent ADD CONSTRAINT ClassStudent_Classroom
    FOREIGN KEY (ClassID)
    REFERENCES Classroom (ClassID);

-- Reference: ClassSubjectTestAttachments_ClassSubjectTest (table: ClassSubjectTestAttachments)
ALTER TABLE ClassSubjectTestAttachments ADD CONSTRAINT ClassSubjectTestAttachments_ClassSubjectTest
    FOREIGN KEY (TestID)
    REFERENCES ClassSubjectTest (TestID);

-- Reference: ClassSubjectTest_ClassSubjectTestType (table: ClassSubjectTest)
ALTER TABLE ClassSubjectTest ADD CONSTRAINT ClassSubjectTest_ClassSubjectTestType
    FOREIGN KEY (TypeID)
    REFERENCES ClassSubjectTestType (TypeID);

-- Reference: ClassSubject_Account (table: ClassSubject)
ALTER TABLE ClassSubject ADD CONSTRAINT ClassSubject_Account
    FOREIGN KEY (SubjectTeacher)
    REFERENCES Account (AccountID);

-- Reference: ClassSubject_Classroom (table: ClassSubject)
ALTER TABLE ClassSubject ADD CONSTRAINT ClassSubject_Classroom
    FOREIGN KEY (ClassID)
    REFERENCES Classroom (ClassID);

-- Reference: ClassSubject_Subject (table: ClassSubject)
ALTER TABLE ClassSubject ADD CONSTRAINT ClassSubject_Subject
    FOREIGN KEY (SubjectID)
    REFERENCES Subject (SubjectID);

-- Reference: ClassTest_ClassSubject (table: ClassSubjectTest)
ALTER TABLE ClassSubjectTest ADD CONSTRAINT ClassTest_ClassSubject
    FOREIGN KEY (CLassSubjectID)
    REFERENCES ClassSubject (CLassSubjectID);

-- Reference: Classroom_Account_CreateBy (table: Classroom)
ALTER TABLE Classroom ADD CONSTRAINT Classroom_Account_CreateBy
    FOREIGN KEY (CreateBy)
    REFERENCES Account (AccountID);

-- Reference: Classroom_Account_FormTeacher (table: Classroom)
ALTER TABLE Classroom ADD CONSTRAINT Classroom_Account_FormTeacher
    FOREIGN KEY (FormTeacherID)
    REFERENCES Account (AccountID);

-- Reference: NotificationOfAccount_Account_To (table: NotificationOfAccount)
ALTER TABLE NotificationOfAccount ADD CONSTRAINT NotificationOfAccount_Account_To
    FOREIGN KEY ("To")
    REFERENCES Account (AccountID);

-- Reference: NotificationOfAccount_Notification (table: NotificationOfAccount)
ALTER TABLE NotificationOfAccount ADD CONSTRAINT NotificationOfAccount_Notification
    FOREIGN KEY (NotificationID)
    REFERENCES Notification (NotificationID);

-- Reference: Notification_Account (table: Notification)
ALTER TABLE Notification ADD CONSTRAINT Notification_Account
    FOREIGN KEY (CreatedBy)
    REFERENCES Account (AccountID);

-- Reference: PostAttachment_ClassPost (table: PostAttachment)
ALTER TABLE PostAttachment ADD CONSTRAINT PostAttachment_ClassPost
    FOREIGN KEY (PostID)
    REFERENCES ClassSubjectPost (PostID);

-- Reference: PostComment_Account (table: PostComment)
ALTER TABLE PostComment ADD CONSTRAINT PostComment_Account
    FOREIGN KEY (PostedBy)
    REFERENCES Account (AccountID);

-- Reference: PostComment_ClassPost (table: PostComment)
ALTER TABLE PostComment ADD CONSTRAINT PostComment_ClassPost
    FOREIGN KEY (PostID)
    REFERENCES ClassSubjectPost (PostID);

-- Reference: StudentTestAnswer_Account (table: StudentTestAnswer)
ALTER TABLE StudentTestAnswer ADD CONSTRAINT StudentTestAnswer_Account
    FOREIGN KEY (StudentID)
    REFERENCES Account (AccountID);

-- Reference: StudentTestAnswer_Answer (table: StudentTestAnswer)
ALTER TABLE StudentTestAnswer ADD CONSTRAINT StudentTestAnswer_Answer
    FOREIGN KEY (QuestionAnswerID)
    REFERENCES Answer (QuestionAnswerID);

-- Reference: StudentTestAnswer_ClassTest (table: StudentTestAnswer)
ALTER TABLE StudentTestAnswer ADD CONSTRAINT StudentTestAnswer_ClassTest
    FOREIGN KEY (TestID)
    REFERENCES ClassSubjectTest (TestID);

-- Reference: StudentTestAnswer_TestQuestion (table: StudentTestAnswer)
ALTER TABLE StudentTestAnswer ADD CONSTRAINT StudentTestAnswer_TestQuestion
    FOREIGN KEY (TestQuestionID)
    REFERENCES TestQuestion (TestQuestionID);

-- Reference: TestQuestion_ClassTest (table: TestQuestion)
ALTER TABLE TestQuestion ADD CONSTRAINT TestQuestion_ClassTest
    FOREIGN KEY (TestID)
    REFERENCES ClassSubjectTest (TestID);

-- End of file.

