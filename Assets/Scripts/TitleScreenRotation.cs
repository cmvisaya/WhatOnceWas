using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenRotation : MonoBehaviour
{
    [SerializeField] private Transform container;
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        container.Rotate(speed, speed, speed);
    }
}
