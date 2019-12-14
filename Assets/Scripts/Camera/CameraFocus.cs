using DG.Tweening;
using UnityEngine;

public class CameraFocus : MonoBehaviour {

	public bool isFocusing = false;
	private Vector3 originalPosition;
	private float size;
	private new Camera camera;
	private float aspect;

	private GameObject player;
	private GameObject ghost;
	private bool hasFocused = false;

	private Sprite playerSprite;
	private Sprite ghostSprite;

	private void Start() {
		camera = GetComponent<Camera>();
		player = GameObject.FindGameObjectWithTag("Player");
		ghost = GameObject.FindGameObjectWithTag("Goast");
		playerSprite = player.GetComponent<SpriteRenderer>().sprite;
		ghostSprite = ghost.GetComponent<SpriteRenderer>().sprite;
		originalPosition = transform.position;
		size = camera.orthographicSize;
		aspect = camera.aspect;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.T)) {
			if (!hasFocused) {
				Vector3 origin = (player.transform.position + ghost.transform.position) / 2;
				origin.z = -10;
				float extendsY = Mathf.Abs(player.transform.position.y - ghost.transform.position.y) + playerSprite.bounds.extents.y + ghostSprite.bounds.extents.y;
				extendsY *= 0.5f;
				Debug.Log(extendsY);
				transform.DOMove(origin, 0.3f);
				camera.DOOrthoSize(extendsY, 0.3f);
				hasFocused = true;
			}
		}
	}
}
