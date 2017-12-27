using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Controller : MonoBehaviour {

	//Player Components
	protected Rigidbody2D rb;
	protected SpriteRenderer[] sr;
	//TODO: Collision and shuriken hit detection
	protected Collider2D coll;
	
	//Static Player Values (Setup)
	[Range(0,20)] public float haltingMultiplier;
	[Range(0, 100)] public float playerSpeed;
	
	//Shuriken Prefab
	public GameObject ShurikenPrefab;
	
	//Current Player Values (Run Time)
	[Range(0, 10)] public int shurikenCount;
	[Range(0, 200)] public float healthPoints;
	protected float horizontalInput;
	protected float verticalInput;

	
	void Awake ()
	{
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponentsInChildren<SpriteRenderer>();
		coll = GetComponent<Collider2D>();
	}
	
	void Update ()
	{
		//Handle Move & Direction
		Move();
		HandleDirection();
		
		//Handle Weightiness
		MomentumMultipliers();
		
		//Handle Shuriken Deployment
		if (Input.GetMouseButtonDown(0))
		{
			DeployShuriken();	
		}
		
		if (Input.GetKeyDown(KeyCode.Space))
		{
			ReturnShuriken();
		}
	}

	void Move()
	{
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");

		rb.velocity = new Vector2(horizontalInput, verticalInput) * playerSpeed;
	}

	void HandleDirection()
	{
		//TODO: Handle local scale * -1
		if (horizontalInput > 0 && sr[0].flipX)
		{
			for(int i = 0; i < sr.Length; i++)
			{
				sr[i].flipX = false;
			}
		} else if (horizontalInput < 0 && !sr[0].flipX)
		{
			for(int i = 0; i < sr.Length; i++)
			{
				sr[i].flipX = true;
			}
		}
	}

	void MomentumMultipliers()
	{
		//TODO: Not working atm
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

	void DeployShuriken()
	{
		if (shurikenCount > 0)
		{
			var shurikenInstance = (GameObject) Instantiate(
				ShurikenPrefab,
				transform.position,
				transform.rotation);
			shurikenCount--;
		}
	}

	void ReturnShuriken()
	{
		shurikenCount--;
	}
}
