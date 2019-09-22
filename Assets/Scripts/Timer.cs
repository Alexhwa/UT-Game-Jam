using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class Timer : MonoBehaviour
{
	private SceneController sceneCont;

	private AudioSource winZoneAudio;
	public AudioClip runOutOfTime;

	Stopwatch timer;
    UnityEngine.UI.Text t;

	private static int limitInInts = 90;
	private float currTime;

    System.TimeSpan lastTime;
    readonly System.TimeSpan timeLimit = new System.TimeSpan(0,1, 30); //I know this is horrendous but I got less than 6 hours left!

    string elapsed;
	private bool activatedLoseState = false;

	void Start()
    {
		currTime = limitInInts;

		sceneCont = GameObject.Find("GameController").GetComponent<SceneController>();

		winZoneAudio = GameObject.Find("WinZone").GetComponent<AudioSource>();

        t = GetComponent<UnityEngine.UI.Text>();
        t.fontSize = 60;

        //buttonText = pauseButton.GetComponent<UnityEngine.UI.Text>();


        timer = new Stopwatch();
        timer.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer.IsRunning)
        {
            elapsed = string.Format("{0:00}:{1:00}", timeLimit.Minutes-timer.Elapsed.Minutes, timeLimit.Seconds-timer.Elapsed.Seconds);
			currTime -= Time.deltaTime;

            t.text = elapsed;
            lastTime = timer.Elapsed;
            

            if(currTime <= 0f && !activatedLoseState)
            {
				activatedLoseState = true;
				UnityEngine.Debug.Log("TIMES UP!");
                timesUp();
            }
        }
    }

    public void updateTimer()
    {
        if (timer.IsRunning)
        {
            lastTime = timer.Elapsed;
            timer.Stop();

            elapsed = string.Format("{0:00}:{1:00}", timeLimit.Minutes - timer.Elapsed.Minutes, timeLimit.Seconds - timer.Elapsed.Seconds);
        }

        else
        {
            timer.Start();
        }
    }

    public void timesUp()
    {
		winZoneAudio.PlayOneShot(runOutOfTime);
		sceneCont.RestartLevel();
	}
}
