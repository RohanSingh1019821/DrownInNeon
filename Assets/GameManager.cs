using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public float startingTime = 30f;
    public float rewardTime = 5f;

    private float time = 0f;
    private float kills = 0f;

    private float timeLeft;

    private bool alive = true;

    public TextMeshProUGUI survivedTime;
    public TextMeshProUGUI killsCount;
    public GameObject timer;
    public GameObject timerBacking;

    public animCharController player;

    public ParticleSystem timerBoon;

    void Start()
    {
        timeLeft = startingTime;
    }

    void Update()
    {
        if (alive)
        {
            time += Time.deltaTime;
            timeLeft -= Time.deltaTime;
            timer.GetComponent<TextMeshProUGUI>().SetText(Mathf.RoundToInt(timeLeft).ToString());
            timerBacking.GetComponent<TextMeshProUGUI>().SetText(Mathf.RoundToInt(timeLeft).ToString());
        }

        if (timeLeft <= 0f && alive)
        {
            player.KillPlayer();
            alive = false;
        }

        if(Input.GetKeyDown("r"))
        {
            
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("Scenes/TopDownShooter");
    }


    public void Menu()
    {
        SceneManager.LoadScene("SpeedTutor_Full_Menu_System/Scenes/MainMenu");
    }


    public void EnemyKilled()
    {
        kills += 1;
        timeLeft += rewardTime;
        timerBoon.Emit(1);
    }

    public void PlayerKilled()
    {
        survivedTime.SetText("HELD OUT FOR: " + Mathf.RoundToInt(time).ToString() + " S");
        killsCount.SetText("KILLS: " + kills);
        alive = false;

    }
}
