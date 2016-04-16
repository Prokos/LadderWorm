using UnityEngine;
using System.Collections;

public class SmoothCamera2D : MonoBehaviour {

	public Vector2 followMultiplicative = new Vector2(0.001f, 0.01f);
	public Transform target;

	void FixedUpdate () 
	{
		Vector2 destination = new Vector2 (target.position.x, target.position.y);
		Vector2 origin = new Vector2 (transform.position.x, transform.position.y);
		Vector2 delta = destination - origin; 
		//Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
		//Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
		transform.position = transform.position + new Vector3 (delta.x * (1 - Mathf.Pow(followMultiplicative.x, Time.deltaTime)), delta.y  * (1 - Mathf.Pow(followMultiplicative.y, Time.deltaTime)), 0);
		//Vector3 destination = transform.position + delta;
		//transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		Debug.Log(destination.y + " " + transform.position.y + " " + delta.y);
	}
}