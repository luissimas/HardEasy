using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Nomes : MonoBehaviour {

	public TMP_Text Jogador1Nome;
	public TMP_Text Jogador2nome;

	public void setNomes()
	{
		if((Jogador1Nome.text.Trim().Length > 2) && (Jogador2nome.text.Trim().Length > 2))
		{
			MenuManager menuManager = new MenuManager();

			Informacoes.NomeJogador1 = Jogador1Nome.text;
			Informacoes.NomeJogador2 = Jogador2nome.text;
			
			menuManager.Play();
		}
		else
		{
			Debug.Log("Os nomes devem ter mais de 2 caracteres cada!");
		}
		
	}
}
