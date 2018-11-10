using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{

	private void Start()
	{
		TimerText.enabled = true;
	}

	private void Update()
	{
		TimerManager();
	}

	public static int TempoDaRodada = Informacoes.tempoRodada + 1; //Variável para controlar o tempo da rodada atual
	public TMP_Text TimerText;
	public bool TimerPausou = false;

	//Gerencia o timer
	public void TimerManager()
	{
		if (((Informacoes.PanelComparar.transform.childCount == 2) || (Informacoes.PanelTrocar.transform.childCount == 1)) && TimerPausou == false)
		{
			CancelInvoke("SetTimer"); //Interrompe a função do timer antigo
			TimerPausou = true;
			return;
		}
		else
		{
			if (TimerPausou && ((Informacoes.PanelComparar.transform.childCount == 0) && (Informacoes.PanelTrocar.transform.childCount == 0)))
			{
				InvokeRepeating("SetTimer", (float)0.0, (float)1.0); //Inicia o timer novamente
				TimerPausou = false;
			}
		}

		//Verifica se a rodada mudou e se o jogo não acabou
		if ((StateMachine.EstadoMudou) && (StateMachine.EstadoAtual != StateMachine.Estados.Fim))
		{
			CancelInvoke("SetTimer"); //Interrompe a função do timer antigo
			TempoDaRodada = Informacoes.tempoRodada + 1; //Reseta o timer
			StateMachine.EstadoMudou = false;
			InvokeRepeating("SetTimer", (float)0.0, (float)1.0); //Inicia o timer novamente
		}
		else if ((StateMachine.EstadoMudou) && (StateMachine.EstadoAtual == StateMachine.Estados.Fim))
		{
			CancelInvoke("SetTimer"); //Interrompe o timer
			TimerText.enabled = false; //Esconde o timer na tela
		}
	}

	//Começa a contar a partir de 30 e avança para a próxima rodada caso o tempo se esgote
	public void SetTimer()
	{
		if (TempoDaRodada == 0)
		{
			CancelInvoke("SetTimer");
			StateMachine.ProximoEstado();
		}
		else
		{
			TempoDaRodada--;

			/*Verifica se o tempo é menor que 10, dessa forma é 
			possível formatar melhor a string com o tempo e 
			também mudar a cor para avisar o jogador que o tempo está se esgotando
			*/
			if (TempoDaRodada < 10)
			{
				TimerText.text = "0" + TempoDaRodada.ToString();
				TimerText.color = Color.red;
			}
			else
			{
				TimerText.text = TempoDaRodada.ToString();
				TimerText.color = Color.white;
			}
		}
	}
}
