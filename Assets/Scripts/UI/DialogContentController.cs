using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System.IO;
using System;

public class DialogContentController : MonoBehaviour {

	private Text text;
	private bool ifPlayNext = false;
	private Vector2 dialogRectShowing = new Vector2();
	public GameObject dialog;
	private Vector2 dialogRect = new Vector2();
	private Tweener tweener;
	public List<string> contentList = new List<string>();
	private List<List<string>> segments = new List<List<string>>();
	private Transform player;
	private bool hasPlayedAct1 = false;
	private bool hasPlayedAct2 = false;
	private bool hasPlayedAct3 = false;
	private bool hasPlayedAct4 = false;
	private Player playerComponent;
	private bool hasShowCatTwice = false;
	public float animationPlayTime = 6;
	private float animationPlayTimer = 0;
	private bool isTimingForAnimation = false;
	public int count;
	// Start is called before the first frame update
	void Start() {
		// 读取剧本
		try {
			using (StreamReader sr = new StreamReader("dialog.txt")) {
				string content = sr.ReadToEnd();
				string[] segment = content.Split('#');
				foreach (var s in segment) {
					segments.Add(new List<string>(s.Split('\n')));
				}
			}
		} catch (Exception e) {
			// 向用户显示出错消息
			Debug.LogError(e.Message);
		}
		player = GameObject.FindGameObjectWithTag("Player").transform;
		dialogRect = dialog.GetComponent<RectTransform>().anchoredPosition;
		dialogRectShowing = dialogRect;
		text = GetComponent<Text>();
		playerComponent = player.GetComponent<Player>();

	}


	// Update is called once per frame
	void Update() {
		ShowDialog();
		if (!hasShowCatTwice) {
			ShowCat();
		}
		if (isTimingForAnimation) {
			if (animationPlayTimer > animationPlayTime) {
				// 开启第四幕的对话框
				TriggerAct();
				animationPlayTimer = 0;
				isTimingForAnimation = false;
			}
			animationPlayTimer += Time.deltaTime;
		}
		
	}

	private void ShowCat() {
		if (hasPlayedAct2 && !hasPlayedAct3 && /*count*/playerComponent.catchCount >= 6) {
			GameObject.FindGameObjectWithTag("Cat").GetComponent<Cat>().ResetState();
			hasShowCatTwice = true;
		}
	}

	public void TriggerAct() {
		if (segments.Count > 0) {
			if (!hasPlayedAct1) {
				UpdateContentList();
				hasPlayedAct1 = true;
			} else if (!hasPlayedAct2) {
				UpdateContentList();
				hasPlayedAct2 = true;
			} else if (!hasPlayedAct3) {
				UpdateContentList();
				hasPlayedAct3 = true;
			} else if (!hasPlayedAct4) {
				UpdateContentList();
				hasPlayedAct4 = true;
			}

		}
	}

	private void UpdateContentList() {
		contentList.Clear();
		// 触发对话框
		contentList = segments[0];
		segments.RemoveAt(0);
		ifPlayNext = true;
		string[] strings = contentList[0].Split('*');
		contentList.RemoveAt(0);
		if (strings.Length != 2) {
			return;
		}
		if (strings[1] != null) {
			text.DOText(strings[1], 1);
		}
	}

	private void ShowDialog() {
		if (contentList.Count <= 0) {
			// 隐藏对话框
			if (segments.Count > 0) {
				dialog.GetComponent<RectTransform>().anchoredPosition = Vector2.down * 1000;
			}
			if (hasPlayedAct1 && hasPlayedAct2 && hasPlayedAct3 && !hasPlayedAct4) {
				isTimingForAnimation = true;
			}
			return;
		} else {
			dialog.GetComponent<RectTransform>().anchoredPosition = dialogRectShowing;
			if (Input.GetKeyDown(KeyCode.Space) && ifPlayNext && contentList.Count > 0) {
				text.DOText("", 0.0001f);
				string[] strings = contentList[0].Split('*');
				contentList.RemoveAt(0);
				if (strings.Length != 2) {
					return;
				}
				// 换对话框处理
				if (strings[1] != null) {
					tweener = text.DOText(strings[1], 1);
					tweener.OnComplete(() => {
						ifPlayNext = true;
					});
					ifPlayNext = false;
				}
			} else if (Input.anyKeyDown && !ifPlayNext) {
				tweener.Goto(1);
				ifPlayNext = true;
			}
		}
		
	}
}
