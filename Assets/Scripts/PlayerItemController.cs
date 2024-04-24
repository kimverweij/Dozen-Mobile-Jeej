using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerItemController : MonoBehaviour
{
    public Sprite RegularBG;
    public Sprite TransparentBG;
    public Sprite StrokeBG;

    public TextMeshProUGUI NameTextField;
    public CanvasGroup PlayerCanvas;


    private int teamIndex;
    private int playerIndex;


    private Image BackgroundImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        BackgroundImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustRaycast(bool _bool)
    {
        PlayerCanvas.blocksRaycasts = _bool;
    }

    public void SetIndex(int _teamindex, int _playerindex)
    {
        teamIndex   = _teamindex;
        playerIndex = _playerindex;
    }

    public void SetPlayerName(string _name)
    {
        NameTextField.text = _name;
    }

    public string GetPlayerName()
    {
        return NameTextField.text;
    }

    public int getTeamIndex()
    {
        return teamIndex;
    }

    public int getPlayerIndex() 
    {
        return playerIndex;
    } 

    public void SetRegularBG()
    {

        if (BackgroundImage != null)
        {
            BackgroundImage.sprite = RegularBG;
            NameTextField.enabled = true;
        }
    }

    public void SetTransparentBG()
    {
        if (BackgroundImage != null)
        {
            BackgroundImage.sprite = TransparentBG;
            NameTextField.enabled = false;
        }
    }

    public void SetStrokeBG()
    {
        if (BackgroundImage != null)
        {
            BackgroundImage.sprite = StrokeBG;
        }
    }
}
