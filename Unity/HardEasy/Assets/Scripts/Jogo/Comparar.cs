using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Comparar : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	#region Drag and Drop

	[HideInInspector] public bool Comparou = false;
	public int RodadasAteComparar;

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
					Comparou = false;
					eventData.pointerDrag.gameObject.transform.SetParent(this.transform); //Coloca a carta arrastada abaixo do painel na hierarquia
					GetComponentInChildren<CanvasGroup>().blocksRaycasts = false; //Bloqueia os raycasts para impedir a interação com a carta

					if (GetComponentInChildren<DisplayPlacaMae>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaPlacaMaeJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaPlacaMaeJogador) < RodadasAteComparar)))) || 
							((RodadaPlacaMaeOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaPlacaMaeOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							
							//Pega a carta do jogador adversário e a coloca na mesa para ser comparada
							if (GetComponentInChildren<DisplayPlacaMae>().gameObject.tag == "PlayerCard")
							{
								Informacoes.PanelOponente.GetComponentInChildren<DisplayPlacaMae>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayPlacaMae>().gameObject.tag == "OpponentCard")
							{
								Informacoes.PanelJogador.GetComponentInChildren<DisplayPlacaMae>().gameObject.transform.SetParent(this.transform);
							}

							StartCoroutine(ComecarComparacao("PlacaMae", 1f));

						}
					}
					else if (GetComponentInChildren<DisplayProcessador>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaProcessadorJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaProcessadorJogador) < RodadasAteComparar)))) || 
							((RodadaProcessadorOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaProcessadorOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							//Pega a carta do jogador adversário e a coloca na mesa para ser comparada
							if (GetComponentInChildren<DisplayProcessador>().gameObject.tag == "PlayerCard")
							{
								Informacoes.PanelOponente.GetComponentInChildren<DisplayProcessador>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayProcessador>().gameObject.tag == "OpponentCard")
							{
								Informacoes.PanelJogador.GetComponentInChildren<DisplayProcessador>().gameObject.transform.SetParent(this.transform);
							}

							StartCoroutine(ComecarComparacao("Processador", 1f));

						}
					}
					else if (GetComponentInChildren<DisplayMemoria>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaMemoriaJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaMemoriaJogador) < RodadasAteComparar)))) ||
							((RodadaMemoriaOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaMemoriaOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							//Pega a carta do jogador adversário e a coloca na mesa para ser comparada
							if (GetComponentInChildren<DisplayMemoria>().gameObject.tag == "PlayerCard")
							{
								Informacoes.PanelOponente.GetComponentInChildren<DisplayMemoria>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayMemoria>().gameObject.tag == "OpponentCard")
							{
								Informacoes.PanelJogador.GetComponentInChildren<DisplayMemoria>().gameObject.transform.SetParent(this.transform);
							}

							StartCoroutine(ComecarComparacao("Memoria", 1f));

						}
					}
					else if (GetComponentInChildren<DisplayPlacaDeVideo>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaPlacaDeVideoJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaPlacaDeVideoJogador) < RodadasAteComparar)))) ||
							((RodadaPlacaDeVideoOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaPlacaDeVideoOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							//Pega a carta do jogador adversário e a coloca na mesa para ser comparada
							if (GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.tag == "PlayerCard")
							{
								Informacoes.PanelOponente.GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.tag == "OpponentCard")
							{
								Informacoes.PanelJogador.GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.transform.SetParent(this.transform);
							}

							StartCoroutine(ComecarComparacao("PlacaDeVideo", 1f));

						}
					}
					else if (GetComponentInChildren<DisplayDisco>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaDiscoJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaDiscoJogador) < RodadasAteComparar)))) ||
							((RodadaDiscoOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaDiscoOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							//Pega a carta do jogador adversário e a coloca na mesa para ser comparada
							if (GetComponentInChildren<DisplayDisco>().gameObject.tag == "PlayerCard")
							{
								Informacoes.PanelOponente.GetComponentInChildren<DisplayDisco>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayDisco>().gameObject.tag == "OpponentCard")
							{
								Informacoes.PanelJogador.GetComponentInChildren<DisplayDisco>().gameObject.transform.SetParent(this.transform);
							}

							StartCoroutine(ComecarComparacao("Disco", 1f));

						}
					}
					else if (GetComponentInChildren<DisplayFonte>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaFonteJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaFonteJogador) < RodadasAteComparar)))) ||
							((RodadaFonteOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaFonteOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							//Pega a carta do jogador adversário e a coloca na mesa para ser comparada
							if (GetComponentInChildren<DisplayFonte>().gameObject.tag == "PlayerCard")
							{
								Informacoes.PanelOponente.GetComponentInChildren<DisplayFonte>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayFonte>().gameObject.tag == "OpponentCard")
							{
								Informacoes.PanelJogador.GetComponentInChildren<DisplayFonte>().gameObject.transform.SetParent(this.transform);
							}

							StartCoroutine(ComecarComparacao("Fonte", 1f));

						}
					}
					else if (GetComponentInChildren<DisplayGabinete>() != null)
					{
						//Verifica a útilma rodada em que a carta foi trocada e a compara com o número mínimo de rodadas que o jogador deve esperar até trocar a carta novamente
						if ((((RodadaGabineteJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaGabineteJogador) < RodadasAteComparar)))) ||
							((RodadaGabineteOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaGabineteOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							//Pega a carta do jogador adversário e a coloca na mesa para ser comparada
							if (GetComponentInChildren<DisplayGabinete>().gameObject.tag == "PlayerCard")
							{
								Informacoes.PanelOponente.GetComponentInChildren<DisplayGabinete>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayGabinete>().gameObject.tag == "OpponentCard")
							{
								Informacoes.PanelJogador.GetComponentInChildren<DisplayGabinete>().gameObject.transform.SetParent(this.transform);
							}

							StartCoroutine(ComecarComparacao("Gabinete", 1f));

						}
					}

					for (int i = 0; i < GetComponentsInChildren<CanvasGroup>().Length; i++)
					{
						GetComponentsInChildren<CanvasGroup>()[i].blocksRaycasts = false;
					}

					StartCoroutine(Pausar(1.5f));

					StartCoroutine(RetornarCartas(3f));
				}
			}
		}
	}

	//Retorna as cartas para o baralho de cada jogador
	IEnumerator RetornarCartas(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		
		while (GetComponentInChildren<CanvasGroup>() != null)
		{
			for (int i = 0; i < GetComponentsInChildren<CanvasGroup>().Length; i++)
			{
				GetComponentsInChildren<CanvasGroup>()[i].gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true;
				GetComponentInChildren<Sliders>().RetornarCores();

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

		if (Comparou)
		{
			StateMachine.Comparando = true;
		}
		else
		{
			Manager.PodeInteragir = true;
		}
	}

	IEnumerator Pausar(float delayTime)
	{
		yield return new WaitForSeconds(delayTime);

		if (Comparou)
		{
			Informacoes.btnContinuar.SetActive(true);
			Time.timeScale = 0f;
		}
	}

	#endregion

	#region Comparação de Atributos

	public static int RodadaPlacaMaeJogador = 0, RodadaProcessadorJogador = 0, RodadaMemoriaJogador = 0, RodadaPlacaDeVideoJogador = 0, RodadaDiscoJogador = 0, RodadaFonteJogador = 0, RodadaGabineteJogador = 0; //Variáveis para armazenar a última rodada em que o jogador comparou determinada carta
	public static int RodadaPlacaMaeOponente = 0, RodadaProcessadorOponente = 0, RodadaMemoriaOponente = 0, RodadaPlacaDeVideoOponente = 0, RodadaDiscoOponente = 0, RodadaFonteOponente = 0, RodadaGabineteOponente = 0; //Variáveis para armazenar a útlima rodada em que o oponente comparou determinada carta

	IEnumerator ComecarComparacao(string componente, float delayTime)
	{
		yield return new WaitForSeconds(delayTime);
		CompararCartas(componente);
	}

	public void CompararCartas(string componente)
	{
		int atributoIndex = Random.Range(0, 4); //Escolhe aleatoriamente qual atributo será comparado
		Sliders CartaJogador = GetComponentsInChildren<Sliders>()[0], CartaOponente = GetComponentsInChildren<Sliders>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < GetComponentsInChildren<Sliders>().Length; i++)
		{
			if (GetComponentsInChildren<Sliders>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = GetComponentsInChildren<Sliders>()[i];
			}
			else if (GetComponentsInChildren<Sliders>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = GetComponentsInChildren<Sliders>()[i];
			}
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			switch (componente)
			{
				case "PlacaMae":
					RodadaPlacaMaeJogador = StateMachine.Rodada;
					break;

				case "Processador":
					RodadaProcessadorJogador = StateMachine.Rodada;
					break;

				case "Memoria":
					RodadaMemoriaJogador = StateMachine.Rodada;
					break;

				case "PlacaDeVideo":
					RodadaPlacaDeVideoJogador = StateMachine.Rodada;
					break;

				case "Disco":
					RodadaDiscoJogador = StateMachine.Rodada;
					break;

				case "Fonte":
					RodadaFonteJogador = StateMachine.Rodada;
					break;

				case "Gabinete":
					RodadaGabineteJogador = StateMachine.Rodada;
					break;
			}
		}
		else if(StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			switch (componente)
			{
				case "PlacaMae":
					RodadaPlacaMaeOponente = StateMachine.Rodada;
					break;

				case "Processador":
					RodadaProcessadorOponente = StateMachine.Rodada;
					break;

				case "Memoria":
					RodadaMemoriaOponente = StateMachine.Rodada;
					break;

				case "PlacaDeVideo":
					RodadaPlacaDeVideoOponente = StateMachine.Rodada;
					break;

				case "Disco":
					RodadaDiscoOponente = StateMachine.Rodada;
					break;

				case "Fonte":
					RodadaFonteOponente = StateMachine.Rodada;
					break;

				case "Gabinete":
					RodadaGabineteOponente = StateMachine.Rodada;
					break;
			}
		}

		Comparou = true;

		CartaJogador.GetComponentInChildren<Sliders>().DestacarSlider(atributoIndex);
		CartaOponente.GetComponentInChildren<Sliders>().DestacarSlider(atributoIndex);

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 0:

				if (CartaJogador.slidersCarta[atributoIndex].value > CartaOponente.slidersCarta[atributoIndex].value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.slidersCarta[atributoIndex].value < CartaOponente.slidersCarta[atributoIndex].value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCash.HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;

			case 1:
				
				if (CartaJogador.slidersCarta[atributoIndex].value > CartaOponente.slidersCarta[atributoIndex].value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.slidersCarta[atributoIndex].value < CartaOponente.slidersCarta[atributoIndex].value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCash.HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;

			case 2:
				
				if (CartaJogador.slidersCarta[atributoIndex].value > CartaOponente.slidersCarta[atributoIndex].value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.slidersCarta[atributoIndex].value < CartaOponente.slidersCarta[atributoIndex].value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCash.HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;

			case 3:

				if (CartaJogador.slidersCarta[atributoIndex].value > CartaOponente.slidersCarta[atributoIndex].value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.slidersCarta[atributoIndex].value < CartaOponente.slidersCarta[atributoIndex].value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCash.HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;
		}
	}

	#endregion
}
