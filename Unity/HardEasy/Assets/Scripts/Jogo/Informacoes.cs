using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Informacoes : MonoBehaviour
{

	//Variáveis que recebem o canvas do jogador e do oponente
	public GameObject CanvasPrincipalInspector;
	public static GameObject CanvasPrincipal;

	public GameObject PanelJogadorInspector, PanelOponenteInspector;
	public static GameObject PanelJogador, PanelOponente;

	public GameObject PanelCompararInspector, PanelTrocarInspector;
	public static GameObject PanelComparar, PanelTrocar;

	//Variáveis para identificar se houve mudança nas cartas
	public static bool JogadorCartaPlacaMaeMudou = false, JogadorCartaProcessadorMudou = false, JogadorCartaMemoriaMudou = false, JogadorCartaPlacaDeVideoMudou = false, JogadorCartaDiscoMudou = false, JogadorCartaFonteMudou = false, JogadorCartaGabineteMudou = false;
	public static bool OponenteCartaPlacaMaeMudou = false, OponenteCartaProcessadorMudou = false, OponenteCartaMemoriaMudou = false, OponenteCartaPlacaDeVideoMudou = false, OponenteCartaDiscoMudou = false, OponenteCartaFonteMudou = false, OponenteCartaGabineteMudou = false;

	//Variáveis para armazenar os nomes dos jogadores
	public static string NomeJogador;
	public static string NomeOponente;

	//Variáveis para armazenar o botão para continuar
	public GameObject btnContinuarInspector;
	public static GameObject btnContinuar;

	//Variavel para armazenar o tempo da rodada
	public static int tempoRodada;

	private void Awake()
	{
		SetPanel();
	}

	//Coloca as informações coletadas no isnpector nas variáveis static
	public void SetPanel()
	{
		PanelJogador = PanelJogadorInspector;
		PanelOponente = PanelOponenteInspector;

		CanvasPrincipal = CanvasPrincipalInspector;

		PanelComparar = PanelCompararInspector;
		PanelTrocar = PanelTrocarInspector;

		btnContinuar = btnContinuarInspector;
	} 
}
