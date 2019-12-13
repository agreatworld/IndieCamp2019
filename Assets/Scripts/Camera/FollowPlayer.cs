using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	/// <summary>
	/// 摄像机
	/// </summary>
	private Camera camera;

	private float cameraViewWidthExtends;

	private float cameraViewHeightExtends;

	/// <summary>
	/// 跟随目标
	/// </summary>
	public GameObject target;

	/// <summary>
	///  地图边界
	/// </summary>
	public GameObject mapBound;

	/// <summary>
	/// 地图边界碰撞器
	/// </summary>
	private BoxCollider2D mapCollider;

	/// <summary>
	/// 相机移动的时长
	/// </summary>
	private float smoothTime = 0.1f;

	private Vector3 currentVelocity = Vector3.zero;

	// Start is called before the first frame update
	void Start() {
		camera = GetComponent<Camera>();
		mapCollider = mapBound.GetComponent<BoxCollider2D>();
		cameraViewHeightExtends = camera.orthographicSize;
		cameraViewWidthExtends = cameraViewHeightExtends * camera.aspect;
	}

	// Update is called once per frame
	void Update() {
		Vector3 targetPosition = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
		if (targetPosition.x - cameraViewWidthExtends < mapCollider.bounds.min.x || targetPosition.x + cameraViewWidthExtends > mapCollider.bounds.max.x) {
			targetPosition.x = transform.position.x;
		}
		if (targetPosition.y - cameraViewHeightExtends < mapCollider.bounds.min.y || targetPosition.y + cameraViewHeightExtends > mapCollider.bounds.max.y) {
			targetPosition.y = transform.position.y;
		}
		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
	}
}
