using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float horizontalSpeed = 8;
	private float verticalSpeed = 8;
	[NonSerialized]
	public Vector2 velocity;

	// Update is called once per frame
	void Update() {
		MoveByGetAxis();
	}



	private void MoveByGetAxis() {
		float translationX = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;
		float translationY = Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed;
		Vector2 translation = new Vector2(translationX, translationY);
		velocity = translation;
		transform.Translate(translation);
	}


}
