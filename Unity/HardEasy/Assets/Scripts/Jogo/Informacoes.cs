using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Informacoes : MonoBehaviour
{

	public GameObject CanvasPrincipalInspector;
	public static GameObject CanvasPrincipal;
	public GameObject PanelJogadorInspector, PanelOponenteInspector;//Variáveis que recebem o canvas do jogador e do oponente
	public static GameObject PanelJogador, PanelOponente;

	//Variáveis para identificar se houve mudança nas cartas
	public static bool JogadorCartaPlacaMaeMudou = false, JogadorCartaProcessadorMudou = false, JogadorCartaMemoriaMudou = false, JogadorCartaPlacaDeVideoMudou = false, JogadorCartaDiscoMudou = false, JogadorCartaFonteMudou = false, JogadorCartaGabineteMudou = false;
	public static bool OponenteCartaPlacaMaeMudou = false, OponenteCartaProcessadorMudou = false, OponenteCartaMemoriaMudou = false, OponenteCartaPlacaDeVideoMudou = false, OponenteCartaDiscoMudou = false, OponenteCartaFonteMudou = false, OponenteCartaGabineteMudou = false;

	private void Start()
	{
		SetPanel();
	}

	public void SetPanel()
	{
		PanelJogador = PanelJogadorInspector;
		PanelOponente = PanelOponenteInspector;
		CanvasPrincipal = CanvasPrincipalInspector;
	}
}
