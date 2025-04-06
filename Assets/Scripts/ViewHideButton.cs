using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ViewHideButton : MonoBehaviour
{
    public Sprite viewSprite;
    public Sprite hideSprite;
    Image buttonImage;
    bool isHidden = false;
    public bool Hidden { get { return isHidden; } }
    // Start is called before the first frame update
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void ChangeSprite()
    {
        if (isHidden)
        {
            isHidden = !isHidden;
            buttonImage.sprite = viewSprite;
        }
        else
        {
            isHidden = !isHidden;
            buttonImage.sprite = hideSprite;
        }
    }
}
