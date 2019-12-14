using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour {

	private new BoxCollider2D collider;

	// Start is called before the first frame update
	void Start() {
		collider = GetComponent<BoxCollider2D>();
	}

	public void HitByPlayer() {
		collider.enabled = false;
	}

	public void ResetState() {
		collider.enabled = true;
	}

}
