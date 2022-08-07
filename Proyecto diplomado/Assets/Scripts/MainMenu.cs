using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Audio;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image imageToFade;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider volume, sensibility;
    // Start is called before the first frame update
    private void Awake()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    void Start()
    {
        imageToFade.DOFade(0, 1);
        sensibility.value = (float)ES3.Load("Sensibility");
        volume.value = (float)ES3.Load("Volume");
        //audioMixer.SetFloat("volume", Mathf.Log10((float)ES3.Load("Volume", 1)) * 20);
    }
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        ES3.Save("Volume", volume.value);
        imageToFade.DOFade(1, 1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
        yield break;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);
    }
    public void SetSensitivity()
    {
        ES3.Save("Sensibility", sensibility.value);
    }
}
