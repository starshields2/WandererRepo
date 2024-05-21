using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoseGar : MonoBehaviour
{
    [Header("Movement Controller")]
    public FollowBeziCurve beziMover;
    public TimeAndDate timemanager;
    [Header("Weather Controller")]
    public EnvironmentManager enviManager;

    [Header("Social Interaction")]
    public GameObject[] friendWith;
    public GameObject[] avoiding;
    public GameObject[] chasing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerStay(Collider other)
    {
        Debug.Log("EnterCollider");
        if(ArrayContainsObject(chasing, other.gameObject))
        {
            Debug.Log("chasing " + other.gameObject.name);
            //chase logic
            //needs to deactivate bezi mover, and become child of fish. 
            //but it can only do that, for one fish. 
           
            beziMover.enabled = false;
            transform.parent = other.gameObject.transform;
            Vector3 offset = new Vector3(0f, 1f, -12f);
            transform.localPosition = offset;
            transform.localRotation = Quaternion.identity;
        }

    }

    bool ArrayContainsObject(GameObject[] array, GameObject obj)
    {
        foreach (GameObject item in array)
        {
            if(item == obj)
            {
                return true;
            }
        }
        return false;
    }
}
