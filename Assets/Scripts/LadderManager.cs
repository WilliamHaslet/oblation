using UnityEngine;

public class LadderManager : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {

            PlayerController.sharedPlayerControllerInstance.isOnLadder = true;

        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {

            PlayerController.sharedPlayerControllerInstance.isOnLadder = false;

        }

    }

}
