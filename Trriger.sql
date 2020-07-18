
CREATE TRIGGER trg_asiento_audit
ON [dbo].[Asientoes]
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
	IF EXISTS(SELECT * FROM AsientosTotal WHERE CuentaContable = (SELECT I.CuentaContable FROM inserted i))
	BEGIN
		DECLARE @AMOUNT BIGINT = (SELECT MontoTotal FROM AsientosTotal WHERE CuentaContable = (SELECT I.CuentaContable FROM inserted i))
		DECLARE @AMOUNTT INT = (SELECT I.Monto FROM inserted i)
		UPDATE AsientosTotal set MontoTotal = @AMOUNT + @AMOUNTT where CuentaContable = (SELECT I.CuentaContable FROM inserted i);
	END
	ELSE
	BEGIN 
	INSERT INTO AsientosTotal(
        FechaGenerado,
		CuentaContable,
		MontoTotal
    )
    SELECT
        GETDATE(),
        I.CuentaContable,
        I.Monto
    FROM
        inserted i
	END
END