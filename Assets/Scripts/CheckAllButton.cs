using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAllButton : MonoBehaviour
{
    Image buttonImage;
    public Sprite allChecked;
    public Sprite notChecked;
    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }
    public void SetChecked()
    {
        buttonImage.sprite = allChecked;
    }
    public void SetNotChecked()
    {
        buttonImage.sprite = notChecked;
    }
}
