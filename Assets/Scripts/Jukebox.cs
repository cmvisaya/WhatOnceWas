using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jukebox : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip  audioClip;
    public Transform positionPlayed;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if(positionPlayed == null) {
            if(!GameObject.Find("GameManager").GetComponent<GameManager>().finalInitiated) {
                audioSource.Play();
            }
        }
        else {
            AudioSource.PlayClipAtPoint(audioClip, positionPlayed.position);
        }
    }
}
