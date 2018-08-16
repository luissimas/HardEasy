using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class IniciarCartas
{

	//Gera cartas aleatórias de todos os tipos baseado no canvas
	public static void IniciarBaralho(GameObject PanelBaralho)
	{
		PanelBaralho.GetComponentInChildren<DisplayPlacaMae>().placaMae = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)];
		PanelBaralho.GetComponentInChildren<DisplayProcessador>().processador = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)];
		PanelBaralho.GetComponentInChildren<DisplayMemoria>().memoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)];
		PanelBaralho.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)];
		PanelBaralho.GetComponentInChildren<DisplayDisco>().disco = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)];
		PanelBaralho.GetComponentInChildren<DisplayFonte>().fonte = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)];
		PanelBaralho.GetComponentInChildren<DisplayGabinete>().gabinete = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)];

		//Informa que houve alteração nas cartas
		if (PanelBaralho == Informacoes.PanelJogador)
		{
			Informacoes.JogadorCartaPlacaMaeMudou = true;
			Informacoes.JogadorCartaProcessadorMudou = true;
			Informacoes.JogadorCartaMemoriaMudou = true;
			Informacoes.JogadorCartaPlacaDeVideoMudou = true;
			Informacoes.JogadorCartaDiscoMudou = true;
			Informacoes.JogadorCartaFonteMudou = true;
			Informacoes.JogadorCartaGabineteMudou = true;
		}
		else if (PanelBaralho == Informacoes.PanelOponente)
		{
			Informacoes.OponenteCartaPlacaMaeMudou = true;
			Informacoes.OponenteCartaProcessadorMudou = true;
			Informacoes.OponenteCartaMemoriaMudou = true;
			Informacoes.OponenteCartaPlacaDeVideoMudou = true;
			Informacoes.OponenteCartaDiscoMudou = true;
			Informacoes.OponenteCartaFonteMudou = true;
			Informacoes.OponenteCartaGabineteMudou = true;
		}
	}
}
