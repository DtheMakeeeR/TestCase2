using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Toggle))]
public class CheckBox : MonoBehaviour
{
    public GameObject managableObject;
    public GUIManager manager;
    public void SelectItem()
    {
        if (manager.selectedObjectPanels.Contains(transform.parent.gameObject)) manager.selectedObjectPanels.Remove(transform.parent.gameObject);
        else manager.selectedObjectPanels.Add(transform.parent.gameObject);
        manager.CheckAllcheckbox();
    }    
}
