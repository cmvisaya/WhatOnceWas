using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

    [SerializeField] private int cutsceneID = 0;
    [SerializeField] private GameObject nextOrb;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (nextOrb != null)
            {
                nextOrb.SetActive(true);
            }
            if (cutsceneID > 0)
            {
                FindObjectOfType<CutsceneManager>().RunCutscene(cutsceneID, 1);
            }
            Destroy(gameObject);
        }
    }
}
