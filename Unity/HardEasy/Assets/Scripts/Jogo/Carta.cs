using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Carta : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler{

	// Use this for initialization
	void Start ()
	{
		PosicaoOriginal = gameObject.transform.position; //Armazena a posição original da carta
		EscalaOriginal = gameObject.GetComponent<RectTransform>().localScale; //Armazena a escala original da carta
	}

	#region Interação com o usuário
	Vector3 PosicaoOriginal;
	Vector3 EscalaOriginal;

	//Identifica se o mouse entrou em cima do objeto
	public void OnPointerEnter(PointerEventData eventData)
	{
		if (Manager.PodeInteragir)
		{
			if(transform.parent.tag != "Drop")
			//Verifica a tag do objeto
			if (gameObject.tag == "PlayerCard")
			{
				gameObject.transform.SetAsLastSibling(); //Joga a carta por último na hierarquia, dessa forma ela fica na frente de todos os outros elementos
				gameObject.GetComponent<RectTransform>().transform.localScale = new Vector3((float)0.5, (float)0.5); //Aumenta a escala do objeto
				gameObject.GetComponent<RectTransform>().transform.position = new Vector3(PosicaoOriginal.x, (PosicaoOriginal.y + 20));
			}
			else if (gameObject.tag == "OpponentCard")
			{
				gameObject.transform.SetAsLastSibling(); //Joga a carta por último na hierarquia, dessa forma ela fica na frente de todos os outros elementos
				gameObject.GetComponent<RectTransform>().transform.localScale = new Vector3((float)0.5, (float)0.5); //Aumenta a escala do objeto
				gameObject.GetComponent<RectTransform>().transform.position = new Vector3(PosicaoOriginal.x, (PosicaoOriginal.y - 20));
			}
		}
	}

	//Identifica se o mouse saiu de cima do objeto
	public void OnPointerExit(PointerEventData eventData)
	{
		if (Manager.PodeInteragir)
		{
			if(transform.parent.tag != "Drop")
			{
				gameObject.GetComponent<RectTransform>().transform.position = PosicaoOriginal; //Volta a posição do objeto para o tamanho normal
				gameObject.GetComponent<RectTransform>().transform.localScale = EscalaOriginal; //Volta a escala do objeto para o tamanho normal
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
			if (transform.parent.tag != "Drop")
			{
				gameObject.transform.position = eventData.position; //Associa a posição da carta com a posição do mouse
			}
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		if (Manager.PodeInteragir)
		{
			if (transform.parent.tag != "Drop")
			{
				gameObject.GetComponent<CanvasGroup>().blocksRaycasts = true; //Desbloqueia os raycasts da carta, permitindo a interação novamente
				gameObject.transform.position = PosicaoOriginal; //Retorna a carta para a posição original
			}
		}
	}

	private void OnTransformParentChanged()
	{
		if (transform.parent.tag != "Drop")
		{
			gameObject.transform.position = PosicaoOriginal;
		}
	}

	#endregion
}
