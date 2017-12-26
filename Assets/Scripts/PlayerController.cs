using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

	//Player Components
	private Rigidbody2D rb;
	
	//Player Values
	[Range(0,20)] public float haltingMultiplier;

	[Range(0, 100)] public float playerSpeed;
	
	void Awake ()
	{
		rb = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
	{
		//Handle Move
		Move();
		
		//Handle Weightiness
		MomentumMultipliers();
	}

	void Move()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		rb.velocity = new Vector2(horizontalInput, verticalInput) * playerSpeed;
	}

	void MomentumMultipliers()
	{
		if (rb.velocity.y != 0.0f && Input.GetAxis("Vertical") == 0.0f)
		{
			Debug.Log("Y Halting");
			rb.velocity += Vector2.up * Physics2D.gravity.y * (haltingMultiplier - 1) * Time.deltaTime;
		}
		if (rb.velocity.x != 0.0f && Input.GetAxis("Horizontal") == 0.0f)
		{
			Debug.Log("X Halting");
			rb.velocity += Vector2.right * Physics2D.gravity.y * (haltingMultiplier - 1) * Time.deltaTime;
		}
	}
}
