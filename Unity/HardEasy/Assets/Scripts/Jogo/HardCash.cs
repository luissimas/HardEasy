using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HardCash : MonoBehaviour
{

	public TMP_Text HardCashJogadorText, HardCashOponenteText;

	public static int HardCashJogador = 0;
	public static int HardCashOponente = 0;

	void Update()
	{
		HardCashJogadorText.text = HardCashJogador.ToString();
		HardCashOponenteText.text = HardCashOponente.ToString();
	}
}
