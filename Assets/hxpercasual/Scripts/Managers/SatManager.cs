using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class SatManager : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private sat[] satprefabs;
    [SerializeField] private sat[] spawnablesats;
    [SerializeField] private Transform satsParent;
    private sat currentsat;
    

    [Header(" Settings ")]
    [SerializeField] private float satYSpawnPos; 
    [SerializeField] private float spawndelay;
    private bool cancontrol;
    private bool isControlling;

    [Header(" Next sat Settings ")]
    private int nextsatindex;


    [Header(" Debug ")]
    [SerializeField] private bool enablegizmos;
    [SerializeField] private LineRenderer dropline;

    [Header(" Actions ")]
    public static Action onNextSatIndexSet ;
    private void Awake()
    {
        MergeManager.onmergeprocess += MergeProcessCallback;
    }

    private void OnDestroy()
    {
        MergeManager.onmergeprocess -= MergeProcessCallback;
    }
  

    
    
    // Start is called before the first frame update
    void Start()
    {
        SetnextsatIndex();
        cancontrol = true;
        HideLine();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGameState())
            return;
        


        if (cancontrol)
        {
            ManagePlayerInput();

        }
        


        

        
        
    }
    private void ManagePlayerInput()
    {
        if(Input.GetMouseButtonDown(0))
            MouseDownCallback();
        else if(Input.GetMouseButton(0))
            {
                if(isControlling)
                    MouseDragCallback();
                else
                    MouseDownCallback();
                    
            }     
        else if(Input.GetMouseButtonUp(0) && isControlling)           
            MouseUpCallback();  
        
    }

    private void MouseDownCallback()
    {
        DisplayLine();
        PlaceLineatClickedPosition();
        
        Spawnsat();
        isControlling = true;
    }

    private void MouseDragCallback()
    {
         PlaceLineatClickedPosition();
         currentsat.MoveTo(new Vector2(GetSpawnPosition().x, satYSpawnPos));
        
    }

    private void MouseUpCallback()
    {
        HideLine();
       
        if(currentsat != null)
            currentsat.enablephysics();

        cancontrol = false;
        StartControltimer();
       
        isControlling = false;
    }

    private void Spawnsat()
    {
         
        Vector2 spawnPosition = GetSpawnPosition();
        sat sattoinstantiate = spawnablesats[nextsatindex];
        

       currentsat = Instantiate(
        sattoinstantiate,
        spawnPosition, 
        Quaternion.identity, 
        satsParent);

        SetnextsatIndex();
    }


    private void SetnextsatIndex()
    {
        nextsatindex = UnityEngine.Random.Range(0,spawnablesats.Length);
        
        onNextSatIndexSet?.Invoke();
    }
    
    public string Getnextsatname()
    {
        return spawnablesats[nextsatindex].name;

    }
    



     private Vector2 GetClickedWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private Vector2 GetSpawnPosition()
    {
        Vector2 worldClickedposition = GetClickedWorldPosition();
            worldClickedposition.y = satYSpawnPos;
        return worldClickedposition;
    }

    private void HideLine()
     {
        dropline.enabled = false;
     } 


    private void DisplayLine()
     {
         dropline.enabled = true;
     }

    private void PlaceLineatClickedPosition()
    {
        dropline.SetPosition(0, GetSpawnPosition());
        dropline.SetPosition(1, GetSpawnPosition() + Vector2.down*15);

    }

    
    private void StartControltimer()
    {
        Invoke("Stopcontroltimer", spawndelay);
    }
  
    private void Stopcontroltimer()
    {
        cancontrol=true;
    }

    private void MergeProcessCallback(Sattype sattype, Vector2 spawnPosition)
    {
        for (int i = 0; i < satprefabs.Length; i++)
        {
            if(satprefabs[i].GetSattype() == sattype)
            {
                Spawnmergesat(satprefabs[i], spawnPosition);
                break;
                
            }
                
        }
        
    }
     private void Spawnmergesat(sat Sat, Vector2 spawnPosition)  
     {
        sat SatInstance = Instantiate(Sat, spawnPosition, Quaternion.identity, satsParent);
        SatInstance.enablephysics(); 
     }

     public Sprite Getnextsatsprite()
     {
        return spawnablesats[nextsatindex].GetSprite();

     }

    





#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(!enablegizmos)
        return; 


        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(-50, satYSpawnPos, 0), new Vector3(50, satYSpawnPos, 0 ));
    }
#endif

}
