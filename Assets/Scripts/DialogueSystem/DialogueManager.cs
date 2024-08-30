using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
 
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
 
    public TextMeshProUGUI dialogueArea;
 
    private Queue<DialogueLine> lines;
    private List<DialogueEvent> branches;

    private bool inTyping = false;

    private DialogueLine currentLine;
    private string currentLineText;

    private int endEventId;

    private bool autoProgress;
    
    public bool isDialogueActive = false;
 
    public float typingSpeed = 5f;
 
 
    private void Awake()
    {
        // continueBtn.onClick.AddListener(() => {
        //     DisplayNextDialogueLine();
        // });

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
 
        lines = new Queue<DialogueLine>();
        typingSpeed = 1 / typingSpeed;
    }

    private void Update() {
        if(Input.GetButtonDown("Fire1") && isDialogueActive) {
            DisplayNextDialogueLine();
        }
    }
 
    public void StartDialogue(Dialogue dialogue)
    {
        endEventId = dialogue.endEventId;
        autoProgress = dialogue.autoProgress;

        isDialogueActive = true;
  
        lines.Clear();
 
        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }
 
        DisplayNextDialogueLine();
    }
 
    public void DisplayNextDialogueLine()
    {
        if(!inTyping) {
            if (lines.Count == 0)
            {
                EndDialogue();
                return;
            }
    
            currentLine = lines.Dequeue();
            currentLineText = currentLine.line;
        
            StopAllCoroutines();
    
            StartCoroutine(TypeSentence(currentLine));
        } else {
            StopAllCoroutines();
            dialogueArea.text = currentLineText;
            inTyping = false;
        }
    }
 
    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        float waitTime = typingSpeed;
        inTyping = true;
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(waitTime);
            //AudioManager.Instance.StopSFX();
        }
        inTyping = false;
        yield return new WaitForSeconds(2f);
        if (autoProgress) DisplayNextDialogueLine();
    }
 
    void EndDialogue()
    {
        isDialogueActive = false;
        dialogueArea.text = "";
        StartCoroutine(EnactEndEvent());
    }

    private IEnumerator EnactEndEvent() {
        switch (endEventId) {
            case 1:
                SceneManager.LoadScene("Diorama2");
                break;
            case 2:
                SceneManager.LoadScene("Diorama1");
                break;
            case 3:
                SceneManager.LoadScene("ThemePark");
                break;
            case 4:
                SceneManager.LoadScene("SampleScene");
                break;
            case 5:
                SceneManager.LoadScene("Diorama3");
                break;
        }

        yield return null;
    }


}