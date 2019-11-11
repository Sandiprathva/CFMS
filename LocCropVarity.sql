create table CFMS_Cropvariety 
(
	varid int primary key identity(1,1),
	verity varchar(50),
	cpid int foreign key references CFMS_Crop(cpid),
	ItypeId int foreign key references CFMS_ItemType(ItypeId),
	deletst int default(0)
)

drop table CFMS_ItemType
select * from CFMS_ItemType