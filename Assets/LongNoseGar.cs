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

    private FollowBeziCurve chaseMover;

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
        if (ArrayContainsObject(chasing, other.gameObject))
        {
            Debug.Log("chasing " + other.gameObject.name);

            // Get the FollowBeziCurve component from the other fish
            chaseMover = other.GetComponent<FollowBeziCurve>();
            if (chaseMover != null)
            {
                StartChasing();
            }
        }
    }

    public void StartChasing()
    {
        if (beziMover != null && chaseMover != null)
        {
            beziMover._points = (Vector3[])chaseMover._points.Clone();
        }
    }

    bool ArrayContainsObject(GameObject[] array, GameObject obj)
    {
        foreach (GameObject item in array)
        {
            if (item == obj)
            {
                return true;
            }
        }
        return false;
    }
}
