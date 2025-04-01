SELECT Id,
       FirstName,
       LastName,
       Email,
       PhoneNumber
  FROM Drivers
 ORDER BY FirstName COLLATE NOCASE ASC, 
          LastName COLLATE NOCASE ASC
 LIMIT @PageSize OFFSET @Offset;
