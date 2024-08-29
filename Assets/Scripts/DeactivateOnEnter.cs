using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateOnEnter : MonoBehaviour
{

    [SerializeField] private GameObject toDeactivate;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            toDeactivate.SetActive(false);
            GameObject.Find("GameManager").GetComponent<GameManager>().finalInitiated = true;
        }
    }
}
