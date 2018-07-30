using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisplayPlacaDeVideo : MonoBehaviour{

	public PlacaDeVideo placaDeVideo;
	
	public TMP_Text NomeText;
	public TMP_Text ClockText;
	public TMP_Text MemoriaText;
	public TMP_Text ConsumoText;
	public TMP_Text FonteMinimaText;

	public Image Imagem;

	public TMP_Text AtributoDesempenhoText;
	public TMP_Text AtributoMemoriaText;
	public TMP_Text AtributoConsumoText;
	public TMP_Text AtributoPrecoText;

	public Slider AtributoDesempenhoSlider;
	public Slider AtributoMemoriaSlider;
	public Slider AtributoConsumoSlider;
	public Slider AtributoPrecoSlider;

	void Start() 
	{
		
	}

	void Update()
	{
		//Verifica se houve alguma alteração na carta se baseando em sua tag
		if (gameObject.tag == "PlayerCard")
		{
			if (Manager.JogadorCartaPlacaDeVideoMudou)
			{
				ExibirInformacoes();
				GerarAtributoDesempenho();
				GerarAtributoMemoria();
				GerarAtributoConsumo();
				GerarAtributoPreco();

				Manager.JogadorCartaPlacaDeVideoMudou = false;
			}
		}
		else if(gameObject.tag == "OpponentCard")
		{
			if (Manager.OponenteCartaPlacaDeVideoMudou)
			{
				ExibirInformacoes();
				GerarAtributoDesempenho();
				GerarAtributoMemoria();
				GerarAtributoConsumo();
				GerarAtributoPreco();

				Manager.OponenteCartaPlacaDeVideoMudou = false;
			}
		}
			
	}

	//Exibe as informações do Scriptable Object
	public void ExibirInformacoes()
	{
		NomeText.text = placaDeVideo.Nome;
		ClockText.text = "Clock: " + placaDeVideo.Clock.ToString() + "Mhz";
		MemoriaText.text = "Memoria: " + placaDeVideo.Memoria.ToString() + "GB";
		ConsumoText.text = "TDP: " + placaDeVideo.Consumo.ToString() + "W";
		FonteMinimaText.text = "Fonte minima: " + placaDeVideo.FonteMinima.ToString() + "W";

		Imagem.sprite = placaDeVideo.Imagem;
	}

	#region Gerar Atributos
	private void GerarAtributoDesempenho()
	{
		float MaiorDesempenho = Lista.ListaPlacaDeVideo[0].Clock, MenorDesempenho = Lista.ListaPlacaDeVideo[0].Clock;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaPlacaDeVideo.Count; i++)
		{
			if (Lista.ListaPlacaDeVideo[i].Clock > MaiorDesempenho)
			{
				MaiorDesempenho = Lista.ListaPlacaDeVideo[i].Clock;
			}

			if (Lista.ListaPlacaDeVideo[i].Clock < MenorDesempenho)
			{
				MenorDesempenho = Lista.ListaPlacaDeVideo[i].Clock;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((placaDeVideo.Clock - MenorDesempenho) / (MaiorDesempenho - MenorDesempenho)) * (10 - 1) + 1);

		AtributoDesempenhoText.text = ValorFinal.ToString();
		AtributoDesempenhoSlider.value = ValorFinal;
	}

	private void GerarAtributoMemoria()
	{
		float MaiorMemoria = Lista.ListaPlacaDeVideo[0].Memoria, MenorMemoria = Lista.ListaPlacaDeVideo[0].Memoria;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaPlacaDeVideo.Count; i++)
		{
			if (Lista.ListaPlacaDeVideo[i].Memoria > MaiorMemoria)
			{
				MaiorMemoria = Lista.ListaPlacaDeVideo[i].Memoria;
			}

			if (Lista.ListaPlacaDeVideo[i].Memoria < MenorMemoria)
			{
				MenorMemoria = Lista.ListaPlacaDeVideo[i].Memoria;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((placaDeVideo.Memoria - MenorMemoria) / (MaiorMemoria - MenorMemoria)) * (10 - 1) + 1);

		AtributoMemoriaText.text = ValorFinal.ToString();
		AtributoMemoriaSlider.value = ValorFinal;
	}

	private void GerarAtributoConsumo()
	{
		float MaiorConsumo = Lista.ListaPlacaDeVideo[0].Consumo, MenorConsumo = Lista.ListaPlacaDeVideo[0].Consumo;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaPlacaDeVideo.Count; i++)
		{
			if (Lista.ListaPlacaDeVideo[i].Consumo > MaiorConsumo)
			{
				MaiorConsumo = Lista.ListaPlacaDeVideo[i].Consumo;
			}

			if (Lista.ListaPlacaDeVideo[i].Consumo < MenorConsumo)
			{
				MenorConsumo = Lista.ListaPlacaDeVideo[i].Consumo;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((placaDeVideo.Consumo - MaiorConsumo) / (MenorConsumo - MaiorConsumo)) * (10 - 1) + 1);

		AtributoConsumoText.text = ValorFinal.ToString();
		AtributoConsumoSlider.value = ValorFinal;
	}

	private void GerarAtributoPreco()
	{
		float MaiorPreco = Lista.ListaPlacaDeVideo[0].Preco, MenorPreco = Lista.ListaPlacaDeVideo[0].Preco;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaPlacaDeVideo.Count; i++)
		{
			if (Lista.ListaPlacaDeVideo[i].Preco > MaiorPreco)
			{
				MaiorPreco = Lista.ListaPlacaDeVideo[i].Preco;
			}

			if (Lista.ListaPlacaDeVideo[i].Preco < MenorPreco)
			{
				MenorPreco = Lista.ListaPlacaDeVideo[i].Preco;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((placaDeVideo.Preco - MaiorPreco) / (MenorPreco - MaiorPreco)) * (10 - 1) + 1);

		AtributoPrecoText.text = ValorFinal.ToString();
		AtributoPrecoSlider.value = ValorFinal;
	}
	#endregion
}
