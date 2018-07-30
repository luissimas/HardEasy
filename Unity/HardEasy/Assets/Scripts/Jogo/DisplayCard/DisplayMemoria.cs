using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class DisplayMemoria : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler{

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
		PosicaoOriginal = gameObject.transform.position; //Armazena a posição original da carta
	}

	void Update()
	{
		//Verifica se houve alguma alteração na carta se baseando em sua tag
		if (gameObject.tag == "PlayerCard")
		{
			if (Manager.JogadorCartaMemoriaMudou)
			{
				ExibirInformacoes();
				GerarAtributoDesempenho();
				GerarAtributoQuantidadeMemoria();
				GerarAtributoLatencia();
				GerarAtributoPreco();

				Manager.JogadorCartaMemoriaMudou = false;
			}
		}
		else if(gameObject.tag == "OpponentCard")
		{
			if (Manager.OponenteCartaMemoriaMudou)
			{
				ExibirInformacoes();
				GerarAtributoDesempenho();
				GerarAtributoQuantidadeMemoria();
				GerarAtributoLatencia();
				GerarAtributoPreco();

				Manager.OponenteCartaMemoriaMudou = false;
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
		if ((transform.parent.tag == "PlayerCard") || (transform.parent.tag == "OpponentCard"))
		{
			gameObject.transform.position = PosicaoOriginal;
		}
	}

	#endregion

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
