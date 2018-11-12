--先建立金鑰相關程序
--以下先mark起來--
--use Center
--	--建立Master Key , 以Password : "ProView736367" 來建立這把金鑰
--	CREATE MASTER KEY ENCRYPTION BY 
--    PASSWORD = 'ProView736367'
    
--	--建立Master Key之後 , 搜尋sys.symmetric_keys資料表 , 可以檢視目前所有的金鑰清單
--    SELECT * FROM sys.symmetric_keys
    
--    --以Master Key建立 名稱'CenterCert99'的Certificate
--    CREATE CERTIFICATE CenterCert99
--	WITH SUBJECT = 'ProView ScoreClose';
    
--    --建立稱'ProViewCert'的Certificate後 , 可以檢視目前所有的Certificate清單
--    Select * from sys.certificates
    
--    --以ProViewCert Certificate建立對稱金鑰 CenterKey99 , 成績結算SP皆用此金鑰來作加密與解密
--    CREATE SYMMETRIC KEY CenterKey99
--    WITH ALGORITHM = AES_256
--    ENCRYPTION BY CERTIFICATE CenterCert99;
    
--    --建立ProViewKey之後 , 搜尋sys.symmetric_keys資料表 , 可以檢視目前所有的金鑰清單
    
--GO

--use All_Center_Data
--GO

--/****** Object:  Table [dbo].[ReplaceStandardEncrypt]    Script Date: 04/23/2015 17:56:31 ******/
--SET ANSI_NULLS ON
--GO

--SET QUOTED_IDENTIFIER ON
--GO

--SET ANSI_PADDING ON
--GO
--以上先mark起來--

--建立資料表[StandardEncrypt_108]
--基本項目成績換算加密資料表(108年1月1日啟用之表格)
use Center
CREATE TABLE [dbo].[StandardEncrypt_108](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[item] [nvarchar](20) NOT NULL,
	[gender] [nvarchar](20) NOT NULL,
	[agemax] [int] NOT NULL,
	[agemin] [int] NOT NULL,
	[standard] [int] NULL,
	[score] [int] NOT NULL
) ON [PRIMARY]
GO
--建立資料表[ReplaceStandardEncrypt_108]
--替代項目成績換算加密資料表(新版增加成績欄位)(108年1月1日啟用之表格)
use Center
CREATE TABLE [dbo].[ReplaceStandardEncrypt_108](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[item] [nvarchar](20) NOT NULL,
	[gender] [nvarchar](20) NOT NULL,
	[agemax] [int] NOT NULL,
	[agemin] [int] NOT NULL,
	[standard] [int] NULL,
	[score] [int] NOT NULL
) ON [PRIMARY]
GO

--建立資料表[Standard_108](原始未加密)
--基本項目成績換算資料表
use Center
CREATE TABLE [dbo].[Standard_108](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[item] [nvarchar](20) NOT NULL,
	[gender] [nvarchar](20) NOT NULL,
	[agemax] [int] NOT NULL,
	[agemin] [int] NOT NULL,
	[standard] [int] NULL,
	[score] [int] NOT NULL
) ON [PRIMARY]
GO
--建立資料表[ReplaceStandard_108](原始未加密)
--替代項目成績換算資料表(新版增加成績欄位)
use Center
CREATE TABLE [dbo].[ReplaceStandard_108](
	[sid] [int] IDENTITY(1,1) NOT NULL,
	[item] [nvarchar](20) NOT NULL,
	[gender] [nvarchar](20) NOT NULL,
	[agemax] [int] NOT NULL,
	[agemin] [int] NOT NULL,
	[standard] [int] NULL,
	[score] [int] NOT NULL
) ON [PRIMARY]
GO

--------------------------------------------------
------接下來要把資料塞進去，才能進行下一步驟------
--------------------------------------------------


--請先確認已經完成資料匯入--
--將基本項目成績欄位屬性更改(加大後要放加密碼的資料)
use Center
Alter Table [StandardEncrypt_108]
ALTER Column standard varbinary(8000)
Alter Table [StandardEncrypt_108]
ALTER Column score varbinary(8000)
Go

--請先確認已經完成資料匯入--
--將替代項目成績欄位屬性更改(加大後要放加密碼的資料)
use Center
Alter Table [ReplaceStandardEncrypt_108]
ALTER Column standard varbinary(8000)
Alter Table [ReplaceStandardEncrypt_108]
ALTER Column score varbinary(8000)
Go

--開始將基本項目成績加密
use Center
OPEN SYMMETRIC KEY CenterKey99
   DECRYPTION BY CERTIFICATE CenterCert99;
 UPDATE [StandardEncrypt_108]
SET score = EncryptByKey(Key_GUID('CenterKey99'), score);
GO

--開始將基本項目次數及秒數標準加密
use Center
OPEN SYMMETRIC KEY CenterKey99
   DECRYPTION BY CERTIFICATE CenterCert99;
 UPDATE [StandardEncrypt_108]
SET standard = EncryptByKey(Key_GUID('CenterKey99'), standard);
GO

--開始將替代項目成績加密
use Center
OPEN SYMMETRIC KEY CenterKey99
   DECRYPTION BY CERTIFICATE CenterCert99;
 UPDATE [ReplaceStandardEncrypt_108]
SET score = EncryptByKey(Key_GUID('CenterKey99'), score);
GO

--開始將替代項目次數及秒數標準加密
use Center
OPEN SYMMETRIC KEY CenterKey99
   DECRYPTION BY CERTIFICATE CenterCert99;
 UPDATE [ReplaceStandardEncrypt_108]
SET standard = EncryptByKey(Key_GUID('CenterKey99'), standard);
GO