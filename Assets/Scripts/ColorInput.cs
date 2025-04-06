using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Text;

public class ColorInput : MonoBehaviour
{
    
    private int Clamp(int value, int min = 0, int max = 255)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
    public void ClampInput()
    {
        string s = GetComponent<TMP_InputField>().text;
        int v;
        if (int.TryParse(s, out v))
        {
            v = Clamp(v);
            GetComponent<TMP_InputField>().text = v.ToString();
        }
        else
        {
            GetComponent<TMP_InputField>().text = "0";
        }       
    }
}
