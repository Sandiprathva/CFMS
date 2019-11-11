create table CFMS_ItemMaster
(
	Imid int primary key identity(1,1),
	ItypeId int foreign key references CFMS_ItemType(ItypeId),
	rid int foreign key references CFMS_User(rid),
	uid int foreign key references CFMS_User(rid),
	name varchar(50),
	img varchar(500),
	date varchar(20),
	qty int,
	price int,
	stusshow int default(0),
	stussubcrop varchar(50) DEFAULT 'Pending' ,
	stuspay varchar(50) DEFAULT 'Pending',
	stuscomp int default (0),
	stusdele int default(0)

)

drop table CFMS_ItemMaster

select * from CFMS_ItemMaster

select * from CFMS_User

ALTER TABLE CFMS_ItemMaster
ADD stussubcrop varchar(50) DEFAULT 'Pending' 

update  CFMS_ItemMaster set  qty=500000000  where Imid=1