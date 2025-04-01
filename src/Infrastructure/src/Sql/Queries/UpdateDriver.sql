UPDATE Drivers
   SET FirstName = @FirstName,
       LastName = @LastName,
       Email = @Email,
       PhoneNumber = @PhoneNumber,
       UpdatedAtUtc = CURRENT_TIMESTAMP
 WHERE Id = @Id;