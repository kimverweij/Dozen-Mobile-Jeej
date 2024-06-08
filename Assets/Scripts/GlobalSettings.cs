using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    // Start is called before the first frame update

    public static GlobalSettings instance;

    // Static vars
    public readonly int maxTeams = 5;
    public readonly int maxPpTeam = 5;
    public readonly int minTeams = 2;


    // time per round
    public readonly float TimePerRound = 60f;


    public bool EnableHaptic = false;
 



    public List<string> PlayerNames = new List<string>();

    public Color[] teamColors = new Color[] { 
            // Hex: #2fb7e7 - Blue
            new Color(47f / 255f, 183f / 255f, 231f / 255f, 1f),
            // Hex: #8d4e9f - Purple
            new Color(141f / 255f, 78f / 255f, 159f / 255f, 1f),
            // Hex: #d0479a - Pink
            new Color(208f / 255f, 71f / 255f, 154f / 255f, 1f), 
            // Hex: #f7bf2f - Yellow
            new Color(247f / 255f, 191f / 255f, 47f / 255f, 1f),
            // Hex: #dc5a2d - Red/Orange
            new Color(220f / 255f, 90f / 255f, 45f / 255f, 1f)
        };

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
        {
        
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
