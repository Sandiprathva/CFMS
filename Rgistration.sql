create table CFMS_User
(
rid int primary key identity(1,1),
rtype varchar(50),
name varchar(80),
gname varchar(100),
addr varchar(500),
email varchar(50),
monum varchar(20),
pass varchar(30),
gpnamestust int default(0),
gamemsatus int default(0),
adgroupsatus int default(0),
adcompnystatus int default(0),
delst int default(0)
)


drop table CFMS_User

select * from CFMS_User

delete CFMS_User where rid=5

UPDATE CFMS_User SET rtype='Administrator'  WHERE rid=1;
