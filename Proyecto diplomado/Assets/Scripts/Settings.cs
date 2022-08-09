using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] public static float currentMouseSensibility;
    public Slider sensibility, volume;

    private void Awake()
    {
        
    }
    private void Start()
    {
        currentMouseSensibility = PlayerController.mouseSensitivity;
        sensibility.value = currentMouseSensibility;
        volume.value = (float)ES3.Load("Volume", 1);

    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }
    public void SetSensibility(float newSpeed)
    {
        PlayerController.mouseSensitivity = newSpeed;
        currentMouseSensibility = PlayerController.mouseSensitivity;
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
