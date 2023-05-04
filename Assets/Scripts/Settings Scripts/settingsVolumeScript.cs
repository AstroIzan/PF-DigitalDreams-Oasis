using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settingsVolumeScript : MonoBehaviour
{
    public Slider VolumeSlider;
    public float VolumeValue;

    // Start is called before the first frame update
    void Start()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("VolumeLevel", 0.5f);
        AudioListener.volume = VolumeSlider.value;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetVolume(float volume)
    {
        VolumeValue = volume;
        PlayerPrefs.SetFloat("VolumeLevel", VolumeValue);
        AudioListener.volume = VolumeValue;
    }
}
