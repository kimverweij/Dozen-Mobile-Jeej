using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetTurn : MonoBehaviour
{

    public TextMeshProUGUI PlayerName;
    public int teamNumer;

    public Sprite[] TeamIcons;
    public GameObject TeamIcon;

    // Start is called before the first frame update
    void Start()
    {
        
        ShowData();
    }

    public void ShowData()
    {
        PlayerName.text = "Britt";
        TeamIcon.GetComponent<Image>().sprite = TeamIcons[teamNumer];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
