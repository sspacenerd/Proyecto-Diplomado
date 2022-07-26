using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool gameIsPaused;
    [SerializeField] private GameObject menu;

    public const int hoursInDay = 24, minutesInHour = 60;
    public float dayDuration = 30f, speed;

    float totalTime = 0, currentTime = 0, goToSpeed;

    public RectTransform minuteHand, hourHand;
    const float hoursToDegrees = 360 / 12, minutesToDegrees = 360 / 60;
    
    bool manipulatinTime, gg;

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
            menu.SetActive(false);
        }
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

}

