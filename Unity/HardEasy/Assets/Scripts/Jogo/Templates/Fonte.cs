using UnityEngine;

[CreateAssetMenu(fileName = "Nova fonte", menuName = "Fonte")]
public class Fonte : ScriptableObject {

	public Sprite Imagem;

	public string Nome;
	public float Capacidade;
	public float Eficiencia;
	public float Preco;

}
