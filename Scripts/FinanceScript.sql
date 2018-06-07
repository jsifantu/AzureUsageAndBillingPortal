/**
 * Object:  Table [dbo].[JdsFinanceTransactions]    
 * Script Date: 9/29/2017
 * Jean-Do Sifantus
 **/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].JdsFinanceTransactions(
	[Uid] [uniqueidentifier] NOT NULL,
	[Description] [nvarchar](256) NOT NULL,
	[Amount] [float] NOT NULL,
	[Category] [nvarchar](256) NOT NULL,
	[TransactionDate] [datetime2](0) NOT NULL,
	[AccountNum] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_JdsFinanceTransactions] PRIMARY KEY CLUSTERED 
 (
	[Uid] ASC
 )
)

GO

