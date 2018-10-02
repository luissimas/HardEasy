using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sliders : MonoBehaviour
{

	public Slider[] slidersCarta;

	public Color corOriginalSliders;
	public Color corDestaqueSliders;

	private void Awake()
	{
		ColetarCores();
	}

	public void ColetarCores()
	{
		for (int i = 0; i < slidersCarta[0].GetComponentsInChildren<RectTransform>().Length; i++)
		{
			if (slidersCarta[0].GetComponentsInChildren<RectTransform>()[i].gameObject.name == "Fill Area")
			{
				corOriginalSliders = slidersCarta[0].GetComponentsInChildren<RectTransform>()[i].GetComponentInChildren<Image>().color;
			}
		}
	}

	public void RetornarCores()
	{
		for (int i = 0; i < slidersCarta.Length; i++)
		{
			for (int j = 0; j < slidersCarta[i].GetComponentsInChildren<RectTransform>().Length; j++)
			{
				if (slidersCarta[i].GetComponentsInChildren<RectTransform>()[j].gameObject.name == "Fill Area")
				{
					slidersCarta[i].GetComponentsInChildren<RectTransform>()[j].GetComponentInChildren<Image>().color = corOriginalSliders;
				}
			}
		}
	}

	public void DestacarSlider(int indexSlider)
	{
		for (int i = 0; i < slidersCarta[indexSlider].GetComponentsInChildren<RectTransform>().Length; i++)
		{
			if (slidersCarta[indexSlider].GetComponentsInChildren<RectTransform>()[i].gameObject.name == "Fill Area")
			{
				slidersCarta[indexSlider].GetComponentsInChildren<RectTransform>()[i].GetComponentInChildren<Image>().color = corDestaqueSliders;
			}
		}
	}
}
