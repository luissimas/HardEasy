using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirarCartas : MonoBehaviour
{

	#region VirarCartas

	public GameObject CardBack;
	public AnimationCurve CurvaDaEscala;
	public float Duracao = 0.5f;

	public void MostrarCarta()
	{
		StopCoroutine(Flip(false));
		StartCoroutine(Flip(false));
	}

	public void EsconderCarta()
	{
		StopCoroutine(Flip(true));
		StartCoroutine(Flip(true));
	}

	IEnumerator Flip(bool EsconderOuNao)
	{
		float Tempo = 0f;

		while (Tempo <= 1)
		{
			float Escala = CurvaDaEscala.Evaluate(Tempo);
			Tempo = Tempo + Time.deltaTime / Duracao;

			Vector3 localScale = gameObject.GetComponent<RectTransform>().localScale;
			localScale.x = Escala;
			gameObject.GetComponent<RectTransform>().localScale = localScale;

			if (Tempo >= 0.5)
			{
				CardBack.SetActive(EsconderOuNao);
			}

			yield return new WaitForFixedUpdate();
		}
	}
	#endregion
}
