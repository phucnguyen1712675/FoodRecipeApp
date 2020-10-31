/* 
Drive chứa folder Images :
https://drive.google.com/drive/folders/1Gynz4CHjAK2kWUN-NP183yyFJ22Yv2iD?usp=sharing
*/

CREATE DATABASE RecipeDATA
go

use RecipeDATA
Go

CREATE TABLE DISH (
 Dish int IDENTITY(1,1),
 Love bit NOT NULL default 0, -- 0 : false    1 = true
 Name nvarchar(40),
 Description nvarchar(600),
 Video nvarchar(100),
 Loai nvarchar(50),
 FilePath nvarchar(100)
 PRIMARY KEY (Dish)
)
go

CREATE TABLE STEP(
 Dish int,
 StepNumber int,
 Desrciption nvarchar(600)
 FOREIGN KEY (Dish) REFERENCES dbo.DISH(DISH)
 PRIMARY KEY (Dish, StepNumber)
)
go

CREATE TABLE IMAGE (
 Dish int,
 StepNumber int,
 FilePath nvarchar(100),
 
 CONSTRAINT fk_IMAGE_STEP FOREIGN KEY (Dish, StepNumber) REFERENCES dbo.STEP (Dish, StepNumber)
)
go

CREATE TABLE QUOTE(
 Quote nvarchar(300)
)
GO


-- INSERT EXAMPLE DATA
/*INSERT INTO dbo.DISH(
	Name , Description, Video, Loai, ImageFilePath
)
VALUES(
	N'',
	N'',
	N'',
	N'',
	N''
)

INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	,
	 ,
	N''
)

INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	,
	 ,
	 N''
)*/

--INSERT DISH
INSERT INTO dbo.DISH(
	Name , Description, Video, Loai, FilePath
)
VALUES( --1
	N'Chè đậu xanh - Khoai lang',
	N'Nguyên liệu rất dễ tìm và rẻ, ai cũng có thể làm món chè đậu xanh khoai lang thơm ngon, bùi hơn. Chiều chiều nấu món này giải nhiệt là nhất luôn đấy!\n
	Thành phần \n
	- Khoai lang : 2 củ\n
	- Đậu xanh : 100gr \n
	- Đường trắng : 150 gr',
	N'https://www.youtube.com/watch?v=KFl6kp4Y6hE',
	N'Chè, ngọt, chay',
	N'\\Image\\1\\recipe18386-636126403006525310.jpg'
),
( --2
N'Canh củ xen hầm táo đỏ',
	N'Củ sen hay còn gọi là liên ngẫu, theo đông y có vị ngọt, tính mát, có tác dụng lợi tiểu, an thần,... Chính vì giá trị dinh dưỡng cao nên củ sen được chế biến thành rất nhiều món ăn. Khi được nấu cùng táo đỏ, nấm hương càng làm món ăn bổ dưỡng hơn. Đặc biệt, món ăn này rất phù hợp cho người ăn chay.\n
	Thành phần \n
	- Củ sen : 400 Gr\n
	- Hạt sen : 100 Gr\n
	- Nấm hương : 30 Gr\n
	- Dừa : 1 Trái\n
	- Cà rốt : 2 Củ\n
	- Đường phèn : 1 Muỗng canh\n
	- Muối : 1/2 Muỗng cà phê\n
	- Hạt nêm : 1 Muỗng cà phê',
	N'https://www.youtube.com/watch?v=nWsXBe5_fjg',
	N'mặn, chay',
	N'\\Image\\2\\Recipe182-635363758513389687.jpg'
),
( --3
N'Bánh mỳ bọc cơm chiên',
	N'Bánh mì bọc cơm chiên là món ăn kết hợp mới lạ giữa bánh mì sandwich giòn giòn, cơm chiên đậm đà, màu sắc đẹp mắt. Hãy nạp năng lượng ngày mới với món ăn này nhé! Không những người lớn mà các bé cũng thích mê.\n
	Thành phần \n
	- Cơm : 1 Chén\n
	- Bánh mì sandwich : 6 Lát\n
	- Cà rốt : 1/2 Củ\n
	- Đậu Hà Lan : 100 Gr\n
	- Bắp hột : 100 Gr\n
	- Sốt cà chua : 1 Muỗng canhn\n
	- Muối : 1/2 Muỗng cà phê',
	N'https://www.youtube.com/watch?v=nvTlubT2vmE',
	N'mặn, chiên, cơm',
	N'\\Image\\3\\recipe11094-636020985974231510.jpg'
),( --4
N'Lẩu kim chi - hải sản',
	N'Lẩu kim chi hải sản với vị chua chua, cay cay đặc trưng của kim chi. Hải sản tươi roi rói hòa quyện cùng nước lẩu và các loại rau củ, chả cá, đậu hũ, xúc xích, ... Trong những ngày lạnh cuối năm mà cả nhà có thể sum tụ, quây quần bên nồi lẩu nóng hổi, vừa ăn vừa hít hà thì ấm cùng biết bao\n
	Thành phần \n
	Kim chi cải thảo : 400 Gr\n
	- Xúc xích heo : 3 Cây\n
	- Bạch tuộc : 1 Kg\n
	- Mực : 1 Con\n
	- Tôm tươi : 10 Con\n
	- Nấm kim châm : 200 Gr\n
	- Đậu hũ chiên : 1 Miếng\n
	- Nấm đông cô : 5 Cái',
	N'https://www.youtube.com/watch?v=12-n7KaMLjE',
	N'Lẩu, mặn, cay, hải sản',
	N'\\Image\\4\\recipe-cover-r25727.jpg'
),( --5
N'Thịt hấp trứng muối',
	N'Thịt hấp trứng muối - món ăn dinh dưỡng cho bữa cơm gia đình thêm ngon miệng hơn trong những ngày mưa mát mẻ. Sự kết hợp giữa trứng muối bùi bùi, mặn mặn và vị béo ngậy của thịt được hấp chín, mùi thơm lan tỏa càng làm cho món ăn càng hấp dẫn.\n
	Thành phần \n
	- Thịt heo xay : 300gr\n
	- Nấm mộc nhĩ : 30gr\n
	- Trứng gà : 1 quả\n
	- Trứng vịt muối : 2 quả\n
	- Tiêu xay  : 1 muỗng cà phê\n
	- Đường : 1/2 muỗng canh',
	N'https://youtu.be/Bs9pTN6F9eU',
	N'mặn, trứng, heo',
	N'\\Image\\5\\ae6ccde4-e2a8-42c0-8ecf-9526ef26b82b.jpg'
),(--6
N'Đùi gà nướng khoai lang',
	N'Đùi gà nướng khoai lang là món ăn thơm ngon, hấp dẫn, bổ dưỡng rất thích hợp để chiêu đãi bạn bè, người thân. Thay vì cách làm thông thường bạn sẽ mất hơn vài giờ đồng hồ để hoàn thành thì Nồi chiên không dầu Corosi sẽ giúp bạn giải quyết vấn đề đó,\n
	Thành phần \n
	- Đùi gà góc tư : 1 đùi\n
	- Khoai lang : 2 củ\n
	- Muối : 1 muỗng cà phê\n
	- Tiêu : 1 muỗng cà phê\n
	- Mật ong : 2 muỗng canh',
	N'https://youtu.be/cMgQ5HfSGgc',
	N'mặn, gà, nướng',
	N'\\Image\\6\\69d38891-be04-4461-8acb-3a4e5c3244a8.jpg'
),( --7
N'Lẩu cá linh bông điền điền',
	N'Lẩu cá linh bông điên điển với hương vị đúng điệu của miền Tây sông nước sẽ là một trong những món ngon cuối tuần cho gia đình bạn. Chỉ mất vài phút chuẩn bị và chế biến theo công thức của Cooky, bạn sẽ có một nồi lẩu thơm nức mũi, hương vị ngọt ngào, đậm đà để mời cả nhà cùng thưởng thức nhé!\n
	Thành phần \n
	- Cá linh : 500gr\n
	- Bông điên điển : 300gr\n
	- Xương ống : 1kg\n
	- Bông súng : 200gr\n
	- Rau ngò gai : 20gr\n
	- Nước cốt me : 30ml',
	N'https://youtu.be/ZvIxQ6zu0EE',
	N'lẩu, mặn, hải sản',
	N'\\Image\\7\\62600c4f-6d27-429f-bbf6-685e44e83441.jpg'
),( --8
N'Snack chuối chiên mè',
	N'Chuối xanh là loại quả không mấy phổ biến bởi nó vừa cứng vừa chát. Tuy nhiên, vẫn có một cách sẽ khiến cho quả chuối xanh vô vị này trở nên hấp dẫn hơn rất nhiều. Cùng bắt tay vào làm món Snack chuối chiên mè sau đây bạn nhé, bạn sẽ thấy món này cực dễ làm mà độ ngon thì đúng chuẩn\n
	Thành phần \n
	- Chuối xiêm xanh : 1 nải\n
	- Chanh : 1 trái\n
	- Muối : 1 muỗng cà phê\n
	- Bột mì : 30gr',
	N'https://youtu.be/EANhMhsZ-A4',
	N'ngọt, chay, chiên',
	N'\\Image\\8\\3fb0ae40-bbad-4eb7-8484-e97f6764c6d3.jpg'
),( --9
N'Bông điền điền muối chua',
	N'Bông điên điển muối chua, mang đậm hương vị miền Tây. Dưa chua bông điên điển giòn ngon, vị chua chua, món ăn không những ngon mà còn hấp dẫn bởi màu vàng của bông điên điển, màu trắng của giá, xanh của hẹ, thêm chút đỏ đỏ của ớt. Món ăn có thể ăn kèm với nhiều món thịt luộc, thịt kho, cá kho... thì rất bắt cơm vào mùa mưa
	\nThành phần \n
	- Bông điên điển : 300gr\n
	- Giá đỗ : 300gr\n
	- Hẹ lá : 100gr\n
	- Tỏi lát : 20gr\n
	- Ớt sừng : 20gr',
	N'https://youtu.be/K_J9_yHXPGQ',
	N'chay, lên men',
	N'\\Image\\9\\40efaa4f-fc88-4327-85c3-89c7722ac279.jpg'
),(--10
N'Cà Phê Dalgona',
	N'Cà phê bọt biển hay còn gọi là cà phê Dalgona là một món cà phê đang rất hot tại Hàn Quốc. Cách làm cực kì đơn giản và những nguyên liệu sẵn có tại nhà. Với hương thơm đầy mê hoặc từ cà phê được đánh bông lên cộng thêm vị sữa tươi béo ngậy, đã tạo nên một ly cà phê độc đáo, thơm ngon và lạ mắt. Hãy cùng Cooky bắt trend năm nay nào. 
	\nThành phần \n
	- Sữa tươi không đường : 150 ml\n
	- Đường trắng : 1 Muỗng canh\n
	- Cà phê hoà tan : 2 Muỗng canh\n
	- Nước sôi : 50 ml',
	N'https://youtu.be/duJMErdoOXU',
	N'ngọt, chay',
	N'\\Image\\10\\recipe52666-cook-step3-637207612645558113.jpg'
),(--11
N'Rau muống xào chao',
	N'Rau muống xào chao là một món ngon dân dã với cách chế biến vô cùng đơn giản. Sự kết hợp giữa vị giòn ngọt của rau muống cùng chao trắng béo ngậy sẽ đem lại vị ngon, lạ và đầy thú vị cho bữa cơm gia đình. Cùng học làm món ăn thanh mát, ngon miệng với công thức đơn giản dưới đây nhé!	
	\nThành phần \n
	- Rau muống : 300 Gr\n
	- Nấm đùi gà : 200 Gr\n
	- Chao : 150 Gr\n
	- Hành boa rô : 20 Gr\n
	- Hạt nêm Maggi : 1 Muỗng canh',
	N'https://youtu.be/0A06x-KtPRg',
	N'mặn, chay, xào',
	N'\\Image\\11\\recipe48273-cook-step5-636975756004892956.jpg'
),(--12
N'Thịt bò xào đậu hà lan',
	N'Thịt bò xào đậu Hà Lan là một món ăn quen thuộc đối với mọi nhà nhưng với sự biến tấu, thêm vào một ít nguyên liệu và gia vị thân quen đã có thể biến món ăn này thành một phiên bản khác, thơm ngon hơn và đầy đủ chất dinh dưỡng hơn. Với sự hỗ trợ của chảo Tchef Fry Pan của Tupperwware sẽ giúp bạn rút ngắn thời gian nấu nướng nhưng vẫn giữ được vẹn nguyên hương vị của món ăn	
	\nThành phần \n
	- Thịt bò : 300 Gr\n
	- Đậu Hà Lan : 50 Gr\n
	- Bắp non : 50 Gr\n
	- Tỏi băm : 10 Gr',
	N'https://youtu.be/x8S482hfOfI',
	N'mặn, xào, bò',
	N'\\Image\\12\\recipe50343-cook-step5-637061211654378235.jpg'
),(--13
N'Canh sườn heo ra củ',
	N'Món canh sườn heo hầm cùng những loại rau củ thơm ngon, hấp dẫn lại cung cấp được đầy đủ những dưỡng chất thiết yếu sẽ giúp cho bữa cơm của gia đình bạn ấm áp và hấp dẫn hơn rất nhiều. Với nồi Tchef Casserole của Tupperwware món canh sườn heo hầm rau củ sẽ trở nên đơn giản hơn bao giờ hết, bạn đã sẵn sàng để thử chưa?	
	\nThành phần \n
	- Sườn non : 300 Gr\n
	- Khoai tây : 100 Gr\n
	- Hạt sen tươi : 100 Gr\n
	- Cà rốt : 100 Gr\n
	- Hạt nêm : 1 Muỗng canh',
	N'https://youtu.be/x8S482hfOfI',
	N'mặn, hầm, heo',
	N'\\Image\\13\\recipe50341-cook-step4-637075998898748530.jpg'
),(--14
N'Bánh Tráng Mắm Ruốc Bằng Chảo Chống Dính',
	N'Bánh tráng mắm ruốc lại có thể dễ dàng làm dễ dàng trên chảo chống chính, bánh khi ăn giòn, mềm kết hợp với vị thơm của bơ, của trứng rồi thịt bằm và nhất là mắm ruốc ngon lành. Món bánh tráng mắm ruốc với những nguyên liệu đơn giản sẽ giúp bạn có món ăn vặt xế chiều chiêu đãi mọi người	
	\nThành phần \n
	- Bánh tráng : 20 Miếng\n
	- Mắm ruốc : 120 Gr\n
	- Thịt băm : 100 Gr\n
	- Trứng cút : 20 Quả\n
	- Bơ : 50 Gr',
	N'https://youtu.be/frQi2dv9wn0',
	N'mặn, chiên, heo, snack',
	N'\\Image\\14\\recipe19443-prepare-step5-636266371717770790.jpg'
),(--15
N'Nem chuối',
	N'Nem chuối là món ăn vặt lý tưởng, béo ngậy cho những ai thích đồ béo nè. Lớp ngoài giòn tan, bên trong ngọt, mềm của chuối, beo béo của phô mai ăn thật thích. Cùng học cách làm với Cooky nào!	
	\nThành phần \n
	- Bánh tráng bía : 1 Gói\n
	- Chuối : 4 Trái\n
	- Phô mai : 50 Gr\n
	- Dầu ăn : 1 Chén',
	N'https://youtu.be/eRfjWIhKARI',
	N'ngọt, chiên, chay',
	N'\\Image\\15\\recipe15965-prepare-step5-635821475775109157.jpg'
),(--16
N'Sò điệp khổng lồ nướng phô mai',
	N'Sò điệp khổng lồ nướng phô mai là món ăn chơi cực ngon và hấp dẫn mà bạn có thể làm ngay tại nhà. Cách làm sò điệp nướng phô mai này quan trọng nhất chính là cách làm sốt phô mai và thời gian nướng sò điệp không để bị cháy. Tất cả các bước đã có, chị em xách giỏ lên đi chợ mua sò về và vào bếp thôi	
	\nThành phần \n
	- Sò điệp Sanriku khổng lồ : 8 Con\n
	- Bơ lạt : 20 Gr\n
	- Xốt mayonnaise : 10 Gr\n
	- Tiêu : 1 Muỗng cà phê\n
	- Muối : 1/2 Muỗng cà phê',
	N'https://youtu.be/RC9rjP6uZVs',
	N'mặn, nướng, hải sản',
	N'\\Image\\16\\recipe52512-cook-step4-637196063038775196.jpg'
),(--17
N'Bánh tráng nướng trứng Đà Lạt',
	N'Bánh tráng trứng Đà Lạt là món ăn vặt trẻ con rất yêu thích. Cuối tuần có nhiều thời gian, bạn có thể tự làm ở nhà cho các bé thưởng thức.	
	\nThành phần \n
	- cái bánh tráng : 10 Cái\n
	- quả trứng gà : 10 Quả\n
	- Khô bò 100 Gr\n
	- Hành lá : 50 Gr\n
	- hộp bơ thực vật : 1 Hộp',
	N'https://youtu.be/WzIUteiwbGY',
	N'mặn, nướng, heo, trứng',
	N'\\Image\\17\\cooky-recipe-637215497149075617.jpg'
),(--18
N'Thịt kho trứng cút',
	N'Thịt kho trứng cút là món ăn tương đối dễ dàm, đơn giản nhưng lại là 1 món ăn ngon, phù hợp với bất kỳ mùa nào trong năm. Đặc biệt vào những ngày Tết mà có 1 nồi thịt kho trứng cút đậm đà, nóng hổi thì rất hao cơm. Hãy cùng Tupperware thực hiện món ngon này nhé!	
	\nThành phần \n
	- Thịt ba chỉ : 250 Gr\n
	- Nước hàng : 50 ml\n
	- Trứng cút : 10 Quả\n
	- Nước mắm : 1.50 Muỗng canh\n
	- Đường trắng : 1/2 Muỗng canh',
	N'https://youtu.be/_fkbfcn24nM',
	N'mặn, kho, heo, trứng',
	N'\\Image\\18\\recipe51998-cook-step3-637147671158673032.jpg'
),(--19
N'Mực và rau củ trộn sốt cay',
	N'Mực và rau củ trộn sốt cay là món ăn giàu dinh dưỡng từ mực tươi và rau củ mà bạn nên thử. Từng miếng mực tươi giòn kết hợp cùng các rau củ bắt mắt và sốt thấm vị, sẽ làm bạn muốn vào bếp thực hiện món này ngay. Cùng Cooky học cách làm mực và rau củ sốt cay siêu hấp dẫn để đãi gia đình thân yêu của mình nhé!	
	\nThành phần \n
	- Mực ống : 2 Con\n
	- Củ cải trắng : 50 Gr\n
	- Cà rốt : 50 Gr\n
	- Dưa leo : 50 Gr\n
	- Hành tây : 50 Gr\n
	- Tương ớt Hàn Quốc : 2 Muỗng canh',
	N'https://youtu.be/4-Lie43c2Ig',
	N'mặn, xào, hải sản',
	N'\\Image\\19\\recipe52668-cook-step5-637221427442396421.jpg'
),(--20
N'Súp hoành thánh lá',
	N'Hoành thánh bắt nguồn từ người Hoa, du nhập và được biến tấu để phù hợp với khẩu vị người Việt nhưng vẫn giữ được nét đặc trưng. Tuy không có nhân nhưng món súp hoành thánh lá ăn kèm tôm, thịt xương ống, trứng cút, giá, huyết. Nghe cái tên đơn giản, nhưng để biết rõ hơn về công thức nấu món súp hoành thánh này thì hãy cùng Cooky vào bếp thử liền nhé	
	\nThành phần \n
	- Vỏ hoành thánh : 300 Gr\n
	- Xương ống : 300 Gr\n
	- Huyết heo : 200 Gr\n
	- Tôm tươi : 200 Gr\n
	- Trứng cút : 10 Quả',
	N'https://youtu.be/Uknq2Vjxvq0',
	N'mặn, hầm, heo, trứng, hải sản',
	N'\\Image\\20\\recipe50103-cook-step5-637129919680310953.jpg'
)
go

-- INSERT INTO step
--DISH 1
INSERT INTO dbo.STEP( 
	Dish, StepNumber, Desrciption
)
VALUES(
	1,
	1,
	N'Đậu xanh rửa sạch, ngâm trong nước ấm qua đêm. Khoai lang gọt vỏ, cắt thành khúc vừa ăn. Sau đó đem hấp khoai khoảng 10 phút, chú ý đừng để bị nát'
),(
	1,
	2,
	N'Nấu đậu xanh cùng nước trong nồi lớn khoảng 30-40 phút. Thỉnh thoảng nhớ quấy từ dưới đáy nồi lên để đậu không bị cháy. Thêm khoai lang và đường vào nấu trong 10 phút nữa là hoàn thành'
),(
	1,
	3,
	N'Múc chè ra chén, có thể ăn nóng hoặc lạnh. Nếu ăn lạnh thì trước đó thêm đường nhiều hơn để lúc đá tan sẽ vừa miệng'
)
go
--DISH 2
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	2,
	 1,
	N'Ngâm nở hạt sen, táo đỏ, nấm, hoài sơn (củ mài) khoảng 45 phút. Đổ nước dừa vào nồi, cho thêm nước vào để có nước canh khoảng 2 tô lớn. Cho hạt sen và hoài sơn vào nấu trước. Nấm sau khi ngâm nở thì vắt cho thật ráo nước. Nấu khoảng 15 phút rồi cho táo đỏ và nấm vào nấu cùng'
),(
	2,
	 2,
	N'Nêm vào nồi 1 muỗng canh đường phèn, 1/2 muỗng cà phê muối, 1 muỗng cà phê hạt nêm. Củ sen gọt vỏ, thái lát mỏng rồi cho vào nồi. Cà rốt gọt vỏ. cắt thành lát dày. Sau khi nấu được khoảng 20 phút nữa thì cho cà rốt vào. Nấu đến khi cà rốt chín là hoàn thành'
),(
	2,
	 3,
	N'Món canh củ sen hầm táo đỏ với vị ngọt thanh từ nước dừa, đường phèn, củ sen sẽ là món ăn lí tưởng cho những ngày bạn thấy ngán thịt cá. Cách làm lại đơn giản, không quá cầu kì hay đòi hỏi nguyên liệu đắt tiền, khó mua. Còn chờ gì nữa mà không nấu thử món này ngay để đãi cả nhà nào'
)
go

-- DISH  3
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	3,
	1,
	N'Cà rốt gọt vỏ, cắt hạt lựu. Sau đó, đem luộc sơ qua cà rốt, đậu Hà Lan, bắp hột cho mềm. Vớt cả 3 ra, để ráo.'
),(
	3,
	2,
	N'Phi thơm tỏi băm với 2 muỗng canh dầu ăn. Cho lần lượt cơm, cà rốt, đậu Hà Lan, bắp hột vào, đảo đều khoảng 2 phút. Nêm gia vị 1/2 muỗng cà phê muối, 1/2 muỗng cà phê đường trắng, 1/3 muỗng cà phê tiêu, 1 muỗng canh sốt cà chua cho vừa ăn.'
),(
	3,
	3,
	N'Bánh mì sandwich cắt bỏ viền vàng bên ngoài, dùng chày cán mỏng, cho ra đĩa.'
),(
	3,
	4,
	N'Trải bánh mì sandwich ra, dùng muỗng múc hỗn hợp cơm chiên vào, túm các mép bánh mì cho dính lại vào nhau'
),(
	3,
	5,
	N'Nhúng bánh mì cơm chiên lần lượt vào trứng gà đánh tan, lăn qua bột chiên xù'
),(
	3,
	6,
	N'Đun sôi dầu ăn. Cho từng miếng bánh mì cơm chiên vào, chiên vàng giòn, nhỏ lửa. Thỉnh thoảng lật đều để bánh mì không bị cháy. Gắp bánh mì bọc cơm chiên ra đĩa có lót sẵn 1 lớp khăn giấy để thấm dầu'
),(
	3,
	7,
	N'Bánh mì bọc cơm chiên cho ra dĩa, ăn cùng với xà lách và rau thơm. Món này ăn nóng cả nhà đều thích'
)
go

--DISH 4
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	4,
	1,
	N'Kim chi cắt khúc ngắn. Tôm cắt bỏ bưới rau và chân, bạch tuộc, mực rửa làm sạch. Mực khứa lát mỏng. Xúc xích thái miếng. Nấm đông cô, chả cá thái lát. Cà chua thái múi cau. Hành lá cắt khúc ngắn. Đậu hũ chiên cắt nhỏ, đậu hũ non cắt khoanh nhỏ'
),(
	4,
	2,
	N'Đun nóng 2 muỗng canh dầu, cho hành tím cắt lát, cắt cắt nhỏ và tỏi băm vào xào thơm. Sau đó cho kim chi vào xào 4-5 phút'
),(
	4,
	3,
	N'Tiếp theo, cho 3 tô nước, nấm đông cô vào nấu. Khi nước sôi thì cho mực, bạch tuộc, tôm và các nguyên liệu còn lại nấu thêm vài phút. Nêm nếm gia vị cho vừa ăn là tắt bếp'
),(
	4,
	4,
	N'Khi dùng có thể nhúng thêm các loại rau xanh, nấm kim châm tùy thích nhé! Ăn cùng bún hay mì đều hợp. Lẩu kim chi hải sản với vị chua chua, cay cay đặc trưng của kim chi. Hải sản tươi roi rói hòa quyện cùng nước lẩu và các loại rau củ, chả cá, đậu hũ, xúc xích, ... '
)
go

--DISH 5
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	5,
	1,
	N'Đầu tiên, cho 300gr thịt heo xay vào một tô sứ, cho tiếp 30gr nấm mọc nhĩ ngâm mềm và băm nhỏ, nêm vào 1 muỗng cà phê tiêu xay, 1/2 muỗng canh đường, 1 muỗng cà phê hạt nêm'
),(
	5,
	2,
	N'Sau đó, cho tiếp vào tô 1 lòng trắng trứng gà và 2 lòng trắng trứng vịt muối ( vì lòng trắng trứng vịt muối đã có vị mặn nên không thêm muối vào món ăn), rồi trộn đều hỗn hợp thịt lại với nhau. Tiếp đó, cho 1 lòng đỏ trứng vịt muối vào lòng giữa tô thịt. Rồi cho vào nồi hấp khoảng 15 phút'
),(
	5,
	3,
	N'Đánh tan 1 lòng đỏ trứng gà với 1 muỗng canh dầu màu điều cho tan, đổ lòng đỏ trứng gà phủ đều lên trên, tiếp tục xếp 4 lòng đỏ trứng muối đã cắt làm tư và 4 lát ớt sừng lên mặt hỗn hợp trứng thịt vừa hấp rồi cho vào xửng hấp tiếp đến khi trứng chín hoàn toàn'
),(
	5,
	4,
	N'Khi ăn các bạn cắt phần trứng thịt vừa hấp thành nhiều miếng nhỏ sẽ lộ ra phần lòng đỏ trứng muối bên trong rất hấp dẫn. Dùng ăn kèm với cơm nóng và dưa leo rất phù hợp và ngon miệng'
)
go

-- DISH 6
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	6,
	1,
	N'Đùi gà rửa sạch, thấm khô. Khoai lang gọt vỏ, rửa sạch, cắt que vừa ăn. Ướp gà với 1 muỗng cà phê muối, 1 muỗng cà phê tiêu, 2 muỗng canh mật ong, 1 muỗng cà phê dầu hào, 1 muỗng cà phê hạt nêm trong 10 phút'
),(
	6,
	2,
	N'Làm nóng nồi chiên không dầu ở chế độ Preheat. Sau đó, cho gà và khoai vào nồi, dùng cọ phết mật ong lên khoai để khoai không bị khô và chỉnh chế độ nướng gà ở nhiệt độ 160 độ C trong 10 phút'
),
(
	6,
	3,
	N'Sau đó, mở lò ra, lật mặt thịt gà lại, phết dầu hào lên gà và nướng thêm 5 phút ở nhiệt độ 160 độ C. Tiếp đến, gắp khoai ra ngoài đĩa, trở mặt thịt một lần nữa rồi nướng ở nhiệt độ 180 độ trong 5 phút để cho phần da của thịt có màu vàng óng đẹp mắt là hoàn thành'
),
(
	6,
	4,
	N'Dọn món ra đĩa, rắc hành lá, tiêu lên trên và thưởng thức ngay. Cắn vào một miếng thịt bạn sẽ cảm nhận ngay phần da, thịt gà mềm béo, thơm ngon, không bị ngấy bởi dầu mỡ thừa, phần khoai lang mềm bùi khiến bạn càng ăn càng mê'
)
go

-- DISH 7
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	7,
	1,
	N'Làm sạch 500gr cá linh tươi , để vào rổ thưa cho ráo nước, ướp cá với 1 muỗng canh tỏi băm, 1 muỗng canh ớt băm, 1 muỗng canh đường, 1 muỗng canh muối, 1/2 muỗng cà phê tiêu trong khoảng 10 phút'
),(
	7,
	2,
	N'Cho 1kg xương ống đã trụng vào nồi cùng 30gr củ cải trắng cắt nhỏ, 10gr hành tím, hầm 20 phút cho ra nước ngọt'
),(
	7,
	3,
	N'Sau đó lọc lấy nước dùng, cho vào nồi 300ml nước dừa tươi, nêm vào 30ml nước cốt me, 2 muỗng canh nước mắm ngon, 1 muỗng canh đường, 1/2 muỗng canh hạt nêm rồi tắt bếp'
),(
	7,
	4,
	N'Trút nước dùng qua nồi ăn lẩu, sau đó cho tỏi phi thơm, thêm ít tóp mỡ, rau ngò gai và nấu cho sôi riu lên. Vì cá linh rất mềm và mau chín, nên trước khi ăn mới trút nhẹ cá linh vào nồi. Vừa ăn, vừa nhúng bông điên điển, bông súng ăn kèm với bún'
)
go

-- DISH 8
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	8,
	1,
	N'Chuối xanh lột vỏ rồi ngâm ngay vào tô nước có pha 1/2 quả chanh và 1 muỗng cà phê muối trong 10 phút để chuối ra bớt mủ và giữ được màu trắng.'
),(
	8,
	2,
	N'Sau đó, vớt chuối ra lau khô, rồi bào miếng không quá dày cũng không quá mỏng. Sau khi bào, thả lại miếng chuối vào tô nước muối pha 1/2 quả chanh, rửa sạch lần nữa. Sau đó cho các miếng chuối lên mẹt phơi khô trước gió khoảng 1 tiếng.'
),(
	8,
	3,
	N'Đem chuối đã phơi khô vào một tô lớn, trộn với 20gr gừng băm, 30gr mè rang, 30gr bột mì, 30gr đường. Tiến hành xóc đều nhẹ nhàng tránh làm chuối bị nát.'
),(
	8,
	4,
	N'Bắc chảo lên bếp, để lửa lớn đến khi dầu sôi, sau đó giảm lửa vừa, cho chuối vào chiên ngập dầu. Cho từng mẻ chuối nhỏ vào chiên lần lượt, dùng đũa tách chuối ra trong lúc chiên để chuối không bị dính nhau. Chiên đến khi chuối vàng giòn thì lấy ra để trên giấy thấm dầu.'
),(
	8,
	5,
	N'Cuối cùng thì chúng ta cũng có một đĩa snack chuối vàng tươi, thơm lừng, cắn vào thì giòn tan với vị ngọt tự nhiên. Chuối chiên xong để nguội cho vào hộp kín bảo quản ở nơi thoáng mát dùng dần bạn nhé, có thể cho vào ngăn mát tủ lạnh để giữ độ khô và giòn lâu hơn!'
)
go

--DISH 9
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	9,
	1,
	N'Đầu tiên, tất cả các rau củ sơ chế rửa sạch, sau đó 200gr cà rốt thái sợi, 100gr hẹ lá cắt khúc, 20gr tỏi và 20gr ớt sừng cắt lát.'
),(
	9,
	2,
	N'Cho tất cả 300gr bông điên điển, 300gr giá đỗ, 100gr hẹ lá, 200gr cà rốt, 20gr tỏi lát và 20gr ớt sừng lát vào cái âu to rồi trộn đều tất cả.'
),(
	9,
	3,
	N'Cho 1 lít nước vo gạo ( đã được lắng, lấy phần nước trong) vào trong âu lớn, thêm 2 muỗng canh muối, 2 muỗng canh đường và khuấy cho tan đều hỗn hợp.'
),(
	9,
	4,
	N'Cho hỗn hợp rau củ vào hủ thủy tinh sau đó, đổ nước vo gạo vào ngập ( đảm bảo rau giá điều ngập trong nước) rồi đậy kín nắp để hủ ở chỗ thoáng mát khoảng 2 ngày là có thể dùng được.'
),(
	9,
	5,
	N'Sau 1-2 ngày, bông điên điển muối sẽ có vị chua dịu, sau đó bạn có thể để tủ lạnh rồi dùng dần. Món ăn có thể dùng ăn kèm với thịt luộc, thịt kho hoặc cá kho...đều rất ngon.'
)
go

--DISH 10
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	10,
	1,
	N'Đầu tiên, cho 2 muỗng cà phê bột cà phê đen hoà tan vào âu thêm 1 muỗng đường cát trắng, sau đó thêm 50 ml nước sôi vào. Tuỳ theo sở thích uống ngọt, bạn có thể cho thêm đường nếu muốn.'
),(
	10,
	2,
	N'Dùng muỗng khuấy đều, sau đó dùng phới lồng đánh bông lên cho đến khi không còn lợn cợn hạt đường và thấy bông cứng là thành công.'
),(
	10,
	3,
	N'Cho đá vào ly, rót từ từ 150 ml sữa tươi vào. Sau đó cho hỗn hợp cà phê đã đánh bông lên trên. Có thể rắc thêm ít bột sô cô la để trang trí cho đẹp mắt. Vậy là bạn đã hoàn thành xong ly cà phê Dalgona độc đáo và thơm ngon rồi.'
)
go

--DISH 11
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	11,
	1,
	N'Chuẩn bị đầy đủ nguyên liệu. Bắt đầu món rau muống xào chao, nấm đùi gà bằng việc cắt nấm đùi gà thành từng khoanh tròn dày khoảng 2-3cm rồi cắt đôi.'
),(
	11,
	2,
	N'Bắc nồi nước sôi, luộc sơ 300gr rau muống rồi ngâm vào nước lạnh.'
),(
	11,
	3,
	N'Làm nước sốt chao bằng cách nghiền nhuyễn 150gr chao rồi cho 2 muỗng canh nước tương, 1 muỗng canh hạt nêm Maggi Nấm hương và 1/2 muỗng canh đường rồi khuấy đều đến khi hỗn hợp hòa quyện.'
),(
	11,
	4,
	N'Bắc chảo nóng, phi thơm hành boa rô với 2 muỗng canh dầu ăn rồi sau đó cho nấm đùi gà vào xào săn trong 3 phút sau đó cho hỗn hợp sốt chao vào đảo đều và nấu đến khi nấm đùi gà ngấm đều sốt chao thì cho rau muống vào xào trong 4 phút.'
),(
	11,
	5,
	N'Rau muống chín, dọn ra đĩa, ăn cùng cơm nóng. Rau muống giòn mát, điểm thêm vị ngọt của nấm đùi gà, quyện cùng mùi thơm và vị béo của chao khiến bữa cơm gia đình ngày chay đậm đà hơn hẳn. Vừa ngon miệng, vừa đơn giản, bạn còn chần chừ gì mà chưa thử?'
)
go

--DISH 12
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	12,
	1,
	N'Thịt bò thái lát mỏng vừa ăn. Ướp với 5gr tỏi băm, 5gr hành tím băm, 1 muỗng canh dầu hào và 1 muỗng cà phê hạt nêm trong 15 phút.'
),(
	12,
	2,
	N'Trụng sơ 50gr đậu Hà Lan trong 2 phút để giúp đậu vẫn giữa được màu xanh khi xào và nhanh chín hơn.'
),(
	12,
	3,
	N'Bắc chảo Tchef Fry Pan, quẹt một lớp dầu mỏng rồi phi thơm 5gr tỏi băm. Sau đó cho thịt bò vào xào sơ rồi trút ra đĩa.'
),(
	12,
	4,
	N'Cho đậu Hà Lan, bắp non vào xào đều, nêm với 1 muỗng cà phê hạt nêm rồi xào trong 3 phút. Sau đó cho thịt bò vào đảo đều đến khi thịt bò nóng thì tắt bếp.'
),(
	12,
	5,
	N'Khi ăn rắc thêm 1 ít tiêu và ngò. Vậy là chỉ với vài phút, bạn đã có thể hoàn thành xong một món ăn vô cùng hấp dẫn và đầy đủ chất dinh dưỡng rồi. Món này ăn nóng sẽ rất ngon đấy.'
)
go

--DISH 13
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	13,
	1,
	N'Bắc nồi nóng, trụng sơ 300gr sườn non để khử mùi hôi và bụi bẩn.'
),(
	13,
	2,
	N'Cho vào nồi Tchef Casserole 2 lít nước, đậy nắp nồi, nấu đến khi sôi thì cho sườn vào hầm trong 10 phút cùng 1 muỗng cà phê muối.'
),(
	13,
	3,
	N'Tiếp theo cho khoai tây đã cắt miếng vừa ăn và hạt sen vào hầm cùng trong 10 phút rồi nêm với 1 muỗng canh hạt nêm, 1/2 muỗng canh đường. Cuối cùng cho cà rốt vào nấu với lửa vừa đến khi các loại củ quả chín mềm là được'
),(
	13,
	4,
	N'Như vậy là chỉ với 30 phút và 3 bước làm đơn giản, món canh sườn hầm rau củ đã hoàn thành rồi, thật đơn giản phải không nào. '
)
go

--DISH 14
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	14,
	1,
	N'Làm nóng chảo, sau đó trải đều bơ và trứng lên bánh'
),(
	14,
	2,
	N'Đầu tiên sẽ làm mắm ruốc cho hơi keo lại và đậm vị hơn. Pha mắm ruốc nguyên chất cùng một ít nước, đường rồi sau đó đảo đều trên chảo dầu cùng với tỏi ớt và một ít nước cốt chanh.'
),(
	14,
	3,
	N'Cho thêm hành lá, thịt băm, pa tê và mắm ruốc đã sơ chế trước vào cùng.'
),(
	14,
	4,
	N'Thêm vào đồ chua và tương đỏ cùng tương đen, nếu bạn không thích đồ chua thì không thêm vào cũng sẽ không làm ảnh hưởng mùi vị bánh.'
),(
	14,
	5,
	N'Sau đó khéo léo cuốn bánh lại, và nướng đều các mặt cho giòn bánh.'
),(
	14,
	6,
	N'Bánh tráng mắm ruốc nướng bằng chảo chống dính vẫn ngon lành như thường, giòn rụm thơm ngon. Mùi bơ trứng rồi pa tê kết hợp cùng vị chua của cà rốt cùng củ cải ngon ơi là ngon!'
)
go

--DISH 15
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	15,
	1,
	N'Chuối bóc vỏ, cắt làm đôi, sau đó, cắt thành 1/4 miếng chuối.'
),(
	15,
	2,
	N'Trải bánh tráng bía ra, cho chuối, phô mai vào, cuốn lại.'
),(
	15,
	3,
	N'Làm nóng dầu ăn trong chảo, cho nem vào, chiên vàng giòn, nhỏ lửa.'
),(
	15,
	4,
	N'Nem chín, gắp ra đĩa có lót sẵn một lớp khăn giấy để thấm dầu.'
),(
	15,
	5,
	N'Món ăn đã hoành thành, béo ngậy, thơm ngon chưa từng thấy. Càng ăn càng ghiền!'
)
go

--DISH 16
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	16,
	1,
	N'Rửa sạch 8 con sò điệp khổng lồ Sanriku với muối để sò bớt mùi tanh và sạch hơn.'
),(
	16,
	2,
	N'Pha sốt phô mai: Cho vào tô 20gr bơ lạt đun chảy, 10gr xốt mayonnaise, 1 muỗng cà phê tiêu xay, 1/2 muỗng cà phê muối rồi trộn đều.'
),(
	16,
	3,
	N'Đặt sò điệp khổng lồ lên khay nướng, rưới phần sốt bơ đã pha lên mặt, nướng lửa vừa đến khi sò chín tái thì rắc phô mai Mozzarella bào lên mặt. Nướng đến khi lớp phô mai chảy ra, rắc thêm mùi tây khô cắt nhuyễn là có thể thưởng thức.'
),(
	16,
	4,
	N'Sò điệp nướng phô mai là món ăn chơi đơn giản mà ngon. Cồi sò điệp tươi giòn dai, ngọt thịt, quyện cùng phô mai dẻo mặn béo thơm ngon. Có thể dùng làm mồi nhấm bia rất tuyệt luôn nhé!'
)
go

--DISH 17
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	17,
	1,
	N'Hành lá rửa sạch, xắt nhỏ. Bày cùng các nguyên liệu khác ra mâm'
),(
	17,
	2,
	N'Bạn có thể dùng bếp ga (chảo chống dính), bếp nướng điện hoặc bếp than để nướng bánh. Đặt bánh tráng lên bếp => phết đều bơ thực vật => rắc hành lá => đập trứng => dùng muỗng dàn đều => rắc đều bò khô khi trứng còn ướt.'
),(
	17,
	3,
	N'Khoảng 3 phút sau xịt tương ớt và sốt mayonnaise lên bánh => Gập đôi lại và nướng cho phần bánh tráng phía ngoài rìa chín vàng.'
),(
	17,
	4,
	N'Sau khi bánh chín, vớt ra đĩa để nguội 1 chút rồi thưởng thức. Bạn có thể dùng kéo cắt nhỏ hoặc dùng giấy kẹp cầm nguyên bánh ăn.'
)
go

--DISH 18
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	18,
	1,
	N'Cho 250gr thịt ba chỉ cắt nhỏ vừa ăn vào xào săn cùng một ít dầu ăn. Tiếp đó đổ 50ml nước hàng vào, cùng 1.5 muỗng canh nước mắm, 1/2 muỗng canh đường, 2 muỗng cà phê hạt nêm và 1/2 muỗng cà phê tiêu xay. Kho đến khi thịt có màu đẹp mắt.'
),(
	18,
	2,
	N'Thả 10 quả trứng cút đã luộc và bóc vỏ vào, nhẹ nhàng đảo đều tay. Kho thêm 10 phút thì tắt bếp'
),(
	18,
	3,
	N'Món thịt kho trứng cút là món ăn rất ngon và đưa cơm, đặc biệt vào những ngày Tết. Cùng Tupperware học cách nấu món này để cả gia đình quây quần thưởng thức bạn nhé'
)
go

--DISH 19
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	19,
	1,
	N'Làm sạch 2 con mực ống, đem khứa vẩy rồng rồi cắt thành miếng vừa ăn. Đem luộc chín rồi vớt ra để riêng.'
),(
	19,
	2,
	N'Làm sốt cay: Cho 2 muỗng canh ớt bột Hàn Quốc, 2 muỗng canh tương ớt Hàn Quốc, 2 muỗng canh nước tương, 1 muỗng canh tỏi băm, 1 muỗng canh sirô bắp, 1 muỗng canh giấm, 20ml nước lọc. Khuấy đều phần sốt'
),(
	19,
	3,
	N'Cho 50gr cà rốt cắt thanh dài, 50gr củ cải trắng cắt thanh dài, 50gr dưa leo cắt thanh dài, 50gr hành tây cắt sợi vào tô trộn đều cùng 3 muỗng canh đường, 3 muỗng canh giấm, 400ml nước lọc. Ngâm rau củ cho đến khi mềm và vớt ra để ráo'
),(
	19,
	4,
	N'Cho mực đã chín vào tô lớn cùng phần rau củ ngâm, cho thêm 20gr hành lá cắt khúc, 10gr ớt sừng cắt khoanh và phần sốt cay vào. Dùng bao tay trộn đều.'
),(
	19,
	5,
	N'Món mực và rau củ sốt cay có thể thưởng thức ngay hoặc dùng với cơm nóng. Món ăn giàu dinh dưỡng và bắt mắt sẽ khiến mâm cơm gia đình bạn càng thêm hấp dẫn'
)
go

--DISH 20
INSERT INTO dbo.STEP(
	Dish, StepNumber, Desrciption
)
VALUES(
	20,
	1,
	N'Cho 300gr xương ống chặt khúc nhỏ hầm với 600ml nước chờ sôi bùng vớt bọt cho nước dùng trong. Hầm 30 phút rồi tắt bếp'
),(
	20,
	2,
	N'Cắt đều thành 4 phần 300gr lá hoành thánh. Đun sôi nồi nước ở lửa vừa, cho lá hoành thánh vào luộc chín. Sau đó vớt ra xả qua nước lạnh để tránh không bị dính vào nhau'
),(
	20,
	3,
	N'Khi nước dùng sôi, nêm 2 muỗng cà phê hạt nêm, 2 muỗng cà phê muối, 2 muỗng cà phê đường rồi cho tiếp 200gr huyết, 200gr tôm làm sạch, 10 quả trứng cút luộc đã bóc vỏ, khuấy đều tay'
),(
	20,
	4,
	N'Cuối cùng cho thêm lá hoành thánh vào nấu sôi súp, tắt bếp múc ra tô rồi rắc lên 1 ít cần tàu và hẹ cắt khúc'
),(
	20,
	5,
	N'Vậy món súp hoành thánh lá theo công thức Cooky đã hoàn thành, tùy theo sở thích bạn có thể biến tấu thêm giò gân hoặc giò nạc ăn kèm. '
)
go


-- INSERT INTO iMAGE
--DISH 1
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	1,
	1,
	N'recipe18386-prepare-step1-636126405796746211.jpg'
),(
	1,
	1,
	N'recipe18386-prepare-step1-636126405797058212.jpg'
),(
	1,
	2,
	N'recipe18386-prepare-step2-636126406009686585.jpg'
),(
	1,
	3,
	N'recipe18386-prepare-step3-636126406189398901.jpg'
)
go

--DISH 2
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	2,
	1,
	N'cooky-recipe-636806860407569840.jpg'
),(
	2,
	1,
	N'cooky-recipe-636806860415851091.jpg'
),(
	2,
	2,
	N'cooky-recipe-636806860585088571.jpg'
),(
	2,
	2,
	N'cooky-recipe-636806860586025767.jpg'
),(
	2,
	3,
	N'cooky-recipe-636806860860592987.jpg'
)
go

--DISH 3
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	3,
	1,
	N'recipe11094-cook-step1-636021121941262323.jpg'
),(
	3,
	2,
	N'recipe11094-prepare-step1-636021124026673986.jpg'
),(
	3,
	3,
	N'recipe11094-cook-step2-636021122456375228.jpg'
),(
	3,
	4,
	N'recipe11094-prepare-step2-636021125035839759.jpg'
),(
	3,
	4,
	N'recipe11094-prepare-step2-636021125053467790.jpg'
),(
	3,
	5,
	N'recipe11094-prepare-step3-636021125317108253.jpg'
),(
	3,
	6,
	N'recipe11094-prepare-step4-636021126067313570.jpg'
),(
	3,
	6,
	N'recipe11094-prepare-step4-636021126277289939.jpg'
),(
	3,
	7,
	N'recipe11094-prepare-step5-636021126809562874.jpg'
),(
	3,
	7,
	N'recipe11094-prepare-step5-636021126827190905.jpg'
)
go

--DISH 4
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	4,
	1,
	N'recipe25727-prepare-step1-636494668327696228.jpg'
),(
	4,
	1,
	N'recipe25727-prepare-step1-636494669188505740.jpg'
),(
	4,
	2,
	N'recipe25727-prepare-step2-636494669524218329.jpg'
),(
	4,
	2,
	N'recipe25727-prepare-step2-636494669991283150.jpg'
),(
	4,
	3,
	N'recipe25727-prepare-step3-636494670232459573.jpg'
),(
	4,
	3,
	N'recipe25727-prepare-step3-636494670507800057.jpg'
),(
	4,
	4,
	N'recipe25727-prepare-step4-636494671150209185.jpg'
)
go

--DISH 5
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	5,
	1,
	N'2fcd0a86-522d-4044-87c8-be001c5521df.jpeg'
),(
	5,
	1,
	N'4c2ed938-9d39-4e26-9078-344ba40a6c3e.jpeg'
),(
	5,
	1,
	N'be27dffb-492b-4b1e-919d-1e41b43a4b23.jpeg'
),(
	5,
	2,
	N'23c29ccf-a9e3-4360-bca8-0249103fca2f.jpeg'
),(
	5,
	2,
	N'b14a808d-0fd4-43eb-ab90-6ee78e4b4cff.jpeg'
),(
	5,
	2,
	N'd34de580-01f9-47b3-a467-e644324f9721.jpeg'
),(
	5,
	3,
	N'7a336ffd-4300-41ea-8cc4-f2c4976f8333.jpeg'
),(
	5,
	3,
	N'25a6b35d-d1cc-453f-a2d4-cec462ec9a0d.jpeg'
),(
	5,
	4,
	N'cce9d165-9466-4209-9a8a-7ae925697a3b.jpeg'
),(
	5,
	4,
	N'7b71cfe5-d5c8-47f9-a537-d0f4bbbd286d.jpeg'
)
go

--DISH 6
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	6,
	1,
	N'1d63be8d-9a68-49b4-99c6-69da68de59a1.jpeg'
),(
	6,
	1,
	N'da18dcfb-8d02-498a-a4ee-248437a81f49.jpeg'
),(
	6,
	1,
	N'e7384f78-c402-4377-b5c8-ea3c5b67ba7e.jpeg'
),(
	6,
	2,
	N'17529212-1089-4db0-bd09-c0fb7d21d7e2.jpeg'
),(
	6,
	2,
	N'2f0c96b4-b163-4ce2-8b73-8d3a5cc1a267.jpeg'
),(
	6,
	2,
	N'9e069aed-55ae-4df3-85fd-864219474ea5.jpeg'
),(
	6,
	3,
	N'4fba3381-e215-441c-9b7b-7c1651fec0f0.jpeg'
),(
	6,
	3,
	N'99688e06-df4c-4a95-9774-0f10cdc0afe4.jpeg'
),(
	6,
	3,
	N'd2513180-1c60-42b5-9d20-8cef28544e0e.jpeg'
),(
	6,
	4,
	N'e57ea161-3d46-458c-9d89-64cd624e5c2f.jpeg'
)
go

--DISH 7
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	7,
	1,
	N'87098a88-833a-4a0d-a7f6-b409e3b768c3.jpeg'
),(
	7,
	1,
	N'b0608b45-2579-4f4b-910d-d1986f0e5575.jpeg'
),(
	7,
	2,
	N'765fb65d-e17f-4087-861a-4cd12481647a.jpeg'
),(
	7,
	2,
	N'835df8a4-64be-4c5e-b22e-134c1f895d68.jpeg'
),(
	7,
	3,
	N'771f2ff4-0e9b-4266-9976-21c14e4e3359.jpeg'
),(
	7,
	4,
	N'1bbd66dd-6b7f-4ad8-81e5-3dd6476121ca.jpeg'
),(
	7,
	4,
	N'a737d511-8566-42a0-8e2a-af15b3dc4303.jpeg'
)
go

--DISH 8
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	8,
	1,
	N'a01db918-11ef-48d5-a77f-5904d54b71c5.jpeg'
),(
	8,
	1,
	N'fd14ec3f-611d-4a32-a10a-83d4db2108c5.jpeg'
),(
	8,
	2,
	N'008f1dfd-d1b5-4b8a-85c1-e5e84486a9fd.jpeg'
),(
	8,
	2,
	N'24324a53-a361-4898-a426-daa626d937e3.jpeg'
),(
	8,
	3,
	N'01e94b8e-2154-48ec-bad5-4f47e3b2a7c3.jpeg'
),(
	8,
	3,
	N'7b89b2ed-6213-4de4-99fb-620527fc3a08.jpeg'
),(
	8,
	4,
	N'1e52130f-12f7-4fef-8f8d-dcd964a1482f.jpeg'
),(
	8,
	4,
	N'7ccda8c8-d6b3-4774-b73b-fcbcf5f93f97.jpeg'
),(
	8,
	5,
	N'3fb0ae40-bbad-4eb7-8484-e97f6764c6d3.jpeg'
),(
	8,
	5,
	N'e5c3c3fe-b0ca-4ed8-9626-0732d5d86b19.jpeg'
)
go

--DISH 9
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	9,
	1,
	N'0195ea98-b56d-488b-908a-5e5293a178ca.jpeg'
),(
	9,
	1,
	N'c3933074-36c7-447e-9d44-7283965cff15.jpeg'
),(
	9,
	2,
	N'132114f7-572b-4860-aaf5-0e5c3a540047.jpeg'
),(
	9,
	2,
	N'734773ff-d99d-4bae-bf73-ca45a2bb8294.jpeg'
),(
	9,
	3,
	N'5de2b0b4-fb43-45df-ba0d-d7e12ee4dcc8.jpeg'
),(
	9,
	3,
	N'bf8ec9b0-2dfe-4b83-ace0-647598c24b1f.jpeg'
),(
	9,
	4,
	N'2f9e03fa-5120-4f62-95b6-bf7eb1b57291.jpeg'
),(
	9,
	4,
	N'af652051-1eb4-4f69-a1e7-6059ac12f5d6.jpeg'
),(
	9,
	5,
	N'3a8dbcc6-43aa-41e1-a4a5-1e8c37b27aac.jpeg'
),(
	9,
	5,
	N'c2b3ce7c-ea29-4c5e-9cfb-a643a3f8270d.jpeg '
)
go

--DISH 10
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	10,
	1,
	N'recipe52666-cook-step1-637207607496802396.jpg'
),(
	10,
	1,
	N'recipe52666-cook-step1-637207607540994928.jpg'
),(
	10,
	2,
	N'recipe52666-cook-step2-637207610401221646.jpg'
),(
	10,
	2,
	N'recipe52666-cook-step2-637207610451624571.jpg'
),(
	10,
	3,
	N'recipe52666-cook-step3-637207612541354196.jpg'
),(
	10,
	3,
	N'recipe52666-cook-step3-637207612576444079.jpg'
)
go

--DISH 11
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	11,
	1,
	N'recipe48273-cook-step1-636975753812463128.jpg'
),(
	11,
	1,
	N'recipe48273-cook-step1-636975754316640922.jpg'
),(
	11,
	2,
	N'recipe48273-cook-step2-636975755074979839.jpg'
),(
	11,
	2,
	N'recipe48273-cook-step2-636978297414124947.jpg'
),(
	11,
	3,
	N'recipe48273-cook-step3-636975755266298428.jpg'
),(
	11,
	3,
	N'recipe48273-cook-step3-636975755399579848.jpg'
),(
	11,
	4,
	N'recipe48273-cook-step4-636975755353329758.jpg'
),(
	11,
	4,
	N'recipe48273-cook-step4-636975755363954777.jpg'
),(
	11,
	5,
	N'recipe48273-cook-step5-636975756001924192.jpg'
),(
	11,
	5,
	N'recipe48273-cook-step5-636975756004892956.jpg'
)
go

--DISH 12
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	12,
	1,
	N'recipe50343-cook-step1-637061191063964958.jpg'
),(
	12,
	1,
	N'recipe50343-cook-step1-637061191092090345.jpg'
),(
	12,
	2,
	N'recipe50343-cook-step2-637061199024159513.jpg'
),(
	12,
	2,
	N'recipe50343-cook-step2-637061199048847408.jpg'
),(
	12,
	3,
	N'recipe50343-cook-step3-637061207865443898.jpg'
),(
	12,
	3,
	N'recipe50343-cook-step3-637075967760533285.jpg'
),(
	12,
	4,
	N'recipe50343-cook-step4-637061209387904547.jpg'
),(
	12,
	4,
	N'recipe50343-cook-step4-637061210007238035.jpg'
),(
	12,
	5,
	N'recipe50343-cook-step5-637061211608753049.jpg'
),(
	12,
	5,
	N'recipe50343-cook-step5-637075968218314061.jpg'
)
go

--DISH 13
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	13,
	1,
	N'recipe50341-cook-step1-637061221180808876.jpg'
),(
	13,
	1,
	N'recipe50341-cook-step1-637061221181278517.jpg'
),(
	13,
	2,
	N'recipe50341-cook-step2-637061222001404879.jpg'
),(
	13,
	2,
	N'recipe50341-cook-step2-637061222018748679.jpg'
),(
	13,
	3,
	N'recipe50341-cook-step3-637061222845276898.jpg'
),(
	13,
	3,
	N'recipe50341-cook-step3-637061222853401928.jpg'
),(
	13,
	3,
	N'recipe50341-cook-step3-637061222876058230.jpg'
),(
	13,
	4,
	N'recipe50341-cook-step4-637075966693195955.jpg'
),(
	13,
	4,
	N'recipe50341-cook-step4-637075966866416754.jpg'
)
go

--DISH 14
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	14,
	1,
	N'recipe19443-prepare-step1-636266363061003585.png'
),(
	14,
	1,
	N'recipe19443-prepare-step1-636266363061003585.png'
),(
	14,
	2,
	N'recipe19443-cook-step1-636266351048826487.png'
),(
	14,
	3,
	N'recipe19443-prepare-step2-636266363195163821.png'
),(
	14,
	3,
	N'recipe19443-prepare-step2-636266363227143877.png'
),(
	14,
	3,
	N'recipe19443-prepare-step2-636266363258967933.png'
),(
	14,
	4,
	N'recipe19443-prepare-step3-636266363406700192.png'
),(
	14,
	4,
	N'recipe19443-prepare-step4-636266365547491952.png'
),(
	14,
	5,
	N'recipe19443-prepare-step4-636266365550611958.png'
),(
	14,
	6,
	N'recipe19443-prepare-step5-636266371719642793.png'
),(
	14,
	6,
	N'recipe19443-prepare-step5-636266371768782879.png'
)
go

--DISH 15
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	15,
	1,
	N'recipe15965-prepare-step1-635821474181722358.jpg'
),(
	15,
	1,
	N'recipe15965-prepare-step1-635821474217914422.jpg'
),(
	15,
	2,
	N'recipe15965-prepare-step2-635821474739423338.jpg'
),(
	15,
	2,
	N'recipe15965-prepare-step2-635821474775459401.jpg'
),(
	15,
	2,
	N'recipe15965-prepare-step2-635821474803695450.jpg'
),(
	15,
	3,
	N'recipe15965-prepare-step3-635821475167488089.jpg'
),(
	15,
	3,
	N'recipe15965-prepare-step3-635821475183400117.jpg'
),(
	15,
	4,
	N'recipe15965-prepare-step4-635821475522700713.jpg'
),(
	15,
	4,
	N'recipe15965-prepare-step5-635821476020497588.jpg'
),(
	15,
	5,
	N'recipe15965-prepare-step5-635821476029545604.jpg'
)
go

-- DISH 16
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	16,
	1,
	N'recipe52512-cook-step1-637196061512639272.jpg'
),(
	16,
	1,
	N'recipe52512-cook-step1-637215447872651492.jpg'
),(
	16,
	2,
	N'recipe52512-cook-step2-637196062093644767.jpg'
),(
	16,
	2,
	N'recipe52512-cook-step2-637215443989053495.jpg'
),(
	16,
	3,
	N'recipe52512-cook-step3-637196062659473281.jpg'
),(
	16,
	3,
	N'recipe52512-cook-step3-637215444029738825.jpg'
),(
	16,
	4,
	N'recipe52512-cook-step4-637196063038775196.jpg'
),(
	16,
	4,
	N'recipe52512-cook-step4-637215444104827323.jpg'
)
go

-- DISH 17
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	17,
	1,
	N'cooky-recipe-637215493342714052.jpg'
),(
	17,
	1,
	N'cooky-recipe-637215493344910753.jpg'
),(
	17,
	2,
	N'cooky-recipe-637215496001081078.jpg'
),(
	17,
	2,
	N'cooky-recipe-637215496001451079.jpg'
),(
	17,
	2,
	N'cooky-recipe-637215496001721059.jpg'
),(
	17,
	3,
	N'cooky-recipe-637215497149075617.jpg'
),(
	17,
	3,
	N'cooky-recipe-637215497149575627.jpg'
),(
	17,
	4,
	N'cooky-recipe-637215498638076114.jpg'
),(
	17,
	4,
	N'cooky-recipe-637215498639141138.jpg'
)
go

-- DISH 18
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	18,
	1,
	N'recipe51998-cook-step1-637147657988051781.jpg'
),(
	18,
	1,
	N'recipe51998-cook-step1-637147657988364418.jpg'
),(
	18,
	2,
	N'recipe51998-cook-step2-637147659202344431.jpg'
),(
	18,
	2,
	N'recipe51998-cook-step2-637147659202656931.jpg'
),(
	18,
	3,
	N'recipe51998-cook-step3-637147671157110520.jpg'
),(
	18,
	3,
	N'recipe51998-cook-step3-637147671409945875.jpg'
)
go

-- DISH 19
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	19,
	1,
	N'recipe52668-cook-step1-637221416651692343.jpg'
),(
	19,
	1,
	N'recipe52668-cook-step1-637221416651848591.jpg'
),(
	19,
	2,
	N'recipe52668-cook-step2-637221416775611505.jpg'
),(
	19,
	2,
	N'recipe52668-cook-step2-637221416775859865.jpg'
),(
	19,
	3,
	N'recipe52668-cook-step3-637221417156727261.jpg'
),(
	19,
	3,
	N'recipe52668-cook-step3-637221417156857290.jpg'
),(
	19,
	4,
	N'recipe52668-cook-step3-637221417157117576.jpg'
),(
	19,
	4,
	N'recipe52668-cook-step4-637221417504594800.jpg'
),(
	19,
	4,
	N'recipe52668-cook-step4-637221417504907310.jpg'
),(
	19,
	5,
	N'recipe52668-cook-step5-637221427439914970.jpg'
),(
	19,
	5,
	N'recipe52668-cook-step5-637221427442396421.jpg'
)
go

-- DISH 20
INSERT INTO dbo.IMAGE(
	Dish,
	StepNumber,
	FilePath
)
VALUES(
	20,
	1,
	N'recipe50103-cook-step1-637129930776572356.jpg'
),(
	20,
	1,
	N'recipe50103-cook-step1-637129930780009907.jpg'
),(
	20,
	2,
	N'recipe50103-cook-step2-637049568590270055.jpg'
),(
	20,
	2,
	N'recipe50103-cook-step2-637049568590426205.jpg'
),(
	20,
	3,
	N'recipe50103-cook-step3-637129918376870272.jpg'
),(
	20,
	3,
	N'recipe50103-cook-step3-637129918377339035.jpg'
),(
	20,
	4,
	N'recipe50103-cook-step4-637129930349071401.jpg'
),(
	20,
	4,
	N'recipe50103-cook-step4-637129930349227575.jpg'
),(
	20,
	5,
	N'recipe50103-cook-step5-637129919679685962.jpg'
),(
	20,
	5,
	N'recipe50103-cook-step5-637129919988905522.jpg'
)
go

-- INSERT QUOTES
INSERT INTO dbo.QUOTE VALUES(
N'Nấu cháo: Để nấu được các món cháo ngon bổ dưỡng mà không bị tràn ra ngoài khi sôi, hãy cho vào đó một ít dầu ăn. Còn nếu muốn khi nấu cháo không bị cháy đáy nồi thì trước khi nấu, đun khô nồi và cho ít dầu ăn tráng đáy và xung quanh nồi sẽ không bị cháy và khê.'
),(
N'Trị bỏng rát khi cắt ớt: Khi cắt hay tỉa ớt, nếu tay bạn bị dính cay, gây bỏng rát, hãy lấy một ít đường cát xoa rồi rửa sạch, hoặc có thể dùng dấm cũng làm tay đỡ rát hơn'
),(
N'Nấu nước dùng cho trong: Nấu nước thật sôi mới cho thịt hoặc xương vào, không được đậy vung xoong. Khi nước sôi lại thì bớt lửa và vớt bọt thường xuyên. Cho vào đó một củ hành tím đã nướng chín.'
),(
N'Cách nêm các gia vị: Theo nguyên tắc loại nào lâu thấm thì nêm trước. Ví dụ như phải nêm muối và đường thì đường nêm trước rồi mới tới muối, sau đó là giấm, xì dầu, nước mắm, cuối cùng là bột ngọt'
),(
N'Tẩy mùi hôi lông của gà, vịt: Khi nhổ lông xong, dùng muối hoặc gừng giã nhuyễn chà xát lên mình con vịt hoặc gà, để độ 5 phút, rửa sạch lại rồi mới mổ ruột.'
),(
N'Cách để măng hết độc tố chỉ cần ớt cay: Bạn phải luôn nhớ mẹo nhỏ là trước khi cho vào nồi để chế biến các món ngon thì hãy luộc măng với ớt cay. Đây là cách làm đơn giản để măng loại bỏ độc tố.'
),(
N'Phân biệt trứng mới, cũ: Ta thả trứng vào chậu nước muối. Nếu trứng chìm thì là trứng mới, trứng mà nổi lên mặt nước là trứng quá cũ, bạn không nên ăn nữa.'
),(
N'Làm sạch ốc cực nhanh với ớt tươi: Bạn chỉ cần cho vài quả ớt tươi vào chậu nước lạnh rồi cho ốc vào thì yên tâm rằng ốc không còn nhớt, thịt ốc sẽ giòn và thơm hơn khi chế biến.'
)
GO


go
create function fsetImage (@dish int , @step int)
returns nvarchar(100)
as begin
declare @newPath nvarchar(100);
set @newPath = N'\\Image\\' + CAST(@dish as nvarchar) + N'\\' + CAST(@step as nvarchar) + N'\\' ;
return @newPath;
end;
go

go
UPDATE dbo.IMAGE set FilePath = dbo.fsetImage(Dish,StepNumber) + FilePath
go

go
update dbo.DISH set Loai = REPLACE (Loai, ' ', '' ) from dbo.DISH
go

go
update dbo.DISH set Love = 1 where Dish = 8 or Dish = 7 or Dish = 9 or Dish = 12 or Dish = 15 or Dish = 3 or Dish = 4 or Dish =1
go
------------PROCEDURE ---------------------
--QUOTES
CREATE PROC USP_getAllQuotes
as begin
select * from dbo.QUOTE
end
go

-- IMAGE
CREATE PROC USP_getImageStepDish
@Dish int , @Step int
as begin
select FilePath from dbo.IMAGE where Dish = @Dish and StepNumber =  @Step
end
go

CREATE PROC USP_getImageDish
@Dish int
as begin
select FilePath from dbo.DISH where Dish = @Dish
end
go

CREATE PROC USP_addNewImage 
@Dish int , @StepNumber int , @FilePath nvarchar(100)
as begin
insert into dbo.IMAGE (Dish, StepNumber, FilePath)
values(@Dish, @StepNumber, @FilePath)
end
go


--STEP 
CREATE PROC USP_getAllStepsInDish
@Dish int
as begin
select * from dbo.STEP where Dish = @Dish
end
go

CREATE PROC USP_addNewStep
 @Dish int , @StepNumber int , @Description nvarchar(600)
 as begin
 insert into dbo.STEP (Dish, StepNumber, Desrciption)
 values (@Dish, @StepNumber, @Description)
 end
 go


--DISH
CREATE PROC USP_getAllDishes
AS BEGIN
SELECT * FROM DBO.DISH
END
GO

CREATE PROC USP_addNewDish
 @IsLove BIT , @Name NVARCHAR(40) , @Video nvarchar(100) , @Description nvarchar(600) , @FilePath nvarchar(100) , @Loai nvarchar(50)
 AS BEGIN
 INSERT INTO dbo.DISH (Love, Name, Video , Description, FilePath, Loai)
 values (@IsLove  , @Name  , @Video , @Description , @FilePath , @Loai )
 select top(1)  * from dbo.DISH order by Dish desc
 END
 GO

go
CREATE FUNCTION dbo.SplitInts
(
   @List      NVARCHAR(MAX),
   @Delimiter NVARCHAR(255)
)
RETURNS TABLE
AS
  RETURN ( SELECT Item  FROM
      ( SELECT Item = x.i.value(N'(./text())[1]', 'nvarchar(max)')
        FROM ( SELECT [XML] = CONVERT(XML, '<i>'
        + REPLACE(@List, @Delimiter, '</i><i>') + '</i>').query('.')
          ) AS a CROSS APPLY [XML].nodes('i') AS x(i) ) AS y
      WHERE Item IS NOT NULL
  );
GO

CREATE PROCEDURE USP_getDishByTypes
  @List NVARCHAR(MAX)
AS
BEGIN
  SET NOCOUNT ON;  
  select * from DISH
where not exists ((select Item from dbo.SplitInts(@List,','))
								except (select Item from dbo.SplitInts(Loai,',')))
END
GO

CREATE PROC USP_getFavouriteDishes
AS 
BEGIN
	SELECT * FROM DBO.DISH
	WHERE LOVE = 1;
END
GO

CREATE PROC USP_updateFavouriteDishes
@DishCode INT
AS 
BEGIN
	update dbo.DISH 
	set Love = 1 ^ Love 
	where Dish = @DishCode
END
GO