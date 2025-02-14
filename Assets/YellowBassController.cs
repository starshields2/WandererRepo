using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBassController : MonoBehaviour
{
    public GameObject[] allYBass;
    public YellowBass[] yBassScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("DeactivateFish")]


    public void DeactivateFish()
    {
        foreach (GameObject fish in allYBass)
        {
            fish.SetActive(false);
        }
    }

}
