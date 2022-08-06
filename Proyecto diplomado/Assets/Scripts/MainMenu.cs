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
        ES3.Load("Sensibility", sensibility.value);
        //audioMixer.SetFloat("Volume", ES3.Load("volume"));
    }
    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    IEnumerator StartGameCoroutine()
    {
        imageToFade.DOFade(1, 1);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
        yield break;
    }
    private void Update()
    {
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
    public void SetSensitivity(float newSpeed)
    {
        ES3.Save("Sensibility", sensibility.value);
    }
}
