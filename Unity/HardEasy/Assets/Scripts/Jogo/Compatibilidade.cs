using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compatibilidade : MonoBehaviour
{

	#region Compatibilidade

	//Testa a compatibilidade entre o processador e a placa-mãe baseado no socket
	public static bool ProcessadorConectaPlacaMae(GameObject Panel)
	{
		//Verifica se os objetos estão ativos, se não estiverem não haverá como comparar
		if ((Panel.GetComponentInChildren<DisplayProcessador>() != null) && (Panel.GetComponentInChildren<DisplayPlacaMae>() != null))
		{
			Processador processador = Panel.GetComponentInChildren<DisplayProcessador>().processador;
			PlacaMae placamae = Panel.GetComponentInChildren<DisplayPlacaMae>().placaMae;

			//Compara o socket do processador ativo com o da placa-mãe ativa
			if (processador.Socket.Trim().ToLowerInvariant() == placamae.Socket.Trim().ToLowerInvariant())
			{
				return (true);
			}
			else
			{
				return (false);
			}
		}
		else
		{
			Debug.Log("Não há elementos suficientes para determinar a compatibilidade entre o processador e a placa-mãe");
			return (false);
		}
	}

	//Testa a compatibilidade entre a memória e a placa-mãe baseado no DDR e na quantidade de memória
	public static bool MemoriaConectaPlacaMae(GameObject Panel)
	{
		//Verifica se os objetos estão ativos, se não estiverem não haverá como comparar
		if ((Panel.GetComponentInChildren<DisplayMemoria>() != null) && (Panel.GetComponentInChildren<DisplayPlacaMae>() != null))
		{
			Memoria memoria = Panel.GetComponentInChildren<DisplayMemoria>().memoria;
			PlacaMae placamae = Panel.GetComponentInChildren<DisplayPlacaMae>().placaMae;

			//Compara o DDR da memória ativa com o DDR da placa-mãe ativa
			if (memoria.DDR.Trim().ToLowerInvariant() == placamae.DDR.Trim().ToLowerInvariant())
			{
				//Compara a quantidade de memória suportada pela placa-mãe ativa com a quantidade de memória da memória ativa
				if (placamae.QuantidadeMemoria >= memoria.QuantidadeMemoria)
				{
					return (true);
				}
				else
				{
					return (false);
				}
			}
			else
			{
				return (false);
			}
		}
		else
		{
			Debug.Log("Não há elementos suficientes para determinar a compatibilidade entre a memória e a placa-mãe");
			return (false);
		}
	}

	//Testa a compatibilidade entre o gabinete e a placa-mãe baseado no formato
	public static bool GabineteConectaPlacaMae(GameObject Panel)
	{
		//Verifica se os objetos estão ativos, se não estiverem não haverá como comparar
		if ((Panel.GetComponentInChildren<DisplayGabinete>() != null) && (Panel.GetComponentInChildren<DisplayPlacaMae>() != null))
		{
			Gabinete gabinete = Panel.GetComponentInChildren<DisplayGabinete>().gabinete;
			PlacaMae placamae = Panel.GetComponentInChildren<DisplayPlacaMae>().placaMae;

			//Verifica se o formato da placa-mãe está entre os fomatos suportados pelo gabinete
			if (gabinete.FormatoPlacaMae.Trim().ToLowerInvariant().Contains(placamae.Formato.Trim().ToLowerInvariant()))
			{
				return (true);
			}
			else
			{
				return (false);
			}
		}
		else
		{
			Debug.Log("Não há elementos suficientes para determinar a compatibilidade entre o gabinete e a placa-mãe");
			return (false);
		}
	}

	//Testa a compatibilidade da placa de vídeo com a placa-mãe baseado no SLI/Crossfire
	public static bool PlacaDeVideoConectaPlacaMae(GameObject Panel)
	{
		//Verifica se os objetos estão ativos, se não estiverem não haverá como comparar
		if ((Panel.GetComponentInChildren<DisplayPlacaDeVideo>() != null) && (Panel.GetComponentInChildren<DisplayPlacaMae>() != null))
		{
			PlacaDeVideo placadevideo = Panel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo;
			PlacaMae placamae = Panel.GetComponentInChildren<DisplayPlacaMae>().placaMae;

			//Compara o suporte de SLI/Crossfire da placa-mãe com o SLI/Crossfire da placa de vídeo
			if (((placamae.SuporteSLI == placadevideo.SLI) || ((placamae.SuporteSLI == true) && (placadevideo.SLI == false))) &&
				((placamae.SuporteCrossfire == placadevideo.Crossfire) || ((placamae.SuporteCrossfire == true) && (placadevideo.Crossfire == false))))
			{
				return (true);
			}
			else
			{
				return (false);
			}
		}
		else
		{
			Debug.Log("Não há elementos suficientes para determinar a compatibilidade entre a placa de vídeo e a placa-mãe");
			return (false);
		}
	}

	//Testa a compatibilidade da fonte com a placa de vídeo baseado na fonte mínima recomendada da placa e na capacidade da fonte
	public static bool FonteConectaPlacaDeVideo(GameObject Panel)
	{
		//Verifica se os objetos estão ativos, se não estiverem não haverá como comparar
		if ((Panel.GetComponentInChildren<DisplayFonte>() != null) && (Panel.GetComponentInChildren<DisplayPlacaDeVideo>() != null))
		{
			Fonte fonte = Panel.GetComponentInChildren<DisplayFonte>().fonte;
			PlacaDeVideo placadevideo = Panel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo;

			//Compara a capacidade da fonte com a fonte mínima recomendada da placa de vídeo
			if (fonte.Capacidade >= placadevideo.FonteMinima)
			{
				return (true);
			}
			else
			{
				return (false);
			}
		}
		else
		{
			Debug.Log("Não há elementos suficientes para determinar a compatibilidade entre a fonte e a placa de vídeo");
			return (false);
		}
	}

	#endregion

}
