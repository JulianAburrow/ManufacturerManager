IF NOT EXISTS (SELECT 1 FROM Category)
BEGIN
	INSERT INTO Category
		( Name )
	VALUES
		( 'Colour' ),
		( 'ColourJustification' ),
		( 'Manufacturer' ),
		( 'Widget' )
END