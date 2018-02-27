CREATE PROCEDURE [dbo].[udf_CreateProject]
	@parentProjectId int = 0,	
	@projectId       int
AS

BEGIN

   DECLARE @parentOrgNode hierarchyid,
           @lastNode      hierarchyid  

   SELECT @parentOrgNode = OrgNode   
   FROM   ProjectOrg
   WHERE  ProjectId = @parentProjectId  

   SET TRANSACTION ISOLATION LEVEL SERIALIZABLE 
   
   BEGIN TRANSACTION
   
	   SELECT @lastNode = max(OrgNode)   
		  FROM ProjectOrg
		  WHERE OrgNode.GetAncestor(1) = @parentOrgNode ;  

		  INSERT ProjectOrg (OrgNode, ProjectId)  
		  VALUES (@parentOrgNode.GetDescendant(@lastNode, NULL), @projectId)  

   COMMIT  

END ;  
GO  