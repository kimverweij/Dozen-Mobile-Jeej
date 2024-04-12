using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScreenManager;

public class ShopScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LeaveShop()
    {
        Debug.Log("Leave shop");
        ScreenManager.Instance.GoToScreen(ScreenTypeEnum.MainScreen);
    }
}
