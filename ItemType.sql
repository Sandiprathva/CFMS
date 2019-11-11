create table CFMS_ItemType
(
	ItypeId int primary key identity(1,1),
	type varchar(50),
	deletst int default(0)
)

drop table CFMS_ItemType
select * from CFMS_ItemType