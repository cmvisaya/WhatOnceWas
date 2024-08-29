using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CableCars : MonoBehaviour
{
    public GameObject[] cars;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < cars.Length; i++) {

            cars[i].GetComponent<Animator>().Play("Cablecar", 0, ((float) i) / cars.Length);
        }
    }
}
