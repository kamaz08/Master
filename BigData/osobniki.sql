	SELECT a.o1, a.o2, a.d, a.h, b.d, b.h FROM 
	  (SELECT  a.[OsobaId] o1, b.[OsobaId] o2, a.[HotelId] h, a.[Dzien] d
		FROM [dbo].[TwoCollisions] a INNER JOIN [dbo].[TwoCollisions] b 
			ON a.[Dzien] = b.[Dzien] AND a.[HotelId] = b.[HotelId] AND a.[OsobaId] <> b.[OsobaId] AND a.[OsobaId] < b.[OsobaId]  ) AS A
	INNER JOIN 
	  (SELECT  a.[OsobaId] o1, b.[OsobaId] o2, a.[HotelId] h, a.[Dzien] d
		FROM [dbo].[TwoCollisions] a INNER JOIN [dbo].[TwoCollisions] b 
			ON a.[Dzien] = b.[Dzien] AND a.[HotelId] = b.[HotelId] AND a.[OsobaId] <> b.[OsobaId] AND a.[OsobaId] < b.[OsobaId]  )  AS B
	ON a.o1 = b.o1 AND a.o2 = b.o2 AND (a.d <> b.d  OR a.h <> b.h )
	WHERE a.d > b.d OR (a.d = b.d AND a.h > b.h)

