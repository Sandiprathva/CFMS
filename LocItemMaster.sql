create table CFMS_ItemMaster
(
	Imid int primary key identity(1,1),
	ItypeId int foreign key references CFMS_ItemType(ItypeId),
	rid int foreign key references CFMS_User(rid),
	uid int foreign key references CFMS_User(rid),
	cpid int foreign key references CFMS_Crop(cpid),
	img varchar(500),
	date date,
	qty int,
	price int,
	stusshow int default(0),
	stussubcrop varchar(50) DEFAULT 'Pending' ,
	stuspay varchar(50) DEFAULT 'Pending',
	stuscomp int default (0),	
	stusdele int default(0),
	stusclu int default(0),

)
drop table CFMS_ItemMaster
drop table CFMS_Sell_Master
drop table CFMS_Cart

drop table CFMS_ItemType

select * from CFMS_ItemMaster

select * from CFMS_User



select * from CFMS_Sell_Master