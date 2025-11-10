using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;
using TasiYokan.Curve;

public class YellowBass : MonoBehaviour
{
    [Header("Movement Controller")]
    public FollowBeziCurve beziMover;
    public FollowBeziCurve followBezi;
    public GameObject _nearestBGill;

    [Header("Social Interaction")]
    public GameObject[] friendWith;
    public GameObject[] avoiding;
    public GameObject[] chasing;
    public bool interacting;

    void Start()
    {
        interacting = false;
    }
  private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "BlueGill")

        {
            interacting = true;
            StartCoroutine(Follow());
        }
    }

    public IEnumerator Follow()

    {
        followBezi = _nearestBGill.GetComponent<FollowBeziCurve>();
        followBezi._fishFollowing = this.gameObject.transform;
        followBezi.currentState = FollowBeziCurve.FishState.Follow;
        yield return new WaitForSeconds(7);
        interacting = false;

    }

}




