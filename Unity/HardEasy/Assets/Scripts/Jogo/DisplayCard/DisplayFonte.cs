using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisplayFonte : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler{

	public Fonte fonte;

	public TMP_Text NomeText;
	public TMP_Text CapacidadeText;
	public TMP_Text EficienciaText;

	public Image Imagem;

	public TMP_Text AtributoPotenciaText;
	public TMP_Text AtributoEficienciaText;
	public TMP_Text AtributoCustoPorWText;
	public TMP_Text AtributoPrecoText;

	public Slider AtributoPotenciaSlider;
	public Slider AtributoEficienciaSlider;
	public Slider AtributoCustoPorWSlider;
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
			if (Manager.JogadorCartaFonteMudou)
			{
				ExibirInformacoes();
				GerarAtributoPotencia();
				GerarAtributoEficiencia();
				GerarAtributoCustoPorW();
				GerarAtributoPreco();

				Manager.JogadorCartaFonteMudou = false;
			}
		}
		else if (gameObject.tag == "OpponentCard")
		{
			if (Manager.OponenteCartaFonteMudou)
			{
				ExibirInformacoes();
				GerarAtributoPotencia();
				GerarAtributoEficiencia();
				GerarAtributoCustoPorW();
				GerarAtributoPreco();

				Manager.OponenteCartaFonteMudou = false;
			}
		}
	}

	//Exibe as informações do Scriptable Object
	public void ExibirInformacoes()
	{
		NomeText.text = fonte.Nome;
		CapacidadeText.text = "Capacidade: " + fonte.Capacidade.ToString() + "W";
		EficienciaText.text = "Eficiencia: " + fonte.Eficiencia.ToString() + "%";

		Imagem.sprite = fonte.Imagem;
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
	private void GerarAtributoPotencia()
	{
		float MaiorPotencia = Lista.ListaFonte[0].Capacidade, MenorPotencia = Lista.ListaFonte[0].Capacidade;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for(int i = 0; i < Lista.ListaFonte.Count; i++)
		{
			if (Lista.ListaFonte[i].Capacidade > MaiorPotencia)
			{
				MaiorPotencia = Lista.ListaFonte[i].Capacidade;
			}

			if (Lista.ListaFonte[i].Capacidade < MenorPotencia)
			{
				MenorPotencia = Lista.ListaFonte[i].Capacidade;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((fonte.Capacidade - MenorPotencia) / (MaiorPotencia - MenorPotencia)) * (10 - 1) + 1);

		AtributoPotenciaText.text = ValorFinal.ToString();
		AtributoPotenciaSlider.value = ValorFinal;
	}

	private void GerarAtributoEficiencia()
	{
		float MaiorEficiencia = Lista.ListaFonte[0].Eficiencia, MenorEficiencia = Lista.ListaFonte[0].Eficiencia;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaFonte.Count; i++)
		{
			if (Lista.ListaFonte[i].Eficiencia > MaiorEficiencia)
			{
				MaiorEficiencia = Lista.ListaFonte[i].Eficiencia;
			}

			if (Lista.ListaFonte[i].Eficiencia < MenorEficiencia)
			{
				MenorEficiencia = Lista.ListaFonte[i].Eficiencia;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((fonte.Eficiencia - MenorEficiencia) / (MaiorEficiencia - MenorEficiencia)) * (10 - 1) + 1);

		AtributoEficienciaText.text = ValorFinal.ToString();
		AtributoEficienciaSlider.value = ValorFinal;
	}

	private void GerarAtributoCustoPorW()
	{
		float MaiorCustoPorW = (Lista.ListaFonte[0].Preco / Lista.ListaFonte[0].Capacidade), MenorCustoPorW = (Lista.ListaFonte[0].Preco / Lista.ListaFonte[0].Capacidade);
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaFonte.Count; i++)
		{
			if ((Lista.ListaFonte[i].Preco / Lista.ListaFonte[i].Capacidade) > MaiorCustoPorW)
			{
				MaiorCustoPorW = Lista.ListaFonte[i].Preco / Lista.ListaFonte[i].Capacidade;
			}

			if ((Lista.ListaFonte[i].Preco / Lista.ListaFonte[i].Capacidade) < MenorCustoPorW)
			{
				MenorCustoPorW = Lista.ListaFonte[i].Preco / Lista.ListaFonte[i].Capacidade;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt((((fonte.Preco / fonte.Capacidade) - MaiorCustoPorW) / (MenorCustoPorW - MaiorCustoPorW)) * (10 - 1) + 1);

		AtributoCustoPorWText.text = ValorFinal.ToString();
		AtributoCustoPorWSlider.value = ValorFinal;
	}

	private void GerarAtributoPreco()
	{
		float MaiorPreco = Lista.ListaFonte[0].Preco, MenorPreco = Lista.ListaFonte[0].Preco;
		int ValorFinal;

		//Identifica a maior e a menor informação da lista
		for (int i = 0; i < Lista.ListaFonte.Count; i++)
		{
			if (Lista.ListaFonte[i].Preco > MaiorPreco)
			{
				MaiorPreco = Lista.ListaFonte[i].Preco;
			}

			if (Lista.ListaFonte[i].Preco < MenorPreco)
			{
				MenorPreco = Lista.ListaFonte[i].Preco;
			}
		}

		// Fórmula da normalização convertendo para uma escala [1-10]
		ValorFinal = Mathf.RoundToInt(((fonte.Preco - MaiorPreco) / (MenorPreco - MaiorPreco)) * (10 - 1) + 1);

		AtributoPrecoText.text = ValorFinal.ToString();
		AtributoPrecoSlider.value = ValorFinal;
	}
	#endregion
}
