using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;

//GameMaganer.cs
//Usando referencias de:
//assetstore.unity.com/packages/3d/props/interior/clock-4250#description
//www.youtube.com/watch?v=oWEiYuVkVOw&t=14s
public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    public static bool gameIsPaused;

    [SerializeField] private GameObject menu, settings;
    [SerializeField] private Image imageToFade;
    [SerializeField] private Volume volume;

    public const int hoursInDay = 24, minutesInHour = 60;
    const float hoursToDegrees = 360 / 12, minutesToDegrees = 360 / 60;

    public float dayDuration = 30f, speed;
    public Settings mySettings;
    public Transform minuteHand, hourHand;
    public TextMeshProUGUI grillo;
    public int grillos;

    private float totalTime = 0, currentTime = 0, goToSpeed;
    private bool manipulatinTime, gg;
    private Camera myCam;
    private DepthOfField depth;





    private void Awake()
    {
        gameManagerInstance = this;
    }
    private void Start()
    {
        Fade(imageToFade, 0, 1);
        volume.profile.TryGet(out depth);
        myCam = Camera.main;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (!manipulatinTime)
        {
            totalTime += Time.deltaTime;
            currentTime = totalTime % dayDuration * speed;
            hourHand.rotation = Quaternion.Euler(0, 0, -GetHour() * hoursToDegrees);
            minuteHand.rotation = Quaternion.Euler(0, 0, -GetMinutes() * minutesToDegrees);
            //hourHand.Rotate(transform.eulerAngles + new Vector3(0, 0, -GetHour() * hoursToDegrees * Time.deltaTime));
            //hourHand.Rotate(transform.eulerAngles + new Vector3(0, 0, -GetMinutes() * minutesToDegrees * Time.deltaTime));
        }
        //Debug.Log(currentTime);

        if (!gg)
        {
            for (int i = 0; i < 5; i++)
            {
                speed += 1;

            }
            gg = true;
        }
    }

    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        if (gameIsPaused)
        {
            depth.active = true;
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            menu.SetActive(true);
            myCam.fieldOfView = 65;
        }
        else
        {
            depth.active = false;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            settings.SetActive(false);
            menu.SetActive(false);
            myCam.fieldOfView = 60;
        }
    }
    public void CloseGame()
    {
        Application.Quit();
    }
    public float GetHour()
    {

        return currentTime * hoursInDay / dayDuration;

    }
    public float GetMinutes()
    {
        return (currentTime * hoursInDay * minutesInHour / dayDuration) % minutesInHour;
    }

    public string Clock12()
    {
        int hour = Mathf.FloorToInt(GetHour());
        string abbreviation = "AM";

        if (hour >= 12)
        {
            abbreviation = "PM";
            hour -= 12;
        }
        if (hour == 0)
        {
            hour = 12;
        }
        return hour.ToString("00") + ":" + Mathf.FloorToInt(GetMinutes()).ToString("00") + " " + abbreviation;
    }
    public void Fade(Image fadeGameObject, float amountOfFade, float timeOfFade)
    {
        fadeGameObject.DOFade(amountOfFade, timeOfFade).SetUpdate(true);
    }


    public void QuitGame()
    {
        StartCoroutine(Quit());
    }
    IEnumerator Quit()
    {
        Fade(imageToFade, 1, 1);
        ES3.Save("Sensibility", Settings.currentMouseSensibility);
        ES3.Save("Volume", mySettings.volume.value);
        yield return new WaitForSecondsRealtime(1);
        PauseGame();
        SceneManager.LoadScene(0);
        yield break;
    }

    public IEnumerator Grillo()
    {
        grillos++;
        grillo.text = "GRILLOS: " + grillos + " /3";
        grillo.DOFade(1, 3);
        yield return new WaitForSeconds(3);
        grillo.DOFade(0, 3);
        yield break;
    }
}

