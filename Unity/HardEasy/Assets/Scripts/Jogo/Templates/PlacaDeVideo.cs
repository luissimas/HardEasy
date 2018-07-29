using UnityEngine;

[CreateAssetMenu(fileName = "Nova placa de vídeo", menuName = "Placa de Vídeo")]
public class PlacaDeVideo : ScriptableObject {

	public Sprite Imagem;

	public string Nome;
	public float Clock;
	public float Memoria;
	public bool SLI;
	public bool Crossfire;
	public float Consumo;
	public float FonteMinima;
	public float Preco;
	
}
