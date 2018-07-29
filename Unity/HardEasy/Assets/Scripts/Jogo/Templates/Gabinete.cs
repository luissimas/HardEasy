using UnityEngine;

[CreateAssetMenu(fileName = "Novo gabinete", menuName = "Gabinete")]
public class Gabinete : ScriptableObject {

	public Sprite Imagem;

	public string Nome;
	public string FormatoPlacaMae;
	public float QuantidadeDeFans;
	public float SlotsPCI;
	public float BaiasHDSSD;
	public float Preco;

}
