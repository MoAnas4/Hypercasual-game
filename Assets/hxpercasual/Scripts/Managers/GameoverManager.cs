using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private GameObject deadline;
    [SerializeField] private Transform satsParent;

    [Header(" Timer ")]
    [SerializeField] private float durationover;
    private float timer;
    private bool timerOn;
    private bool isGameOver;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()

    {
        if(!isGameOver)
            ManageGameOver();

        

    }

    private void ManageGameOver()
    {
        if (timerOn)
            ManageTimerOn();
        
        else
        {
            if (IsSatAboveLine())
              StartTimer();  

        }

    }

    private void ManageTimerOn()
    {
        
            timer += Time.deltaTime;
            

            if (!IsSatAboveLine())
                StopTimer();

            if(timer >= durationover  )
                Gameover();

        
    }

    private bool IsSatAboveLine()
    {
        for (int i = 0; i < satsParent.childCount; i++)
        {
            sat Sat = satsParent.GetChild(i).GetComponent<sat>();

            if (!Sat.HasCollided())
                continue;



              if(IsSatAboveLine(satsParent.GetChild(i)))
                return true;
        }
        return false;
    }


    private void CheckGameOver()
    {
        for (int i = 0; i < satsParent.childCount; i++)
        {
            sat Sat = satsParent.GetChild(i).GetComponent<sat>();

            if (!Sat.HasCollided())
                continue;



            IsSatAboveLine(satsParent.GetChild(i));
        }

    }

    private bool IsSatAboveLine(Transform sat)
    {
        if( sat.position.y > deadline.transform.position.y)
             
             return true;

        return false;     

    }

    private void StartTimer()
    {
        timer = 0;
        timerOn = true;

    }

    private void StopTimer()
    {
        timerOn = false;
    }
    
    private void Gameover()
    {
        Debug.Log(" GameOver");
        isGameOver = true;

        GameManager.Instance.SetGameoverState();
    }

}
