create database DapperDemo

use DapperDemo

create table [User](UserId int primary key identity(1,1),
                    [Name] varchar(50) not null,
					Email varchar(50) not null,
					City varchar(50))

Select * from [User]

Insert into [User] ([Name], Email,City) values ('Chetan','chetan@gmail.com','Bhandara')

Select * from [User] where UserId = 1

Delete from [User] where UserId = 5

Update [User] Set [Name] = 'k', Email = 'k', City ='k' where UserId = 1