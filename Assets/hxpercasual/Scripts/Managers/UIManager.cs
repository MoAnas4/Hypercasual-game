using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private GameObject SettingsPanel;

    private void Awake()
    {
        GameManager.onGameStateChanged += OnGameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.onGameStateChanged -= OnGameStateChangedCallback;

    }

    // Start is called before the first frame update
    void Start()
    {
        //SetMenu();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnGameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Menu:
                SetMenu();
                break;
                
            case GameState.Game:
                 SetGamePanel();
                 break;

            case GameState.Gameover:
                SetGameoverPanel();
                break;   
        }

    }

    private void SetMenu()
    {
        menuPanel.SetActive(true);
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(false);
        SettingsPanel.SetActive(false);

    }

    
    private void SetGamePanel()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        gameoverPanel.SetActive(false);
        SettingsPanel.SetActive(false);

    }

    
    private void SetGameoverPanel()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        gameoverPanel.SetActive(true);
        SettingsPanel.SetActive(false);

    }

    
   public void PlayButtonCallback()
   {
    GameManager.Instance.SetGameState();
    SetGamePanel();
   }

   public void NextButtonCallBack()
   {
    SceneManager.LoadScene(0);
   }

   public void SettingsButtonCallBck()
   {
    
    SettingsPanel.SetActive(true);
    Time.timeScale = 0;

   }

   public void CloseSettingsPanel()
   {

    SettingsPanel.SetActive(false);
    Time.timeScale = 1;

   }


}
