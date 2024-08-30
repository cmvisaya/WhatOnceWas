using System.Collections.Generic;
using System.Collections;
using UnityEngine;
 
[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 10)]
    public string line;
}
 
[System.Serializable]
public class Dialogue
{
    public int endEventId;
    public float delayTime;
    public bool autoProgress;
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}
 
public class DialogueEvent : MonoBehaviour
{
    public Dialogue dialogue;
 
    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
 
    private void Start() //Debug for now
    {
        //StartCoroutine(StartWithDelayTime(dialogue.delayTime));
    }

    public IEnumerator StartWithDelayTime(float dt) {
        yield return new WaitForSeconds(dt);
        TriggerDialogue();
    }
}
 