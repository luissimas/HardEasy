using UnityEngine;

[CreateAssetMenu(fileName = "Novo processador", menuName = "Processador")]
public class Processador : ScriptableObject {

	public Sprite Imagem;

	public string Nome;
	public string Socket;
	public float Nucleos;
	public float Threads;
	public float Clock;
	public float TDP;
	public float Preco;

}
