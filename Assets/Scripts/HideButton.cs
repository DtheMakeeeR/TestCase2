using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HideButton : MonoBehaviour
{
    TMP_Text myText;
    public Object panel;
    bool isHidden;
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<TMP_Text>();
    }
    public void HidePanel()
    {

    }
}
