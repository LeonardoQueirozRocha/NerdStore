DECLARE @AdesivoId   AS UNIQUEIDENTIFIER;
DECLARE @CamisetasId AS UNIQUEIDENTIFIER;
DECLARE @CanecasId   AS UNIQUEIDENTIFIER;

SET @AdesivoId = NEWID();
SET @CamisetasId = NEWID();
SET @CanecasId = NEWID();


INSERT INTO Categories 
VALUES  (@AdesivoId, 'Adesivos', '102'), 
        (@CamisetasId, 'Camisetas', '100'), 
        (@CanecasId, 'Canecas', '101')

INSERT INTO Products (Id, CategoryId, Name, [Description], Active, [Value], RegistrationDate, [Image], QuantityInStock, Height, Width, Depth) 
VALUES  (NEWID(), @CanecasId, 'Caneca No Coffee No Code', 'Caneca de porcelana com impressão térmica.', 1, 10.00, GETDATE(), 'caneca4.jpg', 5, 12, 8, 5),
        (NEWID(), @CamisetasId, 'Camiseta Debugar Preta', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, 110.00, GETDATE(), 'camiseta4.jpg', 23, 5, 5, 5),
        (NEWID(), @CanecasId, 'Caneca Turn Coffee in Code', 'Caneca de porcelana com impressão térmica.', 1, 20.00, GETDATE(), 'caneca3.jpg', 5, 12, 8, 5),
        (NEWID(), @CamisetasId, 'Camiseta Code Life Preta', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, 90.00, GETDATE(), 'camiseta2.jpg', 3, 5, 5, 5),
        (NEWID(), @CamisetasId, 'Camiseta Software Developer', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, 100.00, GETDATE(), 'camiseta1.jpg', 8, 5, 5, 5),
        (NEWID(), @CamisetasId, 'Camiseta Code Life Cinza', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, 80.00, GETDATE(), 'camiseta3.jpg', 15, 5, 5, 5),
        (NEWID(), @CanecasId, 'Caneca Star Bugs Coffee', 'Caneca de porcelana com impressão térmica.', 1, 20.00, GETDATE(), 'caneca1.jpg', 5, 12, 8, 5),
        (NEWID(), @CanecasId, 'Caneca Programmer Code', 'Caneca de porcelana com impressão térmica.', 1, 15.00, GETDATE(), 'caneca2.jpg', 8, 12, 8, 5)


INSERT INTO Vouchers (Id, Code, [Percentage], DiscountValue, Quantity, VoucherDiscountType, RegistrationDate, DateOfUse, ExpirationDate, Active, Used)
VALUES  (NEWID(), 'PROMO-15-REAIS', NULL, 15, 0, 1, GETDATE(), null, GETDATE() + 1, 1, 0), 
        (NEWID(), 'PROMO-10-OFF', 10, null, 50, 0, GETDATE(), null, GETDATE() + 90, 1, 0)