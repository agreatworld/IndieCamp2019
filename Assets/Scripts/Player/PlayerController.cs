using System;
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

	private void MoveByGetAxis() {
		float translationX = Input.GetAxis("Horizontal") * Time.deltaTime * horizontalSpeed;
		float translationY = Input.GetAxis("Vertical") * Time.deltaTime * verticalSpeed;
		Vector2 translation = new Vector2(translationX, translationY);
		velocity = translation;
		transform.Translate(translation);
	}

	/// <summary>
	/// 每一帧进行一次针对所有需要层级的射线检测，一帧调用一次
	/// </summary>
	private void UpdateHits() {
		hits.Clear();
		Vector2 rayOrigin = transform.position;
		Vector2 rayDirection = velocity;
		float rayDistance = 1;
		int layerMask = 0;
		// 用 layerMask 控制检测的各层级
		layerMask = 1 << 8;
		CastRaysWithLayerMask(rayOrigin, rayDirection, rayDistance, layerMask);
		
	}


	private void CastRaysWithLayerMask(Vector2 rayOrigin, Vector2 rayDirection, float rayDistance, int layerMask) {
		Debug.DrawRay(rayOrigin, rayDirection.normalized * rayDistance);
		RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayDistance, layerMask);
		if (hit.collider != null) {
			hits.Add(hit);
		}
	}

	private void HandleHits() {
		foreach (var hit in hits) {
			if (hit.collider != null) {
				Debug.Log($"碰撞检测到 {hit.transform.name}");
			}
		}
		hits.Clear();
	}
}
