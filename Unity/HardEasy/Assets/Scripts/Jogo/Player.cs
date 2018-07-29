using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class Player : NetworkBehaviour {

	public GameObject CartaPlacaMae;

	public void Update() 
	{
		if (isLocalPlayer == true)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				CmdSpawn();
			}
		}
	}

	[Command]
	public void CmdSpawn()
	{
		GameObject PlacaMae = Instantiate(CartaPlacaMae, new Vector3(-399, 189), Quaternion.identity);
		PlacaMae.transform.SetParent(FindObjectOfType<Canvas>().transform);
		PlacaMae.GetComponent<RectTransform>().localPosition = new Vector3(-399, 189);
		
		NetworkServer.SpawnWithClientAuthority(PlacaMae, connectionToClient);
	}
}
