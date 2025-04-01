INSERT INTO Drivers
(
    FirstName,
    LastName,
    Email,
    PhoneNumber,
    CreatedAtUtc,
    UpdatedAtUtc
)
SELECT 'Zyler',
       'Quinlan',
       'zyler.quinlan@example.com',
       '+12025550101',
       CURRENT_TIMESTAMP,
       NULL
WHERE NOT EXISTS
(
    SELECT 1 FROM Drivers WHERE Email = 'zyler.quinlan@example.com'
)
UNION ALL
SELECT 'Elara',
       'Vexley',
       'elara.vexley@example.com',
       '+12025550102',
       CURRENT_TIMESTAMP,
       NULL
WHERE NOT EXISTS
(
    SELECT 1 FROM Drivers WHERE Email = 'elara.vexley@example.com'
)
UNION ALL
SELECT 'Orin',
       'Tandor',
       'orin.tandor@example.com',
       '+12025550103',
       CURRENT_TIMESTAMP,
       NULL
WHERE NOT EXISTS
(
    SELECT 1 FROM Drivers WHERE Email = 'orin.tandor@example.com'
)
UNION ALL
SELECT 'Seraphina',
       'Draven',
       'seraphina.draven@example.com',
       '+12025550104',
       CURRENT_TIMESTAMP,
       NULL
WHERE NOT EXISTS
(
    SELECT 1 FROM Drivers WHERE Email = 'seraphina.draven@example.com'
)
UNION ALL
SELECT 'Kael',
       'Mythos',
       'kael.mythos@example.com',
       '+12025550105',
       CURRENT_TIMESTAMP,
       NULL
WHERE NOT EXISTS
(
    SELECT 1 FROM Drivers WHERE Email = 'kael.mythos@example.com'
)
UNION ALL
SELECT 'Vesper',
       'Nocturne',
       'vesper.nocturne@example.com',
       '+12025550106',
       CURRENT_TIMESTAMP,
       NULL
WHERE NOT EXISTS
(
    SELECT 1 FROM Drivers WHERE Email = 'vesper.nocturne@example.com'
)
UNION ALL
SELECT 'Talon',
       'Wraith',
       'talon.wraith@example.com',
       '+12025550107',
       CURRENT_TIMESTAMP,
       NULL
WHERE NOT EXISTS
(
    SELECT 1 FROM Drivers WHERE Email = 'talon.wraith@example.com'
)
UNION ALL
SELECT 'Lyric',
       'Ember',
       'lyric.ember@example.com',
       '+12025550108',
       CURRENT_TIMESTAMP,
       NULL
WHERE NOT EXISTS
(
    SELECT 1 FROM Drivers WHERE Email = 'lyric.ember@example.com'
)
UNION ALL
SELECT 'Dax',
       'Halloway',
       'dax.halloway@example.com',
       '+12025550109',
       CURRENT_TIMESTAMP,
       NULL
WHERE NOT EXISTS
(
    SELECT 1 FROM Drivers WHERE Email = 'dax.halloway@example.com'
)
UNION ALL
SELECT 'Isolde',
       'Fable',
       'isolde.fable@example.com',
       '+12025550110',
       CURRENT_TIMESTAMP,
       NULL
WHERE NOT EXISTS
(
    SELECT 1 FROM Drivers WHERE Email = 'isolde.fable@example.com'
);