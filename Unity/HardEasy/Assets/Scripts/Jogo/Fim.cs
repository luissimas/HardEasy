using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour {

	public TMP_Text JogadorNomeText;
	public GameObject Panel;

	void Start ()
	{
		JogadorNomeText.text = "VITORIA DE " + StateMachine.NomeDoVencedor.ToUpperInvariant();
		CarregarCartas();
	}

	public void CarregarCartas()
	{
		Panel.GetComponentInChildren<DisplayProcessador>().processador = StateMachine.ProcessadorVitorioso;
		Panel.GetComponentInChildren<DisplayMemoria>().memoria = StateMachine.MemoriaVitorioso;
		Panel.GetComponentInChildren<DisplayPlacaMae>().placaMae = StateMachine.PlacaMaeVitorioso;
		Panel.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = StateMachine.PlacaDeVideoVitorioso;
		Panel.GetComponentInChildren<DisplayDisco>().disco = StateMachine.DiscoVitorioso;
		Panel.GetComponentInChildren<DisplayFonte>().fonte = StateMachine.FonteVitorioso;
		Panel.GetComponentInChildren<DisplayGabinete>().gabinete = StateMachine.GabineteVitorioso;

		Informacoes.JogadorCartaPlacaMaeMudou = true;
		Informacoes.JogadorCartaProcessadorMudou = true;
		Informacoes.JogadorCartaMemoriaMudou = true;
		Informacoes.JogadorCartaPlacaDeVideoMudou = true;
		Informacoes.JogadorCartaDiscoMudou = true;
		Informacoes.JogadorCartaFonteMudou = true;
		Informacoes.JogadorCartaGabineteMudou = true;
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
