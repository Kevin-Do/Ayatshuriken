using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : MonoBehaviour {
	
	[Range(0,1000)] public float spinSpeed;

	[Range(0, 50)] public float deploySpeed;
	
	[Range(0, 50)] public float returnSpeed;

	//TODO: Player singleton and proper access
	public Transform player;
	
	private float startTime, totalDistance;

	private Vector3 endPos;
	
	// Use this for initialization
	void Start () {
		endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		startTime = Time.time;
		totalDistance = Vector3.Distance(transform.position, endPos);
	}
	
	void Update () {
		//Rotation
		transform.Rotate(Vector3.forward * spinSpeed * Time.deltaTime);
		
		//Go To Mouse
		float currentDuration = (Time.time - startTime) * deploySpeed;
		float journeyPercent = currentDuration / totalDistance;
		transform.position = Vector3.Lerp(transform.position, endPos, journeyPercent);
		//Restricting Z Axis Value
		transform.position = new Vector3(transform.position.x, transform.position.y, 0);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log("Space Pressed");
			StartCoroutine(ReturnShuriken());
		}
	}

	IEnumerator ReturnShuriken()
	{
		//while not at player yet -> lerp to player
		//once completed print returned!
		Debug.Log(player.position);
		while (Vector3.Distance(transform.position, player.position) > 0.5f)
		{
			transform.position = Vector3.Lerp(transform.position, player.position, returnSpeed * Time.deltaTime);
			yield return null;
		}
		print("Returned to player!");
		Destroy(gameObject, 1);
	}
}
