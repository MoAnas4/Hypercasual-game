using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [Header(" Elements ")]
    [SerializeField] private AudioSource mergeSource;

    [SerializeField] private AudioSource bgaudioSource;
    [SerializeField] private Slider sliderBGmusic;
    [SerializeField] private Toggle sfxToggle;
    
    [Header(" Actions")] 
    private static Action<bool> onSfxValueChanged;
    private bool canSave;

    

    private void Awake()
    {
        onSfxValueChanged += OnSfxValueChangedCallback;
        MergeManager.onmergeprocess += MergeprocessedCallback;
    }

    private void OnDestroy()
    {
        onSfxValueChanged -= OnSfxValueChangedCallback;
        MergeManager.onmergeprocess -= MergeprocessedCallback;
    }




    
   
    // Start is called before the first frame update
    IEnumerator Start()
    {
        
        Initialize();
        
       
       
        yield return new WaitForSeconds(.25f);
        canSave = true;

        
        
        
        
    }

    private void Initialize()
    {

        onSfxValueChanged?.Invoke(sfxToggle.isOn);

        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }

        else
        {
            Load();
        }
        
        

    }

    // Update is called once per frame
    

    

    private void MergeprocessedCallback(Sattype sattype, Vector2 mergepos)
    {
        PlayMergeSound();
    }
    public void PlayMergeSound()
    {
        mergeSource.pitch = UnityEngine.Random.Range(2f, 3f);
        mergeSource.Play();
    }

    public void ChangeVolume()
    {
        bgaudioSource.volume = sliderBGmusic.value;
        Save();

    }

    public void ToggleCallBack(bool sfxActive)
    {
        
        onSfxValueChanged?.Invoke(sfxActive);
        
        
        Save();

    }
    
    
    private void Load()
    {
       sliderBGmusic.value =  PlayerPrefs.GetFloat("musicVolume");
        sfxToggle.isOn = PlayerPrefs.GetInt("sfxactivekey") == 1;
    }

    private void Save()
    {
        if (!canSave)
            return;

        PlayerPrefs.SetFloat("musicVolume", sliderBGmusic.value);
        PlayerPrefs.SetInt("sfxactivekey",  sfxToggle.isOn ? 1 : 0);
    }

    private void OnSfxValueChangedCallback(bool sfxActive)
    {
        mergeSource.mute = !sfxActive;
       // mergeSource.volume = sfxActive ? 1 : 0;

    }

}
