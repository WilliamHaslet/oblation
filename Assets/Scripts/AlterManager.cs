using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlterManager : MonoBehaviour {

    public GameObject blackMagic;
    public static AlterManager sharedAMIns;

	// Use this for initialization
	void Start () {
        sharedAMIns = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.name.Contains("TargetVillager"))
        {

            GameManager.sharedGameManagerInstance.successScreen.SetActive(true);
            blackMagic.SetActive(true);
            Destroy(other.gameObject);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

        }

    }

}
