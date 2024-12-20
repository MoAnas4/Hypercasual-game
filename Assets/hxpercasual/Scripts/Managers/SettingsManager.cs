using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header( " Elementts")]
    [SerializeField] private GameObject CreditsPanel;
    

    

    
    
    // Start is called before the first frame update
    void Start()
    {
        CreditsPanel.SetActive(false);
        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreditbuttonCallback()
    {
        CreditsPanel.SetActive(true);
        

    }

    public void CloseCredits()
    {
        CreditsPanel.SetActive(false);

    }

    
    
}
