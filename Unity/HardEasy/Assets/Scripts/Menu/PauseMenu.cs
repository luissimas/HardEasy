using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

	public static bool Pausado = false;
	public GameObject PauseMenuUI;
	
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (Pausado)
			{
				Continuar();
			}
			else
			{
				Pausar();
			}
		}
	}

	public void Continuar()
	{
		Informacoes.CanvasPrincipal.SetActive(true);
		Manager.PodeInteragir = true;
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1;
		Pausado = false;
	}

	public void Pausar()
	{
		Informacoes.CanvasPrincipal.SetActive(false);
		Manager.PodeInteragir = false;
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0;
		Pausado = true;
	}

	public void CarregarOpcoes()
	{

	}

	public void SairDoJogo()
	{

	}
}
