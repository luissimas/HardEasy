using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisplayGabinete : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler{

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
		PosicaoOriginal = gameObject.transform.position; //Armazena a posição original da carta
	}

	void Update()
	{
		//Verifica se houve alguma alteração na carta se baseando em sua tag
		if (gameObject.tag == "PlayerCard")
		{
			if (Manager.JogadorCartaGabineteMudou)
			{
				ExibirInformacoes();
				GerarAtributoRefrigeracao();
				GerarAtributoSlotsPCI();
				GerarAtributoBaiasHD();
				GerarAtributoPreco();

				Manager.JogadorCartaGabineteMudou = false;
			}
		}
		else if (gameObject.tag == "OpponentCard")
		{
			if (Manager.OponenteCartaGabineteMudou)
			{
				ExibirInformacoes();
				GerarAtributoRefrigeracao();
				GerarAtributoSlotsPCI();
				GerarAtributoBaiasHD();
				GerarAtributoPreco();

				Manager.OponenteCartaGabineteMudou = false;
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
		if (transform.parent.tag == "PlayerCard" || transform.parent.tag == "OpponentCard")
		{
			gameObject.transform.position = PosicaoOriginal;
		}
	}

	#endregion

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
