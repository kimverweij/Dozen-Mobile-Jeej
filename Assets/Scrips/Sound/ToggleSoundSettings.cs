using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSoundSettings : MonoBehaviour
{

    public GameObject _enabledSounds;
    public GameObject _disabledSounds;

    public GameObject _enabledSFX;
    public GameObject _disabledSFX;

    public GameObject _enableHaptic;
    public GameObject _disableHaptic;
    // Start is called before the first frame update
    void Start()
    {
        ToggleDisplaySFX();
        ToggleDisplaySounds();
        ToggleHaptic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleDisplaySFX()
    {
        bool _toggleBool = AudioManager.instance.sfxSource.mute;

        Debug.Log(_toggleBool + " issfxSource.mute");
        _enabledSFX.SetActive(!_toggleBool);
        _disabledSFX.SetActive(_toggleBool);
    }

    public void ToggleDisplaySounds()
    {
        bool _toggleBool = AudioManager.instance.musicSource.mute;

        Debug.Log(_toggleBool + " is tmusicSource.mute");
        _enabledSounds.SetActive(!_toggleBool);
        _disabledSounds.SetActive(_toggleBool);
    }

    public void ToggleHaptic()
    {
        GlobalSettings.instance.EnableHaptic = !GlobalSettings.instance.EnableHaptic;

        Debug.Log(GlobalSettings.instance.EnableHaptic  + " is GlobalSettings.instance.EnableHaptic ");
        _enableHaptic.SetActive(!GlobalSettings.instance.EnableHaptic);
        _disableHaptic.SetActive(GlobalSettings.instance.EnableHaptic);

       if(GlobalSettings.instance.EnableHaptic) Handheld.Vibrate();
    }
}
