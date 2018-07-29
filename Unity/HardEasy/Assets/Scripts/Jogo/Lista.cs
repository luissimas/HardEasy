using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Lista{

	//Cria e instancia as listas
	public static List<PlacaMae> ListaPlacaMae = new List<PlacaMae>();
	public static List<Processador> ListaProcessador = new List<Processador>();
	public static List<Memoria> ListaMemoria = new List<Memoria>();
	public static List<PlacaDeVideo> ListaPlacaDeVideo = new List<PlacaDeVideo>();
	public static List<Disco> ListaDisco = new List<Disco>();
	public static List<Fonte> ListaFonte = new List<Fonte>();
	public static List<Gabinete> ListaGabinete = new List<Gabinete>();

	//Encontra todos os scriptable objects na pasta Resources e os adiciona nas listas dos seus respectivos tipos
	public static void CarregarListas()
	{
		for (int i = 0; i < Resources.LoadAll<PlacaMae>("Placa-Mãe").Length; i++)
		{
			ListaPlacaMae.Add(Resources.LoadAll<PlacaMae>("Placa-Mãe")[i]);
		}

		for (int i = 0; i < Resources.LoadAll<Processador>("Processador").Length; i++)
		{
			ListaProcessador.Add(Resources.LoadAll<Processador>("Processador")[i]);
		}

		for (int i = 0; i < Resources.LoadAll<Memoria>("Memória").Length; i++)
		{
			ListaMemoria.Add(Resources.LoadAll<Memoria>("Memória")[i]);
		}

		for(int i=0;i<Resources.LoadAll<PlacaDeVideo>("Placa de Vídeo").Length; i++)
		{
			ListaPlacaDeVideo.Add(Resources.LoadAll<PlacaDeVideo>("Placa de Vídeo")[i]);
		}

		for(int i = 0; i < Resources.LoadAll<Disco>("Disco").Length; i++)
		{
			ListaDisco.Add(Resources.LoadAll<Disco>("Disco")[i]);
		}

		for(int i = 0; i < Resources.LoadAll<Fonte>("Fonte").Length; i++)
		{
			ListaFonte.Add(Resources.LoadAll<Fonte>("Fonte")[i]);
		}

		for (int i = 0; i < Resources.LoadAll<Gabinete>("Gabinete").Length; i++)
		{
			ListaGabinete.Add(Resources.LoadAll<Gabinete>("Gabinete")[i]);
		}
	}
}
