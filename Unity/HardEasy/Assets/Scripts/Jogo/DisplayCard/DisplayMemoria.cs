using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisplayMemoria : MonoBehaviour{

	public Memoria memoria;

	public TMP_Text NomeText;
	public TMP_Text QuantidadeMemoriaText;
	public TMP_Text ClockText;
	public TMP_Text DDRText;
	public TMP_Text LatenciaText;

	public Image Imagem;

	public TMP_Text AtributoQuantidadeMemoriaText;
	public TMP_Text AtributoDesempenhoText;
	public TMP_Text AtributoLatenciaText;
	public TMP_Text AtributoPrecoText;

	public Slider AtributoQuantidadeMemoriaSlider;
	public Slider AtributoDesempenhoSlider;
	public Slider AtributoLatenciaSlider;
	public Slider AtributoPrecoSlider;

	void Start() 
	{

	}

	void Update()
	{
		//Verifica se houve alguma alteração na carta se baseando em sua tag
		if (gameObject.tag == "PlayerCard")
		{
			if (Informacoes.JogadorCartaMemoriaMudou)
			{
				ExibirInformacoes();
				GerarAtributoDesempenho();
				GerarAtributoQuantidadeMemoria();
				GerarAtributoLatencia();
				GerarAtributoPreco();

				Informacoes.JogadorCartaMemoriaMudou = false;
			}
		}
		else if(gameObject.tag == "OpponentCard")
		{
			if (Informacoes.OponenteCartaMemoriaMudou)
			{
				ExibirInformacoes();
				GerarAtributoDesempenho();
				GerarAtributoQuantidadeMemoria();
				GerarAtributoLatencia();
				GerarAtributoPreco();

				Informacoes.OponenteCartaMemoriaMudou = false;
			}
		}
	}

	//Exibe as informações do Scriptable Object
	public void ExibirInformacoes()
	{
		NomeText.text = memoria.Nome;
		QuantidadeMemoriaText.text = "Memoria: " + memoria.QuantidadeMemoria.ToString() + "GB";
		ClockText.text = "Clock: " + memoria.Clock.ToString() + "Mhz";
		DDRText.text = "DDR: " + memoria.DDR;
		LatenciaText.text = "Latencia: " + memoria.Latencia;

		Imagem.sprite = memoria.Imagem;
	}

	#region Gerar Atributos
	private void GerarAtributoQuantidadeMemoria()
	{
		float MaiorMemoria = Lista.ListaMemoria[0].QuantidadeMemoria, MenorMemoria = Lista.ListaMemoria[0].QuantidadeMemoria;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaMemoria.Count; i++)
		{
			if (Lista.ListaMemoria[i].QuantidadeMemoria > MaiorMemoria)
			{
				MaiorMemoria = Lista.ListaMemoria[i].QuantidadeMemoria;
			}

			if (Lista.ListaMemoria[i].QuantidadeMemoria < MenorMemoria)
			{
				MenorMemoria = Lista.ListaMemoria[i].QuantidadeMemoria;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((memoria.QuantidadeMemoria - MenorMemoria) / (MaiorMemoria - MenorMemoria)) * (10 - 1) + 1);

		AtributoQuantidadeMemoriaText.text = ValorFinal.ToString();
		AtributoQuantidadeMemoriaSlider.value = ValorFinal;
	}

	private void GerarAtributoDesempenho()
	{
		float MaiorDesempenho = Lista.ListaMemoria[0].Clock, MenorDesempenho = Lista.ListaMemoria[0].Clock;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaMemoria.Count; i++)
		{
			if (Lista.ListaMemoria[i].Clock > MaiorDesempenho)
			{
				MaiorDesempenho = Lista.ListaMemoria[i].Clock;
			}

			if (Lista.ListaMemoria[i].Clock < MenorDesempenho)
			{
				MenorDesempenho = Lista.ListaMemoria[i].Clock;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((memoria.Clock - MenorDesempenho) / (MaiorDesempenho - MenorDesempenho)) * (10 - 1) + 1);

		AtributoDesempenhoText.text = ValorFinal.ToString();
		AtributoDesempenhoSlider.value = ValorFinal;
	}

	private void GerarAtributoLatencia()
	{
		float MaiorLatencia = Lista.ListaMemoria[0].Latencia, MenorLatencia = Lista.ListaMemoria[0].Latencia;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaMemoria.Count; i++)
		{
			if (Lista.ListaMemoria[i].Latencia > MaiorLatencia)
			{
				MaiorLatencia = Lista.ListaMemoria[i].Latencia;
			}

			if (Lista.ListaMemoria[i].Latencia < MenorLatencia)
			{
				MenorLatencia = Lista.ListaMemoria[i].Latencia;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((memoria.Latencia - MaiorLatencia) / (MenorLatencia - MaiorLatencia)) * (10 - 1) + 1);

		AtributoLatenciaText.text = ValorFinal.ToString();
		AtributoLatenciaSlider.value = ValorFinal;
	}

	private void GerarAtributoPreco()
	{
		float MaiorPreco = Lista.ListaMemoria[0].Preco, MenorPreco = Lista.ListaMemoria[0].Preco;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaMemoria.Count; i++)
		{
			if (Lista.ListaMemoria[i].Preco > MaiorPreco)
			{
				MaiorPreco = Lista.ListaMemoria[i].Preco;
			}

			if (Lista.ListaMemoria[i].Preco < MenorPreco)
			{
				MenorPreco = Lista.ListaMemoria[i].Preco;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((memoria.Preco - MaiorPreco) / (MenorPreco - MaiorPreco)) * (10 - 1) + 1);

		AtributoPrecoText.text = ValorFinal.ToString();
		AtributoPrecoSlider.value = ValorFinal;
	}
	#endregion
}
