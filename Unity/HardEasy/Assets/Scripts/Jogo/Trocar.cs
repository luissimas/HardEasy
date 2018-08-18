using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Trocar : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	#region Drag and Drop

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
						if ((((RodadaPlacaMaeJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaPlacaMaeJogador) < RodadasAteTrocar)))) ||
							((RodadaPlacaMaeOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaPlacaMaeOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayPlacaMae>().gameObject.tag == "PlayerCard")
							{
								TrocarPlacaMae(Informacoes.PanelJogador);
							}
							else if (GetComponentInChildren<DisplayPlacaMae>().gameObject.tag == "OpponentCard")
							{
								TrocarPlacaMae(Informacoes.PanelOponente);
							}
						}
					}
					else if (GetComponentInChildren<DisplayProcessador>() != null)
					{
						if ((((RodadaProcessadorJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaProcessadorJogador) < RodadasAteTrocar)))) ||
							((RodadaProcessadorOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaProcessadorOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayProcessador>().gameObject.tag == "PlayerCard")
							{
								TrocarProcessador(Informacoes.PanelJogador);
							}
							else if (GetComponentInChildren<DisplayProcessador>().gameObject.tag == "OpponentCard")
							{
								TrocarProcessador(Informacoes.PanelOponente);
							}
						}
					}
					else if (GetComponentInChildren<DisplayMemoria>() != null)
					{
						if ((((RodadaMemoriaJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaMemoriaJogador) < RodadasAteTrocar)))) ||
							((RodadaMemoriaOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaMemoriaOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayMemoria>().gameObject.tag == "PlayerCard")
							{
								TrocarMemoria(Informacoes.PanelJogador);
							}
							else if (GetComponentInChildren<DisplayMemoria>().gameObject.tag == "OpponentCard")
							{
								TrocarMemoria(Informacoes.PanelOponente);
							}
						}
					}
					else if (GetComponentInChildren<DisplayPlacaDeVideo>() != null)
					{
						if ((((RodadaPlacaDeVideoJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaPlacaDeVideoJogador) < RodadasAteTrocar)))) ||
							((RodadaPlacaDeVideoOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaPlacaDeVideoOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.tag == "PlayerCard")
							{
								TrocarPlacaDeVideo(Informacoes.PanelJogador);
							}
							else if (GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.tag == "OpponentCard")
							{
								TrocarPlacaDeVideo(Informacoes.PanelOponente);
							}
						}
					}
					else if (GetComponentInChildren<DisplayDisco>() != null)
					{
						if ((((RodadaDiscoJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaDiscoJogador) < RodadasAteTrocar)))) ||
							((RodadaDiscoOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaDiscoOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayDisco>().gameObject.tag == "PlayerCard")
							{
								TrocarDisco(Informacoes.PanelJogador);
							}
							else if (GetComponentInChildren<DisplayDisco>().gameObject.tag == "OpponentCard")
							{
								TrocarDisco(Informacoes.PanelOponente);
							}
						}
					}
					else if (GetComponentInChildren<DisplayFonte>() != null)
					{
						if ((((RodadaFonteJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaFonteJogador) < RodadasAteTrocar)))) ||
							((RodadaFonteOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaFonteOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayFonte>().gameObject.tag == "PlayerCard")
							{
								TrocarFonte(Informacoes.PanelJogador);
							}
							else if (GetComponentInChildren<DisplayFonte>().gameObject.tag == "OpponentCard")
							{
								TrocarFonte(Informacoes.PanelOponente);
							}
						}
					}
					else if (GetComponentInChildren<DisplayGabinete>() != null)
					{
						if ((((RodadaGabineteJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaGabineteJogador) < RodadasAteTrocar)))) ||
							((RodadaGabineteOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaGabineteOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayGabinete>().gameObject.tag == "PlayerCard")
							{
								TrocarGabinete(Informacoes.PanelJogador);
							}
							else if (GetComponentInChildren<DisplayGabinete>().gameObject.tag == "OpponentCard")
							{
								TrocarGabinete(Informacoes.PanelOponente);
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
					GetComponentsInChildren<CanvasGroup>()[i].gameObject.transform.SetParent(Informacoes.PanelJogador.transform);
				}
				else if (GetComponentsInChildren<CanvasGroup>()[i].gameObject.tag == "OpponentCard")
				{
					GetComponentsInChildren<CanvasGroup>()[i].gameObject.transform.SetParent(Informacoes.PanelOponente.transform);
				}
			}
		}

		if (Trocou)
		{
			StateMachine.Trocando = true;
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
			if (HardCash.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (HardCash.HardCashOponente == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = Informacoes.PanelOponente;
		}
		else
		{
			OutroPanel = Informacoes.PanelJogador;
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaPlacaMaeJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaPlacaMaeOponente = StateMachine.Rodada;
		}

		PlacaMae placamaeAleatoria = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)]; //Gera uma placa-mãe aleatória da lista

		while ((placamaeAleatoria == GetComponentInChildren<DisplayPlacaMae>().placaMae) || (placamaeAleatoria == OutroPanel.GetComponentInChildren<DisplayPlacaMae>().placaMae))
		{
			placamaeAleatoria = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)];
		}

		GetComponentInChildren<DisplayPlacaMae>().placaMae = placamaeAleatoria;

		//Informa que houve alteração na carta
		if (Panel == Informacoes.PanelJogador)
		{
			Informacoes.JogadorCartaPlacaMaeMudou = true;
			HardCash.HardCashJogador--;
		}
		else if (Panel == Informacoes.PanelOponente)
		{
			Informacoes.OponenteCartaPlacaMaeMudou = true;
			HardCash.HardCashOponente--;
		}

		Trocou = true;
	}

	//Troca a carta do tipo processador por outra carta aleatória do mesmo tipo
	public void TrocarProcessador(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (HardCash.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (HardCash.HardCashOponente == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}

		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = Informacoes.PanelOponente;
		}
		else
		{
			OutroPanel = Informacoes.PanelJogador;
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaProcessadorJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaProcessadorOponente = StateMachine.Rodada;
		}

		Processador processadorAleatorio = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)]; //Gera um processado aleatório da lista

		while ((processadorAleatorio == GetComponentInChildren<DisplayProcessador>().processador) || (processadorAleatorio == OutroPanel.GetComponentInChildren<DisplayProcessador>().processador))
		{
			processadorAleatorio = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)];
		}
		GetComponentInChildren<DisplayProcessador>().processador = processadorAleatorio;

		//Informa que houve alteração na carta
		if (Panel == Informacoes.PanelJogador)
		{
			Informacoes.JogadorCartaProcessadorMudou = true;
			HardCash.HardCashJogador--;
		}
		else if (Panel == Informacoes.PanelOponente)
		{
			Informacoes.OponenteCartaProcessadorMudou = true;
			HardCash.HardCashOponente--;
		}

		Trocou = true;
	}

	//Troca a carta do tipo memóia por outra carta aleatória do mesmo tipo
	public void TrocarMemoria(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (HardCash.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (HardCash.HardCashOponente == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = Informacoes.PanelOponente;
		}
		else
		{
			OutroPanel = Informacoes.PanelJogador;
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaMemoriaJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaMemoriaOponente = StateMachine.Rodada;
		}

		Memoria memoriaAleatoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)]; //Gera uma memória aleatória da lista

		while ((memoriaAleatoria == GetComponentInChildren<DisplayMemoria>().memoria) || (memoriaAleatoria == OutroPanel.GetComponentInChildren<DisplayMemoria>().memoria))
		{
			memoriaAleatoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)];
		}

		GetComponentInChildren<DisplayMemoria>().memoria = memoriaAleatoria;

		//Informa que houve alteração na carta
		if (Panel == Informacoes.PanelJogador)
		{
			Informacoes.JogadorCartaMemoriaMudou = true;
			HardCash.HardCashJogador--;
		}
		else if (Panel == Informacoes.PanelOponente)
		{
			Informacoes.OponenteCartaMemoriaMudou = true;
			HardCash.HardCashOponente--;
		}

		Trocou = true;
	}

	//Troca a carta do tipo placa de vídeo por outra carta aleatória do mesmo tipo
	public void TrocarPlacaDeVideo(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (HardCash.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (HardCash.HardCashOponente == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = Informacoes.PanelOponente;
		}
		else
		{
			OutroPanel = Informacoes.PanelJogador;
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaPlacaDeVideoJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaPlacaDeVideoOponente = StateMachine.Rodada;
		}

		PlacaDeVideo placadevideoAleatoria = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)]; //Gera uma placa de vídeo aleatória da lista

		while ((placadevideoAleatoria == GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo) || (placadevideoAleatoria == OutroPanel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo))
		{
			placadevideoAleatoria = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)];
		}

		GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = placadevideoAleatoria;

		//Informa que houve alteração na carta
		if (Panel == Informacoes.PanelJogador)
		{
			Informacoes.JogadorCartaPlacaDeVideoMudou = true;
			HardCash.HardCashJogador--;
		}
		else if (Panel == Informacoes.PanelOponente)
		{
			Informacoes.OponenteCartaPlacaDeVideoMudou = true;
			HardCash.HardCashOponente--;
		}

		Trocou = true;
	}

	//Troca a carta do tipo disco por outra carta aleatória do mesmo tipo
	public void TrocarDisco(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (HardCash.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (HardCash.HardCashOponente == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = Informacoes.PanelOponente;
		}
		else
		{
			OutroPanel = Informacoes.PanelJogador;
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaDiscoJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaDiscoOponente = StateMachine.Rodada;
		}

		Disco discoAleatorio = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)]; //Gera um disco aleatório da lista

		while ((discoAleatorio == GetComponentInChildren<DisplayDisco>().disco) || (discoAleatorio == OutroPanel.GetComponentInChildren<DisplayDisco>().disco))
		{
			discoAleatorio = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)];
		}

		GetComponentInChildren<DisplayDisco>().disco = discoAleatorio;

		//Informa que houve alteração na carta
		if (Panel == Informacoes.PanelJogador)
		{
			Informacoes.JogadorCartaDiscoMudou = true;
			HardCash.HardCashJogador--;
		}
		else if (Panel == Informacoes.PanelOponente)
		{
			Informacoes.OponenteCartaDiscoMudou = true;
			HardCash.HardCashOponente--;
		}

		Trocou = true;
	}

	//Troca a carta do tipo fonte por outra carta aleatória do mesmo tipo
	public void TrocarFonte(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (HardCash.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (HardCash.HardCashOponente == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = Informacoes.PanelOponente;
		}
		else
		{
			OutroPanel = Informacoes.PanelJogador;
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaFonteJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaFonteOponente = StateMachine.Rodada;
		}

		Fonte fonteAleatoria = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)]; //Gera uma fonte aleatória da lista

		while ((fonteAleatoria == GetComponentInChildren<DisplayFonte>().fonte) || (fonteAleatoria == OutroPanel.GetComponentInChildren<DisplayFonte>().fonte))
		{
			fonteAleatoria = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)];
		}

		GetComponentInChildren<DisplayFonte>().fonte = fonteAleatoria;

		//Informa que houve alteração na carta
		if (Panel == Informacoes.PanelJogador)
		{
			Informacoes.JogadorCartaFonteMudou = true;
			HardCash.HardCashJogador--;
		}
		else if (Panel == Informacoes.PanelOponente)
		{
			Informacoes.OponenteCartaFonteMudou = true;
			HardCash.HardCashOponente--;
		}

		Trocou = true;
	}

	//Troca a carta do tipo gabinete por outra carta aleatória do mesmo tipo
	public void TrocarGabinete(GameObject Panel)
	{
		if (Panel.gameObject.tag == "PlayerCard")
		{
			if (HardCash.HardCashJogador == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}
		else if (Panel.gameObject.tag == "OpponentCard")
		{
			if (HardCash.HardCashOponente == 0)
			{
				Debug.Log("HardCash insuficiente!");
				return;
			}
		}

		Manager.PodeInteragir = false;

		GameObject OutroPanel;

		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = Informacoes.PanelOponente;
		}
		else
		{
			OutroPanel = Informacoes.PanelJogador;
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaGabineteJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaGabineteOponente = StateMachine.Rodada;
		}

		Gabinete gabineteAleatorio = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)]; //Gera um gabinete aleatório da lista

		while ((gabineteAleatorio == GetComponentInChildren<DisplayGabinete>().gabinete) || (gabineteAleatorio == OutroPanel.GetComponentInChildren<DisplayGabinete>().gabinete))
		{
			gabineteAleatorio = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)];
		}

		GetComponentInChildren<DisplayGabinete>().gabinete = gabineteAleatorio;

		//Informa que houve alteração na carta
		if (Panel == Informacoes.PanelJogador)
		{
			Informacoes.JogadorCartaGabineteMudou = true;
			HardCash.HardCashJogador--;
		}
		else if (Panel == Informacoes.PanelOponente)
		{
			Informacoes.OponenteCartaGabineteMudou = true;
			HardCash.HardCashOponente--;
		}

		Trocou = true;
	}

	#endregion
}