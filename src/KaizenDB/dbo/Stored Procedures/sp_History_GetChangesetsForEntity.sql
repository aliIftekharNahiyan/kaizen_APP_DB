
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[sp_History_GetChangesetsForEntity]
	@EntityType NVARCHAR(120),
	@EntityId  NVARCHAR(64)
AS
BEGIN
	SELECT ISNULL(
	(SELECT 
		AbpEntityChangeSets.Id, 
		convert(VARCHAR(10), abpentitychangesets.CreationTime, 103) + ' ' +convert(VARCHAR(8), abpentitychangesets.CreationTime, 14) AS 'CreationTime', 
		abpusers.Name + ' ' + abpusers.Surname AS 'PersonName', 
		abpusers.Id AS 'UserId', 
		CASE 
			WHEN changetype = 0 THEN 'Create'
			WHEN changetype = 1 THEN 'Edit'
			WHEN changetype = 2 THEN 'Delete'
		END AS ChangeType
	FROM AbpEntityChangeSets
	INNER JOIN AbpEntityChanges on AbpEntityChanges.EntityChangeSetId = AbpEntityChangeSets.Id
	INNER JOIN abpusers on abpusers.id = userid
	WHERE EntityTypeFullName = @EntityType
	AND EntityId = @EntityId
	ORDER BY abpentitychangesets.CreationTime DESC
	FOR JSON PATH), '[]')
END
