using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClamp : MonoBehaviour
{

	[Range(-2,2)] public float xMin, xMax, yMin, yMax;
	public float smoothingSpeed;
	public Transform target;
	
	// Update is called once per frame
	void LateUpdate ()
	{
		float trackingX = Mathf.Clamp(target.position.x, xMin, xMax);
		float trackingY = Mathf.Clamp(target.position.y, yMin, yMax);
		Vector3 targetPosition = new Vector3(trackingX, trackingY, transform.position.z);
		Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothingSpeed * Time.deltaTime);
		transform.position = smoothedPosition;
	}
}
