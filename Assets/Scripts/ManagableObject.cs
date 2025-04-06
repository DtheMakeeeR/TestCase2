using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MeshRenderer))]
public class ManagableObject : MonoBehaviour
{
    public float rotationSpeed = 10;
    public GameObject cameraPoint;
    MeshRenderer meshRenderer;
    MaterialPropertyBlock mpb;
    public TMP_InputField rChannel;
    public TMP_InputField gChannel;
    public TMP_InputField bChannel;
    bool isEnabled;
    public bool Enable { get { return isEnabled; } }
    static bool isRotating = false;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        isEnabled = gameObject.active;
        mpb = new MaterialPropertyBlock();
        mpb.SetColor("_Color", meshRenderer.material.color);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating) RotateCameraAroundObject();
    }
    public void ChangeEnabled()
    {
        if (isEnabled)
        {
            isEnabled = !isEnabled;
            gameObject.SetActive(isEnabled);
        }
        else
        {
            isEnabled = !isEnabled;
            gameObject.SetActive(isEnabled);
        }
    }
    public void ChangeTransparency(float v)
    {
        Color c = meshRenderer.material.color;
        c.a = v/255;
        meshRenderer.material.color = c;
    }
    Color GetColor()
    {
        int r , g , b;
        int.TryParse(rChannel.text, out r);
        int.TryParse(gChannel.text, out g);
        int.TryParse(bChannel.text, out b);
        return new Color(r/255, g/255, b/255);
    }
    public void ChangeColor()
    {
        float alpha = meshRenderer.material.color.a;
        Color c = GetColor();
        c.a = alpha;
        meshRenderer.material.color = c;
    }
    public void TurnRotation()
    {
        if (!isRotating)
        {
            cameraPoint.transform.position = transform.position;
            isRotating = !isRotating;
            LookAtMe();
        }
        else if (isRotating && cameraPoint.transform.position != transform.position)
        {
            cameraPoint.transform.position = transform.position;
            LookAtMe();
        }
        else if (isRotating && cameraPoint.transform.position == transform.position)
        {
            isRotating = !isRotating;
        }
        else isRotating = !isRotating;
    }
    public void RotateCameraAroundObject()
    {
        cameraPoint.transform.Rotate(0, rotationSpeed*Time.deltaTime, 0);
    }
    public void LookAtMe()
    {
        //cameraPoint.transform.position = transform.position;
        cameraPoint.transform.GetChild(0).GetComponent<MouseControl>().LookAtPoint(transform);
    }
}
