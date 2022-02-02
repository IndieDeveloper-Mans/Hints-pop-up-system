using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PopUpHint
{
    [Header("Hint Info")]
    [Multiline]
    public string hintText;
    [Space]
    public int hintIndex;
    [Space]
    public bool isHintShowedOnce;
    [Space]
    public bool showPopUpOnce;
    [Space]
    public bool isHintLocked;

    [Header("Hint Timings")]
    public float firstShowHintDelay;
    [Space]
    public float firstHideHintDelay;
    [Space]
    public float secondShowHintDelay;
    [Space]
    public float secondHideHintDelay;
}