create table CFMS_Sell_Master
(
	sid int primary key identity(1,1),
	rid int foreign key references CFMS_User(rid),
	cid int foreign key references CFMS_Cart(cid),
	Imid int foreign key references CFMS_ItemMaster(Imid),
	date varchar(20),
	Quty int,
	price int,
	amut int,
	stusdeli int default(0),
	stussmdele int default(0)
)

select * from CFMS_Sell_Master

select * from CFMS_ItemMaster
update CFMS_ItemMaster set qty=50 where Imid=3

drop table CFMS_Sell_Master