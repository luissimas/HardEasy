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
		ProximoEstadoAoTrocar(); //Avança uma rodada sempre que as cartas forem trocadas

		HardCashJogadorText.text = HardCashJogador.ToString();
		HardCashOponenteText.text = HardCashOponente.ToString();
	}

	#region Interação do Usuário

	public TMP_Text HardCashJogadorText, HardCashOponenteText;

	public static bool JogadorPodeInteragir = false;
	public static bool OponentePodeInteragir = false;
	public static bool PodeInteragir = true;

	public static bool Comparando = false; //Variável para gerenciar se as cartas estão sendo comparadas ou não
	public static bool Trocando = false; //Variável para gerenciar se as cartas estão sendo trocadas ou não

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
		else if (EstadoAtual == Estados.VezDoOponente)
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

	//Avança para o próximo estado após uma troca de cartas
	public void ProximoEstadoAoTrocar()
	{
		if (Trocando)
		{
			Trocando = false;
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

	#region Iniciar as cartas

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
