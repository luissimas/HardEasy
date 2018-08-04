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

	public static bool PodeInteragir = false; //Variável para gerenciar se os jogadores podem ou não interagir
	public static bool Comparando = false; //Variável para gerenciar se as cartas estão sendo comparadas ou não


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
				PodeInteragir = false; //Desabilita a interação do usuário com a interface
				Lista.CarregarListas(); //Carega as listas dos componentes
				DropArea = DropAreaInspector;
				IniciarCartas(PanelJogador); //Embaralha e inicia as cartas do jogador
				IniciarCartas(PanelOponente); //Embaralha e inicia as cartas do oponente
				VerificarAsCartasSeSaoIguais(PanelJogador, PanelOponente); //Verifica se as cartas do jogador e do oponente são iguais
				Rodada++; //Avança uma rodada
				EstadoAtual = EstadoAtual + Random.Range(1, 3); //Escolhe quem irá começar jogando de forma aleatória
				break;
			case (Estados.VezDoJogador):
				PodeInteragir = true; //Habilita a interação do usuário com a interface
				break;
			case (Estados.VezDoOponente):
				PodeInteragir = true; //Habilita a interação do usuário com a interface
				break;
			case (Estados.Fim):
				PodeInteragir = false; //Desabilita a interação do usuário com a interface
				break;
		}
	}

	//Avança para o próximo estado baseado no estado e nas condições de jogo atuais
	public void ProximoEstado()
	{
		//Verifica em qual estado o jogo está no momento
		if (EstadoAtual == Estados.VezDoJogador)
		{
			//Verifica se o jogador venceu o jogo
			if (!(ProcessadorConectaPlacaMae(PanelJogador) && MemoriaConectaPlacaMae(PanelJogador) && GabineteConectaPlacaMae(PanelJogador) 
				&& PlacaDeVideoConectaPlacaMae(PanelJogador) && FonteConectaPlacaDeVideo(PanelJogador)))
			{
				//Avança para o próximo estado normalmente
				Rodada++;
				EstadoAtual = Estados.VezDoOponente;
			}
			else
			{
				//Finaliza o jogo
				EstadoAtual = Estados.Fim;
			}
		}
		else if (EstadoAtual==Estados.VezDoOponente)
		{
			//Verifica se o oponente venceu o jogo
			if (!(ProcessadorConectaPlacaMae(PanelOponente) && MemoriaConectaPlacaMae(PanelOponente) && GabineteConectaPlacaMae(PanelOponente)
				&& PlacaDeVideoConectaPlacaMae(PanelOponente) && FonteConectaPlacaDeVideo(PanelOponente)))
			{
				//Avança para o próximo estado normalmente
				Rodada++;
				EstadoAtual = Estados.VezDoJogador;
			}
			else
			{
				//Finaliza o jogo
				EstadoAtual = Estados.Fim;
			}
		}
	}

	//Avança para o próximo estado após uma comparação de cartas
	public void ProximoEstadoAoComparar()
	{
		if (Comparando)
		{
			ProximoEstado();
		}

		Comparando = false;
	}

	#endregion

	#region Iniciar e trocar as cartas

	public GameObject PanelJogador, PanelOponente;  //Variáveis que recebem o canvas do jogador e do oponente
	//Variáveis para identificar se houve mudança nas cartas
	public static bool JogadorCartaPlacaMaeMudou = false, JogadorCartaProcessadorMudou = false, JogadorCartaMemoriaMudou = false, JogadorCartaPlacaDeVideoMudou = false, JogadorCartaDiscoMudou = false, JogadorCartaFonteMudou = false, JogadorCartaGabineteMudou = false; 
	public static bool OponenteCartaPlacaMaeMudou = false, OponenteCartaProcessadorMudou = false, OponenteCartaMemoriaMudou = false, OponenteCartaPlacaDeVideoMudou = false, OponenteCartaDiscoMudou = false, OponenteCartaFonteMudou = false, OponenteCartaGabineteMudou = false;

	//Verifica se as cartas do jogador e do oponente são iguais
	public void VerificarAsCartasSeSaoIguais(GameObject Panel, GameObject OutroPanel)
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
		GameObject OutroPanel;

		if (PanelBaralho.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		if (PodeInteragir)
		{
			if(PanelBaralho.gameObject.tag == "PlayerCard")
			{
				if(HardCashJogador == 0)
				{
					return;
				}
			}
			else if(PanelBaralho.gameObject.tag == "OpponentCard")
			{
				if (HardCashOponente == 0)
				{
					return;
				}
			}

			PlacaMae placamaeAleatoria = Lista.ListaPlacaMae[Random.Range(0, Lista.ListaPlacaMae.Count)]; //Gera uma placa-mãe aleatória da lista

			//Verifica se a placa-mãe selecionada aleatoriamente é diferente da placa-mãe ativa
			if (PanelBaralho.GetComponentInChildren<DisplayPlacaMae>().placaMae != placamaeAleatoria)
			{
				PanelBaralho.GetComponentInChildren<DisplayPlacaMae>().placaMae = placamaeAleatoria;

				VerificarPlacaMaeIgual(PanelBaralho, OutroPanel);

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
				TrocarPlacaMae(PanelBaralho);
			}
			//Avança para o próximo estado e adiciona uma rodada			
			ProximoEstado();
		}
	}

	//Troca a carta do tipo processador por outra carta aleatória do mesmo tipo
	public void TrocarProcessador(GameObject PanelBaralho)
	{
		GameObject OutroPanel;

		if (PanelBaralho.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		if (PodeInteragir)
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

			Processador processadorAleatorio = Lista.ListaProcessador[Random.Range(0, Lista.ListaProcessador.Count)]; //Gera um processado aleatório da lista

			//Verifica se o processador selecionado aleatoriamente é diferente do processador ativo
			if (PanelBaralho.GetComponentInChildren<DisplayProcessador>().processador != processadorAleatorio)
			{
				PanelBaralho.GetComponentInChildren<DisplayProcessador>().processador = processadorAleatorio;

				VerificarProcessadorIgual(PanelBaralho, OutroPanel);

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
				TrocarProcessador(PanelBaralho);
			}
			//Avança para o próximo estado e adiciona uma rodada
			ProximoEstado();
		}
	}

	//Troca a carta do tipo memóia por outra carta aleatória do mesmo tipo
	public void TrocarMemoria(GameObject PanelBaralho)
	{
		GameObject OutroPanel;

		if (PanelBaralho.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		if (PodeInteragir)
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

			Memoria memoriaAleatoria = Lista.ListaMemoria[Random.Range(0, Lista.ListaMemoria.Count)]; //Gera uma memória aleatória da lista

			//Verfica se a memória selecionada aleatoriamente é diferente da memória ativa
			if (PanelBaralho.GetComponentInChildren<DisplayMemoria>().memoria != memoriaAleatoria)
			{
				PanelBaralho.GetComponentInChildren<DisplayMemoria>().memoria = memoriaAleatoria;

				VerificarMemoriaIgual(PanelBaralho, OutroPanel);

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
				TrocarMemoria(PanelBaralho);
			}
			//Avança para o próximo estado e adiciona uma rodada
			ProximoEstado();
		}
	}

	//Troca a carta do tipo placa de vídeo por outra carta aleatória do mesmo tipo
	public void TrocarPlacaDeVideo(GameObject PanelBaralho)
	{
		GameObject OutroPanel;

		if (PanelBaralho.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		if (PodeInteragir)
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

			PlacaDeVideo placadevideoAleatoria = Lista.ListaPlacaDeVideo[Random.Range(0, Lista.ListaPlacaDeVideo.Count)]; //Gera uma placa de vídeo aleatória da lista

			//Verifica se a placa de vídeo selecionada aleatoriamente é diferente da placa de vídeo ativa
			if (PanelBaralho.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo != placadevideoAleatoria)
			{
				PanelBaralho.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = placadevideoAleatoria;

				VerificarPlacaDeVideoIgual(PanelBaralho, OutroPanel);

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
				TrocarPlacaDeVideo(PanelBaralho);
			}
			//Avança para o próximo estado e adiciona uma rodada
			ProximoEstado();
		}
	}

	//Troca a carta do tipo disco por outra carta aleatória do mesmo tipo
	public void TrocarDisco(GameObject PanelBaralho)
	{
		GameObject OutroPanel;

		if (PanelBaralho.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		if (PodeInteragir)
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

			Disco discoAleatorio = Lista.ListaDisco[Random.Range(0, Lista.ListaDisco.Count)]; //Gera um disco aleatório da lista

			//Verifica se o disco selecionado aleatoriamente é diferente do disco ativo
			if (PanelBaralho.GetComponentInChildren<DisplayDisco>().disco != discoAleatorio)
			{
				PanelBaralho.GetComponentInChildren<DisplayDisco>().disco = discoAleatorio;

				VerificarDiscoIgual(PanelBaralho, OutroPanel);

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
				TrocarDisco(PanelBaralho);
			}
			//Avança para o próximo estado e adiciona uma rodada
			ProximoEstado();
		}
	}

	//Troca a carta do tipo fonte por outra carta aleatória do mesmo tipo
	public void TrocarFonte(GameObject PanelBaralho)
	{
		GameObject OutroPanel;

		if (PanelBaralho.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		if (PodeInteragir)
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

			Fonte fonteAleatoria = Lista.ListaFonte[Random.Range(0, Lista.ListaFonte.Count)]; //Gera uma fonte aleatória da lista

			//Verifica se a fonte selecionada aleatoriamente é diferente da fonte ativa
			if (PanelBaralho.GetComponentInChildren<DisplayFonte>().fonte != fonteAleatoria)
			{
				PanelBaralho.GetComponentInChildren<DisplayFonte>().fonte = fonteAleatoria;

				VerificarFonteIgual(PanelBaralho, OutroPanel);

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
				TrocarFonte(PanelBaralho);
			}
			//Avança para o próximo estado e adiciona uma rodada
			ProximoEstado();
		}
	}

	//Troca a carta do tipo gabinete por outra carta aleatória do mesmo tipo
	public void TrocarGabinete(GameObject PanelBaralho)
	{
		GameObject OutroPanel;

		if (PanelBaralho.gameObject.tag == "PlayerCard")
		{
			OutroPanel = PanelOponente;
		}
		else
		{
			OutroPanel = PanelJogador;
		}

		if (PodeInteragir)
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

			Gabinete gabineteAleatorio = Lista.ListaGabinete[Random.Range(0, Lista.ListaGabinete.Count)]; //Gera um gabinete aleatório da lista

			//Verifica se o gabinete selecionado aleatoriamente é diferente do gabinete ativo
			if (PanelBaralho.GetComponentInChildren<DisplayGabinete>().gabinete != gabineteAleatorio)
			{
				PanelBaralho.GetComponentInChildren<DisplayGabinete>().gabinete = gabineteAleatorio;

				VerificarGabineteIgual(PanelBaralho, OutroPanel);

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
				TrocarGabinete(PanelBaralho);
			}
			//Avança para o próximo estado e adiciona uma rodada
			ProximoEstado();
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

	#region Comparação de Atributos
	public static int HardCashJogador = 0;
	public static int HardCashOponente = 0;
 
	public static void CompararPlacaMae()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayPlacaMae CartaJogador = DropArea.GetComponentsInChildren<DisplayPlacaMae>()[0], CartaOponente = DropArea.GetComponentsInChildren<DisplayPlacaMae>()[0];

		//Identifica de quem é cada carta
		for(int i = 0; i < DropArea.GetComponentsInChildren<DisplayPlacaMae>().Length; i++)
		{
			if (DropArea.GetComponentsInChildren<DisplayPlacaMae>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = DropArea.GetComponentsInChildren<DisplayPlacaMae>()[i];
			}
			else if (DropArea.GetComponentsInChildren<DisplayPlacaMae>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = DropArea.GetComponentsInChildren<DisplayPlacaMae>()[i];
			}
		}

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: quantidade de memória");

				if (CartaJogador.AtributoQuantidadeMemoriaSlider.value > CartaOponente.AtributoQuantidadeMemoriaSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoQuantidadeMemoriaSlider.value < CartaOponente.AtributoQuantidadeMemoriaSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoPortasSataSlider.value < CartaOponente.AtributoPortasSataSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoSlotsPCIESlider.value < CartaOponente.AtributoSlotsPCIESlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;
		}

		Comparando = true;

	}

	public static void CompararProcessador()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayProcessador CartaJogador = DropArea.GetComponentsInChildren<DisplayProcessador>()[0], CartaOponente = DropArea.GetComponentsInChildren<DisplayProcessador>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < DropArea.GetComponentsInChildren<DisplayProcessador>().Length; i++)
		{
			if (DropArea.GetComponentsInChildren<DisplayProcessador>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = DropArea.GetComponentsInChildren<DisplayProcessador>()[i];
			}
			else if (DropArea.GetComponentsInChildren<DisplayProcessador>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = DropArea.GetComponentsInChildren<DisplayProcessador>()[i];
			}
		}

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: desempenho singlecore");

				if (CartaJogador.AtributoDesempenhoSingleCoreSlider.value > CartaOponente.AtributoDesempenhoSingleCoreSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoDesempenhoSingleCoreSlider.value < CartaOponente.AtributoDesempenhoSingleCoreSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoDesempenhoMultiCoreSlider.value < CartaOponente.AtributoDesempenhoMultiCoreSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoConsumoSlider.value < CartaOponente.AtributoConsumoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;
		}

		Comparando = true;

	}

	public static void CompararMemoria()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayMemoria CartaJogador = DropArea.GetComponentsInChildren<DisplayMemoria>()[0], CartaOponente = DropArea.GetComponentsInChildren<DisplayMemoria>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < DropArea.GetComponentsInChildren<DisplayMemoria>().Length; i++)
		{
			if (DropArea.GetComponentsInChildren<DisplayMemoria>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = DropArea.GetComponentsInChildren<DisplayMemoria>()[i];
			}
			else if (DropArea.GetComponentsInChildren<DisplayMemoria>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = DropArea.GetComponentsInChildren<DisplayMemoria>()[i];
			}
		}

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: quantidade de memoria");

				if (CartaJogador.AtributoQuantidadeMemoriaSlider.value > CartaOponente.AtributoQuantidadeMemoriaSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoQuantidadeMemoriaSlider.value < CartaOponente.AtributoQuantidadeMemoriaSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoDesempenhoSlider.value < CartaOponente.AtributoDesempenhoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoLatenciaSlider.value < CartaOponente.AtributoLatenciaSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;
		}

		Comparando = true;

	}

	public static void CompararPlacaDeVideo()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayPlacaDeVideo CartaJogador = DropArea.GetComponentsInChildren<DisplayPlacaDeVideo>()[0], CartaOponente = DropArea.GetComponentsInChildren<DisplayPlacaDeVideo>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < DropArea.GetComponentsInChildren<DisplayPlacaDeVideo>().Length; i++)
		{
			if (DropArea.GetComponentsInChildren<DisplayPlacaDeVideo>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = DropArea.GetComponentsInChildren<DisplayPlacaDeVideo>()[i];
			}
			else if (DropArea.GetComponentsInChildren<DisplayPlacaDeVideo>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = DropArea.GetComponentsInChildren<DisplayPlacaDeVideo>()[i];
			}
		}

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: desempenho");

				if (CartaJogador.AtributoDesempenhoSlider.value > CartaOponente.AtributoDesempenhoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoDesempenhoSlider.value < CartaOponente.AtributoDesempenhoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoMemoriaSlider.value < CartaOponente.AtributoMemoriaSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoConsumoSlider.value < CartaOponente.AtributoConsumoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;
		}

		Comparando = true;

	}

	public static void CompararDisco()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayDisco CartaJogador = DropArea.GetComponentsInChildren<DisplayDisco>()[0], CartaOponente = DropArea.GetComponentsInChildren<DisplayDisco>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < DropArea.GetComponentsInChildren<DisplayDisco>().Length; i++)
		{
			if (DropArea.GetComponentsInChildren<DisplayDisco>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = DropArea.GetComponentsInChildren<DisplayDisco>()[i];
			}
			else if (DropArea.GetComponentsInChildren<DisplayDisco>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = DropArea.GetComponentsInChildren<DisplayDisco>()[i];
			}
		}

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: capacidade de armazenamento");

				if (CartaJogador.AtributoCapacidadeDeArmazenamentoSlider.value > CartaOponente.AtributoCapacidadeDeArmazenamentoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoCapacidadeDeArmazenamentoSlider.value < CartaOponente.AtributoCapacidadeDeArmazenamentoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoVelocidadeSlider.value < CartaOponente.AtributoVelocidadeSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoCustoPorGBSlider.value < CartaOponente.AtributoCustoPorGBSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;
		}

		Comparando = true;

	}

	public static void CompararFonte()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayFonte CartaJogador = DropArea.GetComponentsInChildren<DisplayFonte>()[0], CartaOponente = DropArea.GetComponentsInChildren<DisplayFonte>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < DropArea.GetComponentsInChildren<DisplayFonte>().Length; i++)
		{
			if (DropArea.GetComponentsInChildren<DisplayFonte>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = DropArea.GetComponentsInChildren<DisplayFonte>()[i];
			}
			else if (DropArea.GetComponentsInChildren<DisplayFonte>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = DropArea.GetComponentsInChildren<DisplayFonte>()[i];
			}
		}

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: potência");

				if (CartaJogador.AtributoPotenciaSlider.value > CartaOponente.AtributoPotenciaSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoPotenciaSlider.value < CartaOponente.AtributoPotenciaSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoEficienciaSlider.value < CartaOponente.AtributoEficienciaSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoCustoPorWSlider.value < CartaOponente.AtributoCustoPorWSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;
		}

		Comparando = true;

	}

	public static void CompararGabinete()
	{
		int atributoIndex = Random.Range(1, 5); //Escolhe aleatoriamente qual atributo será comparado
		DisplayGabinete CartaJogador = DropArea.GetComponentsInChildren<DisplayGabinete>()[0], CartaOponente = DropArea.GetComponentsInChildren<DisplayGabinete>()[0];

		//Identifica de quem é cada carta
		for (int i = 0; i < DropArea.GetComponentsInChildren<DisplayGabinete>().Length; i++)
		{
			if (DropArea.GetComponentsInChildren<DisplayGabinete>()[i].gameObject.tag == "PlayerCard")
			{
				CartaJogador = DropArea.GetComponentsInChildren<DisplayGabinete>()[i];
			}
			else if (DropArea.GetComponentsInChildren<DisplayGabinete>()[i].gameObject.tag == "OpponentCard")
			{
				CartaOponente = DropArea.GetComponentsInChildren<DisplayGabinete>()[i];
			}
		}

		//Compara as cartas se baseando no atributo gerado aleatoriamente
		switch (atributoIndex)
		{
			case 1:
				Debug.Log("Atributo a ser comparado: refrigeração");

				if (CartaJogador.AtributoRefrigeracaoSlider.value > CartaOponente.AtributoRefrigeracaoSlider.value)
				{
					Debug.Log("Jogador venceu a comparação!");
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoRefrigeracaoSlider.value < CartaOponente.AtributoRefrigeracaoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoSlotsPCISlider.value < CartaOponente.AtributoSlotsPCISlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoBaiasHDSlider.value < CartaOponente.AtributoBaiasHDSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
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
					HardCashJogador++;
				}
				else if (CartaJogador.AtributoPrecoSlider.value < CartaOponente.AtributoPrecoSlider.value)
				{
					Debug.Log("Oponente venceu a comparação!");
					HardCashOponente++;
				}
				else
				{
					Debug.Log("Empate!");
				}
				break;
		}

		Comparando = true;

	}

	#endregion
}
