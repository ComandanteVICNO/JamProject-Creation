using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class StatRoller : MonoBehaviour
{
    public static StatRoller instance;

    public int roll;
    public float percentage;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    
    public float RollStat()
    {
        roll = Random.Range(1,21);
        switch (roll)
        {
            case 1:
                percentage = -20;
                break;
            case 2:
                percentage = -18;
                break;
            case 3:
                percentage = -16;
                break;
            case 4:
                percentage = -14;
                break;
            case 5:
                percentage = -12;
                break;
            case 6:
                percentage = -10;
                break;
            case 7:
                percentage = -8;
                break;
            case 8:
                percentage = -6;
                break;
            case 9:
                percentage = -4;
                break;
            case 10:
                percentage = 0;
                break;
            case 11:
                percentage = 5;
                break;
            case 12:    
                percentage = 10;
                break;
            case 13:
                percentage = 15;
                break;
            case 14:
                percentage = 20;
                break;
            case 15:
                percentage = 25;
                break;
            case 16:
                percentage = 30;
                break;
            case 17:
                percentage = 35;
                break;
            case 18:
                percentage = 40;
                break;
            case 19:
                percentage = 45;
                break;
            case 20:
                percentage = 50;
                break;
            default:
                percentage = 0;
                break;
        }
        return percentage;
    }
    
    
}
