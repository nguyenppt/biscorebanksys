/***
---------------------------------------------------------------------------------
-- 11 Dec 2016 : Nghia : viet nam currency
---------------------------------------------------------------------------------
***/
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'do la', N' đô la');
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'dong', N'đồng');
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'Viet Nam', N' Việt Nam');
Update bcurrency set [Pence] = REPLACE([Pence], 'dong', N'đồng');
