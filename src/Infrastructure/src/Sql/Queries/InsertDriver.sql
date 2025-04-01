INSERT INTO Drivers (FirstName,
                     LastName,
                     Email,
                     PhoneNumber)
VALUES (@FirstName, @LastName, @Email, @PhoneNumber)
RETURNING Id;
