using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpHintsControl : MonoBehaviour
{
    public List<PopUpHint> hints;

    public string saveFileName;

    private void Start()
    {
        LoadHintsData();
    }

    public void SaveHintsData()
    {
        SaveAndLoadManager.Instance.Save(saveFileName, this, "/local_saves");
    }

    public void LoadHintsData()
    {
        SaveAndLoadManager.Instance.Load(saveFileName, this, "/local_saves");
    }
}