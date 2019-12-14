using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float horizontalSpeed = 8;
	private float verticalSpeed = 8;
	[NonSerialized]
	public Vector2 velocity;

	public float minMovableY;
	public float maxMovableY;
	private void Start() {

	}

	// Update is called once per frame
	void Update() {
		MoveByGetAxis();
	}



	private void MoveByGetAxis() {

		float translationX = Input.GetAxis("Horizontalad") * Time.deltaTime * horizontalSpeed;
		float translationY = Input.GetAxis("Verticalws") * Time.deltaTime * verticalSpeed;
        if (transform.position.y + translationY > maxMovableY) {
			translationY = Mathf.Abs(transform.position.y - maxMovableY) < 0.1f ? 0 : maxMovableY - transform.position.y;
		} else if (transform.position.y + translationY < minMovableY) {
			translationY = Mathf.Abs(transform.position.y - minMovableY) < 0.1f ? 0 : transform.position.y - minMovableY;
		}

        Vector2 translation = new Vector2(translationX, Mathf.Clamp(translationY, minMovableY, maxMovableY));
        velocity = translation;
		transform.Translate(translation);
	}


}
