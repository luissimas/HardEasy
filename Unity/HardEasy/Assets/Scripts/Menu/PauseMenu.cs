using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public static bool Pausado = false;
	public GameObject PauseMenuUI, OptionsMenuUI;
	
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
		Manager.PodeInteragir = true;
		PauseMenuUI.SetActive(false);
		Time.timeScale = 1;
		Pausado = false;
	}

	public void Pausar()
	{
		Manager.PodeInteragir = false;
		PauseMenuUI.SetActive(true);
		Time.timeScale = 0;
		Pausado = true;
	}

	public void CarregarOpcoes()
	{
		OptionsMenuUI.SetActive(true);
	}

	public void SairDoJogo()
	{
		OptionsMenuUI.SetActive(false);
		Continuar();

		SceneManager.LoadScene(0);
	}
}
