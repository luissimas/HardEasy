﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisplayPlacaMae : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler{

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
		PosicaoOriginal = gameObject.transform.position; //Armazena a posição original da carta
	}

	void Update()
	{
		//Verifica se houve alguma alteração na carta se baseando em sua tag
		if (gameObject.tag == "PlayerCard")
		{
			if (Manager.JogadorCartaPlacaMaeMudou)
			{
				ExibirInformacoes();
				GerarAtributoQuantidadeMemoria();
				GerarAtributoPortasSata();
				GerarAtributoSlotsPCIE();
				GerarAtributoPreco();

				Manager.JogadorCartaPlacaMaeMudou = false;
			}
		}
		else if(gameObject.tag == "OpponentCard")
		{
			if (Manager.OponenteCartaPlacaMaeMudou)
			{
				ExibirInformacoes();
				GerarAtributoQuantidadeMemoria();
				GerarAtributoPortasSata();
				GerarAtributoSlotsPCIE();
				GerarAtributoPreco();

				Manager.OponenteCartaPlacaMaeMudou = false;
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

	#region Interação com o usuário
	Vector3 PosicaoOriginal;

	//Identifica se o mouse entrou em cima do objeto
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (Manager.PodeInteragir)
		{
			//Verifica a tag do objeto
			if (gameObject.tag == "PlayerCard")
			{
				gameObject.transform.SetAsLastSibling(); //Joga a carta por último na hierarquia, dessa forma ela fica na frente de todos os outros elementos
				gameObject.GetComponent<RectTransform>().transform.localScale = new Vector3((float)1, (float)1); //Aumenta a escala do objeto
			}
			else if (gameObject.tag == "OpponentCard")
			{
				gameObject.transform.SetAsLastSibling(); //Joga a carta por último na hierarquia, dessa forma ela fica na frente de todos os outros elementos
				gameObject.GetComponent<RectTransform>().transform.localScale = new Vector3((float)1, (float)1); //Aumenta a escala do objeto
			}
		}
	}

	//Identifica se o mouse saiu de cima do objeto
	public void OnPointerExit(PointerEventData eventData)
	{
		if (Manager.PodeInteragir)
		{
			//Verifica a tag do objeto
			if (gameObject.tag == "PlayerCard")
			{
				gameObject.GetComponent<RectTransform>().transform.localScale = new Vector3((float)0.7, (float)0.7); //Volta a escala do objeto para o tamanho normal
			}
			else if (gameObject.tag == "OpponentCard")
			{
				gameObject.GetComponent<RectTransform>().transform.localScale = new Vector3((float)0.7, (float)0.7); //Volta a escala do objeto para o tamanho normal
			}
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (Manager.PodeInteragir)
		{
			gameObject.GetComponent<CanvasGroup>().blocksRaycasts = false; //Bloqueia todos os raycasts da carta, permitindo que a drop zone identifique que a carta foi colocada
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (Manager.PodeInteragir)
		{
			gameObject.transform.position = eventData.position; //Associa a posição da carta com a posição do mouse
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (Manager.PodeInteragir)
		{
			gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true; //Desbloqueia os raycasts da carta, permitindo a interação novamente
			gameObject.transform.position = PosicaoOriginal; //Retorna a carta para a posição original
		}
	}

	private void OnTransformParentChanged()
	{
		if(transform.parent.tag == "PlayerCard" || transform.parent.tag == "OpponentCard")
		{
			gameObject.transform.position = PosicaoOriginal;
		}
	}

	#endregion

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
