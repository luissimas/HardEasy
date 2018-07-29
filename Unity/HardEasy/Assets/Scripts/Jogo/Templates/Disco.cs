using UnityEngine;

[CreateAssetMenu(fileName = "Novo disco", menuName = "Disco")]
public class Disco : ScriptableObject {

	public Sprite Imagem;

	public string Nome;
	public float CapacidadeDeArmazenamento;
	public float Velocidade;
	public float Preco;

}
