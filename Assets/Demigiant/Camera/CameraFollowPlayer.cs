using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

	/// <summary>
	/// 摄像机
	/// </summary>
	private Camera camera;

	private float cameraViewWidthExtends;

	/// <summary>
	/// 跟随目标
	/// </summary>
	private GameObject target;

	/// <summary>
	/// 相机移动的时长
	/// </summary>
	private float smoothTime = 0.3f;

	private Vector3 currentVelocity = Vector3.zero;

	private PlayerController controller;

	public bool isMovingRight = false;

	public bool isMovingLeft = false;

	// Start is called before the first frame update
	void Start() {
		camera = GetComponent<Camera>();
		cameraViewWidthExtends = camera.orthographicSize * camera.aspect;
		target = GameObject.FindGameObjectWithTag("Player");
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
			// 镜头左移
			if (transform.position.x < 0) {
				return;
			}
			Vector3 targetPosition = target.transform.TransformPoint(-cameraViewWidthExtends * 0.5f, 0, 10);
			targetPosition.y = 0;
			if (controller.velocity.x > 0) {
				isMovingLeft = false;
			}
			transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref currentVelocity, smoothTime);
		}
		

	}

}


