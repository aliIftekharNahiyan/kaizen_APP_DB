-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[sp_History_GetForChangeset]
	@ChangesetId int
AS
BEGIN
	SELECT ISNULL(
	(
	SELECT 
		newvalue AS 'NewValue',
		originalvalue AS 'OriginalValue', 
		MAX(PropertyName) AS PropertyName
	FROM AbpEntityChanges
	INNER JOIN AbpEntityPropertyChanges on AbpEntityPropertyChanges.EntityChangeId = AbpEntityChanges.Id
	WHERE AbpEntityChanges.EntityChangeSetId = @ChangesetId
	GROUP BY PropertyName, NewValue, OriginalValue
	ORDER BY PropertyName
	FOR JSON PATH), '[]')
END
