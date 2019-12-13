using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour {

	protected List<RaycastHit2D> hits = new List<RaycastHit2D>();
	private PlayerController controller;
	private new BoxCollider2D collider;
	public float density = 0.2f;
	[Range(0.001f, 0.3f)]
	public float skinWidth = 0.02f;
	private int horizontalRayCount;
	private int verticalRayCount;

	public void Start() {
		controller = GetComponent<PlayerController>();
		collider = GetComponent<BoxCollider2D>();
		verticalRayCount = (int)(collider.bounds.extents.x * 2 / density);
		horizontalRayCount = (int)(collider.bounds.extents.y * 2 / density);
	}

	// Update is called once per frame
	public void Update() {
		UpdateHits();
	}

	public void LateUpdate() {
		HandleHits();
	}

	/// <summary>
	/// 每一帧进行一次针对所有需要层级的射线检测，一帧调用一次
	/// </summary>
	private void UpdateHits() {
		hits.Clear();
		int layerMask = 0;
		layerMask = 1 << 8;
		CastRaysWithLayerMask(layerMask);
	}



	private void CastRaysWithLayerMask(int layerMask) {
		RaycastHit2D hit = Physics2D.Raycast(Vector2.zero, Vector2.zero);
		// 投射水平射线
		float rayDistance = 1;
		for (int i = 0; i <= horizontalRayCount; i++) {
			Vector2 rayOrigin1 = new Vector2(collider.bounds.max.x + skinWidth, collider.bounds.min.y + i * density); // 水平向右的射线原点
			Vector2 rayDirection1 = Vector2.right; // 水平向右方向
			Vector2 rayOrigin2 = new Vector2(collider.bounds.min.x - skinWidth, collider.bounds.min.y + i * density); // 水平向左的射线原点
			Vector2 rayDirection2 = Vector2.left; // 水平向左方向
			hit = Physics2D.Raycast(rayOrigin1, rayDirection1, rayDistance, layerMask);
			Debug.DrawRay(rayOrigin1, rayDirection1.normalized * rayDistance, Color.red);
			if (hit.collider != null) {
				hits.Add(hit);
			}
			hit = Physics2D.Raycast(rayOrigin2, rayDirection2, rayDistance, layerMask);
			Debug.DrawRay(rayOrigin2, rayDirection2.normalized * rayDistance, Color.red);
			if (hit.collider != null) {
				hits.Add(hit);
			}
		}

		// 投射垂直射线
		for (int i = 0; i <= verticalRayCount; i++) {
			Vector2 rayOrigin1 = new Vector2(collider.bounds.min.x + i * density, collider.bounds.max.y + skinWidth); // 垂直向上的射线原点
			Vector2 rayDirection1 = Vector2.up; // 垂直向上方向
			Vector2 rayOrigin2 = new Vector2(collider.bounds.min.x + i * density, collider.bounds.min.y - skinWidth); // 垂直向下的射线原点
			Vector2 rayDirection2 = Vector2.down; // 垂直向下方向
			hit = Physics2D.Raycast(rayOrigin1, rayDirection1, rayDistance, layerMask);
			Debug.DrawRay(rayOrigin1, rayDirection1.normalized * rayDistance, Color.red);
			if (hit.collider != null) {
				hits.Add(hit);
			}
			hit = Physics2D.Raycast(rayOrigin2, rayDirection2, rayDistance, layerMask);
			Debug.DrawRay(rayOrigin2, rayDirection2.normalized * rayDistance, Color.red);
			if (hit.collider != null) {
				hits.Add(hit);
			}
		}
	}

	public virtual void HandleHits() {
		foreach (var hit in hits) {
			if (hit.collider != null) {
				//Debug.Log($"碰撞检测到 {hit.transform.name}");
			}
		}
		hits.Clear();
	}
}
