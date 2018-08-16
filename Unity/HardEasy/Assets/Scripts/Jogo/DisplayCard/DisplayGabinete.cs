using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisplayGabinete : MonoBehaviour{

	public Gabinete gabinete;

	public TMP_Text NomeText;
	public TMP_Text FormatoText;
	public TMP_Text FansText;
	public TMP_Text SlotsText;

	public Image Imagem;

	public TMP_Text AtributoRefrigeracaoText;
	public TMP_Text AtributoSlotsPCIText;
	public TMP_Text AtributoBaiasHDText;
	public TMP_Text AtributoPrecoText;

	public Slider AtributoRefrigeracaoSlider;
	public Slider AtributoSlotsPCISlider;
	public Slider AtributoBaiasHDSlider;
	public Slider AtributoPrecoSlider;

	void Start() 
	{
		
	}

	void Update()
	{
		//Verifica se houve alguma alteração na carta se baseando em sua tag
		if (gameObject.tag == "PlayerCard")
		{
			if (Informacoes.JogadorCartaGabineteMudou)
			{
				ExibirInformacoes();
				GerarAtributoRefrigeracao();
				GerarAtributoSlotsPCI();
				GerarAtributoBaiasHD();
				GerarAtributoPreco();

				Informacoes.JogadorCartaGabineteMudou = false;
			}
		}
		else if (gameObject.tag == "OpponentCard")
		{
			if (Informacoes.OponenteCartaGabineteMudou)
			{
				ExibirInformacoes();
				GerarAtributoRefrigeracao();
				GerarAtributoSlotsPCI();
				GerarAtributoBaiasHD();
				GerarAtributoPreco();

				Informacoes.OponenteCartaGabineteMudou = false;
			}
		}
	}

	//Exibe as informações do Scriptable Object
	public void ExibirInformacoes()
	{
		NomeText.text = gabinete.Nome;
		FormatoText.text = "Formato: " + gabinete.FormatoPlacaMae;
		FansText.text = "Fans: " + gabinete.QuantidadeDeFans.ToString();
		SlotsText.text = "Slots: " + gabinete.SlotsPCI.ToString();

		Imagem.sprite = gabinete.Imagem;
	}

	#region Gerar Atributos
	private void GerarAtributoRefrigeracao()
	{
		float MaiorQuantidadeDeFans = Lista.ListaGabinete[0].QuantidadeDeFans, MenorQuantidadeDeFans = Lista.ListaGabinete[0].QuantidadeDeFans;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for(int i = 0; i < Lista.ListaGabinete.Count; i++)
		{
			if(Lista.ListaGabinete[i].QuantidadeDeFans > MaiorQuantidadeDeFans)
			{
				MaiorQuantidadeDeFans = Lista.ListaGabinete[i].QuantidadeDeFans;
			}

			if (Lista.ListaGabinete[i].QuantidadeDeFans < MenorQuantidadeDeFans)
			{
				MenorQuantidadeDeFans = Lista.ListaGabinete[i].QuantidadeDeFans;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((gabinete.QuantidadeDeFans - MenorQuantidadeDeFans) / (MaiorQuantidadeDeFans - MenorQuantidadeDeFans)) * (10 - 1) + 1);

		AtributoRefrigeracaoText.text = ValorFinal.ToString();
		AtributoRefrigeracaoSlider.value = ValorFinal;
	}

	private void GerarAtributoSlotsPCI()
	{
		float MaiorSlotsPCI = Lista.ListaGabinete[0].SlotsPCI, MenorSlotsPCI = Lista.ListaGabinete[0].SlotsPCI;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaGabinete.Count; i++)
		{
			if (Lista.ListaGabinete[i].SlotsPCI > MaiorSlotsPCI)
			{
				MaiorSlotsPCI = Lista.ListaGabinete[i].SlotsPCI;
			}

			if (Lista.ListaGabinete[i].SlotsPCI < MenorSlotsPCI)
			{
				MenorSlotsPCI = Lista.ListaGabinete[i].SlotsPCI;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((gabinete.SlotsPCI - MenorSlotsPCI) / (MaiorSlotsPCI - MenorSlotsPCI)) * (10 - 1) + 1);

		AtributoSlotsPCIText.text = ValorFinal.ToString();
		AtributoSlotsPCISlider.value = ValorFinal;
	}

	private void GerarAtributoBaiasHD()
	{
		float MaiorBaias = Lista.ListaGabinete[0].BaiasHDSSD, MenorBaias = Lista.ListaGabinete[0].BaiasHDSSD;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaGabinete.Count; i++)
		{
			if (Lista.ListaGabinete[i].BaiasHDSSD > MaiorBaias)
			{
				MaiorBaias = Lista.ListaGabinete[i].BaiasHDSSD;
			}

			if (Lista.ListaGabinete[i].BaiasHDSSD < MenorBaias)
			{
				MenorBaias = Lista.ListaGabinete[i].BaiasHDSSD;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((gabinete.BaiasHDSSD - MenorBaias) / (MaiorBaias - MenorBaias)) * (10 - 1) + 1);

		AtributoBaiasHDText.text = ValorFinal.ToString();
		AtributoBaiasHDSlider.value = ValorFinal;
	}

	private void GerarAtributoPreco()
	{
		float MaiorPreco = Lista.ListaGabinete[0].Preco, MenorPreco = Lista.ListaGabinete[0].Preco;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaGabinete.Count; i++)
		{
			if (Lista.ListaGabinete[i].Preco > MaiorPreco)
			{
				MaiorPreco = Lista.ListaGabinete[i].Preco;
			}

			if (Lista.ListaGabinete[i].Preco < MenorPreco)
			{
				MenorPreco = Lista.ListaGabinete[i].Preco;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((gabinete.Preco - MaiorPreco) / (MenorPreco - MaiorPreco)) * (10 - 1) + 1);

		AtributoPrecoText.text = ValorFinal.ToString();
		AtributoPrecoSlider.value = ValorFinal;
	}
	#endregion
}
