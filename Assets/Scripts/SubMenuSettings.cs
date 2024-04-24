using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScreenManager;

public class SubMenuSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSettings(){
        ScreenManager.Instance.GoToScreen(ScreenTypeEnum.SettingsDialog);
    }

    
}
