using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class sat : MonoBehaviour
{
    [Header(" Elements")]
    [SerializeField] private SpriteRenderer spriteRenderer;


    [Header(" Action ")]
    public static Action<sat, sat> onCollisionwithsat;

    [Header("Data")]
    [SerializeField ] private Sattype sattype;

    [Header(" Effects ")]
    [SerializeField] private ParticleSystem MergeParticles;
    private bool hasCollided;
    private bool canbemerged;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("AllowMerge", .25f);
        
    }

    private void AllowMerge()
    {
        canbemerged = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTo(Vector2 Targetposition)
    {
        transform.position = Targetposition;

    }
    public void enablephysics()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Collider2D>().enabled = true;
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ManageCollision(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ManageCollision(collision);
    } 

    private void ManageCollision(Collision2D collision)
    {
        hasCollided = true;

        if (!canbemerged)
            return;

        


        if (collision.collider.TryGetComponent(out sat othersat))
        {
            if (othersat.GetSattype() != sattype)
                return;

            if (!othersat.CanBeMerged())
                return;
          
            onCollisionwithsat?.Invoke(this, othersat);  
        }

    }

    public void Merge()
    {
        if(MergeParticles != null)
        {
            MergeParticles.transform.SetParent(null);
            MergeParticles.Play();
        }
       

        Destroy(gameObject);
        
    }



     public Sattype GetSattype()
    {
        return sattype;
    }

    public Sprite GetSprite()
    {
        return spriteRenderer.sprite;
    }

    public bool HasCollided()
    {
        return hasCollided;
    }

    public bool CanBeMerged()
    {
        return canbemerged;    
    }
}
