using System.Collections.Generic;
using UnityEngine;
using System;

public class BackgroundManager : MonoBehaviour {

	private GameObject background;

	public List<GameObject> backgroundList = new List<GameObject>();

	private new Camera camera;

	private PlayerController controller;


	private void Start() {
		background = Resources.Load<GameObject>("Background");
		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	// Update is called once per frame
	void Update() {
		float[] xPoses = new float[backgroundList.Count];
		for (int i = 0; i < backgroundList.Count; i++) {
			xPoses[i] = backgroundList[i].transform.position.x;
		}
		Array.Sort(xPoses); // 从小到大排序
		if (controller.velocity.x > 0) {
			// 检测是否需要向右延拓背景
			if (xPoses[xPoses.Length - 2] - camera.transform.position.x < 10) {
				GameObject go = Instantiate(background, new Vector2(xPoses[xPoses.Length - 2] + 38, backgroundList[0].transform.position.y), Quaternion.identity, transform);
				backgroundList.Add(go);
			}
		} else if (controller.velocity.x < 0) {
			if (camera.transform.position.x - xPoses[1] < 10) {
				GameObject go = Instantiate(background, new Vector2(xPoses[1] - 38, backgroundList[0].transform.position.y), Quaternion.identity, transform);
				backgroundList.Add(go);
			}
		}
	}
}
