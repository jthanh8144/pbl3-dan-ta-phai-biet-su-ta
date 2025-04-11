create table UserInfo
(
	UserID int Identity(1, 1) primary key,
	Username nvarchar(50) not null,
	Password nvarchar(50) not null,
	Name nvarchar(MAX),
	Email nvarchar(MAX),
	IsAdmin bit  not null
)

create table GameProcess
(
	GameProcessID int Identity(1, 1) primary key,
	StageID int not null,
	Point int not null,
	UserID int not null foreign key references dbo.UserInfo (UserID),
	IsPass int not null 
)