using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Comparar : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	#region Drag and Drop

	public GameObject PanelJogador, PanelOponente;
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
			if (((eventData.pointerDrag.gameObject.tag == "PlayerCard") && (Manager.JogadorPodeInteragir)) || ((eventData.pointerDrag.gameObject.tag == "OpponentCard") && (Manager.OponentePodeInteragir)))
			{
				if (Manager.PodeInteragir)
				{
					Manager.PodeInteragir = false;
					Comparou = false;
					eventData.pointerDrag.gameObject.transform.SetParent(this.transform);

					if (GetComponentInChildren<DisplayPlacaMae>() != null)
					{
						if((((RodadaPlacaMaeJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaPlacaMaeJogador) < RodadasAteComparar)))) || 
							((RodadaPlacaMaeOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaPlacaMaeOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayPlacaMae>().gameObject.tag == "PlayerCard")
							{
								PanelOponente.GetComponentInChildren<DisplayPlacaMae>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayPlacaMae>().gameObject.tag == "OpponentCard")
							{
								PanelJogador.GetComponentInChildren<DisplayPlacaMae>().gameObject.transform.SetParent(this.transform);
							}

							Invoke("CompararPlacaMae", 2);
						}
					}
					else if (GetComponentInChildren<DisplayProcessador>() != null)
					{
						if ((((RodadaProcessadorJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaProcessadorJogador) < RodadasAteComparar)))) || 
							((RodadaProcessadorOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaProcessadorOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayProcessador>().gameObject.tag == "PlayerCard")
							{
								PanelOponente.GetComponentInChildren<DisplayProcessador>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayProcessador>().gameObject.tag == "OpponentCard")
							{
								PanelJogador.GetComponentInChildren<DisplayProcessador>().gameObject.transform.SetParent(this.transform);
							}

							Invoke("CompararProcessador", 2);
						}
					}
					else if (GetComponentInChildren<DisplayMemoria>() != null)
					{
						if ((((RodadaMemoriaJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaMemoriaJogador) < RodadasAteComparar)))) ||
							((RodadaMemoriaOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaMemoriaOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayMemoria>().gameObject.tag == "PlayerCard")
							{
								PanelOponente.GetComponentInChildren<DisplayMemoria>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayMemoria>().gameObject.tag == "OpponentCard")
							{
								PanelJogador.GetComponentInChildren<DisplayMemoria>().gameObject.transform.SetParent(this.transform);
							}

							Invoke("CompararMemoria", 2);
						}
					}
					else if (GetComponentInChildren<DisplayPlacaDeVideo>() != null)
					{
						if ((((RodadaPlacaDeVideoJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaPlacaDeVideoJogador) < RodadasAteComparar)))) ||
							((RodadaPlacaDeVideoOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaPlacaDeVideoOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.tag == "PlayerCard")
							{
								PanelOponente.GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.tag == "OpponentCard")
							{
								PanelJogador.GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.transform.SetParent(this.transform);
							}

							Invoke("CompararPlacaDeVideo", 2);
						}
					}
					else if (GetComponentInChildren<DisplayDisco>() != null)
					{
						if ((((RodadaDiscoJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaDiscoJogador) < RodadasAteComparar)))) ||
							((RodadaDiscoOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaDiscoOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayDisco>().gameObject.tag == "PlayerCard")
							{
								PanelOponente.GetComponentInChildren<DisplayDisco>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayDisco>().gameObject.tag == "OpponentCard")
							{
								PanelJogador.GetComponentInChildren<DisplayDisco>().gameObject.transform.SetParent(this.transform);
							}

							Invoke("CompararDisco", 2);
						}
					}
					else if (GetComponentInChildren<DisplayFonte>() != null)
					{
						if ((((RodadaFonteJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaFonteJogador) < RodadasAteComparar)))) ||
							((RodadaFonteOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaFonteOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayFonte>().gameObject.tag == "PlayerCard")
							{
								PanelOponente.GetComponentInChildren<DisplayFonte>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayFonte>().gameObject.tag == "OpponentCard")
							{
								PanelJogador.GetComponentInChildren<DisplayFonte>().gameObject.transform.SetParent(this.transform);
							}

							Invoke("CompararFonte", 2);
						}
					}
					else if (GetComponentInChildren<DisplayGabinete>() != null)
					{
						if ((((RodadaGabineteJogador > 0) && (((StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador) && ((StateMachine.Rodada - RodadaGabineteJogador) < RodadasAteComparar)))) ||
							((RodadaGabineteOponente > 0) && ((StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente) && ((StateMachine.Rodada - RodadaGabineteOponente) < RodadasAteComparar)))))
						{
							Debug.Log("O jogador deve esperar pelo menos " + RodadasAteComparar + " rodadas até poder comparar a carta novamente");
						}
						else
						{
							if (GetComponentInChildren<DisplayGabinete>().gameObject.tag == "PlayerCard")
							{
								PanelOponente.GetComponentInChildren<DisplayGabinete>().gameObject.transform.SetParent(this.transform);
							}
							else if (GetComponentInChildren<DisplayGabinete>().gameObject.tag == "OpponentCard")
							{
								PanelJogador.GetComponentInChildren<DisplayGabinete>().gameObject.transform.SetParent(this.transform);
							}

							Invoke("CompararGabinete", 2);
						}
					}

					for (int i = 0; i < GetComponentsInChildren<CanvasGroup>().Length; i++)
					{
						GetComponentsInChildren<CanvasGroup>()[i].blocksRaycasts = false;
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

		if (Comparou)
		{
			StateMachine.Comparando = true;
		}
		else
		{
			Manager.PodeInteragir = true;
		}
	}

	#endregion

	#region Comparação de Atributos

	[HideInInspector] public int RodadaPlacaMaeJogador = 0, RodadaProcessadorJogador = 0, RodadaMemoriaJogador = 0, RodadaPlacaDeVideoJogador = 0, RodadaDiscoJogador = 0, RodadaFonteJogador = 0, RodadaGabineteJogador = 0; //Variáveis para armazenar a última rodada em que o jogador comparou determinada carta
	[HideInInspector] public int RodadaPlacaMaeOponente = 0, RodadaProcessadorOponente = 0, RodadaMemoriaOponente = 0, RodadaPlacaDeVideoOponente = 0, RodadaDiscoOponente = 0, RodadaFonteOponente = 0, RodadaGabineteOponente = 0; //Variáveis para armazenar a útlima rodada em que o oponente comparou determinada carta

	public void CompararPlacaMae()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayPlacaMae CartaJogador = GetComponentsInChildren<DisplayPlacaMae>()[0], CartaOponente = GetComponentsInChildren<DisplayPlacaMae>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < GetComponentsInChildren<DisplayPlacaMae>().Length; i++)
		{
			if (GetComponentsInChildren<DisplayPlacaMae>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = GetComponentsInChildren<DisplayPlacaMae>()[i];
			}
			else if (GetComponentsInChildren<DisplayPlacaMae>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = GetComponentsInChildren<DisplayPlacaMae>()[i];
			}
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaPlacaMaeJogador = StateMachine.Rodada;
		}
		else if(StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaPlacaMaeOponente = StateMachine.Rodada;
		}

		Comparou = true;

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: quantidade de memória");

				if (CartaJogador.AtributoQuantidadeMemoriaSlider.value > CartaOponente.AtributoQuantidadeMemoriaSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoQuantidadeMemoriaSlider.value < CartaOponente.AtributoQuantidadeMemoriaSlider.value)
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
				Debug.Log("Atributo a ser comparado: portas sata");

				if (CartaJogador.AtributoPortasSataSlider.value > CartaOponente.AtributoPortasSataSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoPortasSataSlider.value < CartaOponente.AtributoPortasSataSlider.value)
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
				Debug.Log("Atributo a ser comparado: slots pcie");

				if (CartaJogador.AtributoSlotsPCIESlider.value > CartaOponente.AtributoSlotsPCIESlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoSlotsPCIESlider.value < CartaOponente.AtributoSlotsPCIESlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCash.HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;

			case 4:
				Debug.Log("Atributo a ser comparado: preço");

				if (CartaJogador.AtributoPrecoSlider.value > CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
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

	public void CompararProcessador()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayProcessador CartaJogador = GetComponentsInChildren<DisplayProcessador>()[0], CartaOponente = GetComponentsInChildren<DisplayProcessador>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < GetComponentsInChildren<DisplayProcessador>().Length; i++)
		{
			if (GetComponentsInChildren<DisplayProcessador>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = GetComponentsInChildren<DisplayProcessador>()[i];
			}
			else if (GetComponentsInChildren<DisplayProcessador>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = GetComponentsInChildren<DisplayProcessador>()[i];
			}
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaProcessadorJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaProcessadorOponente = StateMachine.Rodada;
		}

		Comparou = true;

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: desempenho singlecore");

				if (CartaJogador.AtributoDesempenhoSingleCoreSlider.value > CartaOponente.AtributoDesempenhoSingleCoreSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoDesempenhoSingleCoreSlider.value < CartaOponente.AtributoDesempenhoSingleCoreSlider.value)
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
				Debug.Log("Atributo a ser comparado: desempenho multicore");

				if (CartaJogador.AtributoDesempenhoMultiCoreSlider.value > CartaOponente.AtributoDesempenhoMultiCoreSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoDesempenhoMultiCoreSlider.value < CartaOponente.AtributoDesempenhoMultiCoreSlider.value)
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
				Debug.Log("Atributo a ser comparado: consumo");

				if (CartaJogador.AtributoConsumoSlider.value > CartaOponente.AtributoConsumoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoConsumoSlider.value < CartaOponente.AtributoConsumoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCash.HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;

			case 4:
				Debug.Log("Atributo a ser comparado: preço");

				if (CartaJogador.AtributoPrecoSlider.value > CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
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

	public void CompararMemoria()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayMemoria CartaJogador = GetComponentsInChildren<DisplayMemoria>()[0], CartaOponente = GetComponentsInChildren<DisplayMemoria>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < GetComponentsInChildren<DisplayMemoria>().Length; i++)
		{
			if (GetComponentsInChildren<DisplayMemoria>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = GetComponentsInChildren<DisplayMemoria>()[i];
			}
			else if (GetComponentsInChildren<DisplayMemoria>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = GetComponentsInChildren<DisplayMemoria>()[i];
			}
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaMemoriaJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaMemoriaOponente = StateMachine.Rodada;
		}

		Comparou = true;

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: quantidade de memoria");

				if (CartaJogador.AtributoQuantidadeMemoriaSlider.value > CartaOponente.AtributoQuantidadeMemoriaSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoQuantidadeMemoriaSlider.value < CartaOponente.AtributoQuantidadeMemoriaSlider.value)
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
				Debug.Log("Atributo a ser comparado: desempenho");

				if (CartaJogador.AtributoDesempenhoSlider.value > CartaOponente.AtributoDesempenhoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoDesempenhoSlider.value < CartaOponente.AtributoDesempenhoSlider.value)
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
				Debug.Log("Atributo a ser comparado: latência");

				if (CartaJogador.AtributoLatenciaSlider.value > CartaOponente.AtributoLatenciaSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoLatenciaSlider.value < CartaOponente.AtributoLatenciaSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCash.HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;

			case 4:
				Debug.Log("Atributo a ser comparado: preço");

				if (CartaJogador.AtributoPrecoSlider.value > CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
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

	public void CompararPlacaDeVideo()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayPlacaDeVideo CartaJogador = GetComponentsInChildren<DisplayPlacaDeVideo>()[0], CartaOponente = GetComponentsInChildren<DisplayPlacaDeVideo>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < GetComponentsInChildren<DisplayPlacaDeVideo>().Length; i++)
		{
			if (GetComponentsInChildren<DisplayPlacaDeVideo>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = GetComponentsInChildren<DisplayPlacaDeVideo>()[i];
			}
			else if (GetComponentsInChildren<DisplayPlacaDeVideo>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = GetComponentsInChildren<DisplayPlacaDeVideo>()[i];
			}
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaPlacaDeVideoJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaPlacaDeVideoOponente = StateMachine.Rodada;
		}

		Comparou = true;

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: desempenho");

				if (CartaJogador.AtributoDesempenhoSlider.value > CartaOponente.AtributoDesempenhoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoDesempenhoSlider.value < CartaOponente.AtributoDesempenhoSlider.value)
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
				Debug.Log("Atributo a ser comparado: memória");

				if (CartaJogador.AtributoMemoriaSlider.value > CartaOponente.AtributoMemoriaSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoMemoriaSlider.value < CartaOponente.AtributoMemoriaSlider.value)
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
				Debug.Log("Atributo a ser comparado: consumo");

				if (CartaJogador.AtributoConsumoSlider.value > CartaOponente.AtributoConsumoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoConsumoSlider.value < CartaOponente.AtributoConsumoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCash.HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;

			case 4:
				Debug.Log("Atributo a ser comparado: preço");

				if (CartaJogador.AtributoPrecoSlider.value > CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
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

	public void CompararDisco()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayDisco CartaJogador = GetComponentsInChildren<DisplayDisco>()[0], CartaOponente = GetComponentsInChildren<DisplayDisco>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < GetComponentsInChildren<DisplayDisco>().Length; i++)
		{
			if (GetComponentsInChildren<DisplayDisco>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = GetComponentsInChildren<DisplayDisco>()[i];
			}
			else if (GetComponentsInChildren<DisplayDisco>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = GetComponentsInChildren<DisplayDisco>()[i];
			}
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaDiscoJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaDiscoOponente = StateMachine.Rodada;
		}

		Comparou = true;

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: capacidade de armazenamento");

				if (CartaJogador.AtributoCapacidadeDeArmazenamentoSlider.value > CartaOponente.AtributoCapacidadeDeArmazenamentoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoCapacidadeDeArmazenamentoSlider.value < CartaOponente.AtributoCapacidadeDeArmazenamentoSlider.value)
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
				Debug.Log("Atributo a ser comparado: velocidade");

				if (CartaJogador.AtributoVelocidadeSlider.value > CartaOponente.AtributoVelocidadeSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoVelocidadeSlider.value < CartaOponente.AtributoVelocidadeSlider.value)
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
				Debug.Log("Atributo a ser comparado: custo por gb");

				if (CartaJogador.AtributoCustoPorGBSlider.value > CartaOponente.AtributoCustoPorGBSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoCustoPorGBSlider.value < CartaOponente.AtributoCustoPorGBSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCash.HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;

			case 4:
				Debug.Log("Atributo a ser comparado: preço");

				if (CartaJogador.AtributoPrecoSlider.value > CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
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

	public void CompararFonte()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayFonte CartaJogador = GetComponentsInChildren<DisplayFonte>()[0], CartaOponente = GetComponentsInChildren<DisplayFonte>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < GetComponentsInChildren<DisplayFonte>().Length; i++)
		{
			if (GetComponentsInChildren<DisplayFonte>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = GetComponentsInChildren<DisplayFonte>()[i];
			}
			else if (GetComponentsInChildren<DisplayFonte>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = GetComponentsInChildren<DisplayFonte>()[i];
			}
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaFonteJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaFonteOponente = StateMachine.Rodada;
		}

		Comparou = true;

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: potência");

				if (CartaJogador.AtributoPotenciaSlider.value > CartaOponente.AtributoPotenciaSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoPotenciaSlider.value < CartaOponente.AtributoPotenciaSlider.value)
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
				Debug.Log("Atributo a ser comparado: eficiência");

				if (CartaJogador.AtributoEficienciaSlider.value > CartaOponente.AtributoEficienciaSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoEficienciaSlider.value < CartaOponente.AtributoEficienciaSlider.value)
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
				Debug.Log("Atributo a ser comparado: custo por watt");

				if (CartaJogador.AtributoCustoPorWSlider.value > CartaOponente.AtributoCustoPorWSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoCustoPorWSlider.value < CartaOponente.AtributoCustoPorWSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCash.HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;

			case 4:
				Debug.Log("Atributo a ser comparado: preço");

				if (CartaJogador.AtributoPrecoSlider.value > CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
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

	public void CompararGabinete()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayGabinete CartaJogador = GetComponentsInChildren<DisplayGabinete>()[0], CartaOponente = GetComponentsInChildren<DisplayGabinete>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < GetComponentsInChildren<DisplayGabinete>().Length; i++)
		{
			if (GetComponentsInChildren<DisplayGabinete>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = GetComponentsInChildren<DisplayGabinete>()[i];
			}
			else if (GetComponentsInChildren<DisplayGabinete>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = GetComponentsInChildren<DisplayGabinete>()[i];
			}
		}

		if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoJogador)
		{
			RodadaGabineteJogador = StateMachine.Rodada;
		}
		else if (StateMachine.EstadoAtual == StateMachine.Estados.VezDoOponente)
		{
			RodadaGabineteOponente = StateMachine.Rodada;
		}

		Comparou = true;

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: refrigeração");

				if (CartaJogador.AtributoRefrigeracaoSlider.value > CartaOponente.AtributoRefrigeracaoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoRefrigeracaoSlider.value < CartaOponente.AtributoRefrigeracaoSlider.value)
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
				Debug.Log("Atributo a ser comparado: slots pci");

				if (CartaJogador.AtributoSlotsPCISlider.value > CartaOponente.AtributoSlotsPCISlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoSlotsPCISlider.value < CartaOponente.AtributoSlotsPCISlider.value)
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
				Debug.Log("Atributo a ser comparado: baias internas");

				if (CartaJogador.AtributoBaiasHDSlider.value > CartaOponente.AtributoBaiasHDSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoBaiasHDSlider.value < CartaOponente.AtributoBaiasHDSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCash.HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;

			case 4:
				Debug.Log("Atributo a ser comparado: preço");

				if (CartaJogador.AtributoPrecoSlider.value > CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCash.HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
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
