using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using TMPro;
using UnityEngine.UI;

[RequireComponent(typeof(SatManager))]
public class Game__UI : MonoBehaviour
{
    [Header(" Elements ")]
    [SerializeField] private TextMeshProUGUI nextsatText;
    [SerializeField] private Image nextsatimage;
    private SatManager satManager;
    // Start is called before the first frame update

    private void Awake()
    {
        SatManager.onNextSatIndexSet += UpdateNextFruitImage;


    }

    private void OnDestroy()
    {
        SatManager.onNextSatIndexSet -= UpdateNextFruitImage;

    }

    void Start()
    {
        //satManager = GetComponent<SatManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //nextsatText.text = " NEXT:" + satManager.Getnextsatname();
        
    }

    private void UpdateNextFruitImage()
    {
        if (satManager == null)
            satManager = GetComponent<SatManager>();
          
        nextsatimage.sprite = satManager.Getnextsatsprite();

    }
}
