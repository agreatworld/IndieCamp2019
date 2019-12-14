using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundManager : MonoBehaviour {

	private GameObject foreground;
	[SerializeField]
	private List<GameObject> foregroundList = new List<GameObject>();

	private new Camera camera;

	private bool needExtendMap = false;


	private void Start() {
		foreground = Resources.Load<GameObject>("ForeGround");
		camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
	}
	// Update is called once per frame
	void Update() {
		GameObject decisionPoint = foregroundList[foregroundList.Count - 2];
		if (foregroundList[foregroundList.Count - 1].transform.position.x - camera.transform.position.x < 20) {
			if (Mathf.Abs(camera.transform.position.x - decisionPoint.transform.position.x) < 0.5f) {
				needExtendMap = true;
			}
			if (needExtendMap) {
				GameObject go = Instantiate(foreground, new Vector2(decisionPoint.transform.position.x + 38, 0), Quaternion.identity, transform);
				foregroundList.Add(go);
				needExtendMap = false;
			}
		}
		
	}
}
