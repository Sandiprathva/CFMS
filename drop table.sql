

drop table CFMS_User
drop table CFMS_Cart
drop table CFMS_ItemMaster
drop table CFMS_Sell_Master
drop table CFMS_ItemType


select * from CFMS_Cart
select * from CFMS_Sell_Master
select * from CFMS_User
select * from CFMS_ItemType

select * from CFMS_ItemMaster

update CFMS_Sell_Master set date='2019-07-05' where sid=4