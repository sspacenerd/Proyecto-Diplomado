using UnityEngine;
using System.Collections;

public class Clock : MonoBehaviour {


    public GameObject minuteHand, hourHand;
    public const int hoursInDay = 24, minutesInHour = 60;
    const float hoursToDegrees = 360 / 12, minutesToDegrees = 360 / 60;
    public float dayDuration = 30f, speed;
    private float totalTime = 0, currentTime = 0, goToSpeed;
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //
    //  Simple Clock Script / Andre "AEG" Bürger / VIS-Games 2012
    //
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------
    //-----------------------------------------------------------------------------------------------------------------------------------------

    //-- set start time 00:00
    public int minutes = 0;
    public int hour = 0;

    //-- time speed factor
    public float clockSpeed = 1.0f;     // 1.0f = realtime, < 1.0f = slower, > 1.0f = faster
    //-- internal vars
    int seconds;
    float msecs;
    public GameObject pointerSeconds;
    public GameObject pointerMinutes;
    public GameObject pointerHours;

    void Start()
    {


        msecs = 0.0f;
        seconds = 0;
    }
    void Update()
    {
        /*
        //-- calculate time
        msecs += Time.deltaTime * clockSpeed;
        if (msecs >= 1.0f)
        {
            msecs -= 1.0f;
            seconds++;
            if (seconds >= 60)
            {
                seconds = 0;
                minutes++;
                if (minutes > 60)
                {
                    minutes = 0;
                    hour++;
                    if (hour >= 24)
                        hour = 0;
                }
            }
        }


        //-- calculate pointer angles
        float rotationSeconds = (360.0f / 60.0f) * seconds;
        float rotationMinutes = (360.0f / 60.0f) * minutes;
        float rotationHours = ((360.0f / 12.0f) * hour) + ((360.0f / (60.0f * 12.0f)) * minutes);

        //-- draw pointers
        pointerSeconds.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationSeconds);
        pointerMinutes.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationMinutes);
        pointerHours.transform.localEulerAngles = new Vector3(0.0f, 0.0f, rotationHours);
        */

        totalTime += Time.deltaTime;
        currentTime = totalTime % dayDuration * speed;
        hourHand.transform.rotation = Quaternion.Euler(0, 0, GetHour() * hoursToDegrees);
        minuteHand.transform.rotation = Quaternion.Euler(0, 0, GetMinutes() * minutesToDegrees);
        //hourHand.Rotate(transform.eulerAngles + new Vector3(0, 0, -GetHour() * hoursToDegrees * Time.deltaTime));
        //hourHand.Rotate(transform.eulerAngles + new Vector3(0, 0, -GetMinutes() * minutesToDegrees * Time.deltaTime));



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

    public float GetHour()
    {

        return currentTime * hoursInDay / dayDuration;

    }
    public float GetMinutes()
    {
        return (currentTime * hoursInDay * minutesInHour / dayDuration) % minutesInHour;
    }
}



//-----------------------------------------------------------------------------------------------------------------------------------------
//-----------------------------------------------------------------------------------------------------------------------------------------
