using UnityEngine;

public class DoorController : MonoBehaviour {

    public Animator doorAnimatior;

    private void OnTriggerEnter(Collider other)
    {

        doorAnimatior.Play("DoorOpen");
        
    }

    private void OnTriggerExit(Collider other)
    {

        doorAnimatior.Play("DoorClose");

    }

}
		
