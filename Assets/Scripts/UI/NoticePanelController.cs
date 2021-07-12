using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NoticePanelController : MonoBehaviour
{

    [SerializeField]
    Text noticePosition;
    [SerializeField]
    GameObject noticeText;
    public Transform FinialPos;
    static NoticePanelController instance;
    //whether all team member are dead

    public static NoticePanelController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(NoticePanelController)) as NoticePanelController;
                if (instance == null)
                {
                    GameObject obj = new GameObject("NoticePanel");
                    instance = obj.AddComponent<NoticePanelController>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
       // Sequence sequence = DOTween.Sequence();
    }

    public void ShowNotice(string text, Color textcolor)
    {

        GameObject newNoticeText =  Instantiate(noticeText, noticePosition.transform.position,Quaternion.identity) as GameObject;
        newNoticeText.transform.SetParent(gameObject.transform.parent);
        newNoticeText.transform.localScale = Vector3.one;
        newNoticeText.GetComponent<NoticeText>().ShowNotice(text, textcolor);
    }
}
