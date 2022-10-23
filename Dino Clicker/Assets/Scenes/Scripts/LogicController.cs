using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class LogicController : MonoBehaviour
{
    public float Points;
    public float CurrentLevel;
    public float PointsTilNextLevel;
    public float Multiplier;
    public float LevelPoints;
    public float ACSpeed;
    public float ACTime;
    public float Clicks;
    public float level;
    public float rocks;
    public float levelRocks;
    public float multLevel;
    public float autoLevel;

    public bool ACEnabled;

    public EggController Egg;
    public Button clickerUpgrade;
    public Button autoUpgrade;
    public Text warning;
    public ParticleSystem levelUp;
    public ParticleSystem clickBoom;


    void Awake()
    {
        warning.GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
        Points = 0f;
        CurrentLevel = 1f;  
        Multiplier = 1f;
        LevelPoints = 10f;
        PointsTilNextLevel = 10f;
        ACSpeed = 0f;
        ACTime = 1f;
        level = 1f;
        rocks = 0f;
        levelRocks = 2f;
        autoLevel = 1f;
        multLevel = 1f;
        levelUp.Pause();

        ACEnabled = false;
    }

    
    void Update()
    {
        if(PointsTilNextLevel <= 0f)
        {
            Egg.Hatch();
            StartCoroutine(NewLevel());
        }
        if(ACEnabled == true)
        {
            StartCoroutine(AutoClicker());
            ACEnabled = false;
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            level += 1;
        }
    }

    public void Click()
    {
        Points += Multiplier;
        PointsTilNextLevel -= Multiplier;
        Clicks += 1;
        makeBoom();
    }

    public void IncreaseMult()
    {
        if(rocks >= Mathf.Pow(1f, multLevel))
        {
            Multiplier *= 2;
            rocks -= Mathf.Pow(1f, multLevel);
            multLevel += 1;
        }
        else
        {
            StartCoroutine(RedFlash(clickerUpgrade));
        }
    }

    public void EnableAuto()
    {
        if (rocks >= Mathf.Pow(40f, autoLevel))
        {
            ACEnabled = true;
            ACTime *= 1.5f;
            rocks -= Mathf.Pow(40f, autoLevel);
        }
        else
        {
            StartCoroutine(RedFlash(autoUpgrade));
        }
    }

    private IEnumerator AutoClicker()
    {
        Points += 1;
        PointsTilNextLevel -= 1;
        Clicks += 1;
        yield return new WaitForSeconds(1 / ACTime);
        ACEnabled = true;
    }
    private IEnumerator RedFlash(Button button)
    {
        warning.GetComponent<RectTransform>().localScale = new Vector3(.005f, .005f, 0f);
        button.GetComponent<Image>().color = Color.red;
        yield return new WaitForSeconds(.25f);
        button.GetComponent<Image>().color = Color.white;
        yield return new WaitForSeconds(1f);
        warning.GetComponent<RectTransform>().localScale = new Vector3(0f,0f,0f);
    }

    public void makeBoom()
    {
        clickBoom.Play();    
    }

    public IEnumerator NewLevel()
    {
        CurrentLevel += 1;
        LevelPoints = Mathf.Pow(5f, CurrentLevel);
        PointsTilNextLevel = LevelPoints;
        level += 1f;
        rocks += levelRocks;
        levelRocks = LevelPoints / 5;
        levelUp.Play();
        yield return null;
        if (level == 5f)
        {
            Egg.Teen();
        }
        else
        {
            if (level >= 10f)
            {
                Egg.Adult();
            }
        }
    }
}
