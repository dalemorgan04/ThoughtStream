USE [DocketHub_DSA]
GO

CREATE PROCEDURE [dbo].[usp_XML_Report_DmTest]
AS

DECLARE @Xml XML

	BEGIN

		SELECT TOP 10 DepotId, DepotName
		FROM Depot
		FOR XML PATH('Report')

	END 

GO
