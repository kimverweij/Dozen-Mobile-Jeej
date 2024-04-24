using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractebleButtons : MonoBehaviour
{
    public Button button;


    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(ButtonClick);
    }

    void ButtonClick()
    {
        Debug.Log("Clickie");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
