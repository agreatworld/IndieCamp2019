using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForegroundManager : MonoBehaviour {

	public GameObject foreground;
	[SerializeField]
	private List<GameObject> foregroundList = new List<GameObject>();

	public new Camera camera;

	private bool needExtendMap = false;
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
