
INSERT INTO Companies (Name, DateOfFundation)
VALUES
    ('Company A', '2020-01-01'),
    ('Company B', '2015-05-15'),
    ('Company C', '2018-10-10');


INSERT INTO Tirs (Model, CompanyId, DriverId)
VALUES
    ('Tir 1', 1, 1),
    ('Tir 2', 1, 2),
    ('Tir 3', 2, 3),
    ('Tir 4', 3, 4);

INSERT INTO Drivers (Name, Surname, Age, TirId, CompanyId)
VALUES
    ('Driver 1', 'Surname 1', 30, 1, 1),
    ('Driver 2', 'Surname 2', 35, 2, 1),
    ('Driver 3', 'Surname 3', 28, 3, 2),
    ('Driver 4', 'Surname 4', 32, 4, 3);
