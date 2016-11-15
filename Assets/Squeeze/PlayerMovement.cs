using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	void Start () {
	
	}
	
	void Update () {
		if (Input.GetKey(KeyCode.W)) {
			Move(0, 1);
		}
		if (Input.GetKey(KeyCode.S)) {
			Move(0, -1);
		}
		if (Input.GetKey(KeyCode.A)) {
			Move(-1, 0);
		}
		if (Input.GetKey(KeyCode.D)) {
			Move(1, 0);
		}
	}

	void Move(float xAmount, float zAmount)
	{
		transform.Translate(xAmount * Time.deltaTime, 0, zAmount * Time.deltaTime);
	}
}
