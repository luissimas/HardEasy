using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisplayDisco : MonoBehaviour{

	public Disco disco;

	public TMP_Text NomeText;
	public TMP_Text TamanhoText;
	public TMP_Text VelocidadeText;

	public Image Imagem;

	public TMP_Text AtributoCapacidadeDeArmazenamentoText;
	public TMP_Text AtributoVelocidadeText;
	public TMP_Text AtributoCustoPorGBText;
	public TMP_Text AtributoPrecoText;

	public Slider AtributoCapacidadeDeArmazenamentoSlider;
	public Slider AtributoVelocidadeSlider;
	public Slider AtributoCustoPorGBSlider;
	public Slider AtributoPrecoSlider;

	void Start() 
	{
		
	}

	void Update()
	{
		//Verifica se houve alguma alteração na carta se baseando em sua tag
		if (gameObject.tag == "PlayerCard")
		{
			if (Manager.JogadorCartaDiscoMudou)
			{
				ExibirInformacoes();
				GerarAtributoCapacidadeDeArmazenamento();
				GerarAtributoVelocidade();
				GerarAtributoCustoPorGB();
				GerarAtributoPreco();

				Manager.JogadorCartaDiscoMudou = false;
			}
		}
		else if (gameObject.tag == "OpponentCard")
		{
			if (Manager.OponenteCartaDiscoMudou)
			{
				ExibirInformacoes();
				GerarAtributoCapacidadeDeArmazenamento();
				GerarAtributoVelocidade();
				GerarAtributoCustoPorGB();
				GerarAtributoPreco();

				Manager.OponenteCartaDiscoMudou = false;
			}
		}
	}

	//Exibe as informações do Scriptable Object
	public void ExibirInformacoes()
	{
		NomeText.text = disco.Nome;

		if (disco.CapacidadeDeArmazenamento >= 1000)
		{
			TamanhoText.text = "Tamanho: " + (disco.CapacidadeDeArmazenamento / 1000).ToString() + "TB";
		}
		else
		{
			TamanhoText.text = "Tamanho: " + disco.CapacidadeDeArmazenamento.ToString() + "GB";
		}

		VelocidadeText.text = "Velocidade: " + disco.Velocidade.ToString() + "MB/s";

		Imagem.sprite = disco.Imagem;
	}

	#region Gerar Atributos
	private void GerarAtributoCapacidadeDeArmazenamento()
	{
		float MaiorCapacidade = Lista.ListaDisco[0].CapacidadeDeArmazenamento, MenorCapacidade = Lista.ListaDisco[0].CapacidadeDeArmazenamento;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaDisco.Count; i++)
		{
			if (Lista.ListaDisco[i].CapacidadeDeArmazenamento > MaiorCapacidade)
			{
				MaiorCapacidade = Lista.ListaDisco[i].CapacidadeDeArmazenamento;
			}

			if (Lista.ListaDisco[i].CapacidadeDeArmazenamento < MenorCapacidade)
			{
				MenorCapacidade = Lista.ListaDisco[i].CapacidadeDeArmazenamento;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((disco.CapacidadeDeArmazenamento - MenorCapacidade) / (MaiorCapacidade - MenorCapacidade)) * (10 - 1) + 1);

		AtributoCapacidadeDeArmazenamentoText.text = ValorFinal.ToString();
		AtributoCapacidadeDeArmazenamentoSlider.value = ValorFinal;
	}

	private void GerarAtributoVelocidade()
	{
		float MaiorVelocidade = Lista.ListaDisco[0].Velocidade, MenorVelocidade = Lista.ListaDisco[0].Velocidade;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaDisco.Count; i++)
		{
			if (Lista.ListaDisco[i].Velocidade > MaiorVelocidade)
			{
				MaiorVelocidade = Lista.ListaDisco[i].Velocidade;
			}

			if (Lista.ListaDisco[i].Velocidade < MenorVelocidade)
			{
				MenorVelocidade = Lista.ListaDisco[i].Velocidade;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((disco.Velocidade - MenorVelocidade) / (MaiorVelocidade - MenorVelocidade)) * (10 - 1) + 1);

		AtributoVelocidadeText.text = ValorFinal.ToString();
		AtributoVelocidadeSlider.value = ValorFinal;
	}

	private void GerarAtributoCustoPorGB()
	{
		float MaiorCustoPorGB = (Lista.ListaDisco[0].Preco / Lista.ListaDisco[0].CapacidadeDeArmazenamento), MenorCustoPorGB = (Lista.ListaDisco[0].Preco / Lista.ListaDisco[0].CapacidadeDeArmazenamento);
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaDisco.Count; i++)
		{
			if ((Lista.ListaDisco[i].Preco / Lista.ListaDisco[i].CapacidadeDeArmazenamento) > MaiorCustoPorGB)
			{
				MaiorCustoPorGB = Lista.ListaDisco[i].Preco / Lista.ListaDisco[i].CapacidadeDeArmazenamento;
			}

			if ((Lista.ListaDisco[i].Preco / Lista.ListaDisco[i].CapacidadeDeArmazenamento) < MenorCustoPorGB)
			{
				MenorCustoPorGB = Lista.ListaDisco[i].Preco / Lista.ListaDisco[i].CapacidadeDeArmazenamento;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt((((disco.Preco / disco.CapacidadeDeArmazenamento) - MaiorCustoPorGB) / (MenorCustoPorGB - MaiorCustoPorGB)) * (10 - 1) + 1);

		AtributoCustoPorGBText.text = ValorFinal.ToString();
		AtributoCustoPorGBSlider.value = ValorFinal;
	}

	private void GerarAtributoPreco()
	{
		float MaiorPreco = Lista.ListaDisco[0].Preco, MenorPreco = Lista.ListaDisco[0].Preco;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaDisco.Count; i++)
		{
			if (Lista.ListaDisco[i].Preco > MaiorPreco)
			{
				MaiorPreco = Lista.ListaDisco[i].Preco;
			}

			if (Lista.ListaDisco[i].Preco < MenorPreco)
			{
				MenorPreco = Lista.ListaDisco[i].Preco;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((disco.Preco - MaiorPreco) / (MenorPreco - MaiorPreco)) * (10 - 1) + 1);

		AtributoPrecoText.text = ValorFinal.ToString();
		AtributoPrecoSlider.value = ValorFinal;
	}
	#endregion
}
