using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    public static GameManager sharedGameManagerInstance;
    public GameObject story;
    public GameObject tutorial;
    public float deathTime;
    private float deathTimeStore;
    public bool shouldCountDown;
    public GameObject HUD;
    public GameObject canvasDeathScreen;
    public Slider deathTimer;
    public GameObject demonInstrustions;
    public Text demonText;
    public GameObject choiceButtons;
    public GameObject targetVillager;
    public Transform[] targetVillagerSpawnPoints;
    public Transform spawnPoint;
    public Text deathText;
    public GameObject pauseMenu;
    public GameObject successScreen;
    public GameObject cameraHolder;
    public GameObject playerMesh;
    public GuardController[] guards;

    void Start () {

        sharedGameManagerInstance = this;

        deathTimeStore = deathTime;

        deathTimer.maxValue = deathTime;
        
    }
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.P))
        {

            pauseMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            PlayerController.sharedPlayerControllerInstance.canMove = false;

        }

        if (shouldCountDown)
        {

            deathTime -= Time.deltaTime;

        }
        
        if (deathTime <= 0)
        {

            KillPlayer();

            deathText.text = "Time ran out, and the Demon claimed your soul.";

        }

        deathTimer.value = deathTime;
		
	}

    public void KillPlayer()
    {
        
        HUD.SetActive(false);
        canvasDeathScreen.SetActive(true);
        shouldCountDown = false;
        deathTime = deathTimeStore;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PlayerController.sharedPlayerControllerInstance.canMove = false;

    }

    public void RestartGame()
    {

        successScreen.SetActive(false);
        AlterManager.sharedAMIns.blackMagic.SetActive(true);
        PlayerController.sharedPlayerControllerInstance.gameObject.transform.position = spawnPoint.position;
        PlayerController.sharedPlayerControllerInstance.gameObject.transform.rotation = spawnPoint.rotation;
        cameraHolder.transform.rotation = spawnPoint.rotation;
        playerMesh.transform.rotation = spawnPoint.rotation;
        Camera.main.transform.rotation = spawnPoint.rotation;
        story.SetActive(true);
        tutorial.SetActive(true);
        choiceButtons.SetActive(true);
        demonInstrustions.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        canvasDeathScreen.SetActive(false);
        pauseMenu.SetActive(false);
        PlayerController.sharedPlayerControllerInstance.canMove = false;
        deathTime = deathTimeStore;

        foreach (GuardController guard in guards)
        {

            guard.giveUpTime = 0;

        }
        
        foreach (Dragable p in FindObjectsOfType<Dragable>())
        {

            Destroy(p.gameObject);

        }
        
    }

    public void ExitStory()
    {

        story.SetActive(false);

    }

    public void ExitTutorial()
    {

        tutorial.SetActive(false);
        choiceButtons.SetActive(true);
        demonText.text = "Tonight, you will bring a villager wearing a blue shirt to my altar for sacrifice.";
        demonText.fontSize = 20;
        demonText.fontStyle = FontStyle.Normal;

    }

    public void CloseInstructions()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HUD.SetActive(true);
        shouldCountDown = true;
        demonInstrustions.SetActive(false);
        PlayerController.sharedPlayerControllerInstance.canMove = true;
        AlterManager.sharedAMIns.blackMagic.SetActive(false);
        Instantiate(targetVillager, targetVillagerSpawnPoints[Random.Range(0, targetVillagerSpawnPoints.Length)].position, targetVillagerSpawnPoints[Random.Range(0, targetVillagerSpawnPoints.Length)].rotation);

    }

    public void Refuse()
    {

        choiceButtons.SetActive(false);

        StartCoroutine(RefuseAndDie());

    }

    private IEnumerator RefuseAndDie()
    {

        demonText.text = "Then you shall die!";
        demonText.fontSize = 30;
        demonText.fontStyle = FontStyle.Bold;

        yield return new WaitForSeconds(2f);

        demonInstrustions.SetActive(false);

        KillPlayer();
        deathText.text = "You bravely died in defiance of the Demon.";

    }
    
    public void UnPause()
    {

        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PlayerController.sharedPlayerControllerInstance.canMove = true;

    }

}
