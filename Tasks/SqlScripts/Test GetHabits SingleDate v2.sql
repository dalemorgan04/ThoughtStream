DECLARE	@date AS DATE
SET @date = DATEFROMPARTS(2017,11,16);
SET DATEFIRST 1;

--Need to add user
	SELECT [HabitId]
	FROM
		(

			--Daily - Open
				SELECT 
					[HabitId], 
					'Daily - Open' as [Type]	
				FROM [HabitRecurrences]
				WHERE 
					RepeatInterval = 1 AND 	
					StartOnDate <= @date AND 
					(EndOnDate  >= @date OR EndOnDate IS NULL) AND	
					(DateDiff(DAY, StartOnDate, @date) % RepeatCount = 0)
		
		UNION ALL

			--Weekly - Weekday
			SELECT 
				[HabitId], 
				'Weekly - Weekday' as [Type]
			FROM [HabitRecurrences]
			WHERE 
				RepeatInterval = 2 AND 	
				DayOfWeek IS NOT NULL AND
				DayOfWeek = DATEPART(WEEKDAY,@date) AND 
				StartOnDate <= @date AND 
				(EndOnDate  >= @date OR EndOnDate IS NULL) AND	
				(DateDiff(WEEK, DATEADD(DAY,-@@DATEFIRST,StartOnDate), DATEADD(DAY,-@@DATEFIRST,@date)) % RepeatCount = 0)
		
		UNION ALL

			--Weekly - Open

			SELECT 
				[HabitId], 
				'Weekly - Open' as [Type]			
			FROM [HabitRecurrences]
			WHERE 
				RepeatInterval = 2 AND 	
				DayOfWeek IS NULL AND		
				StartOnDate <= @date AND 
				(EndOnDate  >= @date OR EndOnDate IS NULL) AND	
				(DateDiff(WEEK, DATEADD(DAY,-@@DATEFIRST,StartOnDate), DATEADD(DAY,-@@DATEFIRST,@date)) % RepeatCount = 0)

		UNION ALL

			--Monthly - Day

			SELECT 
				[HabitId], 
				'Monthly - Day' as [Type]			
			FROM [HabitRecurrences]
			WHERE 
				RepeatInterval = 3 AND 	
				DayOfWeek IS NULL AND
				WeekdayOfMonth IS NULL AND
				DayOfMonth IS NOT NULL AND
				DayOfMonth = DATEPART(DAY,@date) AND
				StartOnDate <= @date AND 
				(EndOnDate  >= @date OR EndOnDate IS NULL) AND	
			(DateDiff(MONTH, StartOnDate, @date) % RepeatCount = 0)

		UNION ALL

			--Monthly - Weekday

			SELECT 
				[HabitId], 
				'Monthly - Weekday' as [Type]			
			FROM [HabitRecurrences]
			WHERE 
				RepeatInterval = 3 AND
				DayOfMonth IS NULL AND
				DayOfWeek IS NOT NULL AND
				WeekdayOfMonth IS NOT NULL AND
				DayOfWeek =  DATEPART(WEEKDAY,@date) AND
				((DATEPART(WEEK,@date)-DATEPART(WEEK,DATEADD(MONTH,DATEDIFF(MONTH,0,@date),0))) + CASE WHEN DATEDIFF(DAY,0,@date) % 7 >= DATEDIFF(DAY,0,DATEADD(MONTH,DATEDIFF(MONTH,0,@date),0)) % 7 OR
				DATEDIFF(DAY,0,DATEADD(MONTH,DATEDIFF(MONTH,0,@date),0))%7 = 6 THEN 1 ELSE 0 END ) % RepeatCount = 0 AND
				StartOnDate <= @date AND
				(EndOnDate  >= @date OR EndOnDate IS NULL) AND
				(DateDiff(MONTH, StartOnDate, @date) % RepeatCount = 0)
	
		UNION ALL

			--Monthly - Open

			SELECT 
				[HabitId], 
				'Monthly - Open' as [Type]			
			FROM [HabitRecurrences]
			WHERE 
				RepeatInterval = 3 AND 	
				DayOfWeek IS NULL AND
				WeekdayOfMonth IS NULL AND
				DayOfMonth IS NULL AND		
				StartOnDate <= @date AND 
				(EndOnDate  >= @date OR EndOnDate IS NULL) AND	
				(DateDiff(MONTH, StartOnDate, @date) % RepeatCount = 0)

		UNION ALL

			--Yearly - WeekNo

			SELECT 
				[HabitId], 
				'Yearly - WeekNo' as [Type]		
			FROM [HabitRecurrences]
			WHERE 
				RepeatInterval = 4 AND 	
	 			MonthNo IS NULL AND		
				WeekOfYear IS NOT NULL AND		
				WeekOfYear = DATEPART(WEEK,@date) AND
				StartOnDate <= @date AND 
				(EndOnDate  >= @date OR EndOnDate IS NULL) AND	
				(DateDiff(YEAR, StartOnDate, @date) % RepeatCount = 0)
	
		UNION ALL

			--Yearly - MonthNo

			SELECT 
				[HabitId], 
				'Yearly - MonthNo' as [Type]		
			FROM [HabitRecurrences]
			WHERE 
				RepeatInterval = 4 AND 	
	 			WeekOfYear IS NULL AND		
				MonthNo IS NOT NULL AND
				MonthNo = DATEPART(MONTH,@date) AND
				StartOnDate <= @date AND 
				(EndOnDate  >= @date OR EndOnDate IS NULL) AND	
				(DateDiff(YEAR, StartOnDate, @date) % RepeatCount = 0)

		UNION ALL

			--Yearly - OPEN

			SELECT 
				[HabitId], 
				'Yearly - Open' as [Type]		
			FROM [HabitRecurrences]
			WHERE 
				RepeatInterval = 4 AND 	
	 			WeekOfYear IS NULL AND		
				MonthNo IS NULL AND		
				StartOnDate <= @date AND 
				(EndOnDate  >= @date OR EndOnDate IS NULL) AND	
				(DateDiff(YEAR, StartOnDate, @date) % RepeatCount = 0)

		) Recurrences
	GROUP BY HabitId



