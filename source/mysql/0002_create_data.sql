USE MovieDB;

insert into Genre(GenreName) VALUES ('Action'), ('Animation'), ('Comedy'), ('Drama'), ('Mystery'), ('Science Fiction');
select * from Genre;

INSERT INTO UserType(UserTypeName) VALUES ('Admin'), ('User'), ('Guest');
SELECT * FROM UserType;

INSERT INTO User (FirstName, LastName, Username, PASSWORD, Gender, UserTypeID)
VALUES ('André', 'Dunkel', 'andre', 'topsecret', 'm', 1),
('Erika', 'Mustermann', 'emuster', 'abc', 'f', 2);
SELECT * FROM `User`;

INSERT INTO Movie(Title, Overview, Genre, LANGUAGE, Duration, Rating)
VALUES ('Der Terminator', 'Arnold Schwarzenegger kills Sarah Connor', 'Action', 'de', 107, 10);
SELECT * FROM Movie;

INSERT INTO Watchlist (UserID) VALUES (1),(2);
SELECT * FROM Watchlist;

INSERT INTO WatchlistItem (WatchlistID, MovieID)
VALUES (1, 1), (2, 1);
SELECT * FROM WatchlistItem;
