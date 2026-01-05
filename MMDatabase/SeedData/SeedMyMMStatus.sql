IF NOT EXISTS (SELECT 1 FROM MyMMStatus)
BEGIN
	INSERT INTO MyMMStatus
		( StatusName )
	VALUES
		( 'Active' ),
		( 'Inactive' ),
		( 'Pending' )
END