--���إߪ��_�����{��
--�H�U��mark�_��--
--use Center
--	--�إ�Master Key , �HPassword : "ProView736367" �ӫإ߳o����_
--	CREATE MASTER KEY ENCRYPTION BY 
--    PASSWORD = 'ProView736367'
    
--	--�إ�Master Key���� , �j�Msys.symmetric_keys��ƪ� , �i�H�˵��ثe�Ҧ������_�M��
--    SELECT * FROM sys.symmetric_keys
    
--    --�HMaster Key�إ� �W��'CenterCert99'��Certificate
--    CREATE CERTIFICATE CenterCert99
--	WITH SUBJECT = 'ProView ScoreClose';
    
--    --�إߺ�'ProViewCert'��Certificate�� , �i�H�˵��ثe�Ҧ���Certificate�M��
--    Select * from sys.certificates
    
--    --�HProViewCert Certificate�إ߹�٪��_ CenterKey99 , ���Z����SP�ҥΦ����_�ӧ@�[�K�P�ѱK
--    CREATE SYMMETRIC KEY CenterKey99
--    WITH ALGORITHM = AES_256
--    ENCRYPTION BY CERTIFICATE CenterCert99;
    
--    --�إ�ProViewKey���� , �j�Msys.symmetric_keys��ƪ� , �i�H�˵��ثe�Ҧ������_�M��
    
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
--�H�W��mark�_��--

--�إ߸�ƪ�[StandardEncrypt_108]
--�򥻶��ئ��Z����[�K��ƪ�(108�~1��1��ҥΤ����)
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
--�إ߸�ƪ�[ReplaceStandardEncrypt_108]
--���N���ئ��Z����[�K��ƪ�(�s���W�[���Z���)(108�~1��1��ҥΤ����)
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

--�إ߸�ƪ�[Standard_108](��l���[�K)
--�򥻶��ئ��Z�����ƪ�
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
--�إ߸�ƪ�[ReplaceStandard_108](��l���[�K)
--���N���ئ��Z�����ƪ�(�s���W�[���Z���)
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
------���U�ӭn���ƶ�i�h�A�~��i��U�@�B�J------
--------------------------------------------------


--�Х��T�{�w�g������ƶפJ--
--�N�򥻶��ئ��Z����ݩʧ��(�[�j��n��[�K�X�����)
use Center
Alter Table [StandardEncrypt_108]
ALTER Column standard varbinary(8000)
Alter Table [StandardEncrypt_108]
ALTER Column score varbinary(8000)
Go

--�Х��T�{�w�g������ƶפJ--
--�N���N���ئ��Z����ݩʧ��(�[�j��n��[�K�X�����)
use Center
Alter Table [ReplaceStandardEncrypt_108]
ALTER Column standard varbinary(8000)
Alter Table [ReplaceStandardEncrypt_108]
ALTER Column score varbinary(8000)
Go

--�}�l�N�򥻶��ئ��Z�[�K
use Center
OPEN SYMMETRIC KEY CenterKey99
   DECRYPTION BY CERTIFICATE CenterCert99;
 UPDATE [StandardEncrypt_108]
SET score = EncryptByKey(Key_GUID('CenterKey99'), score);
GO

--�}�l�N�򥻶��ئ��Ƥά�Ƽзǥ[�K
use Center
OPEN SYMMETRIC KEY CenterKey99
   DECRYPTION BY CERTIFICATE CenterCert99;
 UPDATE [StandardEncrypt_108]
SET standard = EncryptByKey(Key_GUID('CenterKey99'), standard);
GO

--�}�l�N���N���ئ��Z�[�K
use Center
OPEN SYMMETRIC KEY CenterKey99
   DECRYPTION BY CERTIFICATE CenterCert99;
 UPDATE [ReplaceStandardEncrypt_108]
SET score = EncryptByKey(Key_GUID('CenterKey99'), score);
GO

--�}�l�N���N���ئ��Ƥά�Ƽзǥ[�K
use Center
OPEN SYMMETRIC KEY CenterKey99
   DECRYPTION BY CERTIFICATE CenterCert99;
 UPDATE [ReplaceStandardEncrypt_108]
SET standard = EncryptByKey(Key_GUID('CenterKey99'), standard);
GO