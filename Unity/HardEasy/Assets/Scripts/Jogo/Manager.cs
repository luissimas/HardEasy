using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour{

	void Start() 
	{
		EstadoAtual = Estados.Inicio; //Inicia o jogo no estado de início
	}

	void Update()
	{
		GerenciadorDeRodadas(); //Verifica o gerenciador de rodadas a cada frame
		ProximoEstadoAoComparar(); //Avança uma rodada sempre que as cartas forem comparadas

		HardCashJogadorText.text = HardCashJogador.ToString();
		HardCashOponenteText.text = HardCashOponente.ToString();
	}

	#region Interação do Usuário

	public GameObject DropAreaInspector;
	public static GameObject DropArea;

	public TMP_Text HardCashJogadorText, HardCashOponenteText;

	public static bool JogadorPodeInteragir = false;
	public static bool OponentePodeInteragir = false;
	public static bool PodeInteragir = true;

	public static bool Comparando = false; //Variável para gerenciar se as cartas estão sendo comparadas ou não

	public static int HardCashJogador = 0;
	public static int HardCashOponente = 0;

	#endregion

	#region Sistema de Rodadas

	[HideInInspector] public enum Estados {Inicio, VezDoJogador, VezDoOponente, Fim } //Enumerador contendo todos os estados de jogo possíveis

	public static Estados EstadoAtual; //Variável para identificar o estado atual do jogo
	public static int Rodada = 0; //Variável para identificar em qual rodada o jogo está

	//Gerencia o sistema de rodadas 
	public void GerenciadorDeRodadas()
	{
		Debug.Log(EstadoAtual);
		//Identifica qual o estado atual do jogo
		switch (EstadoAtual)
		{
			case (Estados.Inicio):
				Lista.CarregarListas(); //Carega as listas dos componentes
				DropArea = DropAreaInspector;
				IniciarCartas(PanelJogador); //Embaralha e inicia as cartas do jogador
				IniciarCartas(PanelOponente); //Embaralha e inicia as cartas do oponente
				VerificarSeAsCartasSaoIguais(PanelJogador, PanelOponente); //Verifica se as cartas do jogador e do oponente são iguais
				Rodada++; //Avança uma rodada
				EstadoAtual = EstadoAtual + Random.Range(1, 3); //Escolhe quem irá começar jogando de forma aleatória
				break;
			case (Estados.VezDoJogador):
				JogadorPodeInteragir = true;
				OponentePodeInteragir = false;
				break;
			case (Estados.VezDoOponente):
				JogadorPodeInteragir = false;
				OponentePodeInteragir = true;
				break;
			case (Estados.Fim):
				JogadorPodeInteragir = false;
				OponentePodeInteragir = false;
				PodeInteragir = false;
				break;
		}
	}

	//Avança para o próximo estado baseado no estado e nas condições de jogo atuais
	public void ProximoEstado()
	{
		PodeInteragir = false;

		//Verifica em qual estado o jogo está no momento
		if (EstadoAtual == Estados.VezDoJogador)
		{
			//Verifica se o jogador venceu o jogo
			if ((ProcessadorConectaPlacaMae(PanelJogador) && MemoriaConectaPlacaMae(PanelJogador) && GabineteConectaPlacaMae(PanelJogador) 
				&& PlacaDeVideoConectaPlacaMae(PanelJogador) && FonteConectaPlacaDeVideo(PanelJogador)))
			{
				//Finaliza o jogo
				EstadoAtual = Estados.Fim;
			}
			else
			{
				//Avança para o próximo estado normalmente
				Rodada++;
				EstadoAtual = Estados.VezDoOponente;
			}
		}
		else if (EstadoAtual==Estados.VezDoOponente)
		{
			//Verifica se o oponente venceu o jogo
			if ((ProcessadorConectaPlacaMae(PanelOponente) && MemoriaConectaPlacaMae(PanelOponente) && GabineteConectaPlacaMae(PanelOponente)
				&& PlacaDeVideoConectaPlacaMae(PanelOponente) && FonteConectaPlacaDeVideo(PanelOponente)))
			{
				//Finaliza o jogo
				EstadoAtual = Estados.Fim;
			}
			else
			{
				//Avança para o próximo estado normalmente
				Rodada++;
				EstadoAtual = Estados.VezDoJogador;
			}
		}

		PodeInteragir = true;
	}

	//Avança para o próximo estado após uma comparação de cartas
	public void ProximoEstadoAoComparar()
	{
		if (Comparando)
		{
			Comparando = false;
			ProximoEstado();
		}
	}

	#endregion

	#region Verificar as cartas

	//Verifica se as cartas do jogador e do oponente são iguais
	public void VerificarSeAsCartasSaoIguais(GameObject Panel, GameObject OutroPanel)
	{
		VerificarPlacaMaeIgual(Panel, OutroPanel);
		VerificarProcessadorIgual(Panel, OutroPanel);
		VerificarMemoriaIgual(Panel, OutroPanel);
		VerificarPlacaDeVideoIgual(Panel, OutroPanel);
		VerificarDiscoIgual(Panel, OutroPanel);
		VerificarFonteIgual(Panel, OutroPanel);
		VerificarGabineteIgual(Panel, OutroPanel);
	}

	public void VerificarPlacaMaeIgual(GameObject Panel, GameObject OutroPanel)
	{
		if (Panel.GetComponentInChildren<DisplayPlacaMae>().placaMae == OutroPanel.GetComponentInChildren<DisplayPlacaMae>().placaMae)
		{
			Panel.GetComponentInChildren<DisplayPlacaMae>().placaMae = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)];
			VerificarPlacaMaeIgual(Panel, OutroPanel);
		}
	}

	public void VerificarProcessadorIgual(GameObject Panel, GameObject OutroPanel)
	{
		if (Panel.GetComponentInChildren<DisplayProcessador>().processador == OutroPanel.GetComponentInChildren<DisplayProcessador>().processador)
		{
			Panel.GetComponentInChildren<DisplayProcessador>().processador = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)];
			VerificarProcessadorIgual(Panel, OutroPanel);
		}
	}

	public void VerificarMemoriaIgual(GameObject Panel, GameObject OutroPanel)
	{
		if (Panel.GetComponentInChildren<DisplayMemoria>().memoria == OutroPanel.GetComponentInChildren<DisplayMemoria>().memoria)
		{
			Panel.GetComponentInChildren<DisplayMemoria>().memoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)];
			VerificarMemoriaIgual(Panel, OutroPanel);
		}
	}

	public void VerificarPlacaDeVideoIgual(GameObject Panel, GameObject OutroPanel)
	{
		if (Panel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo == OutroPanel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo)
		{
			PanelJogador.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)];
			VerificarPlacaDeVideoIgual(Panel, OutroPanel);
		}
	}

	public void VerificarDiscoIgual(GameObject Panel, GameObject OutroPanel)
	{
		if (Panel.GetComponentInChildren<DisplayDisco>().disco == OutroPanel.GetComponentInChildren<DisplayDisco>().disco)
		{
			Panel.GetComponentInChildren<DisplayDisco>().disco = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)];
			VerificarDiscoIgual(Panel, OutroPanel);
		}
	}

	public void VerificarFonteIgual(GameObject Panel, GameObject OutroPanel)
	{
		if (Panel.GetComponentInChildren<DisplayFonte>().fonte == OutroPanel.GetComponentInChildren<DisplayFonte>().fonte)
		{
			Panel.GetComponentInChildren<DisplayFonte>().fonte = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)];
			VerificarFonteIgual(Panel, OutroPanel);
		}
	}

	public void VerificarGabineteIgual(GameObject Panel, GameObject OutroPanel)
	{
		if (Panel.GetComponentInChildren<DisplayGabinete>().gabinete == OutroPanel.GetComponentInChildren<DisplayGabinete>().gabinete)
		{
			Panel.GetComponentInChildren<DisplayGabinete>().gabinete = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)];
			VerificarGabineteIgual(Panel, OutroPanel);
		}
	}

	#endregion

	#region Iniciar e trocar as cartas

	public GameObject PanelJogador, PanelOponente;  //Variáveis que recebem o canvas do jogador e do oponente
	//Variáveis para identificar se houve mudança nas cartas
	public static bool JogadorCartaPlacaMaeMudou = false, JogadorCartaProcessadorMudou = false, JogadorCartaMemoriaMudou = false, JogadorCartaPlacaDeVideoMudou = false, JogadorCartaDiscoMudou = false, JogadorCartaFonteMudou = false, JogadorCartaGabineteMudou = false; 
	public static bool OponenteCartaPlacaMaeMudou = false, OponenteCartaProcessadorMudou = false, OponenteCartaMemoriaMudou = false, OponenteCartaPlacaDeVideoMudou = false, OponenteCartaDiscoMudou = false, OponenteCartaFonteMudou = false, OponenteCartaGabineteMudou = false;

	//Gera cartas aleatórias de todos os tipos baseado no canvas
	public void IniciarCartas(GameObject PanelBaralho)
	{
		PanelBaralho.GetComponentInChildren<DisplayPlacaMae>().placaMae = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)];
		PanelBaralho.GetComponentInChildren<DisplayProcessador>().processador = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)];
		PanelBaralho.GetComponentInChildren<DisplayMemoria>().memoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)];
		PanelBaralho.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)];
		PanelBaralho.GetComponentInChildren<DisplayDisco>().disco = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)];
		PanelBaralho.GetComponentInChildren<DisplayFonte>().fonte = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)];
		PanelBaralho.GetComponentInChildren<DisplayGabinete>().gabinete = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)];

		//Informa que houve alteração nas cartas
		if (PanelBaralho == PanelJogador)
		{
			JogadorCartaPlacaMaeMudou = true;
			JogadorCartaProcessadorMudou = true;
			JogadorCartaMemoriaMudou = true;
			JogadorCartaPlacaDeVideoMudou = true;
			JogadorCartaDiscoMudou = true;
			JogadorCartaFonteMudou = true;
			JogadorCartaGabineteMudou = true;
		}
		else if(PanelBaralho == PanelOponente)
		{
			OponenteCartaPlacaMaeMudou = true;
			OponenteCartaProcessadorMudou = true;
			OponenteCartaMemoriaMudou = true;
			OponenteCartaPlacaDeVideoMudou = true;
			OponenteCartaDiscoMudou = true;
			OponenteCartaFonteMudou = true;
			OponenteCartaGabineteMudou = true;
		}
		
		//Prevenir que todas as cartas sejam compatíveis no início do jogo
		if (ProcessadorConectaPlacaMae(PanelBaralho) && MemoriaConectaPlacaMae(PanelBaralho) && GabineteConectaPlacaMae(PanelBaralho) && PlacaDeVideoConectaPlacaMae(PanelBaralho) && FonteConectaPlacaDeVideo(PanelBaralho))
		{
			//Chama a função recursivamente para iniciar as cartas de forma aleatória novamente
			IniciarCartas(PanelBaralho);
		}
	}

	//Troca a carta do tipo placa-mãe por outra carta aleatória do mesmo tipo
	public void TrocarPlacaMae(GameObject PanelBaralho)
	{
		if (PodeInteragir)
		{
			if (((PanelBaralho == PanelJogador) && (JogadorPodeInteragir)) || ((PanelBaralho == PanelOponente) && (OponentePodeInteragir)))
			{
				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					if (HardCashJogador == 0)
					{
						return;
					}
				}
				else if (PanelBaralho.gameObject.tag == "OpponentCard")
				{
					if (HardCashOponente == 0)
					{
						return;
					}
				}

				GameObject OutroPanel;

				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					OutroPanel = PanelOponente;
				}
				else
				{
					OutroPanel = PanelJogador;
				}

				PodeInteragir = false;

				PlacaMae placamaeAleatoria = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)]; //Gera uma placa-mãe aleatória da lista

				//Verifica se a placa-mãe selecionada aleatoriamente é diferente da placa-mãe ativa
				if ((PanelBaralho.GetComponentInChildren<DisplayPlacaMae>().placaMae != placamaeAleatoria) && (OutroPanel.GetComponentInChildren<DisplayPlacaMae>().placaMae != placamaeAleatoria))
				{
					PanelBaralho.GetComponentInChildren<DisplayPlacaMae>().placaMae = placamaeAleatoria;

					//VerificarPlacaMaeIgual(PanelBaralho, OutroPanel);

					//Informa que houve alteração na carta
					if (PanelBaralho == PanelJogador)
					{
						JogadorCartaPlacaMaeMudou = true;
						HardCashJogador--;
					}
					else if (PanelBaralho == PanelOponente)
					{
						OponenteCartaPlacaMaeMudou = true;
						HardCashOponente--;
					}
				}
				else
				{
					//Chama a função recursivamente para gerar outro componente de forma aleatoria
					PodeInteragir = true;
					TrocarPlacaMae(PanelBaralho);
				}
				//Avança para o próximo estado e adiciona uma rodada			
				Invoke("ProximoEstado", 2);
			}
		}
	}

	//Troca a carta do tipo processador por outra carta aleatória do mesmo tipo
	public void TrocarProcessador(GameObject PanelBaralho)
	{
		if (PodeInteragir)
		{
			if (((PanelBaralho == PanelJogador) && (JogadorPodeInteragir)) || ((PanelBaralho == PanelOponente) && (OponentePodeInteragir)))
			{
				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					if (HardCashJogador == 0)
					{
						return;
					}
				}
				else if (PanelBaralho.gameObject.tag == "OpponentCard")
				{
					if (HardCashOponente == 0)
					{
						return;
					}

				}
				PodeInteragir = false;

				GameObject OutroPanel;

				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					OutroPanel = PanelOponente;
				}
				else
				{
					OutroPanel = PanelJogador;
				}

				Processador processadorAleatorio = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)]; //Gera um processado aleatório da lista

				//Verifica se o processador selecionado aleatoriamente é diferente do processador ativo
				if ((PanelBaralho.GetComponentInChildren<DisplayProcessador>().processador != processadorAleatorio) && (OutroPanel.GetComponentInChildren<DisplayProcessador>().processador != processadorAleatorio))
				{
					PanelBaralho.GetComponentInChildren<DisplayProcessador>().processador = processadorAleatorio;

					//VerificarProcessadorIgual(PanelBaralho, OutroPanel);

					//Informa que houve alteração na carta
					if (PanelBaralho == PanelJogador)
					{
						JogadorCartaProcessadorMudou = true;
						HardCashJogador--;
					}
					else if (PanelBaralho == PanelOponente)
					{
						OponenteCartaProcessadorMudou = true;
						HardCashOponente--;
					}
				}
				else
				{
					//Chama a função recursivamente para gerar outro componente de forma aleatoria
					PodeInteragir = true;
					TrocarProcessador(PanelBaralho);
				}
				//Avança para o próximo estado e adiciona uma rodada
				Invoke("ProximoEstado", 2);
			}
		}
	}

	//Troca a carta do tipo memóia por outra carta aleatória do mesmo tipo
	public void TrocarMemoria(GameObject PanelBaralho)
	{
		if (PodeInteragir)
		{
			if (((PanelBaralho == PanelJogador) && (JogadorPodeInteragir)) || ((PanelBaralho == PanelOponente) && (OponentePodeInteragir)))
			{
				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					if (HardCashJogador == 0)
					{
						return;
					}
				}
				else if (PanelBaralho.gameObject.tag == "OpponentCard")
				{
					if (HardCashOponente == 0)
					{
						return;
					}
				}

				PodeInteragir = false;

				GameObject OutroPanel;

				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					OutroPanel = PanelOponente;
				}
				else
				{
					OutroPanel = PanelJogador;
				}

				Memoria memoriaAleatoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)]; //Gera uma memória aleatória da lista

				//Verfica se a memória selecionada aleatoriamente é diferente da memória ativa
				if ((PanelBaralho.GetComponentInChildren<DisplayMemoria>().memoria != memoriaAleatoria) && (OutroPanel.GetComponentInChildren<DisplayMemoria>().memoria != memoriaAleatoria))
				{
					PanelBaralho.GetComponentInChildren<DisplayMemoria>().memoria = memoriaAleatoria;

					//VerificarMemoriaIgual(PanelBaralho, OutroPanel);

					//Informa que houve alteração na carta
					if (PanelBaralho == PanelJogador)
					{
						JogadorCartaMemoriaMudou = true;
						HardCashJogador--;
					}
					else if (PanelBaralho == PanelOponente)
					{
						OponenteCartaMemoriaMudou = true;
						HardCashOponente--;
					}
				}
				else
				{
					//Chama a função recursivamente para gerar outro componente de forma aleatoria
					PodeInteragir = true;
					TrocarMemoria(PanelBaralho);
				}
				//Avança para o próximo estado e adiciona uma rodada
				Invoke("ProximoEstado", 2);
			}
		}
	}

	//Troca a carta do tipo placa de vídeo por outra carta aleatória do mesmo tipo
	public void TrocarPlacaDeVideo(GameObject PanelBaralho)
	{
		if (PodeInteragir)
		{
			if (((PanelBaralho == PanelJogador) && (JogadorPodeInteragir)) || ((PanelBaralho == PanelOponente) && (OponentePodeInteragir)))
			{
				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					if (HardCashJogador == 0)
					{
						return;
					}
				}
				else if (PanelBaralho.gameObject.tag == "OpponentCard")
				{
					if (HardCashOponente == 0)
					{
						return;
					}
				}

				PodeInteragir = false;

				GameObject OutroPanel;

				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					OutroPanel = PanelOponente;
				}
				else
				{
					OutroPanel = PanelJogador;
				}

				PlacaDeVideo placadevideoAleatoria = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)]; //Gera uma placa de vídeo aleatória da lista

				//Verifica se a placa de vídeo selecionada aleatoriamente é diferente da placa de vídeo ativa
				if ((PanelBaralho.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo != placadevideoAleatoria) && (OutroPanel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo != placadevideoAleatoria))
				{
					PanelBaralho.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = placadevideoAleatoria;

					//Informa que houve alteração na carta
					if (PanelBaralho == PanelJogador)
					{
						JogadorCartaPlacaDeVideoMudou = true;
						HardCashJogador--;
					}
					else if (PanelBaralho == PanelOponente)
					{
						OponenteCartaPlacaDeVideoMudou = true;
						HardCashOponente--;
					}
				}
				else
				{
					//Chama a função recursivamente para gerar outro componente de forma aleatoria
					PodeInteragir = true;
					TrocarPlacaDeVideo(PanelBaralho);
				}
				//Avança para o próximo estado e adiciona uma rodada
				Invoke("ProximoEstado", 2);
			}
		}
	}

	//Troca a carta do tipo disco por outra carta aleatória do mesmo tipo
	public void TrocarDisco(GameObject PanelBaralho)
	{
		if (PodeInteragir)
		{
			if (((PanelBaralho == PanelJogador) && (JogadorPodeInteragir)) || ((PanelBaralho == PanelOponente) && (OponentePodeInteragir)))
			{
				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					if (HardCashJogador == 0)
					{
						return;
					}
				}
				else if (PanelBaralho.gameObject.tag == "OpponentCard")
				{
					if (HardCashOponente == 0)
					{
						return;
					}
				}

				PodeInteragir = false;

				GameObject OutroPanel;

				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					OutroPanel = PanelOponente;
				}
				else
				{
					OutroPanel = PanelJogador;
				}

				Disco discoAleatorio = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)]; //Gera um disco aleatório da lista

				//Verifica se o disco selecionado aleatoriamente é diferente do disco ativo
				if ((PanelBaralho.GetComponentInChildren<DisplayDisco>().disco != discoAleatorio) && (OutroPanel.GetComponentInChildren<DisplayDisco>().disco != discoAleatorio))
				{
					PanelBaralho.GetComponentInChildren<DisplayDisco>().disco = discoAleatorio;

					//Informa que houve alteração na carta
					if (PanelBaralho == PanelJogador)
					{
						JogadorCartaDiscoMudou = true;
						HardCashJogador--;
					}
					else if (PanelBaralho == PanelOponente)
					{
						OponenteCartaDiscoMudou = true;
						HardCashOponente--;
					}
				}
				else
				{
					//Chama a função recursivamente para gerar outro componente de forma aleatoria
					PodeInteragir = true;
					TrocarDisco(PanelBaralho);
				}
				//Avança para o próximo estado e adiciona uma rodada
				Invoke("ProximoEstado", 2);
			}
		}
	}

	//Troca a carta do tipo fonte por outra carta aleatória do mesmo tipo
	public void TrocarFonte(GameObject PanelBaralho)
	{
		if (PodeInteragir)
		{
			if (((PanelBaralho == PanelJogador) && (JogadorPodeInteragir)) || ((PanelBaralho == PanelOponente) && (OponentePodeInteragir)))
			{
				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					if (HardCashJogador == 0)
					{
						return;
					}
				}
				else if (PanelBaralho.gameObject.tag == "OpponentCard")
				{
					if (HardCashOponente == 0)
					{
						return;
					}
				}

				PodeInteragir = false;

				GameObject OutroPanel;

				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					OutroPanel = PanelOponente;
				}
				else
				{
					OutroPanel = PanelJogador;
				}
				
				Fonte fonteAleatoria = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)]; //Gera uma fonte aleatória da lista

				//Verifica se a fonte selecionada aleatoriamente é diferente da fonte ativa
				if ((PanelBaralho.GetComponentInChildren<DisplayFonte>().fonte != fonteAleatoria) && (OutroPanel.GetComponentInChildren<DisplayFonte>().fonte != fonteAleatoria))
				{
					PanelBaralho.GetComponentInChildren<DisplayFonte>().fonte = fonteAleatoria;

					//Informa que houve alteração na carta
					if (PanelBaralho == PanelJogador)
					{
						JogadorCartaFonteMudou = true;
						HardCashJogador--;
					}
					else if (PanelBaralho == PanelOponente)
					{
						OponenteCartaFonteMudou = true;
						HardCashOponente--;
					}
				}
				else
				{
					//Chama a função recursivamente para gerar outro componente de forma aleatoria
					PodeInteragir = true;
					TrocarFonte(PanelBaralho);
				}
				//Avança para o próximo estado e adiciona uma rodada
				Invoke("ProximoEstado", 2);
			}
		}
	}

	//Troca a carta do tipo gabinete por outra carta aleatória do mesmo tipo
	public void TrocarGabinete(GameObject PanelBaralho)
	{
		if (PodeInteragir)
		{
			if (((PanelBaralho == PanelJogador) && (JogadorPodeInteragir)) || ((PanelBaralho == PanelOponente) && (OponentePodeInteragir)))
			{

				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					if (HardCashJogador == 0)
					{
						return;
					}
				}
				else if (PanelBaralho.gameObject.tag == "OpponentCard")
				{
					if (HardCashOponente == 0)
					{
						return;
					}
				}

				PodeInteragir = false;

				GameObject OutroPanel;

				if (PanelBaralho.gameObject.tag == "PlayerCard")
				{
					OutroPanel = PanelOponente;
				}
				else
				{
					OutroPanel = PanelJogador;
				}

				Gabinete gabineteAleatorio = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)]; //Gera um gabinete aleatório da lista

				//Verifica se o gabinete selecionado aleatoriamente é diferente do gabinete ativo
				if ((PanelBaralho.GetComponentInChildren<DisplayGabinete>().gabinete != gabineteAleatorio) && (OutroPanel.GetComponentInChildren<DisplayGabinete>().gabinete != gabineteAleatorio))
				{
					PanelBaralho.GetComponentInChildren<DisplayGabinete>().gabinete = gabineteAleatorio;

					//Informa que houve alteração na carta
					if (PanelBaralho == PanelJogador)
					{
						JogadorCartaGabineteMudou = true;
						HardCashJogador--;
					}
					else if (PanelBaralho == PanelOponente)
					{
						OponenteCartaGabineteMudou = true;
						HardCashOponente--;
					}
				}
				else
				{
					//Chama a função recursivamente para gerar outro componente de forma aleatoria
					PodeInteragir = true;
					TrocarGabinete(PanelBaralho);
				}
				//Avança para o próximo estado e adiciona uma rodada
				Invoke("ProximoEstado", 2);
			}
		}
	}

	#endregion

	#region Compatibilidade

	//Testa a compatibilidade entre o processador e a placa-mãe baseado no socket
	public bool ProcessadorConectaPlacaMae(GameObject Panel)
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
	public bool MemoriaConectaPlacaMae(GameObject Panel)
	{
		//Verifica se os objetos estão ativos, se não estiverem não haverá como comparar
		if ((Panel.GetComponentInChildren<DisplayMemoria>() != null) && (Panel.GetComponentInChildren<DisplayPlacaMae>() != null))
		{
			Memoria memoria = Panel.GetComponentInChildren<DisplayMemoria>().memoria;
			PlacaMae placamae = Panel.GetComponentInChildren<DisplayPlacaMae>().placaMae;

			//Compara o DDR da memória ativa com o DDR da placa-mãe ativa
			if(memoria.DDR.Trim().ToLowerInvariant() == placamae.DDR.Trim().ToLowerInvariant())
			{
				//Compara a quantidade de memória suportada pela placa-mãe ativa com a quantidade de memória da memória ativa
				if(placamae.QuantidadeMemoria >= memoria.QuantidadeMemoria)
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
	public bool GabineteConectaPlacaMae(GameObject Panel)
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
	public bool PlacaDeVideoConectaPlacaMae(GameObject Panel)
	{
		//Verifica se os objetos estão ativos, se não estiverem não haverá como comparar
		if ((Panel.GetComponentInChildren<DisplayPlacaDeVideo>() != null) && (Panel.GetComponentInChildren<DisplayPlacaMae>() != null))
		{
			PlacaDeVideo placadevideo = Panel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo;
			PlacaMae placamae = Panel.GetComponentInChildren<DisplayPlacaMae>().placaMae;

			//Compara o suporte de SLI/Crossfire da placa-mãe com o SLI/Crossfire da placa de vídeo
			if(((placamae.SuporteSLI == placadevideo.SLI) || ((placamae.SuporteSLI == true) && (placadevideo.SLI == false))) && 
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
	public bool FonteConectaPlacaDeVideo(GameObject Panel)
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
