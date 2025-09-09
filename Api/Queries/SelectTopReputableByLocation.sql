/*
- Given a "@Location" parameter, write a query that shows the "most reputable" 1000 users that matches the given location or part there-of.
- If @Location is null, show top 1000 users across all locations.  
- "most reputable" is defined by users with the highest Reputation.  
- Order the results from highest to lowest Reputation.  
- If 2 users have the same reputation, show them alphabetically by DisplayName for the same Reputation value
- Show columns: DisplayName, Reputation, Location
*/
SELECT TOP(@top)
    [u].[DisplayName],
    [u].[Reputation],
    [u].[Location]
FROM [dbo].[Users] AS [u]
WHERE [u].[Location] LIKE concat(@startsWith, '%', @contains, '%', @endsWith)
ORDER BY [u].[Reputation] DESC, [u].[DisplayName] ASC;