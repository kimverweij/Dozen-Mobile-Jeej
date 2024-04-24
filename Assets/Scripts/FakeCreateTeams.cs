using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeCreateTeams : MonoBehaviour
{
    // Start is called before the first frame update
    public static FakeCreateTeams instance;

    private List<string> namesList = new List<string>
    {
        "Kim", "Henk", "Lisa", "Gijs", "Eddie",
        "Frank", "Grace"//, "Henry", "Ivy", "Jack",
      //  "Katherine", "Liam", "Mia", "Noah", "Olivia"/*,
       // "Penelope", "Quinn", "Ryan", "Sophia", "Theodore"//,*/
       // "Ursula", "Victor", "Wendy", "Xander", "Yasmine"
    };
    public List<string> PlayerNames { get { return namesList; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
