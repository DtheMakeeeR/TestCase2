using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    public List<GameObject> selectedObjectPanels = new List<GameObject>();
    public List<GameObject> managableObjectPanels = new List<GameObject>(); 
    public GameObject panelPrefab;
    public ScrollRect scrollView;
    public GameObject mainCam;
    public GameObject panel;
    public CheckAllButton allCheckbox;
    public CheckAllButton allEnable;
    public Slider allTransparency;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) panel.active = !panel.active;
    }
    void Start()
    {
        List<GameObject> managableObjects = new List<GameObject>();
        managableObjects.AddRange(GameObject.FindGameObjectsWithTag("ManagableObject"));
        Transform content = scrollView.content;
        foreach (GameObject managableObject in managableObjects)
        {
            ManagableObject mObj = managableObject.GetComponent<ManagableObject>();
            mObj.cameraPoint = mainCam.transform.parent.gameObject;
            GameObject panel = Instantiate(panelPrefab, content);
            PrepareEnableButton(panel, mObj);
            PrepareLookAtButton(panel, mObj);
            PrepareTurnAroundButton(panel, mObj);
            PrepareLabel(panel, mObj);
            PrepareSlider(panel, mObj);
            PrepareCheckBox(panel, managableObject);
            PrepareChangeColorButton(panel, mObj);
            managableObjectPanels.Add(panel);
        }
    }

    private void PrepareChangeColorButton(GameObject panel, ManagableObject mObj)
    {
        GameObject child = panel.transform.Find("ChangeColorButton").gameObject;
        UnityEngine.UI.Button b = child.GetComponent<UnityEngine.UI.Button>();
        b.onClick.AddListener(mObj.ChangeColor);
        TMP_InputField channel = panel.transform.Find("RInput").gameObject.GetComponent<TMP_InputField>();
        mObj.rChannel = channel;
        channel = panel.transform.Find("GInput").gameObject.GetComponent<TMP_InputField>();
        mObj.gChannel = channel;
        channel = panel.transform.Find("BInput").gameObject.GetComponent<TMP_InputField>();
        mObj.bChannel = channel;
    }

    private void PrepareCheckBox(GameObject panel, GameObject mObj)
    {
        GameObject child = panel.transform.Find("SelectCheckbox").gameObject;
        CheckBox box = child.GetComponent<CheckBox>();
        box.managableObject = mObj;
        box.manager = this;
    }

    private void PrepareSlider(GameObject panel, ManagableObject mObj)
    {
        GameObject child = panel.transform.Find("Slider").gameObject;
        Slider s = child.GetComponent<Slider>();
        s.onValueChanged.AddListener(mObj.ChangeTransparency);
    }

    private void PrepareLabel(GameObject panel, ManagableObject mObj)
    {
        GameObject child = panel.transform.Find("Label").gameObject;
        TMP_Text tmp = child.GetComponent<TMP_Text>();
        tmp.text = mObj.name;
    }

    private void PrepareTurnAroundButton(GameObject panel, ManagableObject managableObject)
    {
        GameObject child = panel.transform.Find("TrunArounButton").gameObject;
        Button b = child.GetComponent<Button>();
        b.onClick.AddListener(managableObject.TurnRotation);
    }

    private void PrepareLookAtButton(GameObject panel, ManagableObject managableObject)
    {
        GameObject child = panel.transform.Find("LookAtItemButton").gameObject;
        UnityEngine.UI.Button b = child.GetComponent<UnityEngine.UI.Button>();
        b.onClick.AddListener(managableObject.LookAtMe);
    }

    private void PrepareEnableButton(GameObject panel, ManagableObject managableObject)
    {
        GameObject child = panel.transform.Find("EnableButton").gameObject;
        UnityEngine.UI.Button b = child.GetComponent<UnityEngine.UI.Button>();
        b.onClick.AddListener(managableObject.ChangeEnabled);
    }
    public void CheckAllcheckbox()
    {
        if (managableObjectPanels.Count == selectedObjectPanels.Count) allCheckbox.SetChecked();
        else allCheckbox.SetNotChecked();
        //if (selectedObjectPanels.Count > 0) allTransparency.value = (from s in selectedObjectPanels select s.GetComponentInChildren<Slider>().value).Max();
    }
    public void CheckAll()
    {
        Debug.Log("Clicked");
        if (managableObjectPanels.Count > selectedObjectPanels.Count)
        {
            foreach (GameObject panel in managableObjectPanels)
            {
                Toggle t = panel.GetComponentInChildren<Toggle>();
                t.isOn = true;
            }
        }
        else if (managableObjectPanels.Count == selectedObjectPanels.Count)
        {
            foreach (GameObject panel in managableObjectPanels)
            {
                Toggle t = panel.GetComponentInChildren<Toggle>();
                t.isOn = false;
            }
        }
        CheckAllcheckbox();
    }
    public void EnableAll()
    {
        List<GameObject> enabled = (from p in selectedObjectPanels
                                       where !(p.GetComponentInChildren<ViewHideButton>().Hidden)
                                       select p).ToList();
        if ((enabled.Count < selectedObjectPanels.Count) && (enabled.Count > 0))
        {
            foreach (GameObject p in enabled)
            {
                GameObject child = p.transform.Find("EnableButton").gameObject;
                Button b = child.GetComponent<Button>();
                b.onClick.Invoke();
            }
        }
        else
        {
            
            foreach (GameObject p in selectedObjectPanels)
            {
                GameObject child = p.transform.Find("EnableButton").gameObject;
                Button b = child.GetComponent<Button>();
                b.onClick.Invoke();
            }
        }
        if (enabled.Count == selectedObjectPanels.Count) allEnable.SetNotChecked();
        else allEnable.SetChecked();
    }
    public void TransparencyAll()
    {
        foreach(GameObject panel in selectedObjectPanels)
        {
            Slider tSlider = panel.GetComponentInChildren<Slider>();
            tSlider.value = allTransparency.value;
        }
    }
}
