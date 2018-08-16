using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisplayPlacaMae : MonoBehaviour{

	public PlacaMae placaMae;

	public TMP_Text NomeText;
	public TMP_Text SocketText;
	public TMP_Text MemoriaText;
	public TMP_Text DDRText;
	public TMP_Text FormatoText;
	public TMP_Text SLIText;
	public TMP_Text CrossfireText;

	public Image Imagem;

	public TMP_Text AtributoQuantidadeMemoriaText;
	public TMP_Text AtributoPortasSataText;
	public TMP_Text AtributoSlotsPCIEText;
	public TMP_Text AtributoPrecoText;

	public Slider AtributoQuantidadeMemoriaSlider;
	public Slider AtributoPortasSataSlider;
	public Slider AtributoSlotsPCIESlider;
	public Slider AtributoPrecoSlider;

	void Start() 
	{

	}

	void Update()
	{
		//Verifica se houve alguma alteração na carta se baseando em sua tag
		if (gameObject.tag == "PlayerCard")
		{
			if (Informacoes.JogadorCartaPlacaMaeMudou)
			{
				ExibirInformacoes();
				GerarAtributoQuantidadeMemoria();
				GerarAtributoPortasSata();
				GerarAtributoSlotsPCIE();
				GerarAtributoPreco();

				Informacoes.JogadorCartaPlacaMaeMudou = false;
			}
		}
		else if(gameObject.tag == "OpponentCard")
		{
			if (Informacoes.OponenteCartaPlacaMaeMudou)
			{
				ExibirInformacoes();
				GerarAtributoQuantidadeMemoria();
				GerarAtributoPortasSata();
				GerarAtributoSlotsPCIE();
				GerarAtributoPreco();

				Informacoes.OponenteCartaPlacaMaeMudou = false;
			}
		}
	}

	//Exibe as informações do Scriptable Object
	public void ExibirInformacoes()
	{
		NomeText.text = placaMae.Nome;
		SocketText.text = "Socket: " + placaMae.Socket;
		MemoriaText.text = "Memoria: " + placaMae.QuantidadeMemoria.ToString() + "Gb";
		DDRText.text = "DDR: " + placaMae.DDR;
		FormatoText.text = "Formato: " + placaMae.Formato;

		if (placaMae.SuporteSLI)
		{
			SLIText.text = "SLI: Sim";
		}
		else
		{
			SLIText.text = "SLI: Nao";
		}

		if (placaMae.SuporteCrossfire)
		{
			CrossfireText.text = "Crossfire: Sim";
		}
		else
		{
			CrossfireText.text = "Crossfire: Nao";
		}

		Imagem.sprite = placaMae.Imagem;
	}

	#region Gerar Atributos
	private void GerarAtributoQuantidadeMemoria()
	{
		float MaiorMemoria = Lista.ListaPlacaMae[0].QuantidadeMemoria, MenorMemoria = Lista.ListaPlacaMae[0].QuantidadeMemoria;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaPlacaMae.Count; i++)
		{
			if(Lista.ListaPlacaMae[i].QuantidadeMemoria > MaiorMemoria)
			{
				MaiorMemoria = Lista.ListaPlacaMae[i].QuantidadeMemoria;
			}

			if (Lista.ListaPlacaMae[i].QuantidadeMemoria < MenorMemoria)
			{
				MenorMemoria = Lista.ListaPlacaMae[i].QuantidadeMemoria;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((placaMae.QuantidadeMemoria - MenorMemoria) / (MaiorMemoria - MenorMemoria)) * (10 - 1) + 1);

		AtributoQuantidadeMemoriaText.text = ValorFinal.ToString();
		AtributoQuantidadeMemoriaSlider.value = ValorFinal;
	}

	private void GerarAtributoPortasSata()
	{
		float MaiorPortas = Lista.ListaPlacaMae[0].PortasSata, MenorPortas = Lista.ListaPlacaMae[0].PortasSata;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaPlacaMae.Count; i++)
		{
			if (Lista.ListaPlacaMae[i].PortasSata > MaiorPortas)
			{
				MaiorPortas = Lista.ListaPlacaMae[i].PortasSata;
			}

			if (Lista.ListaPlacaMae[i].PortasSata < MenorPortas)
			{
				MenorPortas = Lista.ListaPlacaMae[i].PortasSata;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((placaMae.PortasSata - MenorPortas) / (MaiorPortas - MenorPortas)) * (10 - 1) + 1);

		AtributoPortasSataText.text = ValorFinal.ToString();
		AtributoPortasSataSlider.value = ValorFinal;
	}

	private void GerarAtributoSlotsPCIE()
	{
		float MaiorSlots = Lista.ListaPlacaMae[0].SlotsPCIE, MenorSlots = Lista.ListaPlacaMae[0].SlotsPCIE;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaPlacaMae.Count; i++)
		{
			if (Lista.ListaPlacaMae[i].SlotsPCIE > MaiorSlots)
			{
				MaiorSlots = Lista.ListaPlacaMae[i].SlotsPCIE;
			}

			if (Lista.ListaPlacaMae[i].SlotsPCIE < MenorSlots)
			{
				MenorSlots = Lista.ListaPlacaMae[i].SlotsPCIE;
			}
		}


		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((placaMae.SlotsPCIE - MenorSlots) / (MaiorSlots - MenorSlots)) * (10 - 1) + 1);

		AtributoSlotsPCIEText.text = ValorFinal.ToString();
		AtributoSlotsPCIESlider.value = ValorFinal;
	}

	private void GerarAtributoPreco()
	{
		float MaiorPreco = Lista.ListaPlacaMae[0].Preco, MenorPreco = Lista.ListaPlacaMae[0].Preco;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaPlacaMae.Count; i++)
		{
			if (Lista.ListaPlacaMae[i].Preco > MaiorPreco)
			{
				MaiorPreco = Lista.ListaPlacaMae[i].Preco;
			}

			if (Lista.ListaPlacaMae[i].Preco < MenorPreco)
			{
				MenorPreco = Lista.ListaPlacaMae[i].Preco;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((placaMae.Preco - MaiorPreco) / (MenorPreco - MaiorPreco)) * (10 - 1) + 1);

		AtributoPrecoText.text = ValorFinal.ToString();
		AtributoPrecoSlider.value = ValorFinal;
	}
	#endregion
}
