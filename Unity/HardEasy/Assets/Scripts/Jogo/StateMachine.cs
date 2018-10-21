using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

	void Start()
	{
		EstadoAtual = Estados.Inicio; //Inicia o jogo no estado de início
	}

	private void Update()
	{
		GerenciadorDeRodadas(); //Verifica o gerenciador de rodadas a cada frame
		ProximoEstadoAoComparar(); //Avança uma rodada sempre que as cartas forem comparadas
		ProximoEstadoAoTrocar(); //Avança uma rodada sempre que as cartas forem trocadas
		JogadorText.text = TextVez;
	}

	public enum Estados { Inicio, VezDoJogador, VezDoOponente, Fim } //Enumerador contendo todos os estados de jogo possíveis

	public static Estados EstadoAtual; //Variável para identificar o estado atual do jogo
	public static int Rodada = 0; //Variável para identificar em qual rodada o jogo está
	public static bool EstadoMudou;

	public static bool Comparando = false; //Variável para gerenciar se as cartas estão sendo comparadas ou não
	public static bool Trocando = false; //Variável para gerenciar se as cartas estão sendo trocadas ou não

	public TMP_Text JogadorText;
	public static string TextVez;

	//Gerencia o sistema de rodadas 
	public static void GerenciadorDeRodadas()
	{
		//Identifica qual o estado atual do jogo
		switch (EstadoAtual)
		{
			case (Estados.Inicio):
				Lista.CarregarListas(); //Carega as listas dos componentes
				IniciarCartas.IniciarBaralho(Informacoes.PanelJogador); //Embaralha e inicia as cartas do jogador
				IniciarCartas.IniciarBaralho(Informacoes.PanelOponente); //Embaralha e inicia as cartas do oponente
				Verificar.VerificarCartas(Informacoes.PanelJogador, Informacoes.PanelOponente); //Verifica se as cartas do jogador e do oponente são iguais e as embaralha novamente
				EstadoMudou = true;
				Rodada++; //Avança uma rodada
				EstadoAtual = EstadoAtual + Random.Range(1, 3); //Escolhe quem irá começar jogando de forma aleatória
				break;
			case (Estados.VezDoJogador):
				Manager.JogadorPodeInteragir = true;
				Manager.OponentePodeInteragir = false;
				TextVez = "Vez de: " + Informacoes.NomeJogador1;
				break;
			case (Estados.VezDoOponente):
				Manager.JogadorPodeInteragir = false;
				Manager.OponentePodeInteragir = true;
				TextVez = "Vez de: " + Informacoes.NomeJogador2;
				break;
			case (Estados.Fim):
				Manager.JogadorPodeInteragir = false;
				Manager.OponentePodeInteragir = false;
				Manager.PodeInteragir = false;
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
}
