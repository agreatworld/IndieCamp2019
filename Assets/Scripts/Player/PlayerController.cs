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
	private Animator animator;
	private void Start() {
		animator = gameObject.GetComponent<Animator>();

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
			translationY = Mathf.Abs(transform.position.y - minMovableY) < 0.1f ? 0 : minMovableY - transform.position.y;
		}

		Vector2 translation = new Vector2(translationX, translationY);
		if (transform.position.x < 0) {
			translationX = translationX < 0 ? 0 : translationX;
			translation = new Vector2(translationX, translation.y);
		}
		velocity = translation;
		transform.Translate(translation,Space.World);
		if (translation != Vector2.zero) {
			animator.SetBool("IsRun", true);
		} else
			animator.SetBool("IsRun", false);
		if (translation.x > 0)
			gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
		if (translation.x < 0)
			gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
	}


}
