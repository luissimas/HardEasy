using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Trocar : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

	#region Drag and Drop

	public GameObject PanelJogador, PanelOponente;
	public bool NaoTemHardcash = false;

	public void OnPointerEnter(PointerEventData eventData)
	{

	}

	public void OnPointerExit(PointerEventData eventData)
	{

	}

	public void OnDrop(PointerEventData eventData)
	{
		if (transform.childCount == 0) //Impede que sejam arrastadas mais de uma carta para a dropzone ao mesmo tempo
		{
			if (((eventData.pointerDrag.gameObject.tag == "PlayerCard") && (Manager.JogadorPodeInteragir)) || ((eventData.pointerDrag.gameObject.tag == "OpponentCard") && (Manager.OponentePodeInteragir)))
			{
				if (Manager.PodeInteragir)
				{
					Manager.PodeInteragir = false;
					eventData.pointerDrag.gameObject.transform.SetParent(this.transform);
					GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;

					if (GetComponentInChildren<DisplayPlacaMae>() != null)
					{
						if (GetComponentInChildren<DisplayPlacaMae>().gameObject.tag == "PlayerCard")
						{
							TrocarPlacaMae(PanelJogador);
						}
						else if (GetComponentInChildren<DisplayPlacaMae>().gameObject.tag == "OpponentCard")
						{
							TrocarPlacaMae(PanelOponente);
						}
					}
					else if (GetComponentInChildren<DisplayProcessador>() != null)
					{
						if (GetComponentInChildren<DisplayProcessador>().gameObject.tag == "PlayerCard")
						{
							TrocarProcessador(PanelJogador);
						}
						else if (GetComponentInChildren<DisplayProcessador>().gameObject.tag == "OpponentCard")
						{
							TrocarProcessador(PanelOponente);
						}
					}
					else if (GetComponentInChildren<DisplayMemoria>() != null)
					{
						if (GetComponentInChildren<DisplayMemoria>().gameObject.tag == "PlayerCard")
						{
							TrocarMemoria(PanelJogador);
						}
						else if (GetComponentInChildren<DisplayMemoria>().gameObject.tag == "OpponentCard")
						{
							TrocarMemoria(PanelOponente);
						}
					}
					else if (GetComponentInChildren<DisplayPlacaDeVideo>() != null)
					{
						if (GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.tag == "PlayerCard")
						{
							TrocarPlacaDeVideo(PanelJogador);
						}
						else if (GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.tag == "OpponentCard")
						{
							TrocarPlacaDeVideo(PanelOponente);
						}
					}
					else if (GetComponentInChildren<DisplayDisco>() != null)
					{
						if (GetComponentInChildren<DisplayDisco>().gameObject.tag == "PlayerCard")
						{
							TrocarDisco(PanelJogador);
						}
						else if (GetComponentInChildren<DisplayDisco>().gameObject.tag == "OpponentCard")
						{
							TrocarDisco(PanelOponente);
						}
					}
					else if (GetComponentInChildren<DisplayFonte>() != null)
					{
						if (GetComponentInChildren<DisplayFonte>().gameObject.tag == "PlayerCard")
						{
							TrocarFonte(PanelJogador);
						}
						else if (GetComponentInChildren<DisplayFonte>().gameObject.tag == "OpponentCard")
						{
							TrocarFonte(PanelOponente);
						}
					}
					else if (GetComponentInChildren<DisplayGabinete>() != null)
					{
						if (GetComponentInChildren<DisplayGabinete>().gameObject.tag == "PlayerCard")
						{
							TrocarGabinete(PanelJogador);
						}
						else if (GetComponentInChildren<DisplayGabinete>().gameObject.tag == "OpponentCard")
						{
							TrocarGabinete(PanelOponente);
						}
					}

					Invoke("RetornarCartas", 2);

				}
			}
		}
	}

	public void RetornarCartas()
	{
		while (GetComponentInChildren<CanvasGroup>() != null)
		{
			for (int i = 0; i < GetComponentsInChildren<CanvasGroup>().Length; i++)
			{
				GetComponentsInChildren<CanvasGroup>()[i].gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;

				if (GetComponentsInChildren<CanvasGroup>()[i].gameObject.tag == "PlayerCard")
				{
					GetComponentsInChildren<CanvasGroup>()[i].gameObject.transform.SetParent(PanelJogador.transform);
				}
				else if (GetComponentsInChildren<CanvasGroup>()[i].gameObject.tag == "OpponentCard")
				{
					GetComponentsInChildren<CanvasGroup>()[i].gameObject.transform.SetParent(PanelOponente.transform);
				}
			}
		}

		if (!NaoTemHardcash)
		{
			Manager.Trocando = true;
		}
		else
		{
			Manager.PodeInteragir = true;
		}
	}

	#endregion

	#region Trocar as cartas

	//Troca a carta do tipo placa-mãe por outra carta aleatória do mesmo tipo
	public void TrocarPlacaMae(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		PlacaMae placamaeAleatoria = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)]; //Gera uma placa-mãe aleatória da lista

		//Verifica se a placa-mãe selecionada aleatoriamente é diferente da placa-mãe ativa
		if ((GetComponentInChildren<DisplayPlacaMae>().placaMae != placamaeAleatoria) && (OutroPanel.GetComponentInChildren<DisplayPlacaMae>().placaMae != placamaeAleatoria))
		{
			GetComponentInChildren<DisplayPlacaMae>().placaMae = placamaeAleatoria;

			//Informa que houve alteração na carta
			if (Panel == PanelJogador)
			{
				Manager.JogadorCartaPlacaMaeMudou = true;
				Manager.HardCashJogador--;
			}
			else if (Panel == PanelOponente)
			{
				Manager.OponenteCartaPlacaMaeMudou = true;
				Manager.HardCashOponente--;
			}
		}
		else
		{
			//Chama a função recursivamente para gerar outro componente de forma aleatoria
			Manager.PodeInteragir = true;
			TrocarPlacaMae(Panel);
		}
	}

	//Troca a carta do tipo processador por outra carta aleatória do mesmo tipo
	public void TrocarProcessador(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}

		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		Processador processadorAleatorio = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)]; //Gera um processado aleatório da lista

		//Verifica se o processador selecionado aleatoriamente é diferente do processador ativo
		if ((GetComponentInChildren<DisplayProcessador>().processador != processadorAleatorio) && (OutroPanel.GetComponentInChildren<DisplayProcessador>().processador != processadorAleatorio))
		{
			GetComponentInChildren<DisplayProcessador>().processador = processadorAleatorio;

			//VerificarProcessadorIgual(PanelBaralho, OutroPanel);

			//Informa que houve alteração na carta
			if (Panel == PanelJogador)
			{
				Manager.JogadorCartaProcessadorMudou = true;
				Manager.HardCashJogador--;
			}
			else if (Panel == PanelOponente)
			{
				Manager.OponenteCartaProcessadorMudou = true;
				Manager.HardCashOponente--;
			}
		}
		else
		{
			//Chama a função recursivamente para gerar outro componente de forma aleatoria
			Manager.PodeInteragir = true;
			TrocarProcessador(Panel);
		}
	}

	//Troca a carta do tipo memóia por outra carta aleatória do mesmo tipo
	public void TrocarMemoria(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		Memoria memoriaAleatoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)]; //Gera uma memória aleatória da lista

		//Verfica se a memória selecionada aleatoriamente é diferente da memória ativa
		if ((GetComponentInChildren<DisplayMemoria>().memoria != memoriaAleatoria) && (OutroPanel.GetComponentInChildren<DisplayMemoria>().memoria != memoriaAleatoria))
		{
			GetComponentInChildren<DisplayMemoria>().memoria = memoriaAleatoria;

			//VerificarMemoriaIgual(PanelBaralho, OutroPanel);

			//Informa que houve alteração na carta
			if (Panel == PanelJogador)
			{
				Manager.JogadorCartaMemoriaMudou = true;
				Manager.HardCashJogador--;
			}
			else if (Panel == PanelOponente)
			{
				Manager.OponenteCartaMemoriaMudou = true;
				Manager.HardCashOponente--;
			}
		}
		else
		{
			//Chama a função recursivamente para gerar outro componente de forma aleatoria
			Manager.PodeInteragir = true;
			TrocarMemoria(Panel);
		}
	}

	//Troca a carta do tipo placa de vídeo por outra carta aleatória do mesmo tipo
	public void TrocarPlacaDeVideo(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		PlacaDeVideo placadevideoAleatoria = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)]; //Gera uma placa de vídeo aleatória da lista

		//Verifica se a placa de vídeo selecionada aleatoriamente é diferente da placa de vídeo ativa
		if ((GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo != placadevideoAleatoria) && (OutroPanel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo != placadevideoAleatoria))
		{
			GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = placadevideoAleatoria;

			//Informa que houve alteração na carta
			if (Panel == PanelJogador)
			{
				Manager.JogadorCartaPlacaDeVideoMudou = true;
				Manager.HardCashJogador--;
			}
			else if (Panel == PanelOponente)
			{
				Manager.OponenteCartaPlacaDeVideoMudou = true;
				Manager.HardCashOponente--;
			}
		}
		else
		{
			//Chama a função recursivamente para gerar outro componente de forma aleatoria
			Manager.PodeInteragir = true;
			TrocarPlacaDeVideo(Panel);
		}
	}

	//Troca a carta do tipo disco por outra carta aleatória do mesmo tipo
	public void TrocarDisco(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		Disco discoAleatorio = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)]; //Gera um disco aleatório da lista

		//Verifica se o disco selecionado aleatoriamente é diferente do disco ativo
		if ((GetComponentInChildren<DisplayDisco>().disco != discoAleatorio) && (OutroPanel.GetComponentInChildren<DisplayDisco>().disco != discoAleatorio))
		{
			GetComponentInChildren<DisplayDisco>().disco = discoAleatorio;

			//Informa que houve alteração na carta
			if (Panel == PanelJogador)
			{
				Manager.JogadorCartaDiscoMudou = true;
				Manager.HardCashJogador--;
			}
			else if (Panel == PanelOponente)
			{
				Manager.OponenteCartaDiscoMudou = true;
				Manager.HardCashOponente--;
			}
		}
		else
		{
			//Chama a função recursivamente para gerar outro componente de forma aleatoria
			Manager.PodeInteragir = true;
			TrocarDisco(Panel);
		}
	}

	//Troca a carta do tipo fonte por outra carta aleatória do mesmo tipo
	public void TrocarFonte(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		Fonte fonteAleatoria = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)]; //Gera uma fonte aleatória da lista

		//Verifica se a fonte selecionada aleatoriamente é diferente da fonte ativa
		if ((GetComponentInChildren<DisplayFonte>().fonte != fonteAleatoria) && (OutroPanel.GetComponentInChildren<DisplayFonte>().fonte != fonteAleatoria))
		{
			GetComponentInChildren<DisplayFonte>().fonte = fonteAleatoria;

			//Informa que houve alteração na carta
			if (Panel == PanelJogador)
			{
				Manager.JogadorCartaFonteMudou = true;
				Manager.HardCashJogador--;
			}
			else if (Panel == PanelOponente)
			{
				Manager.OponenteCartaFonteMudou = true;
				Manager.HardCashOponente--;
			}
		}
		else
		{
			//Chama a função recursivamente para gerar outro componente de forma aleatoria
			Manager.PodeInteragir = true;
			TrocarFonte(Panel);
		}
	}

	//Troca a carta do tipo gabinete por outra carta aleatória do mesmo tipo
	public void TrocarGabinete(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
				NaoTemHardcash = true;
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		Gabinete gabineteAleatorio = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)]; //Gera um gabinete aleatório da lista

		//Verifica se o gabinete selecionado aleatoriamente é diferente do gabinete ativo
		if ((GetComponentInChildren<DisplayGabinete>().gabinete != gabineteAleatorio) && (OutroPanel.GetComponentInChildren<DisplayGabinete>().gabinete != gabineteAleatorio))
		{
			GetComponentInChildren<DisplayGabinete>().gabinete = gabineteAleatorio;

			//Informa que houve alteração na carta
			if (Panel == PanelJogador)
			{
				Manager.JogadorCartaGabineteMudou = true;
				Manager.HardCashJogador--;
			}
			else if (Panel == PanelOponente)
			{
				Manager.OponenteCartaGabineteMudou = true;
				Manager.HardCashOponente--;
			}
		}
		else
		{
			//Chama a função recursivamente para gerar outro componente de forma aleatoria
			Manager.PodeInteragir = true;
			TrocarGabinete(Panel);
		}
	}

	#endregion
}