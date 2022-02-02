using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PopUpHintShower : MonoBehaviour
{
    public PopUpHintsControl popUpHintsControl;
    [Space]
    public int showHintIndex;

    [Header("Hint Control")]
    public StringVariable hintTextSO;
    [Space]
    public bool canHideHintByKeyPress = true;
    [Space]
    public bool showingHint;
    [Space]
    public bool blockHint;

    [Header("Hint Timings")]
    public float showHintDelay;
    [Space]
    public float hideHintDelay;

    [Header("Events")]
    public UnityEvent OnShowHint;
    [Space]
    public UnityEvent OnHideHintByKeyPress;
    [Space]
    public UnityEvent OnHideHint;

    WaitForSeconds showHintYield;
    WaitForSeconds hideHintYield;

    private void Start()
    {
        if (popUpHintsControl == null)
        {
            popUpHintsControl = FindObjectOfType<PopUpHintsControl>();
        }
    }

    private void Update()
    {
        if (showingHint && canHideHintByKeyPress)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                canHideHintByKeyPress = false;

                HideHintByKeyPress();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!blockHint)
        {
            if (other.TryGetComponent<Player>(out var player))
            {
                ShowHint();

                blockHint = true;
            }
        }
    }

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.TryGetComponent<Player>(out var player))
    //    {
    //        if (!showingHint)
    //        {
    //            ShowHint();
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.TryGetComponent<Player>(out var player))
    //    {
    //        if (popUpHintsControl.hints[showHintIndex].isHintShowedOnce)
    //        {
    //            StopAllCoroutines();

    //            hintTextSO.Value = null;

    //            OnHideHint?.Invoke();

    //            showingHint = false;
    //        }
    //    }
    //}

    public void ShowHint()
    {
        if (popUpHintsControl == null)
        {
            popUpHintsControl = FindObjectOfType<PopUpHintsControl>();
        }

        if (!showingHint && !popUpHintsControl.hints[showHintIndex].isHintLocked)
        {
            showingHint = true;

            // check if we showed this hint once if true, set second pop up timings
            if (popUpHintsControl.hints[showHintIndex].isHintShowedOnce)
            {
                showHintDelay = popUpHintsControl.hints[showHintIndex].secondShowHintDelay;

                hideHintDelay = popUpHintsControl.hints[showHintIndex].secondHideHintDelay;
            }
            else
            {
                popUpHintsControl.hints[showHintIndex].isHintShowedOnce = true;

                if (popUpHintsControl.hints[showHintIndex].showPopUpOnce)
                {
                    popUpHintsControl.hints[showHintIndex].isHintLocked = true;
                }

                showHintDelay = popUpHintsControl.hints[showHintIndex].firstShowHintDelay;

                hideHintDelay = popUpHintsControl.hints[showHintIndex].firstHideHintDelay;
            }

            popUpHintsControl.SaveHintsData();

            showHintYield = new WaitForSeconds(showHintDelay);

            StartCoroutine(ShowHintRoutine(showHintYield));
        }
    }

    public IEnumerator ShowHintRoutine(WaitForSeconds waitForSeconds)
    {
        hintTextSO.Value = popUpHintsControl.hints[showHintIndex].hintText;

        yield return waitForSeconds;

        OnShowHint?.Invoke();

        canHideHintByKeyPress = true;

        hideHintYield = new WaitForSeconds(hideHintDelay);

        StartCoroutine(HideHintRoutine(hideHintYield));
    }

    public IEnumerator HideHintRoutine(WaitForSeconds waitForSeconds)
    {
        hintTextSO.Value = null;

        yield return waitForSeconds;

        OnHideHint?.Invoke();

        showingHint = false;
    }

    public void HideHintByKeyPress()
    {
        StopAllCoroutines();

        OnHideHintByKeyPress?.Invoke();

        showingHint = false;

        hintTextSO.Value = null;
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        hintTextSO.Value = null;

        OnHideHint?.Invoke();

        showingHint = false;
    }
}