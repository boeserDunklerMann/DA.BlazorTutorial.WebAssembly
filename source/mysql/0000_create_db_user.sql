CREATE DATABASE MovieDB;
CREATE USER 'moviedb_user'@'%' IDENTIFIED BY 'moviedb';
GRANT ALL ON MovieDB.* TO 'moviedb_user'@'%';
