using System.Collections;
using UnityEngine;
using SocketIO;

public class Network : MonoBehaviour
{
	static SocketIOComponent socket;

	// Use this for initialization
	void Start ()
	{
		socket = GetComponent<SocketIOComponent>();
		socket.On("open", OnConnected);
	}

	void OnConnected(SocketIOEvent e)
	{
		Debug.Log("connected");
		socket.Emit("move");
	}
}
