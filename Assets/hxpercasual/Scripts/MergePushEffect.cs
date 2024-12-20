using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MergePushEffect : MonoBehaviour
{

    [Header(" Settings ")]
    [SerializeField] private float pushRadius;
    [SerializeField] private float pushMagnitude;

    
    
    private Vector2 pushPosition;

    [Header(" DEBUG ")]
    [SerializeField] private bool enableGizmos;


    private void Awake()
    {
        MergeManager.onmergeprocess += MergeprocessedCallback;
        
    }

    private void OnDestroy()
    {
        MergeManager.onmergeprocess -= MergeprocessedCallback;
    }   
    // Start is called before the first frame update
    
  

    private void MergeprocessedCallback(Sattype sattype, Vector2 mergepos)
    {
        pushPosition = mergepos;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(mergepos, pushRadius);


        foreach (Collider2D collider in colliders)
        {
            if (collider.TryGetComponent(out sat sat))
            {
                Vector2 force = ((Vector2)sat.transform.position - mergepos).normalized;
                force *= pushMagnitude;
                
                sat.GetComponent<Rigidbody2D>().AddForce(force);
            }
        }
    }

    


#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        if(!enableGizmos)
            return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(pushPosition, pushRadius);    
    }

#endif
}
