using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{
    public MeshRenderer rend;
    public bool isWhite;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeColor()
    {
        // Toggles the color of the sphere between red and white

        isWhite = !isWhite;

        if (isWhite) // if sphere is white, make it red
        {
            rend.material.color = Color.red;
        }
        else // if sphere is not white (i.e., it's red), make it white
        {
            rend.material.color = Color.white;
        }
    }

    public void Green()
    {
        rend.material.color = Color.green;
    }
}
