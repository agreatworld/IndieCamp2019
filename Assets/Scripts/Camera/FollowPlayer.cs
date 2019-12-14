using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	/// <summary>
	/// 摄像机
	/// </summary>
	private Camera camera;

	private float cameraViewWidthExtends;

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

	private PlayerController controller;

	private bool isMovingRight = false;

	private bool isMovingLeft = false;

	// Start is called before the first frame update
	void Start() {
		camera = GetComponent<Camera>();
		mapCollider = mapBound.GetComponent<BoxCollider2D>();
		cameraViewWidthExtends = camera.orthographicSize * camera.aspect;
		controller = target.GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update() {
		// 判断镜头是否需要移动
		if (target.transform.position.x - transform.position.x > cameraViewWidthExtends * 0.5f) {
			isMovingRight = true;
		} else if (transform.position.x - target.transform.position.x > cameraViewWidthExtends * 0.5f) {
			isMovingLeft = true;
		}
		if (isMovingRight) {
			// 镜头右移
			Vector3 targetPosition = target.transform.TransformPoint(-cameraViewWidthExtends * 0.5f, 0, -10);
			targetPosition.y = 0;
			if (controller.velocity.x < 0) {
				isMovingRight = false;
			}

			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);

		} else if (isMovingLeft) {
			Vector3 targetPosition = target.transform.TransformPoint(cameraViewWidthExtends * 0.5f, 0, -10);
			targetPosition.y = 0;
			if (controller.velocity.x > 0) {
				isMovingLeft = false;
			}
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
		}
		

	}

}


