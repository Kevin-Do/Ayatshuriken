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
	[Range(0, 10)] public int shurikenCount;
	[Range(0, 100)] public float deploySpeed;
	
	//Shuriken Prefab
	public GameObject ShurikenPrefab;
	
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
		
		//Handle Shuriken Deployment
		if (Input.GetMouseButtonDown(0))
		{
			StartCoroutine(DeployShuriken());
		}
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

	IEnumerator DeployShuriken()
	{
		if (shurikenCount > 0)
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			var shurikenInstance = (GameObject) Instantiate(
				ShurikenPrefab,
				transform.position,
				transform.rotation);
			shurikenInstance.transform.position = Vector3.Lerp(transform.position, mousePos, Time.deltaTime * deploySpeed);
			shurikenCount--;
		}
		yield return new WaitForSeconds(.1f);
	}
}
