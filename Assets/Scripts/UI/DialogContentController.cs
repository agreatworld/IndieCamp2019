using UnityEngine.UI;
using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class DialogContentController : MonoBehaviour {

	private Text content;

	private int index = 0;

	public List<string> contentList = new List<string>();
	// Start is called before the first frame update
	void Start() {
		content = GetComponent<Text>();
		InitDialogContentList();
	}

	private void InitDialogContentList() {
		// act 1
		contentList.Add("这个影子简直是像活着的一样......");
		contentList.Add("但是，很熟悉的感觉，像是似曾相识，很久以前就很熟悉的温柔的气息");
		contentList.Add("能听见我说话么？");
		contentList.Add("你给我一种好熟悉的感觉啊。");

		// act 2
		contentList.Add("你收集这些灵魂是为了什么？");
		contentList.Add("不知道，只是下意识的");
		contentList.Add("被关在这里也太可怜了");
		contentList.Add("唔......");
		contentList.Add("你的心里缺了一块，但你却浑然不知。");
		contentList.Add("？！！");
		contentList.Add("你把你的心的一部分给了一个人，你认为除了他谁都不配拥有。\n但也正因为如此，你才能凭借这仅有的维系来到这里啊。");
		contentList.Add("......");
		contentList.Add("我们做个交易，你把这些灵魂给我，我告诉你怎么去寻找那一片心的碎片。");
		contentList.Add("成交。");

		contentList.Add("");
		contentList.Add("");
		contentList.Add("");
		contentList.Add("");
		contentList.Add("");
		contentList.Add("");
		contentList.Add("");
		contentList.Add("");
		contentList.Add("");

	
	}

	// Update is called once per frame
	void Update() {
		if (Input.anyKeyDown) {
			content.text = "";
			if (index < contentList.Count) {
				content.DOText(contentList[index++], 2);
			}
		}
	}

}
