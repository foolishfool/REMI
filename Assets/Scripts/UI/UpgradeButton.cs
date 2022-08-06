using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UpgradeButton : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject ShowPart;
    public GameObject OriginalPart;
    public GameObject ConflictPart;

    public Sprite SelectedImage;
    public Sprite UnSelectedIMage;
    public Button ConflictButton;
    private bool isChosen;
    public int Energy;
    private bool isScalingUp;
    private bool isScalingDown;
    private Vector3 initialScale;
    public  bool isHovering;
    private UIController uicontroller;
    void Start()
    {
        initialScale = transform.localScale;
        uicontroller = GameObject.Find("UIController").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (DataHandler.Instance.Score < Energy)
        {
            GetComponent<Button>().enabled = false;
            GetComponent<Image>().color = Color.gray;
            return;
        }


        if (isHovering)
        {
            transform.localScale = initialScale * 1.1f;
        }
        else transform.localScale = initialScale;


    }

    public void ScaleUp()
    {
        isHovering = true;
        //transform.localScale = initialScale * 1.1f;
        //if (!isScalingUp)
        //{
        //    transform.DOScale(initialScale * 1.1f, 0.5f).OnComplete(()=> isScalingDown = false);
        //    isScalingUp = true;
        //}
      
    }

    public void ScaleDown()
    {
        isHovering = false;
        //transform.localScale = initialScale;
       //if (!isScalingDown)
       //{
       //    isScalingDown = true;
       //    transform.DOScale(initialScale, 0.5f).OnComplete(()=> isScalingUp = false);
       //}
    
    }

    public void ChoosePart()
    {
        isChosen = !isChosen;
        if (isChosen)
        {
            ShowPart.SetActive(true);
            ConflictPart.SetActive(false);
            if (OriginalPart)
                OriginalPart.SetActive(false);
            GetComponent<Image>().sprite = SelectedImage;
            ConflictButton.enabled = false;
            ConflictButton.GetComponent<Image>().color = Color.gray;
            Debug.Log(Energy);
            uicontroller.Energy.text = (int.Parse(uicontroller.Energy.text) - Energy).ToString();
        }

        else
        {
            ShowPart.SetActive(false);
            if (OriginalPart)
            OriginalPart.SetActive(true);
            GetComponent<Image>().sprite = UnSelectedIMage;
            ConflictButton.enabled = true;
            ConflictButton.GetComponent<Image>().color = Color.white;
            uicontroller.Energy.text = (int.Parse(uicontroller.Energy.text) + Energy).ToString();
        }
    }


}
