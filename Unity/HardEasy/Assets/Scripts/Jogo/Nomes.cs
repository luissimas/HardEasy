using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Nomes : MonoBehaviour {

	public TMP_Text JogadorNome;
	public TMP_Text OponenteNome;

	public void setNomes()
	{
		if((JogadorNome.text.Trim().Length > 2) && (OponenteNome.text.Trim().Length > 2))
		{
			MenuManager menuManager = new MenuManager();

			Informacoes.NomeJogador = JogadorNome.text;
			Informacoes.NomeOponente = OponenteNome.text;
			
			menuManager.Play();
		}
		else
		{
			Debug.Log("Os nomes devem ter mais de 2 caracteres cada!");
		}
		
	}
}
