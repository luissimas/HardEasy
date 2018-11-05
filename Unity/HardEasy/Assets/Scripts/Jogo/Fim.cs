using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour {

	public TMP_Text JogadorNomeText;

	void Start ()
	{
		JogadorNomeText.text = StateMachine.NomeDoVencedor + "venceu o jogo!";
		CarregarCartas();
	}

	public void CarregarCartas()
	{
		GetComponentInChildren<DisplayProcessador>().processador = StateMachine.ProcessadorVitorioso;
		GetComponentInChildren<DisplayMemoria>().memoria = StateMachine.MemoriaVitorioso;
		GetComponentInChildren<DisplayPlacaMae>().placaMae = StateMachine.PlacaMaeVitorioso;
		GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = StateMachine.PlacaDeVideoVitorioso;
		GetComponentInChildren<DisplayDisco>().disco = StateMachine.DiscoVitorioso;
		GetComponentInChildren<DisplayFonte>().fonte = StateMachine.FonteVitorioso;
		GetComponentInChildren<DisplayGabinete>().gabinete = StateMachine.GabineteVitorioso;
	}

	public void JogarNovamente()
	{
		SceneManager.LoadScene(1);
	}

	public void MenuPrincipal()
	{
		SceneManager.LoadScene(0);
	}

	public void Sair()
	{
		Debug.Log("Quit");
		Application.Quit();
	}
}
