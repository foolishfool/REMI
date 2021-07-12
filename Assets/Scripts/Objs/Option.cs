using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{

    public int NextDialogueID;
    public Sprite pressedSprite;
    private Sprite normalSprite;
    // Start is called before the first frame update
    void Start()
    {

        normalSprite = GetComponent<Button>().GetComponent<Image>().sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicked()
    {
        GameController.Instance.OptionClicked = true;
        transform.parent.GetComponent<Dialogue>().NextButton.SetActive(true);
        GameController.Instance.NewDialogueID = NextDialogueID;
        GetComponent<Image>().sprite = transform.parent.GetComponent<Dialogue>().OptionObj1.pressedSprite;
        ResetOtherOptions();
    }


    public void ResetOtherOptions()
    {
        if (transform.parent.GetComponent<Dialogue>().OptionObj1 != this)
        {
            transform.parent.GetComponent<Dialogue>().OptionObj1.GetComponent<Image>().sprite = transform.parent.GetComponent<Dialogue>().OptionObj1.normalSprite;
        }

        if (transform.parent.GetComponent<Dialogue>().OptionObj2 != this)
        {
            transform.parent.GetComponent<Dialogue>().OptionObj2.GetComponent<Image>().sprite = transform.parent.GetComponent<Dialogue>().OptionObj2.normalSprite;
        }

        if (transform.parent.GetComponent<Dialogue>().OptionObj3 != this)
        {
            transform.parent.GetComponent<Dialogue>().OptionObj3.GetComponent<Image>().sprite = transform.parent.GetComponent<Dialogue>().OptionObj3.normalSprite;
        }
    }

}
