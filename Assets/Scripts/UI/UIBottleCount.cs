using UnityEngine.UI;
using UnityEngine;

public class UIBottleCount : MonoBehaviour {
	public Player player;
	private Text text;


	private void Start() {
		text = GetComponent<Text>();
	}
	// Update is called once per frame
	void Update() {
		text.text = player.catchCount.ToString();
	}
}
