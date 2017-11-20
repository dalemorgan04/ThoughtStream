DECLARE	@date AS DATE
SET @date = DATEFROMPARTS(2017,11,16);


--Monthly - Open

	SELECT 
		@date as [GetDate],
		'Monthly - On Day' as [Type],
		AllRecurrences.HabitId, 
		AllRecurrences.[Description],
		AllRecurrences.RepeatInterval,
		AllRecurrences.RepeatCount,				
		AllRecurrences.StartOnDate,
		AllRecurrences.EndOnOccurrenceNo,
		0 as [IntervalsBetween],				
		0 as [Mod]
	FROM
		(
			SELECT
				[Habits].HabitId, 
				[Habits].[Description],
				[HabitRecurrences].StartOnDate, 
				[HabitRecurrences].RepeatInterval, 
				[HabitRecurrences].[RepeatCount], 
				[HabitRecurrences].[DayOfWeek],
				[HabitRecurrences].EndOnDate, 
				[HabitRecurrences].EndOnOccurrenceNo
			FROM [Habits]
			LEFT JOIN [HabitRecurrences]
			ON Habits.HabitId = HabitRecurrences.HabitId	
			WHERE 
				[HabitRecurrences].RepeatInterval = 3 AND
				[HabitRecurrences].DayOfMonth IS NULL AND
				[HabitRecurrences]. IS NULL AND
				[HabitRecurrences].StartOnDate <= @date AND 
				([HabitRecurrences].EndOnDate  >= @date OR [HabitRecurrences].EndOnDate IS NULL)
		) as AllRecurrences





--All

	SELECT 
		[GetDate],
		DATEPART(WEEKDAY,@date) as [DayOfWeek],
		DATEPART(DAY,@date) as [DayOfMonth],
		DATEPART(WEEK,@date) as [WeekOfYear],
		DATEPART(MONTH,@date) as [MonthNo],
		DATEPART(YEAR,@date) as [Year],
		HabitId, 
		[Type],
		[Description],
		RepeatCount,		
		StartOnDate,
		EndOnOccurrenceNo,
		[IntervalsBetween],
		[IntervalsBetween] % RepeatCount as [Mod]
	FROM 
		(
		
		--Daily
			SELECT 
				@date as [GetDate],
				'Daily' as [Type],
				AllRecurrences.HabitId, 
				AllRecurrences.[Description],
				AllRecurrences.RepeatInterval,
				AllRecurrences.RepeatCount,		
				AllRecurrences.StartOnDate,
				AllRecurrences.EndOnOccurrenceNo,
				DateDiff(day, AllRecurrences.StartOnDate, @date) as [IntervalsBetween],				
				DateDiff(day, AllRecurrences.StartOnDate, @date) % AllRecurrences.RepeatCount as [Mod]
			FROM
				(
					SELECT
						[Habits].HabitId, 
						[Habits].[Description],
						[HabitRecurrences].StartOnDate, 
						[HabitRecurrences].RepeatInterval, 
						[HabitRecurrences].[RepeatCount], 
						[HabitRecurrences].EndOnDate, 
						[HabitRecurrences].EndOnOccurrenceNo
					FROM [Habits]
					LEFT JOIN [HabitRecurrences]
					ON Habits.HabitId = HabitRecurrences.HabitId	
					WHERE 
						[HabitRecurrences].RepeatInterval = 1 AND
						[HabitRecurrences].StartOnDate <= @date AND 
					   ([HabitRecurrences].EndOnDate   >= @date OR [HabitRecurrences].EndOnDate IS NULL)					   
				) as AllRecurrences	
			WHERE DateDiff(day, AllRecurrences.StartOnDate, @date) % AllRecurrences.RepeatCount = 0
		
		
		UNION ALL	
		--Weekly - On WeekDay

			SELECT 
				@date as [GetDate],
				'Weekly - On Day' as [Type],
				AllRecurrences.HabitId, 
				AllRecurrences.[Description],
				AllRecurrences.RepeatInterval,
				AllRecurrences.RepeatCount,						
				AllRecurrences.StartOnDate,
				AllRecurrences.EndOnOccurrenceNo,
				DateDiff(DAY, AllRecurrences.StartOnDate, @date) as [IntervalsBetween],				
				DateDiff(day, AllRecurrences.StartOnDate, @date) % (AllRecurrences.RepeatCount * 7) as [Mod]
			FROM
				(
					SELECT
						[Habits].HabitId, 
						[Habits].[Description],
						[HabitRecurrences].StartOnDate, 
						[HabitRecurrences].RepeatInterval, 
						[HabitRecurrences].[RepeatCount], 
						[HabitRecurrences].[DayOfWeek],
						[HabitRecurrences].EndOnDate, 
						[HabitRecurrences].EndOnOccurrenceNo
					FROM [Habits]
					LEFT JOIN [HabitRecurrences]
					ON Habits.HabitId = HabitRecurrences.HabitId	
					WHERE 
						[HabitRecurrences].RepeatInterval = 2 AND
						[HabitRecurrences].DayOfWeek IS NOT NULL AND
						[HabitRecurrences].DayOfWeek = DATEPART(WEEKDAY,@date) AND
						[HabitRecurrences].StartOnDate <= @date AND 
						([HabitRecurrences].EndOnDate  >= @date OR [HabitRecurrences].EndOnDate IS NULL)		
				) as AllRecurrences


		UNION ALL
		--Weekly - Open

			SELECT 
				@date as [GetDate],
				'Weekly - Open' as [Type],
				AllRecurrences.HabitId, 
				AllRecurrences.[Description],
				AllRecurrences.RepeatInterval,
				AllRecurrences.RepeatCount,				
				AllRecurrences.StartOnDate,
				AllRecurrences.EndOnOccurrenceNo,
				DateDiff(DAY, AllRecurrences.StartOnDate, @date) as [IntervalsBetween],				
				DateDiff(DAY, AllRecurrences.StartOnDate, @date) % (AllRecurrences.RepeatCount * 7) as [Mod]
			FROM
				(
					SELECT
						[Habits].HabitId, 
						[Habits].[Description],
						[HabitRecurrences].StartOnDate, 
						[HabitRecurrences].RepeatInterval, 
						[HabitRecurrences].[RepeatCount], 				
						[HabitRecurrences].EndOnDate, 
						[HabitRecurrences].EndOnOccurrenceNo
					FROM [Habits]
					LEFT JOIN [HabitRecurrences]
					ON Habits.HabitId = HabitRecurrences.HabitId	
					WHERE 
						[HabitRecurrences].RepeatInterval = 2 AND
						[HabitRecurrences].DayOfWeek IS NULL AND
						[HabitRecurrences].StartOnDate <= @date AND 
						([HabitRecurrences].EndOnDate   >= @date OR [HabitRecurrences].EndOnDate IS NULL)					   
				) as AllRecurrences	
			WHERE DateDiff(DAY, AllRecurrences.StartOnDate, @date) % (AllRecurrences.RepeatCount * 7) = 0
			

		UNION ALL
		--Monthly - On Day
		
		SELECT 
			@date as [GetDate],
			'Monthly - On Day' as [Type],
			AllRecurrences.HabitId, 
			AllRecurrences.[Description],
			AllRecurrences.RepeatInterval,
			AllRecurrences.RepeatCount,
			AllRecurrences.StartOnDate,
			AllRecurrences.EndOnOccurrenceNo,
			0 as [IntervalsBetween],				
			0 as [Mod]
		FROM
			(
				SELECT
					[Habits].HabitId, 
					[Habits].[Description],
					[HabitRecurrences].StartOnDate, 
					[HabitRecurrences].RepeatInterval, 
					[HabitRecurrences].[RepeatCount], 
					[HabitRecurrences].[DayOfWeek],
					[HabitRecurrences].EndOnDate, 
					[HabitRecurrences].EndOnOccurrenceNo
				FROM [Habits]
				LEFT JOIN [HabitRecurrences]
				ON Habits.HabitId = HabitRecurrences.HabitId	
				WHERE 
					[HabitRecurrences].RepeatInterval = 3 AND
					[HabitRecurrences].DayOfMonth IS NOT NULL AND
					[HabitRecurrences].DayOfMonth = DATEPART(DAY,@date) AND
					[HabitRecurrences].StartOnDate <= @date AND 
					([HabitRecurrences].EndOnDate  >= @date OR [HabitRecurrences].EndOnDate IS NULL)
			) as AllRecurrences


		UNION ALL
		--Monthly - Weekday

		SELECT 
			@date as [GetDate],
			'Monthly - On Day' as [Type],
			AllRecurrences.HabitId, 
			AllRecurrences.[Description],
			AllRecurrences.RepeatInterval,
			AllRecurrences.RepeatCount,					
			AllRecurrences.StartOnDate,
			AllRecurrences.EndOnOccurrenceNo,		
			WeekdayNoOfMonth as [IntervalsBetween],				
			WeekdayNoOfMonth % AllRecurrences.RepeatCount as [Mod]
		FROM
			(
				SELECT
					[Habits].HabitId, 
					[Habits].[Description],
					[HabitRecurrences].StartOnDate, 
					[HabitRecurrences].RepeatInterval, 
					[HabitRecurrences].[RepeatCount], 
					[HabitRecurrences].[DayOfWeek],
					[HabitRecurrences].EndOnDate, 
					[HabitRecurrences].EndOnOccurrenceNo,
					(DATEPART(WEEK,@date)-DATEPART(WEEK,DATEADD(MONTH,DATEDIFF(MONTH,0,@date),0))) + 
					CASE WHEN DATEDIFF(DAY,0,@date)%7 >= DATEDIFF(DAY,0,DATEADD(MONTH,DATEDIFF(MONTH,0,@date),0))%7  OR 
					DATEDIFF(DAY,0,DATEADD(MONTH,DATEDIFF(MONTH,0,@date),0))%7 = 6 THEN 1 ELSE 0 END as WeekdayNoOfMonth
				FROM [Habits]
				LEFT JOIN [HabitRecurrences]
				ON Habits.HabitId = HabitRecurrences.HabitId	
				WHERE 
					[HabitRecurrences].RepeatInterval = 3 AND
					[HabitRecurrences].DayOfWeek IS NOT NULL AND				
					[HabitRecurrences].DayOfWeek = 5 AND				
					[HabitRecurrences].StartOnDate <= @date AND 
					([HabitRecurrences].EndOnDate  >= @date OR [HabitRecurrences].EndOnDate IS NULL)
			) as AllRecurrences	
		) as AllRecurrences
