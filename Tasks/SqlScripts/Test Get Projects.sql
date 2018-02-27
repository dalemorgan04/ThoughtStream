

SELECT	
		p.ProjectId,
		sp.ProjectId
FROM Projects p

--Get sub projects
LEFT JOIN	Projects sp
ON			p.ProjectId = sp.ParentProjectId
