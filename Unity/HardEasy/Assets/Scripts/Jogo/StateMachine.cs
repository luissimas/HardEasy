using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateMachine : MonoBehaviour
{

	void Start()
	{
		EstadoAtual = Estados.Inicio; //Inicia o jogo no estado de início
	}

	void Update()
	{
		GerenciadorDeRodadas(); //Verifica o gerenciador de rodadas a cada frame
		ProximoEstadoAoComparar(); //Avança uma rodada sempre que as cartas forem comparadas
		ProximoEstadoAoTrocar(); //Avança uma rodada sempre que as cartas forem trocadas
		JogadorText.text = TextVez;
	}

	private void OnEnable()
	{
		EstadoAtual = Estados.Inicio;
	}

	public enum Estados { Inicio, VezDoJogador, VezDoOponente, Fim } //Enumerador contendo todos os estados de jogo possíveis

	public static Estados EstadoAtual; //Variável para identificar o estado atual do jogo
	public static Estados EstadoVitorioso; //Variável para identificar quem ganhou o jogo
	public static int Rodada = 0; //Variável para identificar em qual rodada o jogo está
	public static bool EstadoMudou;

	public static string NomeDoVencedor;

	public static Processador ProcessadorVitorioso;
	public static Memoria MemoriaVitorioso;
	public static PlacaMae PlacaMaeVitorioso;
	public static PlacaDeVideo PlacaDeVideoVitorioso;
	public static Disco DiscoVitorioso;
	public static Fonte FonteVitorioso;
	public static Gabinete GabineteVitorioso;

	public static bool Comparando = false; //Variável para gerenciar se as cartas estão sendo comparadas ou não
	public static bool Trocando = false; //Variável para gerenciar se as cartas estão sendo trocadas ou não

	public TMP_Text JogadorText;
	public static string TextVez;

	//Gerencia o sistema de rodadas 
	public void GerenciadorDeRodadas()
	{
		//Identifica qual o estado atual do jogo
		switch (EstadoAtual)
		{
			case (Estados.Inicio):
				Manager.PodeInteragir = false;
				Lista.CarregarListas(); //Carega as listas dos componentes
				IniciarCartas.IniciarBaralho(Informacoes.PanelJogador); //Embaralha e inicia as cartas do jogador
				IniciarCartas.IniciarBaralho(Informacoes.PanelOponente); //Embaralha e inicia as cartas do oponente
				Verificar.VerificarCartas(Informacoes.PanelJogador, Informacoes.PanelOponente); //Verifica se as cartas do jogador e do oponente são iguais e as embaralha novamente

				resetarRodadas();

				Manager.PodeInteragir = true;
				EstadoMudou = true;
				Rodada++; //Avança uma rodada
				EstadoAtual = EstadoAtual + Random.Range(1, 3); //Escolhe quem irá começar jogando de forma aleatória
				break;
			case (Estados.VezDoJogador):
				Manager.JogadorPodeInteragir = true;
				Manager.OponentePodeInteragir = false;
				TextVez = "Vez de: " + Informacoes.NomeJogador;
				break;
			case (Estados.VezDoOponente):
				Manager.JogadorPodeInteragir = false;
				Manager.OponentePodeInteragir = true;
				TextVez = "Vez de: " + Informacoes.NomeOponente;
				break;
			case (Estados.Fim):
				Manager.JogadorPodeInteragir = false;
				Manager.OponentePodeInteragir = false;
				Manager.PodeInteragir = false;

				if (EstadoVitorioso == Estados.VezDoJogador)
				{
					NomeDoVencedor = Informacoes.NomeJogador;

					ProcessadorVitorioso = Informacoes.PanelJogador.gameObject.GetComponentInChildren<DisplayProcessador>().processador;
					MemoriaVitorioso = Informacoes.PanelJogador.gameObject.GetComponentInChildren<DisplayMemoria>().memoria;
					PlacaMaeVitorioso = Informacoes.PanelJogador.gameObject.GetComponentInChildren<DisplayPlacaMae>().placaMae;
					PlacaDeVideoVitorioso = Informacoes.PanelJogador.gameObject.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo;
					DiscoVitorioso = Informacoes.PanelJogador.gameObject.GetComponentInChildren<DisplayDisco>().disco;
					FonteVitorioso = Informacoes.PanelJogador.gameObject.GetComponentInChildren<DisplayFonte>().fonte;
					GabineteVitorioso = Informacoes.PanelJogador.gameObject.GetComponentInChildren<DisplayGabinete>().gabinete;
				}
				else if (EstadoVitorioso == Estados.VezDoOponente)
				{
					NomeDoVencedor = Informacoes.NomeOponente;

					ProcessadorVitorioso = Informacoes.PanelOponente.gameObject.GetComponentInChildren<DisplayProcessador>().processador;
					MemoriaVitorioso = Informacoes.PanelOponente.gameObject.GetComponentInChildren<DisplayMemoria>().memoria;
					PlacaMaeVitorioso = Informacoes.PanelOponente.gameObject.GetComponentInChildren<DisplayPlacaMae>().placaMae;
					PlacaDeVideoVitorioso = Informacoes.PanelOponente.gameObject.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo;
					DiscoVitorioso = Informacoes.PanelOponente.gameObject.GetComponentInChildren<DisplayDisco>().disco;
					FonteVitorioso = Informacoes.PanelOponente.gameObject.GetComponentInChildren<DisplayFonte>().fonte;
					GabineteVitorioso = Informacoes.PanelOponente.gameObject.GetComponentInChildren<DisplayGabinete>().gabinete;
				}

				StartCoroutine(CarregarATelaFinal());
				
				break;
		}

	}

	//Avança para o próximo estado baseado no estado e nas condições de jogo atuais
	public static void ProximoEstado()
	{
		//Verifica em qual estado o jogo está no momento
		if (EstadoAtual == Estados.VezDoJogador)
		{
			//Verifica se o jogador venceu o jogo
			if ((Compatibilidade.ProcessadorConectaPlacaMae(Informacoes.PanelJogador) && Compatibilidade.MemoriaConectaPlacaMae(Informacoes.PanelJogador) && Compatibilidade.GabineteConectaPlacaMae(Informacoes.PanelJogador)
				&& Compatibilidade.PlacaDeVideoConectaPlacaMae(Informacoes.PanelJogador) && Compatibilidade.FonteConectaPlacaDeVideo(Informacoes.PanelJogador)))
			{
				//Finaliza o jogo
				EstadoVitorioso = EstadoAtual;
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
			if ((Compatibilidade.ProcessadorConectaPlacaMae(Informacoes.PanelOponente) && Compatibilidade.MemoriaConectaPlacaMae(Informacoes.PanelOponente) && Compatibilidade.GabineteConectaPlacaMae(Informacoes.PanelOponente)
				&& Compatibilidade.PlacaDeVideoConectaPlacaMae(Informacoes.PanelOponente) && Compatibilidade.FonteConectaPlacaDeVideo(Informacoes.PanelOponente)))
			{
				//Finaliza o jogo
				EstadoVitorioso = EstadoAtual;
				EstadoAtual = Estados.Fim;
			}
			else
			{
				//Avança para o próximo estado normalmente
				Rodada++;
				EstadoAtual = Estados.VezDoJogador;
			}
		}

		EstadoMudou = true;

		Manager.PodeInteragir = true;
	}

	//Avança para o próximo estado após uma comparação de cartas
	public static void ProximoEstadoAoComparar()
	{
		if (Comparando)
		{
			Comparando = false;
			ProximoEstado();
		}
	}

	//Avança para o próximo estado após uma troca de cartas
	public static void ProximoEstadoAoTrocar()
	{
		if (Trocando)
		{
			Trocando = false;
			ProximoEstado();
		}
	}

	public IEnumerator CarregarATelaFinal()
	{
		yield return new WaitForSeconds(1f);

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void resetarRodadas()
	{
		Comparar.RodadaPlacaMaeJogador = 0;
		Comparar.RodadaProcessadorJogador = 0;
		Comparar.RodadaMemoriaJogador = 0;
		Comparar.RodadaPlacaDeVideoJogador = 0;
		Comparar.RodadaDiscoJogador = 0;
		Comparar.RodadaFonteJogador = 0;
		Comparar.RodadaGabineteJogador = 0;

		Comparar.RodadaPlacaMaeOponente = 0;
		Comparar.RodadaProcessadorOponente = 0;
		Comparar.RodadaMemoriaOponente = 0;
		Comparar.RodadaPlacaDeVideoOponente = 0;
		Comparar.RodadaDiscoOponente = 0;
		Comparar.RodadaFonteOponente = 0;
		Comparar.RodadaGabineteOponente = 0;

		HardCash.HardCashJogador = 0;
		HardCash.HardCashOponente = 0;
		Rodada = 0;
	}
}
