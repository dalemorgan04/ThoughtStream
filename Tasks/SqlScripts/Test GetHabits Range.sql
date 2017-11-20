
DECLARE @fromDate AS Date
DECLARE @toDate AS DATE
DECLARE @firstDayOfWeek AS INT = 1

SET @fromDate = DATEFROMPARTS(2017,11,16);
SET @toDate = DATEFROMPARTS(2017,11,30);

--DateTable
SELECT 
	DateTable.Date,
	Habits.HabitId
FROM 
	tvf_DateTable(@fromDate, @toDate) DateTable, 
	tvf_GetHabitsFromDate(@fromDate, @firstDayOfWeek) Habits
GROUP BY 
	DateTable.Date,
	Habits.HabitId
ORDER BY
	DateTable.Date,
	Habits.HabitId
	

