using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class DisplayProcessador : MonoBehaviour{

	public Processador processador;

	public TMP_Text NomeText;
	public TMP_Text SocketText;
	public TMP_Text NucleosText;
	public TMP_Text ThreadsText;
	public TMP_Text ClockText;
	public TMP_Text TDPText;

	public Image Imagem;

	public TMP_Text AtributoDesempenhoSingleCoreText;
	public TMP_Text AtributoDesempenhoMultiCoreText;
	public TMP_Text AtributoConsumoText;
	public TMP_Text AtributoPrecoText;

	public Slider AtributoDesempenhoSingleCoreSlider;
	public Slider AtributoDesempenhoMultiCoreSlider;
	public Slider AtributoConsumoSlider;
	public Slider AtributoPrecoSlider;

	void Start() 
	{
		
	}

	void Update()
	{
		//Verifica se houve alguma alteração na carta se baseando em sua tag
		if(gameObject.tag == "PlayerCard")
		{
			if (Manager.JogadorCartaProcessadorMudou)
			{
				ExibirInformacoes();
				GerarAtributoDesempenhoSingleCore();
				GerarAtributoDesempenhoMultiCore();
				GerarAtributoConsumo();
				GerarAtributoPreco();

				Manager.JogadorCartaProcessadorMudou = false;
			}
		}
		else if(gameObject.tag == "OpponentCard")
		{
			if (Manager.OponenteCartaProcessadorMudou)
			{
				ExibirInformacoes();
				GerarAtributoDesempenhoSingleCore();
				GerarAtributoDesempenhoMultiCore();
				GerarAtributoConsumo();
				GerarAtributoPreco();

				Manager.OponenteCartaProcessadorMudou = false;
			}
		}

	}

	//Exibe as informações do Scriptable Object
	public void ExibirInformacoes()
	{
		NomeText.text = processador.Nome;
		SocketText.text = "Socket: " + processador.Socket;
		NucleosText.text = "Nucleos: " + processador.Nucleos.ToString();
		ThreadsText.text = "Threads: " + processador.Threads.ToString();
		ClockText.text = "Clock: " + processador.Clock.ToString() + "Ghz";
		TDPText.text = "TDP: " + processador.TDP.ToString() + "W";

		Imagem.sprite = processador.Imagem;
	}

	#region Gerar Atributos
	private void GerarAtributoDesempenhoSingleCore()
	{
		float MaiorClock = Lista.ListaProcessador[0].Clock, MenorClock = Lista.ListaProcessador[0].Clock;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaProcessador.Count; i++)
		{
			if (Lista.ListaProcessador[i].Clock > MaiorClock)
			{
				MaiorClock = Lista.ListaProcessador[i].Clock;
			}

			if (Lista.ListaProcessador[i].Clock < MenorClock)
			{
				MenorClock = Lista.ListaProcessador[i].Clock;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((processador.Clock - MenorClock) / (MaiorClock - MenorClock)) * (10 - 1) + 1);

		AtributoDesempenhoSingleCoreText.text = ValorFinal.ToString();
		AtributoDesempenhoSingleCoreSlider.value = ValorFinal;
	}

	private void GerarAtributoDesempenhoMultiCore()
	{
		float MaiorThreads = Lista.ListaProcessador[0].Threads, MenorThreads = Lista.ListaProcessador[0].Threads;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i=0; i< Lista.ListaProcessador.Count; i++)
		{ 
			if(Lista.ListaProcessador[i].Threads > MaiorThreads)
			{
				MaiorThreads = Lista.ListaProcessador[i].Threads;
			}

			if(Lista.ListaProcessador[i].Threads < MenorThreads)
			{
				MenorThreads = Lista.ListaProcessador[i].Threads;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((processador.Threads - MenorThreads) / (MaiorThreads - MenorThreads)) * (10 - 1) + 1);

		AtributoDesempenhoMultiCoreText.text = ValorFinal.ToString();
		AtributoDesempenhoMultiCoreSlider.value = ValorFinal;
	}

	private void GerarAtributoConsumo()
	{
		float MaiorTDP = Lista.ListaProcessador[0].TDP, MenorTDP = Lista.ListaProcessador[0].TDP;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaProcessador.Count; i++)
		{
			if(Lista.ListaProcessador[i].TDP > MaiorTDP)
			{
				MaiorTDP = Lista.ListaProcessador[i].TDP;
			}

			if(Lista.ListaProcessador[i].TDP < MenorTDP)
			{
				MenorTDP = Lista.ListaProcessador[i].TDP;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((processador.TDP - MaiorTDP) / (MenorTDP - MaiorTDP)) * (10 - 1) + 1);

		AtributoConsumoText.text = ValorFinal.ToString();
		AtributoConsumoSlider.value = ValorFinal;
	}

	private void GerarAtributoPreco()
	{
		float MaiorPreco = Lista.ListaProcessador[0].Preco, MenorPreco = Lista.ListaProcessador[0].Preco;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaProcessador.Count; i++)
		{
			if (Lista.ListaProcessador[i].Preco > MaiorPreco)
			{
				MaiorPreco = Lista.ListaProcessador[i].Preco;
			}

			if(Lista.ListaProcessador[i].Preco < MenorPreco)
			{
				MenorPreco = Lista.ListaProcessador[i].Preco;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((processador.Preco - MaiorPreco) / (MenorPreco - MaiorPreco)) * (10 - 1) + 1);

		AtributoPrecoText.text = ValorFinal.ToString();
		AtributoPrecoSlider.value = ValorFinal;
	}

	#endregion
}
