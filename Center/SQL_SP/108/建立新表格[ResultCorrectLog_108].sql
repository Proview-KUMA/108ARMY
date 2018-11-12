use Center
CREATE TABLE [dbo].[ResultCorrectLog_108]
(
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[id] [varchar](10) NOT NULL,
	[name] [nvarchar](10) NOT NULL,
	[date] [datetime] NOT NULL,
	[old_sit_ups] [nvarchar](4) NULL,
	[old_push_ups] [nvarchar](4) NULL,
	[old_run] [nvarchar](4) NULL,
	[new_sit_ups] [nvarchar](4) NULL,
	[new_push_ups] [nvarchar](4) NULL,
	[new_run] [nvarchar](4) NULL,
	[account] [nvarchar](20) NOT NULL,
	[account_id] [varchar](10) NULL,
	[update_time] [datetime] NOT NULL
) ON [PRIMARY]
GO