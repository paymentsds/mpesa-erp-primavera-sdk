USE [PRICOMPUWORKS]
GO

/****** Object:  View [dbo].[View_INV_ArtigoMargem]    Script Date: 19-12-2020 10:44:53 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[View_INV_ArtigoMargem]
as
select a.Artigo,am.Unidade, a.Descricao,Familia,SubFamilia,CDU_PrecCustoUltimo,

	a.CDU_MPVP1,am.PVP1,isnull(round((case when am.PVP1 > 0 and a.CDU_PrecCustoUltimo > 0 then (a.CDU_PrecCustoUltimo + (a.CDU_PrecCustoUltimo * a.CDU_MPVP1 / 100)) else am.PVP1 end),2),0) as PVP1_Novo, 
	a.CDU_MPVP2,am.PVP2,isnull(round((case when am.PVP2 > 0 and a.CDU_PrecCustoUltimo > 0 then  (a.CDU_PrecCustoUltimo + (a.CDU_PrecCustoUltimo * a.CDU_MPVP1 / 100)) else am.PVP2 end),2),0) as PVP2_Novo, 
	a.CDU_MPVP3,am.PVP3,isnull(round((case when am.PVP3 > 0 and a.CDU_PrecCustoUltimo > 0 then  (a.CDU_PrecCustoUltimo + (a.CDU_PrecCustoUltimo * a.CDU_MPVP1 / 100)) else am.PVP3 end),2),0) as PVP3_Novo, 
	a.CDU_MPVP4,am.PVP4,isnull(round((case when am.PVP4 > 0 and a.CDU_PrecCustoUltimo > 0 then  (a.CDU_PrecCustoUltimo + (a.CDU_PrecCustoUltimo * a.CDU_MPVP1 / 100)) else am.PVP4 end),2),0) as PVP4_Novo, 
	a.CDU_MPVP5,am.PVP5,isnull(round((case when am.PVP5 > 0 and a.CDU_PrecCustoUltimo > 0 then  (a.CDU_PrecCustoUltimo + (a.CDU_PrecCustoUltimo * a.CDU_MPVP1 / 100)) else am.PVP5 end),2),0) as PVP5_Novo, 
	a.CDU_MPVP6,am.PVP6,isnull(round((case when am.PVP6 > 0 and a.CDU_PrecCustoUltimo > 0 then  (a.CDU_PrecCustoUltimo + (a.CDU_PrecCustoUltimo * a.CDU_MPVP1 / 100)) else am.PVP6 end),2),0) as PVP6_Novo
	from ArtigoMoeda am 
inner join artigo a on a.Artigo = am.Artigo
where Moeda = 'MT'
GO


