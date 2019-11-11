create table CFMS_Sell_Master
(
	sid int primary key identity(1,1),
	rid int foreign key references CFMS_User(rid),
	cid int foreign key references CFMS_Cart(cid),
	Imid int foreign key references CFMS_ItemMaster(Imid),
	varidsell int default(0),
	date date,
	Quty int,
	price int,
	amut int,
	totamu int default(0),
	stusdeli int default(0),
	stussmdele int default(0)

)

create table CFMS_Cart
(
	cid int primary key identity(1,1),
	rid int foreign key references CFMS_User(rid),
	Imid int foreign key references CFMS_ItemMaster(Imid),
	varidCart int default(0),
	qt int ,
	pric int default(0),
	amtot int default(0),
	stucart int default(0),
)

drop table CFMS_Cart
drop table CFMS_Sell_Master

select * from CFMS_Cart
select * from CFMS_ItemMaster

select * from CFMS_Sell_Master

select * from CFMS_Sell_Master s inner join CFMS_ItemMaster i on s.rid=i.rid inner join CFMS_User r on s.rid=r.rid where uid=2 

--select CFMS_Sell_Master.rid,CFMS_Sell_Master.cid,CFMS_Sell_Master.Imid,CFMS_Sell_Master.date,CFMS_Sell_Master.Quty,CFMS_Sell_Master.price,CFMS_Sell_Master.amut,CFMS_ItemMaster.rid,CFMS_ItemMaster.uid,CFMS_ItemMaster.name  from CFMS_Sell_Master inner join CFMS_ItemMaster on  CFMS_Sell_Master.rid=CFMS_ItemMaster.rid  AND  CFMS_ItemMaster.Imid=CFMS_Sell_Master.Imid  where CFMS_Sell_Master; 