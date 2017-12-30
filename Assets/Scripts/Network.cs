using System.Collections;
using UnityEngine;
using SocketIO;

public class Network : MonoBehaviour
{
	static SocketIOComponent socket;
	public GameObject playerPrefab;

	// Use this for initialization
	void Start ()
	{
		socket = GetComponent<SocketIOComponent>();
		socket.On("open", OnConnected);
		socket.On("spawn", OnSpawn);

	}

	void OnSpawn(SocketIOEvent e)
	{
		Debug.Log("Spawned");
		Instantiate(playerPrefab);
	}
	
	void OnConnected(SocketIOEvent e)
	{
		Debug.Log("connected");
		socket.Emit("move");
	}
}
