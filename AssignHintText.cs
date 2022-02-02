using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignHintText : MonoBehaviour
{
    public TextMeshProUGUI hintText;

    public void AssignText(StringVariable hintTextSO)
    {
        hintText.text = hintTextSO.Value;
    }
}