use MovieDB;
/*****************************************************
 * THIS IS THE WORST DB-DESIGN I'VE EVER SEEN!!
 * BUT I ADOPTED IT FROM THE TUTORIAL AND WILL REWORK
 * IT LATER
 *****************************************************/

create table Genre
(
	GenreID	int identity(1, 1) primary key,
	GenreName varchar(20) not null,
);
go

create table UserType
(
	UserTypeID int identity(1, 1) primary key,
	UserTypeName varchar(20) not null
)
go

/* Very bad db-design but adopted from tutorial (I hate tutorials!)
 * redesign will be done later */
 create table UserMaster
(
	UserID int identity(1, 1) primary key,
	FirstName varchar(20) not null,
	LastName varchar(20) not null,
	Username varchar(20) not null,
	Password varchar(40) not null,
	Gender varchar(6) not null,
	UserTypeName varchar(20) not null	/* WTF? */
)
go

create table Movie
(
	MovieID int identity(1,1) primary key,
	Title varchar(100) not null,
	Overview varchar(1024) not null,
	Genre varchar(20) not null,
	Language varchar(20) not null,
	Duration int not null,
	Rating decimal(2,1) null,
	PosterPath varchar(100) null
)
go

create table Watchlist
(
	WatchlistID varchar(36) primary key,
	UserID int not null,
	DateCreated datetime not null
)
go

create table WatchlistItems
(
	WatchlistItemID int identity(1,1) primary key,
	WatchlistID varchar(36) not null,
	MovieID int not null
)
go
