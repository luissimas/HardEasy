using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler{

	public GameObject CartasJogador, CartasOponente;

	public void OnPointerEnter(PointerEventData eventData)
	{

	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (Manager.PodeInteragir)
		{
			if (eventData.pointerDrag.gameObject != null)
			{
				if (eventData.pointerDrag.gameObject.tag == "PlayerCard")
				{
					eventData.pointerDrag.gameObject.transform.SetParent(CartasJogador.transform);
				}
				else if (eventData.pointerDrag.gameObject.tag == "OpponentCard")
				{
					eventData.pointerDrag.gameObject.transform.SetParent(CartasOponente.transform);
				}
			}
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		if (Manager.PodeInteragir)
		{
			eventData.pointerDrag.gameObject.transform.SetParent(this.transform);
			Manager.PodeInteragir = false;

			if (GetComponentInChildren<DisplayPlacaMae>() != null)
			{
				if (GetComponentInChildren<DisplayPlacaMae>().gameObject.tag == "PlayerCard")
				{
					CartasOponente.GetComponentInChildren<DisplayPlacaMae>().gameObject.transform.SetParent(this.transform);
				}
				else if (GetComponentInChildren<DisplayPlacaMae>().gameObject.tag == "OpponentCard")
				{
					CartasJogador.GetComponentInChildren<DisplayPlacaMae>().gameObject.transform.SetParent(this.transform);
				}

				Manager.CompararPlacaMae();
			}
			else if (GetComponentInChildren<DisplayProcessador>() != null)
			{
				if (GetComponentInChildren<DisplayProcessador>().gameObject.tag == "PlayerCard")
				{
					CartasOponente.GetComponentInChildren<DisplayProcessador>().gameObject.transform.SetParent(this.transform);
				}
				else if (GetComponentInChildren<DisplayProcessador>().gameObject.tag == "OpponentCard")
				{
					CartasJogador.GetComponentInChildren<DisplayProcessador>().gameObject.transform.SetParent(this.transform);
				}

				Manager.CompararProcessador();
			}
			else if (GetComponentInChildren<DisplayMemoria>() != null)
			{
				if (GetComponentInChildren<DisplayMemoria>().gameObject.tag == "PlayerCard")
				{
					CartasOponente.GetComponentInChildren<DisplayMemoria>().gameObject.transform.SetParent(this.transform);
				}
				else if (GetComponentInChildren<DisplayMemoria>().gameObject.tag == "OpponentCard")
				{
					CartasJogador.GetComponentInChildren<DisplayMemoria>().gameObject.transform.SetParent(this.transform);
				}

				Manager.CompararMemoria();
			}
			else if (GetComponentInChildren<DisplayPlacaDeVideo>() != null)
			{
				if (GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.tag == "PlayerCard")
				{
					CartasOponente.GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.transform.SetParent(this.transform);
				}
				else if (GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.tag == "OpponentCard")
				{
					CartasJogador.GetComponentInChildren<DisplayPlacaDeVideo>().gameObject.transform.SetParent(this.transform);
				}

				Manager.CompararPlacaDeVideo();
			}
			else if (GetComponentInChildren<DisplayDisco>() != null)
			{
				if (GetComponentInChildren<DisplayDisco>().gameObject.tag == "PlayerCard")
				{
					CartasOponente.GetComponentInChildren<DisplayDisco>().gameObject.transform.SetParent(this.transform);
				}
				else if (GetComponentInChildren<DisplayDisco>().gameObject.tag == "OpponentCard")
				{
					CartasJogador.GetComponentInChildren<DisplayDisco>().gameObject.transform.SetParent(this.transform);
				}

				Manager.CompararDisco();
			}
			else if (GetComponentInChildren<DisplayFonte>() != null)
			{
				if (GetComponentInChildren<DisplayFonte>().gameObject.tag == "PlayerCard")
				{
					CartasOponente.GetComponentInChildren<DisplayFonte>().gameObject.transform.SetParent(this.transform);
				}
				else if (GetComponentInChildren<DisplayFonte>().gameObject.tag == "OpponentCard")
				{
					CartasJogador.GetComponentInChildren<DisplayFonte>().gameObject.transform.SetParent(this.transform);
				}

				Manager.CompararFonte();
			}
			else if (GetComponentInChildren<DisplayGabinete>() != null)
			{
				if (GetComponentInChildren<DisplayGabinete>().gameObject.tag == "PlayerCard")
				{
					CartasOponente.GetComponentInChildren<DisplayGabinete>().gameObject.transform.SetParent(this.transform);
				}
				else if (GetComponentInChildren<DisplayGabinete>().gameObject.tag == "OpponentCard")
				{
					CartasJogador.GetComponentInChildren<DisplayGabinete>().gameObject.transform.SetParent(this.transform);
				}

				Manager.CompararGabinete();
			}

			Invoke("RetornarCartas", 2);
		}
	}

	public void RetornarCartas()
	{
		while (GetComponentInChildren<CanvasGroup>() != null)
		{
			for (int i = 0; i < GetComponentsInChildren<CanvasGroup>().Length; i++)
			{
				if (GetComponentsInChildren<CanvasGroup>()[i].gameObject.tag == "PlayerCard")
				{
					GetComponentsInChildren<CanvasGroup>()[i].gameObject.transform.SetParent(CartasJogador.transform);
				}
				else if (GetComponentsInChildren<CanvasGroup>()[i].gameObject.tag == "OpponentCard")
				{
					GetComponentsInChildren<CanvasGroup>()[i].gameObject.transform.SetParent(CartasOponente.transform);
				}
			}
		}
	}

}
