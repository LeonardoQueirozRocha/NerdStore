INSERT INTO Categories 
VALUES  (NEWID(), 'Adesivos', '102'), 
        (NEWID(), 'Camisetas', '100'), 
        (NEWID(), 'Canecas', '101')

INSERT INTO Products (Id, CategoryId, Name, [Description], Active, [Value], RegistrationDate, [Image], QuantityInStock, Height, Width, Depth) 
VALUES  (NEWID(), 'b9fe90e9-00a8-4a0f-8884-a2039e1b7cd0', 'Caneca No Coffee No Code', 'Caneca de porcelana com impressão térmica.', 1, 10.00, GETDATE(), 'caneca4.jpg', 5, 12, 8, 5),
        (NEWID(), '4b6daffb-4722-40cf-80fd-18401d784432', 'Camiseta Debugar Preta', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, 110.00, GETDATE(), 'camiseta4.jpg', 23, 5, 5, 5),
        (NEWID(), 'b9fe90e9-00a8-4a0f-8884-a2039e1b7cd0', 'Caneca Turn Coffee in Code', 'Caneca de porcelana com impressão térmica.', 1, 20.00, GETDATE(), 'caneca3.jpg', 5, 12, 8, 5),
        (NEWID(), '4b6daffb-4722-40cf-80fd-18401d784432', 'Camiseta Code Life Preta', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, 90.00, GETDATE(), 'camiseta2.jpg', 3, 5, 5, 5),
        (NEWID(), '4b6daffb-4722-40cf-80fd-18401d784432', 'Camiseta Software Developer', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, 100.00, GETDATE(), 'camiseta1.jpg', 8, 5, 5, 5),
        (NEWID(), '4b6daffb-4722-40cf-80fd-18401d784432', 'Camiseta Code Life Cinza', 'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, 80.00, GETDATE(), 'camiseta3.jpg', 15, 5, 5, 5),
        (NEWID(), 'b9fe90e9-00a8-4a0f-8884-a2039e1b7cd0', 'Caneca Star Bugs Coffee', 'Caneca de porcelana com impressão térmica.', 1, 20.00, GETDATE(), 'caneca1.jpg', 5, 12, 8, 5),
        (NEWID(), 'b9fe90e9-00a8-4a0f-8884-a2039e1b7cd0', 'Caneca Programmer Code', 'Caneca de porcelana com impressão térmica.', 1, 15.00, GETDATE(), 'caneca2.jpg', 8, 12, 8, 5)


INSERT INTO Vouchers (Id, Code, [Percentage], DiscountValue, Quantity, VoucherDiscountType, RegistrationDate, DateOfUse, ExpirationDate, Active, Used)
VALUES  (NEWID(), 'PROMO-15-REAIS', NULL, 15, 0, 1, GETDATE(), null, GETDATE() + 1, 1, 0), 
        (NEWID(), 'PROMO-10-OFF', 10, null, 50, 0, GETDATE(), null, GETDATE() + 90, 1, 0)