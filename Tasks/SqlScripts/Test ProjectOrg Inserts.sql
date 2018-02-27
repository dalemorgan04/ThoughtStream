--EXEC udf_CreateProject 2,5;
/*
Finding descendants
--------------------
DECLARE	@p hierarchyid

SELECT	@p = OrgNode
FROM	ProjectOrg
WHERE	ProjectId = 2

SELECT	OrgNode.ToString() AS TextNode, *
FROM	ProjectOrg
Where OrgNode.IsDescendantOf(@p) = 1
*/

/*
--Moving a single row
---------------------
DECLARE	@currentProject hierarchyid,
		@oldParent		hierarchyid,
		@newParent		hierarchyid

SELECT	@currentProject = OrgNode
FROM	ProjectOrg
WHERE	ProjectId = 5

SELECT	@oldParent = OrgNode
FROM	ProjectOrg
WHERE	ProjectId = 2

SELECT	@newParent = OrgNode
FROM	ProjectOrg
WHERE	ProjectId = 3


DECLARE children_cursor CURSOR FOR
SELECT	OrgNode
FROM	ProjectOrg


UPDATE	ProjectOrg
SET		OrgNode = @currentProject.GetReparentedValue(@oldParent,@newParent)
WHERE	OrgNode = @currentProject;
GO
*/


--Moving a subtree
--CREATE PROCEDURE MoveOrg(@oldMgr nvarchar(256), @newMgr nvarchar(256) )  


DECLARE @moveProjectId int = 2 , 
		@newParentId int = 3,

		@oldProject hierarchyid, 
		@newProject hierarchyid  

SELECT @oldProject = OrgNode FROM ProjectOrg WHERE ProjectId = @moveProjectId ;  

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE  
BEGIN TRANSACTION  

	SELECT @newProject = OrgNode FROM ProjectOrg WHERE ProjectId = @newParentId ;  
	
	SELECT @oldProject.ToString()
	SELECT @newProject.ToString()

	SELECT @newProject = @newProject.GetDescendant(max(OrgNode), NULL)   
	FROM ProjectOrg WHERE OrgNode.GetAncestor(1) = @newProject ;

	SELECT @newProject.ToString()
	
	UPDATE ProjectOrg
	SET OrgNode = OrgNode.GetReparentedValue(@oldProject, @newProject)  
	WHERE OrgNode.IsDescendantOf(@oldProject) = 1 ;  
	
COMMIT TRANSACTION  
