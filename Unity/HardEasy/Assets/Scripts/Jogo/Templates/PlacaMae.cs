using UnityEngine;

[CreateAssetMenu(fileName = "Nova placa-mãe", menuName = "Placa-Mãe")]
public class PlacaMae : ScriptableObject {

	public Sprite Imagem;

	public string Nome;
	public string Socket;
	public string DDR;
	public float QuantidadeMemoria;
	public float SlotsPCIE;
	public float PortasSata;
	public bool SuporteSLI;
	public bool SuporteCrossfire;
	public string Formato;
	public float Preco;

}
