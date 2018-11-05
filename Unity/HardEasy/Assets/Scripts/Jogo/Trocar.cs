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
			if (((eventData.pointerDrag.gameObject.tag == "PlayerCard") && (Manager.JogadorPodeInteragir)) || ((eventData.pointerDrag.gameObject.tag == "OpponentCard") && (Manager.OponentePodeInteragir))) //Verifica se era a vez do jogador e se ele realmente poderia interagir
			{
				if (Manager.PodeInteragir) //Verifica se o jogo está em um estado em que é possível interagir
				{
					Manager.PodeInteragir = false; //Impede que haja qualquer outra interação com o jogo enquanto a carta estiver sendo trocada
					Trocou = false;
					eventData.pointerDrag.gameObject.transform.SetParent(this.transform); //Coloca a carta arrastada abaixo do painel na hierarquia
					GetComponentInChildren<CanvasGroup>().blocksRaycasts = false; //Bloqueia os raycasts para impedir a interação com a carta

					if (GetComponentInChildren<DisplayPlacaMae>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaPlacaMaeJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaPlacaMaeJogador) < RodadasAteTrocar)))) ||
							((RodadaPlacaMaeOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaPlacaMaeOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							//Troca a carta se baseado na tag do objeto
							if (GetComponentInChildren<DisplayPlacaMae>().gameObject.tag == "PlayerCard")
							{
								StartCoroutine(ComecarTroca("PlacaMae", 0.5f, Informacoes.PanelJogador));
							}
							else if (GetComponentInChildren<DisplayPlacaMae>().gameObject.tag == "OpponentCard")
							{
								StartCoroutine(ComecarTroca("PlacaMae", 0.5f, Informacoes.PanelOponente));
							}
						}
					}
					else if (GetComponentInChildren<DisplayProcessador>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaProcessadorJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaProcessadorJogador) < RodadasAteTrocar)))) ||
							((RodadaProcessadorOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaProcessadorOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							//Troca a carta se baseado na tag do objeto
							if (GetComponentInChildren<DisplayProcessador>().gameObject.tag == "PlayerCard")
							{
								StartCoroutine(ComecarTroca("Processador", 0.5f, Informacoes.PanelJogador));
							}
							else if (GetComponentInChildren<DisplayProcessador>().gameObject.tag == "OpponentCard")
							{
								StartCoroutine(ComecarTroca("Processador", 0.5f, Informacoes.PanelOponente));
							}
						}
					}
					else if (GetComponentInChildren<DisplayMemoria>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaMemoriaJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaMemoriaJogador) < RodadasAteTrocar)))) ||
							((RodadaMemoriaOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaMemoriaOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							//Troca a carta se baseado na tag do objeto
							if (GetComponentInChildren<DisplayMemoria>().gameObject.tag == "PlayerCard")
							{
								StartCoroutine(ComecarTroca("Memoria", 0.5f, Informacoes.PanelJogador));
							}
							else if (GetComponentInChildren<DisplayMemoria>().gameObject.tag == "OpponentCard")
							{
								StartCoroutine(ComecarTroca("Memoria", 0.5f, Informacoes.PanelOponente));
							}
						}
					}
					else if (GetComponentInChildren<DisplayPlacaDeVideo>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaPlacaDeVideoJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaPlacaDeVideoJogador) < RodadasAteTrocar)))) ||
							((RodadaPlacaDeVideoOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaPlacaDeVideoOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							//Troca a carta se baseado na tag do objeto
							if (GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.tag == "PlayerCard")
							{
								StartCoroutine(ComecarTroca("PlacaDeVideo", 0.5f, Informacoes.PanelJogador));
							}
							else if (GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.tag == "OpponentCard")
							{
								StartCoroutine(ComecarTroca("PlacaDeVideo", 0.5f, Informacoes.PanelOponente));
							}
						}
					}
					else if (GetComponentInChildren<DisplayDisco>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaDiscoJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaDiscoJogador) < RodadasAteTrocar)))) ||
							((RodadaDiscoOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaDiscoOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							//Troca a carta se baseado na tag do objeto
							if (GetComponentInChildren<DisplayDisco>().gameObject.tag == "PlayerCard")
							{
								StartCoroutine(ComecarTroca("Disco", 0.5f, Informacoes.PanelJogador));
							}
							else if (GetComponentInChildren<DisplayDisco>().gameObject.tag == "OpponentCard")
							{
								StartCoroutine(ComecarTroca("Disco", 0.5f, Informacoes.PanelOponente));
							}
						}
					}
					else if (GetComponentInChildren<DisplayFonte>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaFonteJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaFonteJogador) < RodadasAteTrocar)))) ||
							((RodadaFonteOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaFonteOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							//Troca a carta se baseado na tag do objeto
							if (GetComponentInChildren<DisplayFonte>().gameObject.tag == "PlayerCard")
							{
								StartCoroutine(ComecarTroca("Fonte", 0.5f, Informacoes.PanelJogador));
							}
							else if (GetComponentInChildren<DisplayFonte>().gameObject.tag == "OpponentCard")
							{
								StartCoroutine(ComecarTroca("Fonte", 0.5f, Informacoes.PanelOponente));
							}
						}
					}
					else if (GetComponentInChildren<DisplayGabinete>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaGabineteJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaGabineteJogador) < RodadasAteTrocar)))) ||
							((RodadaGabineteOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaGabineteOponente) < RodadasAteTrocar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteTrocar + " rodadas até poder trocar a carta novamente");
						}
						else
						{
							//Troca a carta se baseado na tag do objeto
							if (GetComponentInChildren<DisplayGabinete>().gameObject.tag == "PlayerCard")
							{
								StartCoroutine(ComecarTroca("Gabinete", 0.5f, Informacoes.PanelJogador));
							}
							else if (GetComponentInChildren<DisplayGabinete>().gameObject.tag == "OpponentCard")
							{
								StartCoroutine(ComecarTroca("Gabinete", 0.5f, Informacoes.PanelOponente));
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

				//Retorna as cartas para o panel de origem baseado na tag do objeto
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

	public static int RodadaPlacaMaeJogador = 0, RodadaProcessadorJogador = 0, RodadaMemoriaJogador = 0, RodadaPlacaDeVideoJogador = 0, RodadaDiscoJogador = 0, RodadaFonteJogador = 0, RodadaGabineteJogador = 0; //Variáveis para armazenar a última rodada em que o jogador trocou determinada carta
	public static int RodadaPlacaMaeOponente = 0, RodadaProcessadorOponente = 0, RodadaMemoriaOponente = 0, RodadaPlacaDeVideoOponente = 0, RodadaDiscoOponente = 0, RodadaFonteOponente = 0, RodadaGabineteOponente = 0; //Variáveis para armazenar a útlima rodada em que o oponente trocou determinada carta

	IEnumerator ComecarTroca(string componente, float delayTime, GameObject Panel)
	{
		yield return new WaitForSeconds(delayTime);
		TrocarCartas(componente, Panel);
	}
	
	public void TrocarCartas(string componente, GameObject Panel)
	{
		//Verifica se o jogador que jogou a carta tem HardCash suficiente para trocar
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

		//Encontra o panel do jogador adversário se baseando no panel do jogador que jogou a carta
		if (Panel.gameObject.tag == "PlayerCard")
		{
			OutroPanel = Informacoes.PanelOponente;
		}
		else
		{
			OutroPanel = Informacoes.PanelJogador;
		}

		//Armazena a rodada que o jogador jogou a carta baseado no parâmetro recebido pela função
		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			switch (componente)
			{
				case "PlacaMae":
					RodadaPlacaMaeJogador = StateMachine.Rodada;

					PlacaMae placamaeAleatoria = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)]; //Gera uma placa-mãe aleatória da lista

					//Verifica se a carta é igual a carta que o jogador já tinha ou a carta do jogador adversário
					while ((placamaeAleatoria == GetComponentInChildren<DisplayPlacaMae>().placaMae) || (placamaeAleatoria == OutroPanel.GetComponentInChildren<DisplayPlacaMae>().placaMae))
					{
						placamaeAleatoria = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)];
					}

					GetComponentInChildren<DisplayPlacaMae>().placaMae = placamaeAleatoria;

					//Informa que houve alteração na carta
					Informacoes.JogadorCartaPlacaMaeMudou = true;
					HardCash.HardCashJogador--;

					break;

				case "Processador":
					RodadaProcessadorJogador = StateMachine.Rodada;

					Processador processadorAleatorio = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)]; //Gera um processador aleatório da lista

					while ((processadorAleatorio == GetComponentInChildren<DisplayProcessador>().processador) || (processadorAleatorio == OutroPanel.GetComponentInChildren<DisplayProcessador>().processador))
					{
						processadorAleatorio = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)];
					}

					GetComponentInChildren<DisplayProcessador>().processador = processadorAleatorio;

					//Informa que houve alteração na carta
					Informacoes.JogadorCartaProcessadorMudou = true;
					HardCash.HardCashJogador--;

					break;

				case "Memoria":
					RodadaMemoriaJogador = StateMachine.Rodada;

					Memoria memoriaAleatoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)]; //Gera uma memória aleatória da lista

					while ((memoriaAleatoria == GetComponentInChildren<DisplayMemoria>().memoria) || (memoriaAleatoria == OutroPanel.GetComponentInChildren<DisplayMemoria>().memoria))
					{
						memoriaAleatoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)];
					}

					GetComponentInChildren<DisplayMemoria>().memoria = memoriaAleatoria;

					//Informa que houve alteração na carta
					Informacoes.JogadorCartaMemoriaMudou = true;
					HardCash.HardCashJogador--;

					break;

				case "PlacaDeVideo":
					RodadaPlacaDeVideoJogador = StateMachine.Rodada;

					PlacaDeVideo placadevideoAleatoria = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)]; //Gera uma placa de vídeo aleatória da lista

					while ((placadevideoAleatoria == GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo) || (placadevideoAleatoria == OutroPanel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo))
					{
						placadevideoAleatoria = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)];
					}

					GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = placadevideoAleatoria;

					//Informa que houve alteração na carta
					Informacoes.JogadorCartaPlacaDeVideoMudou = true;
					HardCash.HardCashJogador--;

					break;

				case "Disco":
					RodadaDiscoJogador = StateMachine.Rodada;

					Disco discoAleatorio = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)]; //Gera um disco aleatório da lista

					while ((discoAleatorio == GetComponentInChildren<DisplayDisco>().disco) || (discoAleatorio == OutroPanel.GetComponentInChildren<DisplayDisco>().disco))
					{
						discoAleatorio = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)];
					}

					GetComponentInChildren<DisplayDisco>().disco = discoAleatorio;

					//Informa que houve alteração na carta
					Informacoes.JogadorCartaDiscoMudou = true;
					HardCash.HardCashJogador--;

					break;

				case "Fonte":
					RodadaFonteJogador = StateMachine.Rodada;

					Fonte fonteAleatoria = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)]; //Gera uma fonte aleatória da lista

					while ((fonteAleatoria == GetComponentInChildren<DisplayFonte>().fonte) || (fonteAleatoria == OutroPanel.GetComponentInChildren<DisplayFonte>().fonte))
					{
						fonteAleatoria = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)];
					}

					GetComponentInChildren<DisplayFonte>().fonte = fonteAleatoria;

					//Informa que houve alteração na carta
					Informacoes.JogadorCartaFonteMudou = true;
					HardCash.HardCashJogador--;

					break;

				case "Gabinete":
					RodadaGabineteJogador = StateMachine.Rodada;

					Gabinete gabineteAleatorio = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)]; //Gera um gabinete aleatório da lista

					while ((gabineteAleatorio == GetComponentInChildren<DisplayGabinete>().gabinete) || (gabineteAleatorio == OutroPanel.GetComponentInChildren<DisplayGabinete>().gabinete))
					{
						gabineteAleatorio = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)];
					}

					GetComponentInChildren<DisplayGabinete>().gabinete = gabineteAleatorio;

					//Informa que houve alteração na carta
					Informacoes.JogadorCartaGabineteMudou = true;
					HardCash.HardCashJogador--;

					break;
			}
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			switch (componente)
			{
				case "PlacaMae":
					RodadaPlacaMaeOponente = StateMachine.Rodada;

					PlacaMae placamaeAleatoria = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)]; //Gera uma placa-mãe aleatória da lista

					//Verifica se a carta é igual a carta que o jogador já tinha ou a carta do jogador adversário
					while ((placamaeAleatoria == GetComponentInChildren<DisplayPlacaMae>().placaMae) || (placamaeAleatoria == OutroPanel.GetComponentInChildren<DisplayPlacaMae>().placaMae))
					{
						placamaeAleatoria = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)];
					}

					GetComponentInChildren<DisplayPlacaMae>().placaMae = placamaeAleatoria;

					//Informa que houve alteração na carta
					Informacoes.OponenteCartaPlacaMaeMudou = true;
					HardCash.HardCashOponente--;

					break;

				case "Processador":
					RodadaProcessadorOponente = StateMachine.Rodada;

					Processador processadorAleatorio = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)]; //Gera um processador aleatório da lista

					while ((processadorAleatorio == GetComponentInChildren<DisplayProcessador>().processador) || (processadorAleatorio == OutroPanel.GetComponentInChildren<DisplayProcessador>().processador))
					{
						processadorAleatorio = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)];
					}

					GetComponentInChildren<DisplayProcessador>().processador = processadorAleatorio;

					//Informa que houve alteração na carta
					Informacoes.OponenteCartaProcessadorMudou = true;
					HardCash.HardCashOponente--;

					break;

				case "Memoria":
					RodadaMemoriaOponente = StateMachine.Rodada;

					Memoria memoriaAleatoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)]; //Gera uma memória aleatória da lista

					while ((memoriaAleatoria == GetComponentInChildren<DisplayMemoria>().memoria) || (memoriaAleatoria == OutroPanel.GetComponentInChildren<DisplayMemoria>().memoria))
					{
						memoriaAleatoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)];
					}

					GetComponentInChildren<DisplayMemoria>().memoria = memoriaAleatoria;

					//Informa que houve alteração na carta
					Informacoes.OponenteCartaMemoriaMudou = true;
					HardCash.HardCashOponente--;

					break;

				case "PlacaDeVideo":
					RodadaPlacaDeVideoOponente = StateMachine.Rodada;

					PlacaDeVideo placadevideoAleatoria = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)]; //Gera uma placa de vídeo aleatória da lista

					while ((placadevideoAleatoria == GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo) || (placadevideoAleatoria == OutroPanel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo))
					{
						placadevideoAleatoria = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)];
					}

					GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = placadevideoAleatoria;

					//Informa que houve alteração na carta
					Informacoes.OponenteCartaPlacaDeVideoMudou = true;
					HardCash.HardCashOponente--;

					break;

				case "Disco":
					RodadaDiscoOponente = StateMachine.Rodada;

					Disco discoAleatorio = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)]; //Gera um disco aleatório da lista

					while ((discoAleatorio == GetComponentInChildren<DisplayDisco>().disco) || (discoAleatorio == OutroPanel.GetComponentInChildren<DisplayDisco>().disco))
					{
						discoAleatorio = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)];
					}

					GetComponentInChildren<DisplayDisco>().disco = discoAleatorio;

					//Informa que houve alteração na carta
					Informacoes.OponenteCartaDiscoMudou = true;
					HardCash.HardCashOponente--;

					break;

				case "Fonte":
					RodadaFonteOponente = StateMachine.Rodada;

					Fonte fonteAleatoria = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)]; //Gera uma fonte aleatória da lista

					while ((fonteAleatoria == GetComponentInChildren<DisplayFonte>().fonte) || (fonteAleatoria == OutroPanel.GetComponentInChildren<DisplayFonte>().fonte))
					{
						fonteAleatoria = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)];
					}

					GetComponentInChildren<DisplayFonte>().fonte = fonteAleatoria;

					//Informa que houve alteração na carta
					Informacoes.OponenteCartaFonteMudou = true;
					HardCash.HardCashOponente--;

					break;

				case "Gabinete":
					RodadaGabineteOponente = StateMachine.Rodada;

					Gabinete gabineteAleatorio = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)]; //Gera um gabinete aleatório da lista

					while ((gabineteAleatorio == GetComponentInChildren<DisplayGabinete>().gabinete) || (gabineteAleatorio == OutroPanel.GetComponentInChildren<DisplayGabinete>().gabinete))
					{
						gabineteAleatorio = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)];
					}

					GetComponentInChildren<DisplayGabinete>().gabinete = gabineteAleatorio;

					//Informa que houve alteração na carta
					Informacoes.OponenteCartaGabineteMudou = true;
					HardCash.HardCashOponente--;

					break;
			}
		}

		Trocou = true;
	}
	
	#endregion
}