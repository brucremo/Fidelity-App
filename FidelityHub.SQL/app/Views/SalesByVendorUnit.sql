CREATE VIEW app.SalesByVendorUnit AS
(
SELECT VU.Id AS VendorUnitId, PS.Id AS SaleId, PS.PromotionId, PS.UserId, PS.Timestamp, PS.Amount 
	FROM app.VendorUnit AS VU
	JOIN app.Promotion AS PR
	ON VU.VendorId = PR.VendorUnitId
	JOIN app.PromotionSale AS PS
	ON PR.Id = PS.PromotionId
)