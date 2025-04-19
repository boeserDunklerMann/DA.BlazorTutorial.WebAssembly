USE MovieDB;

CREATE TABLE Genre
(
	GenreID INT AUTO_INCREMENT NOT null,
	GenreName NVARCHAR(30) NOT NULL,
	CONSTRAINT PK_Genre PRIMARY KEY (GenreID)
);

CREATE TABLE Movie
(
	MovieID INT AUTO_INCREMENT NOT NULL,
	Title NVARCHAR(100) NOT NULL,
	Overview NVARCHAR(1024) NOT NULL,
	Genre NVARCHAR(30) NOT NULL,
	LANGUAGE NVARCHAR(30) NOT NULL,
	Duration INT NOT NULL,
	Rating DECIMAL(2,1) NULL,
	PosterPath VARCHAR(100) NULL,
	CONSTRAINT PK_Movie PRIMARY KEY (MovieID)
);
