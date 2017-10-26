

--Params
DECLARE @thoughtId INT = 69
DECLARE @moveToSortId INT = 2
DECLARE @DebugEnabled BIT = 1
--Local
DECLARE @OldSortId AS INT
DECLARE @MaxSortId AS INT

--OldSortId
	SET @OldSortId = (
		SELECT Thoughts.SortId
		FROM Thoughts
		WHERE Thoughts.ThoughtId  = @thoughtId)	

--Temporary placement for 
	SET @MaxSortId = (SELECT MAX(Thoughts.SortId) +1 FROM Thoughts)

--Temporarily shift the thought to the end so the rest of the ids can be shifted to make room
	UPDATE Thoughts
	SET Thoughts.SortId = @MaxSortId
	WHERE Thoughts.ThoughtId = @thoughtId
	--works

--If moving upward
IF (@moveToSortId > @OldSortId)
	BEGIN

		PRINT 'Moving upwards'				
		;WITH ShiftThoughts AS 			
			(
			SELECT Thoughts.ThoughtId, Thoughts.SortId
			FROM Thoughts
			WHERE Thoughts.SortId BETWEEN @OldSortId AND @moveToSortId
			)		
		UPDATE ShiftThoughts
		SET ShiftThoughts.SortId = ShiftThoughts.SortId - 1 

	END
--If moving downward
ELSE
	BEGIN

		PRINT 'Moving downward'				
		;WITH ShiftThoughts AS 			
			(
			SELECT Thoughts.ThoughtId, Thoughts.SortId
			FROM Thoughts
			WHERE Thoughts.SortId BETWEEN @moveToSortId AND @OldSortId
			)		
		UPDATE ShiftThoughts
		SET ShiftThoughts.SortId = ShiftThoughts.SortId + 1

	END
	
--Move thought to new place	
	
	UPDATE Thoughts
	SET Thoughts.SortId = @moveToSortId
	WHERE Thoughts.ThoughtId = @thoughtId					
	SELECT Thoughts.ThoughtId, Thoughts.SortId FROM Thoughts WHERE Thoughts.ThoughtId = @thoughtId	

--Sort the SortId numbers to fix any gaps that may have been created	

	UPDATE Thoughts
	SET SortId = SortedThoughts.Row
	FROM Thoughts
	INNER JOIN
		(SELECT *  , ROW_NUMBER() OVER (ORDER BY Thoughts.SortId) AS Row
		FROM Thoughts) SortedThoughts
	ON Thoughts.ThoughtId = SortedThoughts.ThoughtId

--DEBUG-------
IF (@DebugEnabled = 1)
	SELECT *
	FROM Thoughts
	ORDER BY SortId