using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		if (transform.name == "Begin") {
			SceneManager.LoadScene("MainScene");
		}
	}

	public void OnPointerEnter(PointerEventData eventData) {
		transform.DOScale(Vector2.one * 1.2f, 0.2f);
	}

	public void OnPointerExit(PointerEventData eventData) {
		transform.DOScale(Vector2.one, 0.1f);
	}

	// Start is called before the first frame update
	void Start() {
	}

}
