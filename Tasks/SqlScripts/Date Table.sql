
CREATE FUNCTION [dbo].[DateTable]
(
	@fromDate Date,
	@toDate Date
)
RETURNS @returntable TABLE
(
	[Date] Date	
)
AS
BEGIN
	
	DECLARE @CurrentDate AS DATE
	SET @CurrentDate = @fromDate

		LOOP:	

			INSERT @returntable
			SELECT @fromDate
			
			SET @CurrentDate = DATEADD(DAY, 1 , @CurrentDate);

		IF (@CurrentDate = @ToDate) GOTO LOOP;

	RETURN

END