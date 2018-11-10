using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour {

	public TMP_Text JogadorNomeText;
	public GameObject PanelFinal;

	void Start ()
	{
		JogadorNomeText.text = "VITORIA DE " + StateMachine.NomeDoVencedor.ToUpperInvariant();
		CarregarCartas();
	}

	public void CarregarCartas()
	{
		PanelFinal.GetComponentInChildren<DisplayProcessador>().processador = StateMachine.ProcessadorVitorioso;
		PanelFinal.GetComponentInChildren<DisplayMemoria>().memoria = StateMachine.MemoriaVitorioso;
		PanelFinal.GetComponentInChildren<DisplayPlacaMae>().placaMae = StateMachine.PlacaMaeVitorioso;
		PanelFinal.GetComponentInChildren<DisplayPlacaDeVideo>().placaDeVideo = StateMachine.PlacaDeVideoVitorioso;
		PanelFinal.GetComponentInChildren<DisplayDisco>().disco = StateMachine.DiscoVitorioso;
		PanelFinal.GetComponentInChildren<DisplayFonte>().fonte = StateMachine.FonteVitorioso;
		PanelFinal.GetComponentInChildren<DisplayGabinete>().gabinete = StateMachine.GabineteVitorioso;

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
