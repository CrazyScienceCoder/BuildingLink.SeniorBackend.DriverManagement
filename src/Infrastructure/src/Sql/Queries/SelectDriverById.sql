SELECT Id,
       FirstName,
       LastName,
       Email,
       PhoneNumber
  FROM Drivers
 WHERE Id = @Id
 LIMIT 1;
