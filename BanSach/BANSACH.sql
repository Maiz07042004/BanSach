CREATE DATABASE BANSACH
GO
USE BANSACH
GO
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Address NVARCHAR(255) NULL,
    Role int not null
);
CREATE TABLE Categories (
    CategoryId INT PRIMARY KEY IDENTITY,
    CategoryName NVARCHAR(100) NOT NULL
);
CREATE TABLE Books (
    BookId INT PRIMARY KEY IDENTITY,
    Title NVARCHAR(200) NOT NULL,
    Author NVARCHAR(100) NOT NULL,
    Price DECIMAL(18, 2) NOT NULL,
	Image NVARCHAR(255),
	Discount DECIMAL(5, 2) NULL,
	Description NVARCHAR(MAX) NULL,
    CategoryId INT,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY IDENTITY,        -- Mã đơn hàng
    UserId INT,                              -- Mã người dùng (liên kết với bảng Users)
    CustomerName NVARCHAR(255),              -- Tên khách hàng
    OrderDate DATETIME NOT NULL,             -- Ngày đặt hàng
    TotalAmount DECIMAL(18, 2),              -- Tổng số tiền đơn hàng
    Address NVARCHAR(255),                   -- Địa chỉ giao hàng
    PhoneNumber NVARCHAR(15),                -- Số điện thoại của khách hàng
    Email NVARCHAR(255),					 -- Email của khách hàng
	OrtherNotes NVARCHAR(MAX),
	Status NVARCHAR(50) NOT NULL DEFAULT 'Pending'
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
CREATE TABLE OrderItems (
    OrderId INT,
    BookId INT,
    Quantity INT,
    UnitPrice DECIMAL(18, 2),
    PRIMARY KEY (OrderId, BookId),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);
CREATE TABLE Carts (
    CartId INT PRIMARY KEY IDENTITY,      -- Mã giỏ hàng (mỗi người dùng chỉ có một giỏ hàng)
    UserId INT,                           -- Mã người dùng
    CreatedDate DATETIME NOT NULL,         -- Ngày tạo giỏ hàng
    UpdatedDate DATETIME NOT NULL,         -- Ngày cập nhật giỏ hàng
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
CREATE TABLE CartItems (
    CartItemId INT PRIMARY KEY IDENTITY,  -- Mã giỏ hàng chi tiết
    CartId INT,                            -- Mã giỏ hàng (liên kết với Cart)
    BookId INT,                            -- Mã sách
    Quantity INT,                          -- Số lượng sách
    UnitPrice DECIMAL(18, 2),              -- Giá của mỗi cuốn sách
    FOREIGN KEY (CartId) REFERENCES Carts(CartId),
    FOREIGN KEY (BookId) REFERENCES Books(BookId)
);

-- Cho phép chèn giá trị vào cột identity
SET IDENTITY_INSERT [dbo].[Categories] ON

-- Chèn các dữ liệu vào bảng Categories
INSERT INTO [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (1, N'Tâm lý')
INSERT INTO [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (2, N'Giáo dục')
INSERT INTO [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (3, N'Tiểu thuyết')
INSERT INTO [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (4, N'Tư liệu')
INSERT INTO [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (6, N'Nấu ăn')
INSERT INTO [dbo].[Categories] ([CategoryId], [CategoryName]) VALUES (10, N'Văn học')

-- Tắt chế độ chèn giá trị vào cột identity
SET IDENTITY_INSERT [dbo].[Categories] OFF

-- Insert products from Product table into Books table with Discount added
INSERT INTO Books (Title, Author, Price, Image, Discount, Description, CategoryId)
VALUES 
(N'Sự im lặng của bầy cừu', N'Thomas Harris', 35000, N'suimlangbaycuu.jpg', 10.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 1),
(N'Đàn ông', N'Thomas Harris', 35000, N'osho1.jpg', 15.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 1),
(N'Phụ nữ', N'Thomas Harris', 35000, N'oshophunu.jpg', 20.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 1),
(N'Đồi gió hú', N'Thomas Harris', 35000, N'doigiohu.jpg', 12.00, N'', 1),
(N'Rừng Nauy', N'Thomas Harris', 35000, N'rungnauy.jpg', 25.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 1),
(N'Lịch sử thế giới', N'Thomas Harris', 35000, N'lichsuthegioi.jpg', 5.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 1),
(N'Kafka bên bờ biển', N'Thomas Harris', 35000, N'kafka.jpg', 18.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 1),
(N'Không gia đình', N'Thomas Harris', 25000, N'khongnha.jpg', 22.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 2),
(N'Tôi là Bê Tô', N'Thomas Harris', 25000, N'beto.jpg', 17.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 2),
(N'Cảm xúc', N'Thomas Harris', 10000, N'osho.jpg', 30.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 3),
(N'Hai số phận', N'Thomas Harris', 15000, N'haisophan.jpg', 8.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 3),
(N'Nhà giả kim', N'Thomas Harris', 10000, N'nhagiakim.jpg', 20.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 3),
(N'Ác quỷ rừng phế tích', N'Thomas Harris', 10000, N'acquyrung.jpg', 15.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 3),
(N'Thanh xuân để dành', N'Thomas Harris', 10000, N'thanhxuan.jpg', 12.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 3),
(N'Hoàng tử bé', N'Thomas Harris', 10000, N'hoangtube.jpg', 10.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 3),
(N'Đời thừa', N'Thomas Harris', 35000, N'doithua.jpg', 28.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 1),
(N'Người đua diều', N'Thomas Harris', 45000, N'duadieu.jpg', 14.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 1),
(N'Về nhà với mẹ', N'Thomas Harris', 30000, N'venha.jpg', 20.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 1),
(N'Tiệm sách cơn mưa', N'Thomas Harris', 37000, N'tiemsach.jpg', 18.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 1),
(N'Đại dương đen', N'Thomas Harris', 35000, N'daiduongden.jpg', 10.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 1),
(N'Nghi can X', N'Thomas Harris', 40000, N'nghicanx.jpg', 12.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 2),
(N'Hãy chăm sóc mẹ', N'Thomas Harris', 40000, N'chamsocme.jpg', 17.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 2),
(N'Bố con cá gai', N'Thomas Harris', 50000, N'boconca.jpg', 8.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 4),
(N'Lolita', N'Thomas Harris', 32000, N'lolita.jpg', 22.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 4),
(N'Cây cam ngọt của tôi', N'Thomas Harris', 23000, N'caycamngot.jpg', 25.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 4),
(N'Hoàng tử bé 2', N'Thomas Harris', 36000, N'hoangtu.jpg', 5.00, N'Tác giả: Thomas Harris; Dịch giả: Phạm Hồng Anh; Nhà xuất bản: Hội nhà văn; Số trang: 359; Kích thước: 15 x 24 cm', 4);


select *from Users

INSERT INTO Users (Name, Email, Password, Address, Role) 
VALUES (N'Nguyễn Văn A', N'nguyenvana@example.com', N'123', N'123 Đường ABC, TP.HCM', 0);

INSERT INTO Users (Name, Email, Password, Address, Role) 
VALUES (N'Phan Minh Nhat', N'nhatphan601@gmail.com', N'123', N'123 Đường ABC, TP.HCM', 0);

INSERT INTO Users (Name, Email, Password, Address, Role) 
VALUES (N'Đặng Thanh Mai', N'dangmai19112016@gmail.com', N'123', N' Võ Nguyên Giáp, Quảng Nam', 1);

DECLARE @UserId INT = (SELECT UserId FROM Users WHERE Name = N'Nguyễn Văn A');
INSERT INTO Carts (UserId, CreatedDate, UpdatedDate)
VALUES (@UserId, GETDATE(), GETDATE());

DECLARE @UserId INT = (SELECT UserId FROM Users WHERE Name = N'Nguyễn Văn A');
DECLARE @CartId INT = (SELECT CartId FROM Carts WHERE UserId = @UserId);

-- Chèn sách vào giỏ hàng (giả sử sách có BookId là 1, 2, 3...)
INSERT INTO CartItems (CartId, BookId, Quantity, UnitPrice)
VALUES
(@CartId, 1, 2, 35000),  -- "Sự im lặng của bầy cừu" (BookId = 1)
(@CartId, 2, 1, 35000),  -- "Đàn ông" (BookId = 2)
(@CartId, 3, 3, 35000);  -- "Phụ nữ" (BookId = 3)


DECLARE @UserId INT = (SELECT UserId FROM Users WHERE Name = N'Nguyễn Văn A');
DECLARE @CartId INT = (SELECT CartId FROM Carts WHERE UserId = @UserId);
DECLARE @OrderId INT;
DECLARE @TotalAmount DECIMAL(18, 2) = (SELECT SUM(CartItems.Quantity * CartItems.UnitPrice) 
                                        FROM CartItems
                                        WHERE CartItems.CartId = @CartId);

INSERT INTO Orders (UserId, CustomerName, OrderDate, TotalAmount, Address, PhoneNumber, Email)
VALUES 
(@UserId, N'Nguyễn Văn A', GETDATE(), @TotalAmount, N'123 Đường ABC, TP.HCM', N'0123456789', N'nguyenvana@example.com');

-- Lấy OrderId vừa chèn vào
DECLARE @UserId INT = (SELECT UserId FROM Users WHERE Name = N'Nguyễn Văn A');
DECLARE @CartId INT = (SELECT CartId FROM Carts WHERE UserId = @UserId);
DECLARE @OrderId INT=(SELECT OrderId FROM Orders WHERE UserId = @UserId);
DECLARE @TotalAmount DECIMAL(18, 2) = (SELECT SUM(CartItems.Quantity * CartItems.UnitPrice) 
                                        FROM CartItems
                                        WHERE CartItems.CartId = @CartId);

INSERT INTO Orders (UserId, CustomerName, OrderDate, TotalAmount, Address, PhoneNumber, Email)
VALUES 

(@UserId, N'Nguyễn Văn A', GETDATE(), @TotalAmount, N'123 Đường ABC, TP.HCM', N'0123456789', N'nguyenvana@example.com');

-- Lấy OrderId vừa chèn vào
DECLARE @UserId INT = (SELECT UserId FROM Users WHERE Name = N'Nguyễn Văn A');
DECLARE @OrderId INT=(SELECT OrderId FROM Orders WHERE UserId = @UserId);
INSERT INTO OrderItems (OrderId, BookId, Quantity, UnitPrice)
SELECT @OrderId, CartItems.BookId, CartItems.Quantity, CartItems.UnitPrice
FROM CartItems
WHERE CartItems.CartId = @CartId;


INSERT INTO Users (Name, Email, Password, Address, Role) 
VALUES (N'Phạm Thị B', N'phamthib@example.com', N'password456', N'456 Đường DEF, Hà Nội', 1);

INSERT INTO Users (Name, Email, Password, Address, Role) 
VALUES (N'Trần Minh C', N'tranminhc@example.com', N'password789', N'789 Đường GHI, Đà Nẵng', 1);


select * from Books
select * from Orders
select * from OrderItems



-- Thống kê số lượng sách theo danh mục
SELECT c.CategoryName, COUNT(b.BookId) AS BookCount
FROM Books b
LEFT JOIN Categories c ON b.CategoryId = c.CategoryId
GROUP BY c.CategoryName


--Thống kê doanh thu theo thời gian
SELECT CAST(o.OrderDate AS DATE) AS OrderDay, SUM(oi.Quantity * oi.UnitPrice) AS TotalRevenue
FROM Orders o
JOIN OrderItems oi ON o.OrderId = oi.OrderId
GROUP BY CAST(o.OrderDate AS DATE)
ORDER BY CAST(o.OrderDate AS DATE)

--Thống kê đơn hàng theo trạng thái
SELECT 
    Status, 
    COUNT(*) AS TotalCount
FROM Orders
GROUP BY Status;