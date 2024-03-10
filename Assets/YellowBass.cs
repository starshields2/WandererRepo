using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARSubsystems;

public class YellowBass : MonoBehaviour
{
    [Header("Transforms")]
    public Transform[] swimSpots;
    public Transform runSpot;
    public Transform hideSpot; 
    public Transform human;
    public bool isSwim;
    public TestCube cube; 

    [Header("Distance Checks")]
    public float runDist = 10f;
    public float hideDist = 6f;
    public float curDist = 0f;

    //fish state enums
    public FishState currentState = FishState.RegularSwim;

    public enum FishState
    {
        Idle,
        RegularSwim,
        Run,
        Hide
    }

    void Start()
    {
        currentState = FishState.RegularSwim;
    }
        // Update is called once per frame
        void Update()
        {
        

        

        switch (currentState)
            {
                case FishState.Idle:
                    break;
                case FishState.RegularSwim:
                StartSwimming(1f);
                    break;
                case FishState.Run:
                StopCoroutine(Swim(1f));
                StartCoroutine(RunSequence());
                    break;
                case FishState.Hide:
                    Hide();
                    break;
                default:
                    break;
            }

            curDist = Vector3.Distance(human.position, transform.position);

            if (curDist <= runDist)
            {
               // currentState = FishState.Run;
            }
            else if (curDist <= hideDist)
            {
               // currentState = FishState.Hide;
            }
            else
            {
               // currentState = FishState.RegularSwim;
            }
        }

        public void Run()
        {
        StartCoroutine(RunSequence());
        }
    public IEnumerator RunSequence()
    {
        if (Vector3.Distance(transform.position, hideSpot.position) > 0.1f)
        {
            transform.LookAt(hideSpot);
            float speed = 3f;
            transform.position = Vector3.MoveTowards(transform.position, hideSpot.position, speed * Time.deltaTime);
            cube.Green();
        }
        else
        {
            // If the fish has reached the hide spot, change its state
            yield return new WaitForSeconds(5f);
            currentState = FishState.RegularSwim;
        }
    }
        public void Hide()
        {
            if (Vector3.Distance(transform.position, runSpot.position) > 0.1f)
            {
                float speed = 1f;
                transform.position = Vector3.MoveTowards(transform.position, runSpot.position, speed * Time.deltaTime);
            }
            else
            {
                // If the fish has reached the run spot, change its state
                currentState = FishState.Run;
            }
        }

      public void OnTriggerStay(Collider other)
    {
        GameObject[] fish;
        fish = GameObject.FindGameObjectsWithTag("fish"); //get all the fish game objects
        if(fish.Length > 8)
        {
            //run away
        }
    }
    private int currentIndex = 0;

    public void StartSwimming(float dTime)
    {
        // Start the swim coroutine
        StartCoroutine(Swim(dTime));
    }

    public IEnumerator Swim(float dTime)
    {
        while (true) // Continue swimming indefinitely
        {
            // Move to the current swim spot
            yield return StartCoroutine(MoveToSwimSpot(swimSpots[currentIndex], dTime));

            // Move to the next swim spot index
            currentIndex = (currentIndex + 1) % Random.Range(1, swimSpots.Length);

            // Add a delay before moving to the next swim spot
            yield return new WaitForSeconds(dTime);
        }
    }


    private IEnumerator MoveToSwimSpot(Transform spot, float delayTime)
    {
        transform.LookAt(spot);
        float speed = 0.0005f;
        
        while (Vector3.Distance(transform.position, spot.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, spot.position, speed * Time.deltaTime);
            yield return null;
        }
    }

}




