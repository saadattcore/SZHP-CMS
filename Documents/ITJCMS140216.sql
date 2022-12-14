USE [ITJCMS]
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[Banner_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title_Ar] [nvarchar](100) NULL,
	[Title_En] [nvarchar](100) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[Banner_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Banner_Documents]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner_Documents](
	[Banner_Document_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Banner_Id] [bigint] NULL,
	[Document_Id] [bigint] NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_BannerImage] PRIMARY KEY CLUSTERED 
(
	[Banner_Document_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Career_Industry]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Career_Industry](
	[Career_Industry_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_Ar] [nvarchar](255) NULL,
	[Description_Ar] [nvarchar](max) NULL,
	[Name_En] [nvarchar](255) NULL,
	[Description_En] [nvarchar](max) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_CareerIndustry] PRIMARY KEY CLUSTERED 
(
	[Career_Industry_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Career_Job]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Career_Job](
	[Career_Job_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Career_Industry_Id] [bigint] NULL,
	[Career_Type_Id] [bigint] NULL,
	[Title_Ar] [nvarchar](255) NULL,
	[Description_Ar] [nvarchar](max) NULL,
	[Location_Ar] [nvarchar](100) NULL,
	[Salary_Ar] [nvarchar](50) NULL,
	[Salary_Currency_Ar] [nvarchar](10) NULL,
	[Minumum_Eduction_Ar] [nvarchar](100) NULL,
	[Minumum_Experience_Ar] [nvarchar](50) NULL,
	[Desired_Skills_Ar] [nvarchar](max) NULL,
	[Benefits_Ar] [nvarchar](max) NULL,
	[Responsibilities_Ar] [nvarchar](max) NULL,
	[Shift_Ar] [nvarchar](50) NULL,
	[Title_En] [nvarchar](255) NULL,
	[Description_En] [nvarchar](max) NULL,
	[Location_En] [nvarchar](100) NULL,
	[Salary_En] [nvarchar](50) NULL,
	[Salary_Currency_En] [nvarchar](10) NULL,
	[Minumum_Eduction_En] [nvarchar](100) NULL,
	[Minumum_Experience_En] [nvarchar](50) NULL,
	[Desired_Skills_En] [nvarchar](max) NULL,
	[Benefits_En] [nvarchar](max) NULL,
	[Responsibilities_En] [nvarchar](max) NULL,
	[Shift_En] [nvarchar](50) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_CareerJob] PRIMARY KEY CLUSTERED 
(
	[Career_Job_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Career_Job_Applied]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Career_Job_Applied](
	[Career_Job_Applied_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Career_Job_Id] [bigint] NULL,
	[First_Name] [nvarchar](255) NULL,
	[Last_Name] [nvarchar](255) NULL,
	[DOB] [date] NULL,
	[Marital_Status] [nvarchar](50) NULL,
	[Home_Address] [nvarchar](500) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Mobile] [nvarchar](50) NULL,
	[Eduction] [nvarchar](100) NULL,
	[Experience] [nvarchar](100) NULL,
	[Skills] [nvarchar](max) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_CareerJobApplied] PRIMARY KEY CLUSTERED 
(
	[Career_Job_Applied_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Career_Job_Document]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Career_Job_Document](
	[Career_Job_Document_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Career_Job_Id] [bigint] NULL,
	[Document_Id] [bigint] NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_Career_Job_Document] PRIMARY KEY CLUSTERED 
(
	[Career_Job_Document_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Career_Type]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Career_Type](
	[Career_Type_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_Ar] [nvarchar](100) NULL,
	[Name_En] [nvarchar](100) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_CareerType] PRIMARY KEY CLUSTERED 
(
	[Career_Type_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contact]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Contact_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[City_Ar] [nvarchar](255) NULL,
	[City_En] [nvarchar](255) NULL,
	[Location_En] [nvarchar](500) NULL,
	[Location_Ar] [nvarchar](500) NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
	[Phone] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Email] [nvarchar](255) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Contact_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Documents]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Documents](
	[Document_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[File_Type_Id] [bigint] NULL,
	[File_Name] [nvarchar](200) NULL,
	[Extenstion] [varchar](50) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[Document_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Event]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[Event_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Event_Type_Id] [bigint] NULL,
	[Title_Ar] [nvarchar](500) NULL,
	[Description_Ar] [nvarchar](max) NULL,
	[Title_En] [nvarchar](500) NULL,
	[Description_En] [nvarchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Location_Ar] [nvarchar](100) NULL,
	[Location_En] [nvarchar](100) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[Event_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Event_Document]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event_Document](
	[Event_Document_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Event_Id] [bigint] NULL,
	[Document_Id] [bigint] NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_EventImage] PRIMARY KEY CLUSTERED 
(
	[Event_Document_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Event_Type]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event_Type](
	[Event_Type_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_Ar] [nvarchar](255) NULL,
	[Name_En] [nvarchar](255) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_EventType] PRIMARY KEY CLUSTERED 
(
	[Event_Type_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FAQ]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQ](
	[FAQ_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Question_Ar] [nvarchar](500) NULL,
	[Answer_Ar] [nvarchar](max) NULL,
	[Question_En] [nvarchar](500) NULL,
	[Answer_En] [nvarchar](max) NULL,
	[FAQ_Category_Id] [bigint] NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_FAQ] PRIMARY KEY CLUSTERED 
(
	[FAQ_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FAQ_Category]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQ_Category](
	[FAQ_Category_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_Ar] [nvarchar](255) NULL,
	[Name_En] [nvarchar](255) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_FAQCategory] PRIMARY KEY CLUSTERED 
(
	[FAQ_Category_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[File_Types]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[File_Types](
	[File_Type_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_En] [nvarchar](50) NULL,
	[Name_Ar] [nvarchar](50) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_File_Types] PRIMARY KEY CLUSTERED 
(
	[File_Type_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gallery]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gallery](
	[Gallery_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Document_Id] [bigint] NULL,
	[Title_En] [nvarchar](255) NULL,
	[Title_Ar] [nvarchar](255) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[Gallery_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Link]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Link](
	[Link_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title_Ar] [nvarchar](255) NULL,
	[Link_Url] [nvarchar](100) NULL,
	[Title_En] [nvarchar](255) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_Link] PRIMARY KEY CLUSTERED 
(
	[Link_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Link_Document]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Link_Document](
	[Link_Document_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Link_Id] [bigint] NULL,
	[Document_Id] [bigint] NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_Link_Document] PRIMARY KEY CLUSTERED 
(
	[Link_Document_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menu]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Menu_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Menu_Name_En] [nvarchar](255) NULL,
	[Menu_Name_Ar] [nvarchar](255) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Menu_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[News_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title_Ar] [nvarchar](100) NULL,
	[Title_En] [nvarchar](100) NULL,
	[Description_Ar] [text] NULL,
	[Description_En] [text] NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[News_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News_Documents]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News_Documents](
	[News_Document_Id] [bigint] NOT NULL,
	[News_Id] [bigint] NULL,
	[Document_Id] [bigint] NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_News_Documents] PRIMARY KEY CLUSTERED 
(
	[News_Document_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Page]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[Page_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Page_Name_En] [nvarchar](100) NULL,
	[Page_Content_En] [nvarchar](max) NULL,
	[Parent_Id] [bigint] NULL,
	[Meta_Title_En] [nvarchar](255) NULL,
	[Meta_Keywords_En] [nvarchar](max) NULL,
	[Meta_Description_En] [nvarchar](max) NULL,
	[IsStandalone] [bit] NULL,
	[Page_Name_Ar] [nvarchar](100) NULL,
	[Page_Content_Ar] [nvarchar](max) NULL,
	[Meta_Title_Ar] [nvarchar](255) NULL,
	[Meta_Keywords_Ar] [nvarchar](max) NULL,
	[Meta_Description_Ar] [nvarchar](max) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[Page_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Page_Menu]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page_Menu](
	[Page_Menu_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Page_Id] [bigint] NULL,
	[Menu_Id] [bigint] NULL,
 CONSTRAINT [PK_PageMenu] PRIMARY KEY CLUSTERED 
(
	[Page_Menu_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Row_Statuses]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Row_Statuses](
	[Row_Status_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_Ar] [nvarchar](50) NULL,
	[Name_En] [nvarchar](50) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
 CONSTRAINT [PK_Row_Statuses] PRIMARY KEY CLUSTERED 
(
	[Row_Status_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subscriber]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscriber](
	[Subscriber_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_Ar] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Subscription_Type_Id] [bigint] NULL,
	[Name_En] [nvarchar](255) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_Subscriber] PRIMARY KEY CLUSTERED 
(
	[Subscriber_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subscription_Type]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscription_Type](
	[Subscription_Type_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_Ar] [nvarchar](255) NULL,
	[Name_En] [nvarchar](255) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_SubscriptionType] PRIMARY KEY CLUSTERED 
(
	[Subscription_Type_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Survey]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Survey](
	[Survey_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Title_Ar] [nvarchar](255) NULL,
	[Description_Ar] [nvarchar](max) NULL,
	[Title_En] [nvarchar](255) NULL,
	[Description_En] [nvarchar](max) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_Survey] PRIMARY KEY CLUSTERED 
(
	[Survey_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Survey_Question]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Survey_Question](
	[Survey_Question_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Survey_Id] [bigint] NULL,
	[Question_Ar] [nvarchar](255) NULL,
	[Question_En] [nvarchar](255) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_SurveyQuestion] PRIMARY KEY CLUSTERED 
(
	[Survey_Question_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Survey_Question_Answer]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Survey_Question_Answer](
	[Survey_Question_Answer_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Survery_Id] [bigint] NULL,
	[Survey_Question_Id] [bigint] NULL,
	[Surver_Question_Option_Id] [bigint] NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_SurveyQuestionAnswer] PRIMARY KEY CLUSTERED 
(
	[Survey_Question_Answer_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Survey_Question_Option]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Survey_Question_Option](
	[Survey_Question_Option_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Survey_Question_Id] [bigint] NULL,
	[Option_Text_Ar] [nvarchar](255) NULL,
	[Option_Type_En] [nvarchar](50) NULL,
	[Option_Text_En] [nvarchar](255) NULL,
	[Option_Type_Ar] [nvarchar](50) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_SurveyQuestionOption] PRIMARY KEY CLUSTERED 
(
	[Survey_Question_Option_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Training_Plan]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Training_Plan](
	[Training_Plan_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Training_Program_Id] [bigint] NULL,
	[Location_Ar] [nvarchar](50) NULL,
	[Theme_Ar] [nvarchar](255) NULL,
	[Date] [date] NULL,
	[Audience_Ar] [nvarchar](255) NULL,
	[Notes_Ar] [nvarchar](max) NULL,
	[Location_En] [nvarchar](50) NULL,
	[Audience_En] [nvarchar](255) NULL,
	[Notes_En] [nvarchar](max) NULL,
	[Theme_En] [nvarchar](255) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_TrainingPlan] PRIMARY KEY CLUSTERED 
(
	[Training_Plan_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Training_Program]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Training_Program](
	[Training_Program_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name_Ar] [nvarchar](255) NULL,
	[Description_Ar] [nvarchar](255) NULL,
	[Name_En] [nvarchar](255) NULL,
	[Description_En] [nvarchar](255) NULL,
	[Created_By] [bigint] NULL,
	[Created_Date] [datetime] NULL,
	[Updated_By] [bigint] NULL,
	[Updated_Date] [datetime] NULL,
	[Row_Status_Id] [bigint] NULL,
 CONSTRAINT [PK_TrainingProgram] PRIMARY KEY CLUSTERED 
(
	[Training_Program_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User_Temp]    Script Date: 2/14/2016 11:01:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_Temp](
	[User_Id] [bigint] IDENTITY(1,1) NOT NULL,
	[First_Name] [nvarchar](100) NULL,
	[Last_Name] [nvarchar](100) NULL,
	[Password] [nvarchar](20) NULL,
	[DOB] [date] NULL,
	[Education] [nvarchar](50) NULL,
	[Experience] [nvarchar](50) NULL,
	[Home_Address] [nvarchar](100) NULL,
	[Phone] [nvarchar](50) NULL,
	[Mobile] [nvarchar](50) NULL,
 CONSTRAINT [PK_User_Temp] PRIMARY KEY CLUSTERED 
(
	[User_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Banner] ADD  CONSTRAINT [DF_Banner_Created_At]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Banner_Documents] ADD  CONSTRAINT [DF_Banner_Documents_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Career_Industry] ADD  CONSTRAINT [DF_CareerIndustry_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Career_Job] ADD  CONSTRAINT [DF_CareerJob_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Career_Job_Applied] ADD  CONSTRAINT [DF_CareerJobApplied_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Career_Job_Document] ADD  CONSTRAINT [DF_Career_Job_Document_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Career_Type] ADD  CONSTRAINT [DF_CareerType_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Contact] ADD  CONSTRAINT [DF_Contact_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Documents] ADD  CONSTRAINT [DF_Documents_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Event] ADD  CONSTRAINT [DF_Event_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Event_Document] ADD  CONSTRAINT [DF_EventImage_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Event_Type] ADD  CONSTRAINT [DF_EventType_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[FAQ] ADD  CONSTRAINT [DF_FAQ_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[FAQ_Category] ADD  CONSTRAINT [DF_FAQCategory_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[File_Types] ADD  CONSTRAINT [DF_File_Types_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Gallery] ADD  CONSTRAINT [DF_Gallery_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Link] ADD  CONSTRAINT [DF_Link_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Link_Document] ADD  CONSTRAINT [DF_Link_Document_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[News] ADD  CONSTRAINT [DF_News_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[News_Documents] ADD  CONSTRAINT [DF_News_Documents_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Page] ADD  CONSTRAINT [DF_Page_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Row_Statuses] ADD  CONSTRAINT [DF_Row_Statuses_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Subscriber] ADD  CONSTRAINT [DF_Subscriber_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Subscription_Type] ADD  CONSTRAINT [DF_SubscriptionType_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Survey] ADD  CONSTRAINT [DF_Survey_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Survey_Question] ADD  CONSTRAINT [DF_SurveyQuestion_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Survey_Question_Answer] ADD  CONSTRAINT [DF_SurveyQuestionAnswer_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Survey_Question_Option] ADD  CONSTRAINT [DF_SurveyQuestionOption_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Training_Plan] ADD  CONSTRAINT [DF_TrainingPlan_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Training_Program] ADD  CONSTRAINT [DF_TrainingProgram_Created_Date]  DEFAULT (getdate()) FOR [Created_Date]
GO
ALTER TABLE [dbo].[Banner_Documents]  WITH CHECK ADD  CONSTRAINT [FK_Banner_Documents_Banner] FOREIGN KEY([Banner_Id])
REFERENCES [dbo].[Banner] ([Banner_Id])
GO
ALTER TABLE [dbo].[Banner_Documents] CHECK CONSTRAINT [FK_Banner_Documents_Banner]
GO
ALTER TABLE [dbo].[Banner_Documents]  WITH CHECK ADD  CONSTRAINT [FK_Banner_Documents_Banner_Documents] FOREIGN KEY([Document_Id])
REFERENCES [dbo].[Documents] ([Document_Id])
GO
ALTER TABLE [dbo].[Banner_Documents] CHECK CONSTRAINT [FK_Banner_Documents_Banner_Documents]
GO
ALTER TABLE [dbo].[Career_Job]  WITH CHECK ADD  CONSTRAINT [FK_CareerJob_CareerIndustry] FOREIGN KEY([Career_Industry_Id])
REFERENCES [dbo].[Career_Industry] ([Career_Industry_Id])
GO
ALTER TABLE [dbo].[Career_Job] CHECK CONSTRAINT [FK_CareerJob_CareerIndustry]
GO
ALTER TABLE [dbo].[Career_Job]  WITH CHECK ADD  CONSTRAINT [FK_CareerJob_CareerType] FOREIGN KEY([Career_Type_Id])
REFERENCES [dbo].[Career_Type] ([Career_Type_Id])
GO
ALTER TABLE [dbo].[Career_Job] CHECK CONSTRAINT [FK_CareerJob_CareerType]
GO
ALTER TABLE [dbo].[Career_Job_Applied]  WITH CHECK ADD  CONSTRAINT [FK_CareerJobApplied_CareerJob] FOREIGN KEY([Career_Job_Id])
REFERENCES [dbo].[Career_Job] ([Career_Job_Id])
GO
ALTER TABLE [dbo].[Career_Job_Applied] CHECK CONSTRAINT [FK_CareerJobApplied_CareerJob]
GO
ALTER TABLE [dbo].[Career_Job_Document]  WITH CHECK ADD  CONSTRAINT [FK_Career_Job_Document_Career_Job] FOREIGN KEY([Career_Job_Id])
REFERENCES [dbo].[Career_Job] ([Career_Job_Id])
GO
ALTER TABLE [dbo].[Career_Job_Document] CHECK CONSTRAINT [FK_Career_Job_Document_Career_Job]
GO
ALTER TABLE [dbo].[Career_Job_Document]  WITH CHECK ADD  CONSTRAINT [FK_Career_Job_Document_Documents] FOREIGN KEY([Document_Id])
REFERENCES [dbo].[Documents] ([Document_Id])
GO
ALTER TABLE [dbo].[Career_Job_Document] CHECK CONSTRAINT [FK_Career_Job_Document_Documents]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_EventType] FOREIGN KEY([Event_Type_Id])
REFERENCES [dbo].[Event_Type] ([Event_Type_Id])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_EventType]
GO
ALTER TABLE [dbo].[Event_Document]  WITH CHECK ADD  CONSTRAINT [FK_Event_Document_Documents] FOREIGN KEY([Document_Id])
REFERENCES [dbo].[Documents] ([Document_Id])
GO
ALTER TABLE [dbo].[Event_Document] CHECK CONSTRAINT [FK_Event_Document_Documents]
GO
ALTER TABLE [dbo].[Event_Document]  WITH CHECK ADD  CONSTRAINT [FK_Event_Document_Event] FOREIGN KEY([Event_Id])
REFERENCES [dbo].[Event] ([Event_Id])
GO
ALTER TABLE [dbo].[Event_Document] CHECK CONSTRAINT [FK_Event_Document_Event]
GO
ALTER TABLE [dbo].[FAQ]  WITH CHECK ADD  CONSTRAINT [FK_FAQ_FAQCategory] FOREIGN KEY([FAQ_Category_Id])
REFERENCES [dbo].[FAQ_Category] ([FAQ_Category_Id])
GO
ALTER TABLE [dbo].[FAQ] CHECK CONSTRAINT [FK_FAQ_FAQCategory]
GO
ALTER TABLE [dbo].[Gallery]  WITH CHECK ADD  CONSTRAINT [FK_Gallery_Documents] FOREIGN KEY([Document_Id])
REFERENCES [dbo].[Documents] ([Document_Id])
GO
ALTER TABLE [dbo].[Gallery] CHECK CONSTRAINT [FK_Gallery_Documents]
GO
ALTER TABLE [dbo].[Link_Document]  WITH CHECK ADD  CONSTRAINT [FK_Link_Document_Documents] FOREIGN KEY([Document_Id])
REFERENCES [dbo].[Documents] ([Document_Id])
GO
ALTER TABLE [dbo].[Link_Document] CHECK CONSTRAINT [FK_Link_Document_Documents]
GO
ALTER TABLE [dbo].[Link_Document]  WITH CHECK ADD  CONSTRAINT [FK_Link_Document_Link] FOREIGN KEY([Link_Id])
REFERENCES [dbo].[Link] ([Link_Id])
GO
ALTER TABLE [dbo].[Link_Document] CHECK CONSTRAINT [FK_Link_Document_Link]
GO
ALTER TABLE [dbo].[News_Documents]  WITH CHECK ADD  CONSTRAINT [FK_Document_Id] FOREIGN KEY([Document_Id])
REFERENCES [dbo].[Documents] ([Document_Id])
GO
ALTER TABLE [dbo].[News_Documents] CHECK CONSTRAINT [FK_Document_Id]
GO
ALTER TABLE [dbo].[News_Documents]  WITH CHECK ADD  CONSTRAINT [FK_News_Id] FOREIGN KEY([News_Id])
REFERENCES [dbo].[News] ([News_Id])
GO
ALTER TABLE [dbo].[News_Documents] CHECK CONSTRAINT [FK_News_Id]
GO
ALTER TABLE [dbo].[Page]  WITH CHECK ADD  CONSTRAINT [FK_Page_Page] FOREIGN KEY([Parent_Id])
REFERENCES [dbo].[Page] ([Page_Id])
GO
ALTER TABLE [dbo].[Page] CHECK CONSTRAINT [FK_Page_Page]
GO
ALTER TABLE [dbo].[Page_Menu]  WITH CHECK ADD  CONSTRAINT [FK_PageMenu_Menu] FOREIGN KEY([Menu_Id])
REFERENCES [dbo].[Menu] ([Menu_Id])
GO
ALTER TABLE [dbo].[Page_Menu] CHECK CONSTRAINT [FK_PageMenu_Menu]
GO
ALTER TABLE [dbo].[Page_Menu]  WITH CHECK ADD  CONSTRAINT [FK_PageMenu_Page] FOREIGN KEY([Page_Id])
REFERENCES [dbo].[Page] ([Page_Id])
GO
ALTER TABLE [dbo].[Page_Menu] CHECK CONSTRAINT [FK_PageMenu_Page]
GO
ALTER TABLE [dbo].[Subscriber]  WITH CHECK ADD  CONSTRAINT [FK_Subscriber_SubscriptionType] FOREIGN KEY([Subscription_Type_Id])
REFERENCES [dbo].[Subscription_Type] ([Subscription_Type_Id])
GO
ALTER TABLE [dbo].[Subscriber] CHECK CONSTRAINT [FK_Subscriber_SubscriptionType]
GO
ALTER TABLE [dbo].[Survey_Question]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestion_Survey] FOREIGN KEY([Survey_Id])
REFERENCES [dbo].[Survey] ([Survey_Id])
GO
ALTER TABLE [dbo].[Survey_Question] CHECK CONSTRAINT [FK_SurveyQuestion_Survey]
GO
ALTER TABLE [dbo].[Survey_Question_Answer]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestionAnswer_Survey] FOREIGN KEY([Survery_Id])
REFERENCES [dbo].[Survey] ([Survey_Id])
GO
ALTER TABLE [dbo].[Survey_Question_Answer] CHECK CONSTRAINT [FK_SurveyQuestionAnswer_Survey]
GO
ALTER TABLE [dbo].[Survey_Question_Answer]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestionAnswer_SurveyQuestion] FOREIGN KEY([Survey_Question_Id])
REFERENCES [dbo].[Survey_Question] ([Survey_Question_Id])
GO
ALTER TABLE [dbo].[Survey_Question_Answer] CHECK CONSTRAINT [FK_SurveyQuestionAnswer_SurveyQuestion]
GO
ALTER TABLE [dbo].[Survey_Question_Answer]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestionAnswer_SurveyQuestionOption] FOREIGN KEY([Surver_Question_Option_Id])
REFERENCES [dbo].[Survey_Question_Option] ([Survey_Question_Option_Id])
GO
ALTER TABLE [dbo].[Survey_Question_Answer] CHECK CONSTRAINT [FK_SurveyQuestionAnswer_SurveyQuestionOption]
GO
ALTER TABLE [dbo].[Survey_Question_Option]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestionOption_SurveyQuestion] FOREIGN KEY([Survey_Question_Id])
REFERENCES [dbo].[Survey_Question] ([Survey_Question_Id])
GO
ALTER TABLE [dbo].[Survey_Question_Option] CHECK CONSTRAINT [FK_SurveyQuestionOption_SurveyQuestion]
GO
ALTER TABLE [dbo].[Training_Plan]  WITH CHECK ADD  CONSTRAINT [FK_TrainingPlan_TrainingProgram] FOREIGN KEY([Training_Program_Id])
REFERENCES [dbo].[Training_Program] ([Training_Program_Id])
GO
ALTER TABLE [dbo].[Training_Plan] CHECK CONSTRAINT [FK_TrainingPlan_TrainingProgram]
GO
