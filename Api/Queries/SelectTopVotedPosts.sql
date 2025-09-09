/*
Show me posts with the most "Up Votes" 
- Don't show community wiki posts (CommunityOwnedDate) 
- "Up Votes" is the Votes.VoteTypeId = 2 (VoteTypes.Name = 'UpMod') 
- Order the results from most upvotes to least upvotes. 
- Show: PostId, UpVotes 
*/
; WITH [CummunityPosts] AS (
    SELECT [Id]
    FROM [Posts]
    WHERE [CommunityOwnedDate] IS NULL
)
SELECT top(@top)
    [v].[PostId],
    COUNT(1) AS [UpVotes]
FROM [dbo].[Votes] AS [v]
JOIN [CummunityPosts] AS [p]
    ON [p].[Id] = [v].[PostId]
WHERE [v].[VoteTypeId] = 2
GROUP BY [v].[PostId]
ORDER BY [UpVotes] DESC;