using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Trocar : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	#region Drag and Drop

	public GameObject PanelJogador, PanelOponente;
	[HideInInspector] public bool Trocou = false;
	public int RodadasAteTrocar;

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
					Trocou = false;
					eventData.pointerDrag.gameObject.transform.SetParent(this.transform);
					GetComponentInChildren<CanvasGroup>().blocksRaycasts = false;

					if (GetComponentInChildren<DisplayPlacaMae>() != null)
					{
						if ((((RodadaPlacaMaeJogador > 0) && (((Manager.EstadoAtual == Manager.Estados.VezDoJogador) && ((Manager.Rodada - RodadaPlacaMaeJogador) < RodadasAteTrocar)))) ||
							((RodadaPlacaMaeOponente > 0) && ((Manager.EstadoAtual == Manager.Estados.VezDoOponente) && ((Manager.Rodada - RodadaPlacaMaeOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
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
					}
					else if (GetComponentInChildren<DisplayProcessador>() != null)
					{
						if ((((RodadaProcessadorJogador > 0) && (((Manager.EstadoAtual == Manager.Estados.VezDoJogador) && ((Manager.Rodada - RodadaProcessadorJogador) < RodadasAteTrocar)))) ||
							((RodadaProcessadorOponente > 0) && ((Manager.EstadoAtual == Manager.Estados.VezDoOponente) && ((Manager.Rodada - RodadaProcessadorOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
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
					}
					else if (GetComponentInChildren<DisplayMemoria>() != null)
					{
						if ((((RodadaMemoriaJogador > 0) && (((Manager.EstadoAtual == Manager.Estados.VezDoJogador) && ((Manager.Rodada - RodadaMemoriaJogador) < RodadasAteTrocar)))) ||
							((RodadaMemoriaOponente > 0) && ((Manager.EstadoAtual == Manager.Estados.VezDoOponente) && ((Manager.Rodada - RodadaMemoriaOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
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
					}
					else if (GetComponentInChildren<DisplayPlacaDeVideo>() != null)
					{
						if ((((RodadaPlacaDeVideoJogador > 0) && (((Manager.EstadoAtual == Manager.Estados.VezDoJogador) && ((Manager.Rodada - RodadaPlacaDeVideoJogador) < RodadasAteTrocar)))) ||
							((RodadaPlacaDeVideoOponente > 0) && ((Manager.EstadoAtual == Manager.Estados.VezDoOponente) && ((Manager.Rodada - RodadaPlacaDeVideoOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
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
					}
					else if (GetComponentInChildren<DisplayDisco>() != null)
					{
						if ((((RodadaDiscoJogador > 0) && (((Manager.EstadoAtual == Manager.Estados.VezDoJogador) && ((Manager.Rodada - RodadaDiscoJogador) < RodadasAteTrocar)))) ||
							((RodadaDiscoOponente > 0) && ((Manager.EstadoAtual == Manager.Estados.VezDoOponente) && ((Manager.Rodada - RodadaDiscoOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
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
					}
					else if (GetComponentInChildren<DisplayFonte>() != null)
					{
						if ((((RodadaFonteJogador > 0) && (((Manager.EstadoAtual == Manager.Estados.VezDoJogador) && ((Manager.Rodada - RodadaFonteJogador) < RodadasAteTrocar)))) ||
							((RodadaFonteOponente > 0) && ((Manager.EstadoAtual == Manager.Estados.VezDoOponente) && ((Manager.Rodada - RodadaFonteOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
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
					}
					else if (GetComponentInChildren<DisplayGabinete>() != null)
					{
						if ((((RodadaGabineteJogador > 0) && (((Manager.EstadoAtual == Manager.Estados.VezDoJogador) && ((Manager.Rodada - RodadaGabineteJogador) < RodadasAteTrocar)))) ||
							((RodadaGabineteOponente > 0) && ((Manager.EstadoAtual == Manager.Estados.VezDoOponente) && ((Manager.Rodada - RodadaGabineteOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
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

		if (Trocou)
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

	[HideInInspector] public int RodadaPlacaMaeJogador = 0, RodadaProcessadorJogador = 0, RodadaMemoriaJogador = 0, RodadaPlacaDeVideoJogador = 0, RodadaDiscoJogador = 0, RodadaFonteJogador = 0, RodadaGabineteJogador = 0; //Variáveis para armazenar a última rodada em que o jogador trocou determinada carta
	[HideInInspector] public int RodadaPlacaMaeOponente = 0, RodadaProcessadorOponente = 0, RodadaMemoriaOponente = 0, RodadaPlacaDeVideoOponente = 0, RodadaDiscoOponente = 0, RodadaFonteOponente = 0, RodadaGabineteOponente = 0; //Variáveis para armazenar a útlima rodada em que o oponente trocou determinada carta

	//Troca a carta do tipo placa-mãe por outra carta aleatória do mesmo tipo
	public void TrocarPlacaMae(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
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

		if (Manager.EstadoAtual == Manager.Estados.VezDoJogador)
		{
			RodadaPlacaMaeJogador = Manager.Rodada;
		}
		else if (Manager.EstadoAtual == Manager.Estados.VezDoOponente)
		{
			RodadaPlacaMaeOponente = Manager.Rodada;
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

		Trocou = true;
	}

	//Troca a carta do tipo processador por outra carta aleatória do mesmo tipo
	public void TrocarProcessador(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
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

		if (Manager.EstadoAtual == Manager.Estados.VezDoJogador)
		{
			RodadaProcessadorJogador = Manager.Rodada;
		}
		else if (Manager.EstadoAtual == Manager.Estados.VezDoOponente)
		{
			RodadaProcessadorOponente = Manager.Rodada;
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

		Trocou = true;
	}

	//Troca a carta do tipo memóia por outra carta aleatória do mesmo tipo
	public void TrocarMemoria(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
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

		if (Manager.EstadoAtual == Manager.Estados.VezDoJogador)
		{
			RodadaMemoriaJogador = Manager.Rodada;
		}
		else if (Manager.EstadoAtual == Manager.Estados.VezDoOponente)
		{
			RodadaMemoriaOponente = Manager.Rodada;
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

		Trocou = true;
	}

	//Troca a carta do tipo placa de vídeo por outra carta aleatória do mesmo tipo
	public void TrocarPlacaDeVideo(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
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

		if (Manager.EstadoAtual == Manager.Estados.VezDoJogador)
		{
			RodadaPlacaDeVideoJogador = Manager.Rodada;
		}
		else if (Manager.EstadoAtual == Manager.Estados.VezDoOponente)
		{
			RodadaPlacaDeVideoOponente = Manager.Rodada;
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

		Trocou = true;
	}

	//Troca a carta do tipo disco por outra carta aleatória do mesmo tipo
	public void TrocarDisco(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
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

		if (Manager.EstadoAtual == Manager.Estados.VezDoJogador)
		{
			RodadaDiscoJogador = Manager.Rodada;
		}
		else if (Manager.EstadoAtual == Manager.Estados.VezDoOponente)
		{
			RodadaDiscoOponente = Manager.Rodada;
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

		Trocou = true;
	}

	//Troca a carta do tipo fonte por outra carta aleatória do mesmo tipo
	public void TrocarFonte(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
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

		if (Manager.EstadoAtual == Manager.Estados.VezDoJogador)
		{
			RodadaFonteJogador = Manager.Rodada;
		}
		else if (Manager.EstadoAtual == Manager.Estados.VezDoOponente)
		{
			RodadaFonteOponente = Manager.Rodada;
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

		Trocou = true;
	}

	//Troca a carta do tipo gabinete por outra carta aleatória do mesmo tipo
	public void TrocarGabinete(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (Manager.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (Manager.HardCashOponente == 0)
			{
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

		if (Manager.EstadoAtual == Manager.Estados.VezDoJogador)
		{
			RodadaGabineteJogador = Manager.Rodada;
		}
		else if (Manager.EstadoAtual == Manager.Estados.VezDoOponente)
		{
			RodadaGabineteOponente = Manager.Rodada;
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

		Trocou = true;
	}

	#endregion
}