using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGhosts : MonoBehaviour {

	private DialogContentController dialogContentController;

	// Start is called before the first frame update
	void Start() {
		dialogContentController = GameObject.FindGameObjectWithTag("DialogContent").GetComponent<DialogContentController>();
	}

	private void OnDestroy() {
		if (GameObject.FindGameObjectsWithTag("Goast").Length == 0) {
			if (dialogContentController.hasPlayedAct1 && dialogContentController.hasPlayedAct2 && !dialogContentController.hasPlayedAct3 && !dialogContentController.hasPlayedAct4) {
				dialogContentController.generateTheOtherThreeGhosts = true;
			}
		}
	}

}
