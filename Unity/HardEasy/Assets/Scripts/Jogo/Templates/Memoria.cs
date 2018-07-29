using UnityEngine;

[CreateAssetMenu(fileName = "Nova memória", menuName = "Memória")]
public class Memoria : ScriptableObject {

	public Sprite Imagem;

	public string Nome;
	public float QuantidadeMemoria;
	public string DDR;
	public float Clock;
	public float Latencia;
	public float Preco;
	
}
