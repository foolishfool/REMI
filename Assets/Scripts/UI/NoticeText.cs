using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class NoticeText : MonoBehaviour

{ 
    private Text notice;


    public void ShowNotice(string text, Color textcolor)
    {
        notice = GetComponent<Text>();
        notice.text = text;
        notice.color = textcolor;
        //790= UI parent 640+ destination 150
        GetComponent<RectTransform>().DOMoveY(790, 1f).OnComplete(() => 
        {
            notice.DOColor(new Color(0, 0, 0, 0), 1f).OnComplete(() => Destroy(gameObject));
            GetComponent<RectTransform>().DOMoveY(NoticePanelController.Instance.FinialPos.position.y, 3f);
        });

    }
}
