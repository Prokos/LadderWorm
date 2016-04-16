using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {

	public float followMultiplicative = 0.2f;
	public Transform target;

	void FixedUpdate () 
	{
		Vector2 destination = new Vector2 (target.position.x, target.position.y);
		Vector2 origin = new Vector2 (transform.position.x, transform.position.y);
		Vector2 delta = destination - origin; 
		//Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
		//Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
		transform.position = transform.position + new Vector3 (delta.x, delta.y, 0) * (1 - Mathf.Pow(followMultiplicative, Time.deltaTime));
		//Vector3 destination = transform.position + delta;
		//transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		Debug.Log(destination.y + " " + transform.position.y + " " + delta.y);
	}
}