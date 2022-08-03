using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager gameManagerInstance;
    public static bool gameIsPaused;
    [SerializeField] private GameObject menu, settings;
    [SerializeField] private Image imageToFade;

    public const int hoursInDay = 24, minutesInHour = 60;
    public float dayDuration = 30f, speed;

    float totalTime = 0, currentTime = 0, goToSpeed;

    public RectTransform minuteHand, hourHand;
    const float hoursToDegrees = 360 / 12, minutesToDegrees = 360 / 60;
    
    bool manipulatinTime, gg;

    private void Awake()
    {
        gameManagerInstance = this;
    }
    private void Start()
    {
        Fade(imageToFade, 0, 1);
    }

    // Update is called once per frame
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
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            menu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            settings.SetActive(false);
            menu.SetActive(false);
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
        return (currentTime * hoursInDay * minutesInHour / dayDuration)%minutesInHour;
    }

    public string Clock12()
    {
        int hour = Mathf.FloorToInt(GetHour());
        string abbreviation = "AM";

        if(hour >= 12)
        {
            abbreviation = "PM";
            hour -= 12;
        }
        if(hour == 0)
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
        yield return new WaitForSecondsRealtime(1);
        PauseGame();
        SceneManager.LoadScene(0);
        yield break;
    }

}

