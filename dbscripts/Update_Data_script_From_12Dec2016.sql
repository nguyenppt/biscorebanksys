/***
---------------------------------------------------------------------------------
-- 11 Dec 2016 : Nghia : viet nam currency
-- 8  Jan 2017 : Nghia : viet nam currency - My
---------------------------------------------------------------------------------
***/
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'do la', N' đô la');
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'dong', N'đồng');
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'Viet Nam', N' Việt Nam');
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'My', N'Mỹ');
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'Uc', N'Úc');
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'Nhan Dan Te', N'Nhân Dân Tệ');
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'Bang Anh', N'Bảng Anh');
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'Vang', N'Vàng');
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'Hong Kong', N'Hồng Kông');
Update bcurrency set [vietnamese] = REPLACE([vietnamese], 'Yen Nhat', N'Yên Nhật');
Update bcurrency set [Pence] = REPLACE([Pence], 'dong', N'đồng');
