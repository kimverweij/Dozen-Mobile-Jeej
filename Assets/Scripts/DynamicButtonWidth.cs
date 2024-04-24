using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DynamicButtonWidth : MonoBehaviour
{
    public TMP_Text textField;
    public UnityEngine.UI.Image icon;
    private float offset = 10.0f;

    private float prefferedWidth = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        if (textField != null)
        {
            prefferedWidth += textField.preferredWidth;
            if (icon != null)
            {
                prefferedWidth += icon.rectTransform.sizeDelta.x;
            }
        }
        else return;

        RectTransform buttonRectTransform = GetComponent<RectTransform>();
        buttonRectTransform.sizeDelta = new Vector2(prefferedWidth, buttonRectTransform.sizeDelta.y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
