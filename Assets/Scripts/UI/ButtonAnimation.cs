using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ButtonAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler {
	public void OnPointerClick(PointerEventData eventData) {
		if (transform.name == "Begin") {
			SceneManager.LoadSceneAsync("MainScene");
			transform.DOMove(Vector2.up * 1.5f, 0.3f);
			GetComponent<Text>().DOFade(0, 0.3f);
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
