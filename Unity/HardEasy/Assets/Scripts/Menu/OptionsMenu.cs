using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

	public Slider SliderVolumePrincipal;
	public Slider SliderVolumeDaMusica;
	public TMP_Text TextVolumePrincial;
	public TMP_Text TextVolumeDaMusica;

	private void Update()
	{
		TextVolumePrincial.text = SliderVolumePrincipal.value.ToString() + "%";
		TextVolumeDaMusica.text = SliderVolumeDaMusica.value.ToString() + "%";
	}

	public void Redefinir()
	{
		SliderVolumeDaMusica.value = 50;
		SliderVolumePrincipal.value = 50;
	}

	public void Confirmar()
	{
		Debug.Log("Configurações alteradas com sucesso!");
	}
}
