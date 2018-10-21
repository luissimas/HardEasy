using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

	public GameObject MenuPrincipalInspector, MenuOpcoesInspector, MenuTutorialInspector;
	public static GameObject MenuPrincipal, MenuOpcoes, MenuTutorial;

	private void Start()
	{
		SetMenu();
	}

	public void SetMenu()
	{
		MenuPrincipal = MenuPrincipalInspector;
		MenuOpcoes = MenuOpcoesInspector;
		MenuTutorial = MenuTutorialInspector;
	}

	public void Play()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Opcoes()
	{
		if (MenuOpcoes.activeSelf)
		{
			MenuOpcoes.SetActive(false);
			MenuTutorial.SetActive(false);
		}
		else
		{
			MenuOpcoes.SetActive(true);
			MenuTutorial.SetActive(false);
		}
	}

	public void Tutorial()
	{
		if (MenuTutorial.activeSelf)
		{
			MenuTutorial.SetActive(false);
			MenuOpcoes.SetActive(false);
		}
		else
		{
			MenuTutorial.SetActive(true);
			MenuOpcoes.SetActive(false);
		}
	}

	public void Sair()
	{
		Debug.Log("Quit");
		Application.Quit();
	}
}
