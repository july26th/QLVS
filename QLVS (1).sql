USE MASTER
drop database QLVS
create database QLVS

use QLVS

create table DaiLy
(
	MaDaiLy varchar(20) primary key,
	TenDaiLy nvarchar(50) not null,
	DiaChi nvarchar(100) not null,
	SDT varchar(15) not null
)


create table LoaiVeso
(
	MaLoaiVeSo varchar(20) primary key,
	Tinh nvarchar(20),
	GiaVe decimal (19,3) DEFAULT 10000 -- Mặc định 10k, nhân viên có thể sửa trên giao diện
)


create table SoLuongDK -- Đại lý đăng ký số lượng vé mong muốn cho đợt phát hành kế tiếp 
(
	ID varchar(20) primary key,
	MaDaiLy varchar(20),
	NgayDK date not null,
	SLDK decimal not null,
	foreign key (MaDaiLy) references DaiLy(MaDaiLy)
)


create table PhatHanh -- Đợt phát hành
(
	ID int identity ,
	MaDaiLy varchar(20) ,
	MaLoaiVeSo varchar(20) ,
	SoLuong decimal, -- Tính ở procedure
	NgayNhan date not null,
	SLBan int,
	DoanhThuDPH decimal(19, 3), --doanhthuDPH = slban * GiaVe
	HoaHong decimal(2, 0), --vi du: min:10, max: 50 
	TienThanhToan decimal(19,3), /* (100-HoaHong)/100* DoanhThuDPH */
	primary key (ID,MaDaiLy, MaLoaiVeSo),
	foreign key (MaDaiLy) references DaiLy(MaDaiLy),
	foreign key (MaLoaiVeSo) references LoaiVeso(MaLoaiVeSo)
)


create table Giai (
	MaGiai varchar(20) primary key,
	TenGiai nvarchar(30),
	SoTienNhan decimal(19, 3)
)


create table KetQuaSoXo
(
	ID int primary key,  
	MaLoaiVeSo varchar(20),
	MaGiai varchar(20),
	NgaySo date,
	SoTrung varchar(10),

	foreign key (MaLoaiVeSo) references LoaiVeSo(MaLoaiVeSo),
	foreign key (MaGiai) references Giai(MaGiai)
)


create table PhieuThu -- Số tiền đại lý đã nộp cho công ty
(
	MaPhieuThu varchar(20) primary key,
	MaDaiLy varchar(20),
	NgayNop date,
	SoTienNop decimal(19,3),
	foreign key (MaDaiLy) references DaiLy(MaDaiLy)
)


create table PhieuChi -- Số tiền công ty chi với những chi phí phát sinh
(
	MaPhieuChi varchar(20) primary key,
	Ngay date,
	NoiDung nvarchar(200),
	SoTien decimal (19, 3)
)




/*NHẬP DỮ LIỆU*/

insert into DaiLy values ('DL01', N'Hoài Linh', N'223 Phạm Văn Chiêu, Quận Gò Vấp, TP.Hồ Chí Minh', '093 847 2446')
insert into DaiLy values ('DL02', N'Làm Giàu Không Khó', N'150 Nguyễn Huệ, Quận 1, TP.Hồ Chí Minh', '090 365 9762')
insert into DaiLy values ('DL04', N'Trúng Số', N'392 An Dương Vương, Quận 5, TP.Hồ Chí Minh', '093 672 9972')
insert into DaiLy values ('DL05', N'Phát Tài Phát Lộc', N'912 Hai Bà Trưng, Quận 1, Quận Phú Nhuận, TP.Hồ Chí Minh', '099 810 0208')
insert into DaiLy values ('DL06', N'Phá Sản', N'14 Lò Gốm, Quận 6, TP.Hồ Chí Minh', '090 599 0981')

insert into LoaiVeso values ('AG', N'An Giang', 10000)
insert into LoaiVeso values ('BD', N'Bình Dương', 10000)
insert into LoaiVeso values ('BP', N'Bình Phước', 10000)
insert into LoaiVeso values ('BL', N'Bạc Liêu', 10000)
insert into LoaiVeso values ('BT', N'Bến Tre', 10000)
insert into LoaiVeso values ('CM', N'Cà Mau', 10000)
insert into LoaiVeso values ('CT', N'Cần Thơ', 10000)
insert into LoaiVeso values ('DN', N'Đồng Nai', 10000)
insert into LoaiVeso values ('DT', N'Đồng Tháp', 10000)
insert into LoaiVeso values ('VT', N'Vũng Tàu', 10000)
insert into LoaiVeso values ('VL', N'Vĩnh Long', 10000)
insert into LoaiVeso values ('KG', N'Kiên Giang', 10000)
insert into LoaiVeso values ('LA', N'Long An', 10000)
insert into LoaiVeso values ('TN', N'Tây Ninh', 10000)
insert into LoaiVeso values ('TP', N'TP.Hồ Chí Minh', 10000)


insert into Giai values ('GI01', N'Giải nhất', 30000000)
insert into Giai values ('GI02', N'Giải nhì', 15000000)
insert into Giai values ('GI03', N'Giải ba', 10000000)
insert into Giai values ('GI04', N'Giải tư', 3000000)
insert into Giai values ('GI05', N'Giải năm', 1000000)
insert into Giai values ('GI06', N'Giải sáu', 400000)
insert into Giai values ('GI07', N'Giải bảy', 200000)
insert into Giai values ('GI08', N'Giải tám', 100000)

insert into Giai values ('GI01', N'Giải nhất', 30000000)
insert into Giai values ('GI02', N'Giải nhì', 15000000)
insert into Giai values ('GI03', N'Giải ba', 10000000)
insert into Giai values ('GI04', N'Giải tư', 3000000)
insert into Giai values ('GI05', N'Giải năm', 1000000)
insert into Giai values ('GI06', N'Giải sáu', 400000)
insert into Giai values ('GI07', N'Giải bảy', 200000)
insert into Giai values ('GI08', N'Giải tám', 100000)
insert into Giai values ('GIDB', N'Giải đặc biệt', 2000000000)
insert into Giai values ('GIPDB', N'Giải phụ đặc biệt', 50000000)
insert into Giai values ('GIKK', N'Giải Khuyến khích', 6000000)


insert into SoLuongDK values ('DK001', 'DL01', '03/21/2018', 100)
insert into SoLuongDK values ('DK002', 'DL02', '04/20/2018', 150)
insert into SoLuongDK values ('DK004', 'DL04', '06/27/2018', 150)
insert into SoLuongDK values ('DK005', 'DL05', '07/26/2018', 200)
insert into SoLuongDK values ('DK006', 'DL06', '08/30/2018', 300)
insert into SoLuongDK values ('DK007', 'DL02', '09/25/2018', 200)

/*so luong giao tiep theo = sldk * ti le ban 3 dot gan nhat*/
insert into PhatHanh values ('DL01', 'TN', 100, '2018/12/05', 80, 800000, 10, 720000)
insert into PhatHanh values ('DL01', 'AG', 100, '2018/12/05', 90, 900000, 10, 810000)
insert into PhatHanh values ('DL01', 'BT', 100, '2018/12/05', 100, 1000000, 10, 900000)
insert into PhatHanh values ('DL02', 'TN', 150, '2018/12/05', 130, 1300000, 10, 1170000)
insert into PhatHanh values ('DL02', 'AG', 150, '2018/12/05', 140, 1400000, 10, 1260000)
insert into PhatHanh values ('DL05', 'TN', 200, '2018/12/05', 190, 1900000, 10, 1710000)
insert into PhatHanh values ('DL05', 'BT', 200, '2018/12/05', 180, 1800000, 10, 1620000)
insert into PhatHanh values ('DL06', 'AG', 300, '2018/12/05', 250, 2500000, 10, 2250000)
insert into PhatHanh values ('DL01', 'VL', 90, '2018/12/06', 80, 800000, 10, 720000)
insert into PhatHanh values ('DL01', 'BD', 90, '2018/12/06', 70, 700000, 10, 630000)
insert into PhatHanh values ('DL02', 'VL', 135, '2018/12/06', 135, 1350000, 10, 1215000)
insert into PhatHanh values ('DL02', 'BD', 135, '2018/12/06', 130, 1300000, 10, 1170000)
insert into PhatHanh values ('DL06', 'BD', 250, '2018/12/06', 240, 2400000, 10, 2160000)
insert into PhatHanh values ('DL02', 'TP', 145, '2018/12/07', 145, 1450000, 10, 1305000)
insert into PhatHanh values ('DL02', 'LA', 145, '2018/12/07', 140, 1400000, 10, 1260000)
insert into PhatHanh values ('DL06', 'LA', 145, '2018/12/07', 100, 1400000, 10, 1260000)
insert into PhatHanh values ('DL06', 'DT', 145, '2018/12/07', 100, 1400000, 10, 1260000)

insert into KetQuaSoXo values ('1', 'TP', 'GI01','2018/10/06','77282')
insert into KetQuaSoXo values ('2', 'TP', 'GI02','2018/10/06','75104')
insert into KetQuaSoXo values ('3', 'TP', 'GI03','2018/10/06','42663')
insert into KetQuaSoXo values ('4', 'TP', 'GI03','2018/10/06','30772')
insert into KetQuaSoXo values ('5', 'TP', 'GI04','2018/10/06','35641')
insert into KetQuaSoXo values ('6', 'TP', 'GI04','2018/10/06','15591')
insert into KetQuaSoXo values ('7', 'TP', 'GI04','2018/10/06','03619')
insert into KetQuaSoXo values ('8', 'TP', 'GI04','2018/10/06','30705')
insert into KetQuaSoXo values ('9', 'TP', 'GI04','2018/10/06','99993')
insert into KetQuaSoXo values ('10', 'TP', 'GI04','2018/10/06','36204')
insert into KetQuaSoXo values ('11', 'TP', 'GI04','2018/10/06','74553')
insert into KetQuaSoXo values ('12', 'TP', 'GI05','2018/10/06','9840')
insert into KetQuaSoXo values ('13', 'TP', 'GI06','2018/10/06','7076')
insert into KetQuaSoXo values ('14', 'TP', 'GI06','2018/10/06','5152')
insert into KetQuaSoXo values ('15', 'TP', 'GI06','2018/10/06','2296')
insert into KetQuaSoXo values ('16', 'TP', 'GI07','2018/10/06','279')
insert into KetQuaSoXo values ('17', 'TP', 'GI08','2018/10/06','38')
insert into KetQuaSoXo values ('18', 'TP', 'GIDB','2018/10/06','075811')

insert into PhieuThu values ('PTH0001', 'DL02', '2018/10/06',1000000)
insert into PhieuThu values ('PTH0002', 'DL05', '2018/10/06',500000)
insert into PhieuThu values ('PTH0003', 'DL01', '2018/10/07',1000000)
insert into PhieuThu values ('PTH0004', 'DL02', '2018/10/07',1000000)
insert into PhieuThu values ('PTH0006', 'DL06', '2018/10/07',500000)

insert into	 PhieuChi values ('PCH0001', '2018/10/05',N'Trúng giải đặc biệt',2000000000)
insert into PhieuChi values ('PCH0002', '2018/10/06',N'Trúng 8 giải tám',8000000)


---------------------CAP NHAT SO TIEN PHAI TRA KHI CAP NHAT So Luong Ban --------------------------------------------------------
CREATE TRIGGER UPDATE_SLB_TPT
ON PhatHanh
FOR UPDATE
AS
	DECLARE @SLB int, @TienThanhToan decimal(19,3), @TiLeHH float, @id int 
	IF EXISTS (SELECT * FROM inserted, PhatHanh 
		WHERE inserted.ID	= PhatHanh.ID
		AND PhatHanh.SLBan > 0 )
		BEGIN
--------SET id------------------------------------------------------------------------------
		SET @id = (SELECT INSERTED.ID FROM inserted, PhatHanh
			WHERE inserted.ID = PhatHanh.ID and inserted.MaDaiLy = PhatHanh.MaDaiLy AND inserted.MaLoaiVeSo	= PhatHanh.MaLoaiVeSo)
--------SET So Luong Ve Ban Duoc ---------------------------------------------------------------- 
		SET @SLB = (SELECT Inserted.SLBan FROM inserted, PhatHanh
			WHERE inserted.ID = PhatHanh.ID and inserted.MaDaiLy = PhatHanh.MaDaiLy AND inserted.MaLoaiVeSo	= PhatHanh.MaLoaiVeSo)
--------SET Ti Le Hoa Hong		----------------------------------------------------------
		SET @TiLeHH = 1 - ((SELECT HoaHong FROM Inserted)*0.01)
--------SET Tien Thanh Toan----------------------------------------------------------------------
		SET @TienThanhToan = (SELECT GiaVe FROM inserted, LoaiVeso 
			WHERE inserted.MaLoaiVeSo = LoaiVeso.MaLoaiVeSo) * @SLB * @TiLeHH 
--------Update Tien Thanh Toan Trong Table PhatHang---------------------------------------------		
		update PhatHanh set TienThanhToan = @TienThanhToan where PhatHanh.ID = @id 	
		update PhatHanh set DoanhThuDPH = ((SELECT GiaVe FROM inserted, LoaiVeso 
			WHERE inserted.MaLoaiVeSo = LoaiVeso.MaLoaiVeSo) * @SLB) where PhatHanh.ID = @id 	
		END;

update PhatHanh set SLBan = 60 where PhatHanh.ID = 21 
drop trigger UPDATE_SLB_TPT

CREATE TRIGGER INSERT_SLB_TPT
ON PhatHanh
FOR INSERT 
AS
	DECLARE @SLB int, @TienThanhToan decimal(19,3), @TiLeHH float, @id int 
	IF EXISTS (SELECT * FROM inserted, PhatHanh 
		WHERE inserted.ID	= PhatHanh.ID
		AND PhatHanh.SLBan > 0 )
		BEGIN
--------SET id------------------------------------------------------------------------------
		SET @id = (SELECT INSERTED.ID FROM inserted, PhatHanh
			WHERE inserted.ID = PhatHanh.ID and inserted.MaDaiLy = PhatHanh.MaDaiLy AND inserted.MaLoaiVeSo	= PhatHanh.MaLoaiVeSo)
--------SET So Luong Ve Ban Duoc ---------------------------------------------------------------- 
		SET @SLB = (SELECT Inserted.SLBan FROM inserted, PhatHanh
			WHERE inserted.ID = PhatHanh.ID and inserted.MaDaiLy = PhatHanh.MaDaiLy AND inserted.MaLoaiVeSo	= PhatHanh.MaLoaiVeSo)
--------SET Ti Le Hoa Hong		----------------------------------------------------------
		SET @TiLeHH = 1 - ((SELECT HoaHong FROM Inserted)*0.01)
--------SET Tien Thanh Toan----------------------------------------------------------------------
		SET @TienThanhToan = (SELECT GiaVe FROM inserted, LoaiVeso 
			WHERE inserted.MaLoaiVeSo = LoaiVeso.MaLoaiVeSo) * @SLB * @TiLeHH 
--------Update Tien Thanh Toan Trong Table PhatHang---------------------------------------------		
		update PhatHanh set TienThanhToan = @TienThanhToan where PhatHanh.ID = @id 	
		update PhatHanh set DoanhThuDPH = ((SELECT GiaVe FROM inserted, LoaiVeso 
			WHERE inserted.MaLoaiVeSo = LoaiVeso.MaLoaiVeSo) * @SLB) where PhatHanh.ID = @id 	
		END;



---------------------------RANG BUOC KHI THEM MOI NEU SLBAN=0 THI SO TIEN PHAI TRA =0--------------------------
CREATE TRIGGER RB_INSERT_SLB_TPT
ON PhatHanh
FOR INSERT
AS
	DECLARE @ID INT
	IF EXISTS (SELECT * FROM inserted, PhatHanh WHERE inserted.ID = PhatHanh.ID 
	AND PhatHanh.SLBan = 0 and PhatHanh.TienThanhToan >0)
	BEGIN
		SET @ID = (SELECT INSERTED.ID FROM inserted)
		UPDATE PhatHanh set TienThanhToan = 0  WHERE PhatHanh.ID = @ID	
	END

DROP TRIGGER RB_INSERT_SLB_TPT

---------------------------RANG BUOC KHI UPDATE NEU SLBAN=0 THI SO TIEN PHAI TRA =0--------------------------
CREATE TRIGGER RB_UPDATE_SLB_TPT
ON PhatHanh
FOR UPDATE
AS
	DECLARE @ID INT
	IF EXISTS (SELECT * FROM inserted, PhatHanh WHERE inserted.ID = PhatHanh.ID 
	AND PhatHanh.SLBan = 0 and PhatHanh.TienThanhToan > 0)
	BEGIN
		SET @ID = (SELECT ID FROM inserted)
		UPDATE PhatHanh set TienThanhToan = 0  WHERE PhatHanh.ID = @ID	
	END

DROP TRIGGER RB_UPDATE_SLB_TPT
---------------- TINH SUM CONG NO --------------------------------------------------
CREATE PROCEDURE CONGNO 
@MADAILY VARCHAR(10),
@NGAY DATE
AS
BEGIN
	DECLARE @CN DECIMAL (19,3) , @SUM_STPT DECIMAL (19,3), @SUM_STDT DECIMAL (19,3)
	SELECT @SUM_STPT = SUM(TienThanhToan) FROM PhatHanh WHERE MaDaiLy = @MADAILY AND NgayNhan < = @NGAY AND SLBan > 0
	SELECT @SUM_STDT = SUM(SoTienNop) FROM PhieuThu WHERE MaDaiLy = @MADAILY and NgayNop < = @NGAY 
	SET @CN = @SUM_STPT - @SUM_STDT
	PRINT @CN
END

exec CONGNO 'DL06', '2018-10-11'
------------------------
CREATE PROCEDURE DOANHTHU
AS
BEGIN
	declare @SUM_THU DECIMAL(19,3), @SUM_CHI DECIMAL(19,3), @NGAY DATE, @DOANHTHUTHANG DECIMAL(19,3)
	SET @NGAY = (SELECT GETDATE())
	SELECT @SUM_THU = SUM(SoTienNop) FROM PhieuThu WHERE MONTH(NgayNop) = MONTH(@NGAY)
	SELECT @SUM_CHI = SUM(SoTien) FROM PhieuChi WHERE MONTH(Ngay) = MONTH(@NGAY)  
	SET @DOANHTHUTHANG = @SUM_THU - @SUM_CHI
	PRINT @DOANHTHUTHANG
END

drop procedure DOANHTHU
exec DOANHTHU