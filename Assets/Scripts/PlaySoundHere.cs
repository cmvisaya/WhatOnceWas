using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundHere : MonoBehaviour
{

    [SerializeField] private AudioClip audio;
    [SerializeField] private float volume;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(audio, this.transform.position, volume);
    }
}
