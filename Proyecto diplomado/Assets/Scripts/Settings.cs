using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] public static float currentMouseSensitivity;

    private void Start()
    {
        currentMouseSensitivity = PlayerController.mouseSensitivity;
    }
    private void Update()
    {

            Debug.Log(currentMouseSensitivity);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    public void SetSensitivity(float newSpeed)
    {
        PlayerController.mouseSensitivity = newSpeed;
        currentMouseSensitivity = PlayerController.mouseSensitivity;
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
