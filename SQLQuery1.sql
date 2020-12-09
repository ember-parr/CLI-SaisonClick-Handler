SELECT p.id,
                                               p.Title,
                                               p.Url,
                                               p.PublishDateTime,
                                               p.AuthorId,
                                               p.BlogId,
                                               a.FirstName,
                                               a.LastName,
                                               a.Bio,
                                               b.Title AS BlogTitle,
                                               b.URL AS BlogUrl
                                          FROM Post p 
                                               LEFT JOIN PostTag pt on p.Id = pt.PostId
                                               LEFT JOIN Tag t on t.Id = pt.TagId
                                               LEFT JOIN Author a on p.AuthorId = a.Id
                                               LEFT JOIN Blog b on p.BlogId = b.Id 
                                         WHERE t.Name LIKE Happy


SELECT *
FROM Tag

SELECT b.Id, b.Title, b.Url
FROM Blog b
LEFT JOIN PostTag pt on p.Id = pt.PostId
LEFT JOIN Tag t on t.Id = pt.TagId
WHERE t.Name LIKE Happy

SELECT a.id,
    a.FirstName,
    a.LastName,
    a.Bio
FROM Author a
    LEFT JOIN AuthorTag at on a.Id = at.AuthorId
    LEFT JOIN Tag t on t.Id = at.TagId
WHERE t.Name LIKE Happy


