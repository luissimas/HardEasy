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

	public static int TempoDaRodada = 31; //Variável para controlar o tempo da rodada atual
	public TMP_Text TimerText;

	//Gerencia o timer
	public void TimerManager()
	{
		if ((StateMachine.EstadoMudou) && (StateMachine.EstadoAtual != StateMachine.Estados.Fim))
		{
			CancelInvoke("SetTimer");
			TempoDaRodada = 31;
			StateMachine.EstadoMudou = false;
			InvokeRepeating("SetTimer", (float)0.0, (float)1.0);
		}
		else if ((StateMachine.EstadoMudou) && (StateMachine.EstadoAtual == StateMachine.Estados.Fim))
		{
			CancelInvoke("SetTimer");
			TimerText.enabled = false;
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
