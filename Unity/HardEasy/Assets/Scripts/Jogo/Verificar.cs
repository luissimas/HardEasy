using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Verificar
{

	//Verifica se as cartas do jogador e do oponente são iguais
	public static void VerificarCartas(GameObject Panel, GameObject OutroPanel)
	{
		VerificarPlacaMaeIgual(Panel, OutroPanel);
		VerificarProcessadorIgual(Panel, OutroPanel);
		VerificarMemoriaIgual(Panel, OutroPanel);
		VerificarPlacaDeVideoIgual(Panel, OutroPanel);
		VerificarDiscoIgual(Panel, OutroPanel);
		VerificarFonteIgual(Panel, OutroPanel);
		VerificarGabineteIgual(Panel, OutroPanel);

		//Impede que o processador e a placa-mãe sejam compatíveis no início do jogo, evitando assim que o jogador tenha todas as cartas compatíveis logo no início do jogo
		while (Compatibilidade.ProcessadorConectaPlacaMae(Panel) || Compatibilidade.ProcessadorConectaPlacaMae(OutroPanel))
		{
			if (Compatibilidade.ProcessadorConectaPlacaMae(Panel))
			{
				Panel.GetComponentInChildren<DisplayProcessador>().processador = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)];
				VerificarProcessadorIgual(Panel, OutroPanel);
			}
			else
			{
				OutroPanel.GetComponentInChildren<DisplayProcessador>().processador = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)];
				VerificarProcessadorIgual(OutroPanel, Panel);
			}
		}
	}

	public static void VerificarPlacaMaeIgual(GameObject Panel, GameObject OutroPanel)
	{
		int Count = 0; //Contador para impedir um eventual loop infinito

		while(Panel.GetComponentInChildren<DisplayPlacaMae>().placaMae == OutroPanel.GetComponentInChildren<DisplayPlacaMae>().placaMae)
		{
			if (Count <= 20)
			{
				Panel.GetComponentInChildren<DisplayPlacaMae>().placaMae = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)];
				Count++;
			}
			else
			{
				break;
			}
		}
	}

	public static void VerificarProcessadorIgual(GameObject Panel, GameObject OutroPanel)
	{
		int Count = 0; //Contador para impedir um eventual loop infinito

		while (Panel.GetComponentInChildren<DisplayProcessador>().processador == OutroPanel.GetComponentInChildren<DisplayProcessador>().processador)
		{
			if (Count <= 20)
			{
				Panel.GetComponentInChildren<DisplayProcessador>().processador = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)];
				Count++;
			}
			else
			{
				break;
			}
		}
	}

	public static void VerificarMemoriaIgual(GameObject Panel, GameObject OutroPanel)
	{
		int Count = 0; //Contador para impedir um eventual loop infinito

		while (Panel.GetComponentInChildren<DisplayMemoria>().memoria == OutroPanel.GetComponentInChildren<DisplayMemoria>().memoria)
		{
			if (Count <= 20)
			{
				Panel.GetComponentInChildren<DisplayMemoria>().memoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)];
				Count++;
			}
			else
			{
				break;
			}
		}
	}

	public static void VerificarPlacaDeVideoIgual(GameObject Panel, GameObject OutroPanel)
	{
		int Count = 0; //Contador para impedir um eventual loop infinito

		while (Panel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo == OutroPanel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo)
		{
			if (Count <= 20)
			{
				Panel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)];
				Count++;
			}
			else
			{
				break;
			}
		}
	}

	public static void VerificarDiscoIgual(GameObject Panel, GameObject OutroPanel)
	{
		int Count = 0; //Contador para impedir um eventual loop infinito

		while (Panel.GetComponentInChildren<DisplayDisco>().disco == OutroPanel.GetComponentInChildren<DisplayDisco>().disco)
		{
			if (Count <= 20)
			{
				Panel.GetComponentInChildren<DisplayDisco>().disco = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)];
				Count++;
			}
			else
			{
				break;
			}
		}
	}

	public static void VerificarFonteIgual(GameObject Panel, GameObject OutroPanel)
	{
		int Count = 0; //Contador para impedir um eventual loop infinito

		while (Panel.GetComponentInChildren<DisplayFonte>().fonte == OutroPanel.GetComponentInChildren<DisplayFonte>().fonte)
		{
			if (Count <= 20)
			{
				Panel.GetComponentInChildren<DisplayFonte>().fonte = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)];
				Count++;
			}
			else
			{
				break;
			}
		}
	}

	public static void VerificarGabineteIgual(GameObject Panel, GameObject OutroPanel)
	{
		int Count = 0; //Contador para impedir um eventual loop infinito

		while (Panel.GetComponentInChildren<DisplayGabinete>().gabinete == OutroPanel.GetComponentInChildren<DisplayGabinete>().gabinete)
		{
			if (Count <= 20)
			{
				Panel.GetComponentInChildren<DisplayGabinete>().gabinete = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)];
				Count++;
			}
			else
			{
				break;
			}
		}
	}
}
