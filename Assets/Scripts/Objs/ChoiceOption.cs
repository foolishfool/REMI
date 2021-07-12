using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ChoiceOption : MonoBehaviour
{

    public string OptionNum;
    public TMP_InputField Input;
    public bool HasInput;
    public int CharacterLimited;
    // Start is called before the first frame update
    void Start()
    {
        if (HasInput && CharacterLimited!= 0)
        {
            Input.characterLimit = CharacterLimited;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
