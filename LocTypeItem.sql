create table CFMS_ItemType
(
	ItypeId int primary key identity(1,1),
	type varchar(50),
	deletst int default(0)
)
select * from CFMS_ItemType

create table CFMS_Crop
(
	cpid int primary key identity(1,1),
	cpname varchar(50),
	ItypeId int foreign key references CFMS_ItemType(ItypeId),
	deletst int default(0)
)