using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VillagerController : MonoBehaviour {
    
    public NavMeshAgent agent;
    public float travelTimeMax;
    public float travelTimeMin;
    private float travelTime;
    public float range;
    public float moveSpeed;
    public float allowedError;
    private Vector3 newPoint;

    public GameObject particleEffect;

    private float timeToControl;

    public bool influenced = false;

    void Start()
    {

        agent.speed = moveSpeed;
        
        agent.autoBraking = false;

        travelTime = Random.Range(travelTimeMin, travelTimeMax);
        timeToControl = Random.Range(15, 30);
        FindNewPoint();
        
    }
    
    void Update()
    {

        travelTime -= Time.deltaTime;

        if (transform.position.x < newPoint.x + allowedError && transform.position.x > newPoint.x - allowedError
            && transform.position.z < newPoint.z + allowedError && transform.position.z > newPoint.z - allowedError)
        {

            FindNewPoint();

        }
        else if (travelTime <= 0)
        {

            travelTime = Random.Range(travelTimeMin, travelTimeMax);

            FindNewPoint();

        }

    }

    private void FindNewPoint()
    {

        newPoint = transform.position + new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
        
        agent.destination = newPoint;
        
    }

    private IEnumerator CountDown()
    {
        while (timeToControl > 0)
        {
            timeToControl -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        //Increase Lifetime
        Destroy(this.gameObject);
    }

    public void Influence()
    {
        influenced = true;
        StartCoroutine(CountDown());
        particleEffect.SetActive(true);
        //Enable Villager to be killed by Guards

    }

    public void RagDoll()
    {

        //RagDollPhysics
        Debug.Log("RagDoll Enabled");

    }

}
