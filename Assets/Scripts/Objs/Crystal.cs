using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Crystal : MonoBehaviour
{
    public int OptionID;
    private Vector3 initalScale;

    // Start is called before the first frame update
    void Start()
    {
        float y = transform.position.y;

        transform.DOMoveY(y+1,2f).SetLoops(-1,LoopType.Yoyo);
        initalScale = transform.localScale;

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, transform.up, Time.deltaTime * -90f);
    }

    public void Selected()
    {

        if (!gameObject.activeSelf )
        {
            return;
        }

        transform.DOPunchScale(initalScale * 1.001f, 0.5f, 10, 1);

        switch (OptionID)
        {
            case 1:
                GameController.Instance.CurrentDialogue.OptionObj1.Clicked();
                break;
            case 2:
                GameController.Instance.CurrentDialogue.OptionObj2.Clicked();
                break;
            case 3:
                GameController.Instance.CurrentDialogue.OptionObj3.Clicked();
                break;
            default:
                break;
        }
     
    }

}
