USE [ITJCMS]
GO
/****** Object:  Table [dbo].[Banner]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Banner](
	[BannerId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_Banner] PRIMARY KEY CLUSTERED 
(
	[BannerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BannerImage]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BannerImage](
	[BannerImageId] [int] IDENTITY(1,1) NOT NULL,
	[BannerId] [int] NULL,
	[ImageUrl] [nvarchar](100) NULL,
	[ImageAltText] [nvarchar](255) NULL,
	[IsPublished] [bit] NULL,
 CONSTRAINT [PK_BannerImage] PRIMARY KEY CLUSTERED 
(
	[BannerImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CareerIndustry]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CareerIndustry](
	[CareerIndustryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_CareerIndustry] PRIMARY KEY CLUSTERED 
(
	[CareerIndustryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CareerJob]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CareerJob](
	[CareerJobId] [int] IDENTITY(1,1) NOT NULL,
	[CareerIndustryId] [int] NULL,
	[CareerTypeId] [int] NULL,
	[Title] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[Location] [nvarchar](100) NULL,
	[Salary] [nvarchar](50) NULL,
	[SalaryCurrency] [nvarchar](10) NULL,
	[MinumumEduction] [nvarchar](100) NULL,
	[MinumumExperience] [nvarchar](50) NULL,
	[DesiredSkills] [nvarchar](max) NULL,
	[Benefits] [nvarchar](max) NULL,
	[Responsibilities] [nvarchar](max) NULL,
	[Shift] [nvarchar](50) NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_CareerJob] PRIMARY KEY CLUSTERED 
(
	[CareerJobId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CareerJobApplied]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CareerJobApplied](
	[CareerJobAppliedId] [int] IDENTITY(1,1) NOT NULL,
	[CareerJobId] [int] NULL,
	[FirstName] [nvarchar](255) NULL,
	[LastName] [nvarchar](255) NULL,
	[DOB] [date] NULL,
	[MaritalStatus] [nvarchar](50) NULL,
	[HomeAddress] [nvarchar](500) NULL,
	[Phone] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[Mobile] [nvarchar](50) NULL,
	[Eduction] [nvarchar](100) NULL,
	[Experience] [nvarchar](100) NULL,
	[Skills] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](100) NULL,
	[ResumeUrl] [nvarchar](100) NULL,
	[CreationDate] [datetime] NULL,
 CONSTRAINT [PK_CareerJobApplied] PRIMARY KEY CLUSTERED 
(
	[CareerJobAppliedId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CareerType]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CareerType](
	[CareerTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_CareerType] PRIMARY KEY CLUSTERED 
(
	[CareerTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Contact]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactId] [int] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](255) NULL,
	[Location] [nvarchar](500) NULL,
	[Latitude] [float] NULL,
	[Longitude] [float] NULL,
	[Phone] [nvarchar](50) NULL,
	[Fax] [nvarchar](50) NULL,
	[Email] [nvarchar](255) NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Event]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Event](
	[EventId] [int] IDENTITY(1,1) NOT NULL,
	[EventTypeId] [int] NULL,
	[Title] [nvarchar](500) NULL,
	[Description] [nvarchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Location] [nvarchar](100) NULL,
	[MainImageUrl] [nvarchar](100) NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED 
(
	[EventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventImage]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventImage](
	[EventImageId] [int] IDENTITY(1,1) NOT NULL,
	[EventId] [int] NULL,
	[EventImageUrl] [nvarchar](100) NULL,
	[EventImageAltText] [nvarchar](255) NULL,
	[IsPublished] [bit] NULL,
 CONSTRAINT [PK_EventImage] PRIMARY KEY CLUSTERED 
(
	[EventImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventType]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventType](
	[EventTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_EventType] PRIMARY KEY CLUSTERED 
(
	[EventTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FAQ]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQ](
	[FAQId] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](500) NULL,
	[Answer] [nvarchar](max) NULL,
	[FAQCategoryId] [int] NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_FAQ] PRIMARY KEY CLUSTERED 
(
	[FAQId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FAQCategory]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQCategory](
	[FAQCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_FAQCategory] PRIMARY KEY CLUSTERED 
(
	[FAQCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gallery]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gallery](
	[GalleryId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[FileUrl] [nvarchar](100) NULL,
	[FileFormat] [nvarchar](50) NULL,
	[AltText] [nvarchar](255) NULL,
	[IsVideo] [bit] NULL,
	[IsPublished] [bit] NULL,
	[CreationBy] [int] NULL,
	[CreationDate] [datetime] NULL,
 CONSTRAINT [PK_Gallery] PRIMARY KEY CLUSTERED 
(
	[GalleryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Link]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Link](
	[LinkId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[LinkUrl] [nvarchar](100) NULL,
	[ImageUrl] [nvarchar](255) NULL,
	[ImageAltText] [nvarchar](255) NULL,
	[IsPublished] [bit] NULL,
	[CreationBy] [int] NULL,
	[CreationDate] [datetime] NULL,
 CONSTRAINT [PK_Link] PRIMARY KEY CLUSTERED 
(
	[LinkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Menu]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuId] [int] IDENTITY(1,1) NOT NULL,
	[MenuName] [nvarchar](255) NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[News]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[News](
	[NewsId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[ImageUrl] [nvarchar](100) NULL,
	[ImageAltText] [nvarchar](255) NULL,
	[NewsDate] [datetime] NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[NewsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Page]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Page](
	[PageId] [int] IDENTITY(1,1) NOT NULL,
	[PageName] [nvarchar](100) NULL,
	[PageContent] [nvarchar](max) NULL,
	[ParentId] [int] NULL,
	[MetaTitle] [nvarchar](255) NULL,
	[MetaKeywords] [nvarchar](max) NULL,
	[MetaDescription] [nvarchar](max) NULL,
	[IsStandalone] [bit] NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[PageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PageMenu]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PageMenu](
	[PageMenuId] [int] IDENTITY(1,1) NOT NULL,
	[PageId] [int] NULL,
	[MenuId] [int] NULL,
 CONSTRAINT [PK_PageMenu] PRIMARY KEY CLUSTERED 
(
	[PageMenuId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subscriber]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscriber](
	[SubscriberId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[SubscriptionTypeId] [int] NULL,
 CONSTRAINT [PK_Subscriber] PRIMARY KEY CLUSTERED 
(
	[SubscriberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubscriptionType]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriptionType](
	[SubscriptionTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_SubscriptionType] PRIMARY KEY CLUSTERED 
(
	[SubscriptionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Survey]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Survey](
	[SurveyId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Description] [nvarchar](max) NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_Survey] PRIMARY KEY CLUSTERED 
(
	[SurveyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SurveyQuestion]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyQuestion](
	[SurveyQuestionId] [int] IDENTITY(1,1) NOT NULL,
	[SurveyId] [int] NULL,
	[Question] [nvarchar](255) NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_SurveyQuestion] PRIMARY KEY CLUSTERED 
(
	[SurveyQuestionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SurveyQuestionAnswer]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyQuestionAnswer](
	[SurveyQuestionAnswerId] [int] IDENTITY(1,1) NOT NULL,
	[SurveryId] [int] NULL,
	[SurveyQuestionId] [int] NULL,
	[SurverQuestionOptionId] [int] NULL,
	[StatInfo] [nvarchar](500) NULL,
 CONSTRAINT [PK_SurveyQuestionAnswer] PRIMARY KEY CLUSTERED 
(
	[SurveyQuestionAnswerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SurveyQuestionOption]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SurveyQuestionOption](
	[SurveyQuestionOptionId] [int] IDENTITY(1,1) NOT NULL,
	[SurveyQuestionId] [int] NULL,
	[OptionText] [nvarchar](255) NULL,
	[OptionType] [nvarchar](50) NULL,
 CONSTRAINT [PK_SurveyQuestionOption] PRIMARY KEY CLUSTERED 
(
	[SurveyQuestionOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrainingPlan]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingPlan](
	[TrainingPlanId] [int] IDENTITY(1,1) NOT NULL,
	[TrainingProgramId] [int] NULL,
	[Location] [nvarchar](50) NULL,
	[Theme] [nvarchar](255) NULL,
	[Date] [date] NULL,
	[Audience] [nvarchar](255) NULL,
	[Notes] [nvarchar](max) NULL,
	[IsPublished] [bit] NULL,
	[CreationBy] [int] NULL,
	[CreationDate] [datetime] NULL,
 CONSTRAINT [PK_TrainingPlan] PRIMARY KEY CLUSTERED 
(
	[TrainingPlanId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TrainingProgram]    Script Date: 2/9/2016 2:26:23 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TrainingProgram](
	[TrainingProgramId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NULL,
	[Description] [nvarchar](255) NULL,
	[IsPublished] [bit] NULL,
	[CreationDate] [datetime] NULL,
	[CreationBy] [int] NULL,
 CONSTRAINT [PK_TrainingProgram] PRIMARY KEY CLUSTERED 
(
	[TrainingProgramId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BannerImage]  WITH CHECK ADD  CONSTRAINT [FK_BannerImage_Banner] FOREIGN KEY([BannerId])
REFERENCES [dbo].[Banner] ([BannerId])
GO
ALTER TABLE [dbo].[BannerImage] CHECK CONSTRAINT [FK_BannerImage_Banner]
GO
ALTER TABLE [dbo].[CareerJob]  WITH CHECK ADD  CONSTRAINT [FK_CareerJob_CareerIndustry] FOREIGN KEY([CareerIndustryId])
REFERENCES [dbo].[CareerIndustry] ([CareerIndustryId])
GO
ALTER TABLE [dbo].[CareerJob] CHECK CONSTRAINT [FK_CareerJob_CareerIndustry]
GO
ALTER TABLE [dbo].[CareerJob]  WITH CHECK ADD  CONSTRAINT [FK_CareerJob_CareerType] FOREIGN KEY([CareerTypeId])
REFERENCES [dbo].[CareerType] ([CareerTypeId])
GO
ALTER TABLE [dbo].[CareerJob] CHECK CONSTRAINT [FK_CareerJob_CareerType]
GO
ALTER TABLE [dbo].[CareerJobApplied]  WITH CHECK ADD  CONSTRAINT [FK_CareerJobApplied_CareerJob] FOREIGN KEY([CareerJobId])
REFERENCES [dbo].[CareerJob] ([CareerJobId])
GO
ALTER TABLE [dbo].[CareerJobApplied] CHECK CONSTRAINT [FK_CareerJobApplied_CareerJob]
GO
ALTER TABLE [dbo].[Event]  WITH CHECK ADD  CONSTRAINT [FK_Event_EventType] FOREIGN KEY([EventTypeId])
REFERENCES [dbo].[EventType] ([EventTypeId])
GO
ALTER TABLE [dbo].[Event] CHECK CONSTRAINT [FK_Event_EventType]
GO
ALTER TABLE [dbo].[EventImage]  WITH CHECK ADD  CONSTRAINT [FK_EventImage_Event] FOREIGN KEY([EventId])
REFERENCES [dbo].[Event] ([EventId])
GO
ALTER TABLE [dbo].[EventImage] CHECK CONSTRAINT [FK_EventImage_Event]
GO
ALTER TABLE [dbo].[FAQ]  WITH CHECK ADD  CONSTRAINT [FK_FAQ_FAQCategory] FOREIGN KEY([FAQCategoryId])
REFERENCES [dbo].[FAQCategory] ([FAQCategoryId])
GO
ALTER TABLE [dbo].[FAQ] CHECK CONSTRAINT [FK_FAQ_FAQCategory]
GO
ALTER TABLE [dbo].[PageMenu]  WITH CHECK ADD  CONSTRAINT [FK_PageMenu_Menu] FOREIGN KEY([MenuId])
REFERENCES [dbo].[Menu] ([MenuId])
GO
ALTER TABLE [dbo].[PageMenu] CHECK CONSTRAINT [FK_PageMenu_Menu]
GO
ALTER TABLE [dbo].[PageMenu]  WITH CHECK ADD  CONSTRAINT [FK_PageMenu_Page] FOREIGN KEY([PageId])
REFERENCES [dbo].[Page] ([PageId])
GO
ALTER TABLE [dbo].[PageMenu] CHECK CONSTRAINT [FK_PageMenu_Page]
GO
ALTER TABLE [dbo].[Subscriber]  WITH CHECK ADD  CONSTRAINT [FK_Subscriber_SubscriptionType] FOREIGN KEY([SubscriptionTypeId])
REFERENCES [dbo].[SubscriptionType] ([SubscriptionTypeId])
GO
ALTER TABLE [dbo].[Subscriber] CHECK CONSTRAINT [FK_Subscriber_SubscriptionType]
GO
ALTER TABLE [dbo].[SurveyQuestion]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestion_Survey] FOREIGN KEY([SurveyId])
REFERENCES [dbo].[Survey] ([SurveyId])
GO
ALTER TABLE [dbo].[SurveyQuestion] CHECK CONSTRAINT [FK_SurveyQuestion_Survey]
GO
ALTER TABLE [dbo].[SurveyQuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestionAnswer_Survey] FOREIGN KEY([SurveryId])
REFERENCES [dbo].[Survey] ([SurveyId])
GO
ALTER TABLE [dbo].[SurveyQuestionAnswer] CHECK CONSTRAINT [FK_SurveyQuestionAnswer_Survey]
GO
ALTER TABLE [dbo].[SurveyQuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestionAnswer_SurveyQuestion] FOREIGN KEY([SurveyQuestionId])
REFERENCES [dbo].[SurveyQuestion] ([SurveyQuestionId])
GO
ALTER TABLE [dbo].[SurveyQuestionAnswer] CHECK CONSTRAINT [FK_SurveyQuestionAnswer_SurveyQuestion]
GO
ALTER TABLE [dbo].[SurveyQuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestionAnswer_SurveyQuestionOption] FOREIGN KEY([SurverQuestionOptionId])
REFERENCES [dbo].[SurveyQuestionOption] ([SurveyQuestionOptionId])
GO
ALTER TABLE [dbo].[SurveyQuestionAnswer] CHECK CONSTRAINT [FK_SurveyQuestionAnswer_SurveyQuestionOption]
GO
ALTER TABLE [dbo].[SurveyQuestionOption]  WITH CHECK ADD  CONSTRAINT [FK_SurveyQuestionOption_SurveyQuestion] FOREIGN KEY([SurveyQuestionId])
REFERENCES [dbo].[SurveyQuestion] ([SurveyQuestionId])
GO
ALTER TABLE [dbo].[SurveyQuestionOption] CHECK CONSTRAINT [FK_SurveyQuestionOption_SurveyQuestion]
GO
ALTER TABLE [dbo].[TrainingPlan]  WITH CHECK ADD  CONSTRAINT [FK_TrainingPlan_TrainingProgram] FOREIGN KEY([TrainingProgramId])
REFERENCES [dbo].[TrainingProgram] ([TrainingProgramId])
GO
ALTER TABLE [dbo].[TrainingPlan] CHECK CONSTRAINT [FK_TrainingPlan_TrainingProgram]
GO
