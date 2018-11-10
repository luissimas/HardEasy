using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Nomes : MonoBehaviour {

	public TMP_Text JogadorNome;
	public TMP_Text OponenteNome;
	public TMP_Text tempoText;
	public Slider tempoSlider;

	private void Start()
	{
		tempoSlider.value = 30;
	}

	private void Update()
	{
		tempoText.text = "Tempo da rodada: " + tempoSlider.value.ToString();
	}

	public void setNomes()
	{
		if((JogadorNome.text.Trim().Length > 2) && (OponenteNome.text.Trim().Length > 2))
		{
			MenuManager menuManager = new MenuManager();

			Informacoes.NomeJogador = JogadorNome.text;
			Informacoes.NomeOponente = OponenteNome.text;

			Informacoes.tempoRodada = (int)tempoSlider.value;
			
			menuManager.Play();
		}
		else
		{
			Debug.Log("Os nomes devem ter mais de 2 caracteres cada!");
		}
		
	}
}
