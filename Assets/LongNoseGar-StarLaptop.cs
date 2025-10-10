using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongNoseGar : MonoBehaviour
{
    [Header("Movement Controller")]
    public FollowBeziCurve beziMover;


   [Header("Social Interaction")]
    public GameObject[] friendWith;
    public GameObject[] avoiding;
    public GameObject[] BlueGills;

    private FollowBeziCurve chaseMover;

    // Start is called before the first frame update


    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("EnterCollider");
        if (other.tag == "BlueGill")
        {
            Debug.Log("Chasing bluegill");
            beziMover.currentState = FollowBeziCurve.FishState.Follow;
            FollowBeziCurve _otherFish = other.gameObject.GetComponent<FollowBeziCurve>();
            _otherFish.beingChased = true;

        }
    }

//    public void StartChasing()
//    {
//        if (beziMover != null && chaseMover != null)
//        {
//            beziMover._points = (Vector3[])chaseMover._points.Clone();
//        }
//    }

//    bool ArrayContainsObject(GameObject[] array, GameObject obj)
//    {
//        foreach (GameObject item in array)
//        {
//            if (item == obj)
//            {
//                return true;
//            }
//        }
//        return false;
//    }
}
