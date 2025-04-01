CREATE TABLE IF NOT EXISTS Drivers (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName TEXT NOT NULL CHECK(length(FirstName) <= 50),
    LastName TEXT NOT NULL CHECK(length(LastName) <= 50),
    Email TEXT NOT NULL UNIQUE CHECK(length(Email) <= 150),
    PhoneNumber TEXT NOT NULL CHECK(length(PhoneNumber) <= 20),
    CreatedAtUtc DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAtUtc DATETIME NULL
);