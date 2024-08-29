using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    public GameObject dialogueBox;
    public GameObject helpBox;
    public TextMeshProUGUI dialogueText;
    public GameObject playerGO;
    private PlayerController player;
    [SerializeField] private GameObject airdashTutorial;
    [SerializeField] private GameObject door;

    [SerializeField] private int cutsceneID = 1;
    [SerializeField] private int slideID = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = playerGO.GetComponent<PlayerController>();
        RunCutscene(cutsceneID, slideID);
        slideID++;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Continue"))
        {
            RunCutscene(cutsceneID, slideID);
            slideID++;
        }
    }

    public void RunCutscene(int csid, int slid)
    {
        switch(csid)
        {
            case 1:
                {
                    switch(slid)
                    {
                        case 1:
                            {
                                slideID = 1;
                                player.hasControl = false;
                                dialogueBox.SetActive(true);
                                helpBox.SetActive(true);
                                dialogueText.text = "Welcome to PROJECT AIRDASH";
                                break;
                            }
                        case 2:
                            {
                                helpBox.SetActive(false);
                                dialogueText.text = "Let's get started...";
                                break;
                            }
                        case 3:
                            {
                                dialogueText.text = "Collect the CLOUD ORBS to advance";
                                break;
                            }
                        case 4:
                            {
                                dialogueText.text = "WASD to MOVE";
                                player.hasControl = true;
                                break;
                            }
                    }
                    break;
                }
            case 2:
                {
                    switch(slid)
                    {
                        case 1:
                            {
                                dialogueText.text = "MOUSE to LOOK AROUND";
                                break;
                            }
                    }
                }
                break;
            case 3:
                {
                    switch(slid)
                    {
                        case 1:
                            {
                                dialogueText.text = "SPACE to JUMP";
                                break;
                            }
                    }
                    break;
                }
            case 4:
                {
                    switch(slid)
                    {
                        case 1:
                            {
                                airdashTutorial.SetActive(true);
                                dialogueText.text = "LEFT SHIFT to DASH";
                                break;
                            }
                    }
                }
                break;
            case 5:
                {
                    switch(slid)
                    {
                        case 1:
                            {
                                cutsceneID = csid;
                                slideID = slid + 1;
                                player.hasControl = false;
                                dialogueText.text = "Good job...";
                                break;
                            }
                        case 2:
                            {
                                dialogueText.text = "You are now ready...";
                                break;
                            }
                        case 3:
                            {
                                dialogueText.text = "Welcome again to PROJECT AIRDASH";
                                break;
                            }
                        case 4:
                            {
                                dialogueText.text = "The door will now open...";
                                break;
                            }
                        case 6:
                            {
                                door.SetActive(false);
                                dialogueText.text = "Get to the end of the course as quickly as possible!";
                                break;
                            }
                        case 7:
                            {
                                player.hasControl = true;
                                dialogueBox.SetActive(false);
                                break;
                            }
                    }
                }
                break;
            case 6: {
                switch(slid) {
                    case 1: {
                        cutsceneID = csid;
                        slideID = slid + 1;
                        player.hasControl = false;
                        dialogueBox.SetActive(true);
                        dialogueText.text = "Congratulations! You beat the course!";
                        break;
                    }
                    case 2: {
                        dialogueText.text = "Thanks for playing!";
                        break;
                    }
                    case 3: {
                        dialogueText.text = "You have been granted additional controls.";
                        break;
                    }
                    case 4: {
                        dialogueText.text = "You may now JUMP in the air.";
                        break;
                    }
                    case 5: {
                        dialogueText.text = "You no longer need to touch the ground to refresh your DASH.";
                        break;
                    }
                    case 6: {
                        dialogueText.text = "Use LEFT CONTROL to toggle the FLOAT ability.";
                        break;
                    }
                    case 7: {
                        player.canAirJump = true;
                        player.airDashRefreshInAir = true;
                        player.canFloat = true;
                        player.hasControl = true;
                        dialogueBox.SetActive(false);
                        break;
                    }
                }
                break;
            }
            case 7: {
                dialogueText.text = "What are you doing over here? The game's over.";
                dialogueBox.SetActive(true);
                break;
            }
            default:
                break;
        }
    }
}
