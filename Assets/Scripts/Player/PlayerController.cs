using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private float horizontalSpeed = 8;
	private float verticalSpeed = 8;
	private Vector2 velocity;
	private List<RaycastHit2D> hits = new List<RaycastHit2D>();

	// Update is called once per frame
	void Update() {
		MoveByGetAxis();
		UpdateHits();
	}

	private void LateUpdate() {
		HandleHits();
	}

	private void HandleHits() {
		foreach (var hit in hits) {
			if (hit.collider != null) {
				Debug.Log(hit.transform.name);
			}
		}
	}

	private void MoveByGetAxis() {
		float translationX = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;
		float translationY = Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed;
		Vector2 translation = new Vector2(translationX, translationY);
		velocity = translation;
		transform.Translate(translation);
	}

	private void UpdateHits() {
		hits.Clear();
		Vector2 origin = transform.position;
		Vector2 direction = velocity;
		float distance = 1;
		Debug.DrawRay(origin, direction);
		hits.Add(Physics2D.Raycast(origin, direction, distance));
	}
}
