using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MergeManager : MonoBehaviour
{
   [Header(" Actions ")]
    public static Action<Sattype, Vector2> onmergeprocess;

    [Header("Settings")]
    sat LastSender;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        sat.onCollisionwithsat += Collisionbetweensatcallback;

    }

    private void OnDestroy()
    {
        sat.onCollisionwithsat -= Collisionbetweensatcallback;

    }



    private void Collisionbetweensatcallback(sat sender, sat othersat)
    {
        if (LastSender != null)
          return;

        LastSender = sender;

        ProcessMerge(sender, othersat);

    
        Debug.Log("collisooon!!" + sender.name);

 
    }  

    private void ProcessMerge(sat sender, sat othersat)
    {
        Sattype mergeSattype = sender.GetSattype();
        mergeSattype += 1;

        Vector2 satSpawnPos = (sender.transform.position + othersat.transform.position) / 2;


        sender.Merge();
        othersat.Merge();

        StartCoroutine(ResetLastSenderCoroutine());

        onmergeprocess?.Invoke(mergeSattype, satSpawnPos);
    }

    IEnumerator ResetLastSenderCoroutine()
    {
        yield return new WaitForEndOfFrame();
        LastSender = null;
    }
}
