using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
	//Variáveis para gerenciar a interação dos jogadores
	public static bool JogadorPodeInteragir = false;
	public static bool OponentePodeInteragir = false;
	public static bool PodeInteragir = true;

	public void ContinuarJogo()
	{
		Time.timeScale = 1f;

		Informacoes.btnContinuar.SetActive(false);
	}
}