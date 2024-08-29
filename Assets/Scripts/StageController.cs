using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour //Light controller
{
    public float maxH;
    public float maxV;
    public float hSpeed;
    public float vSpeed;
    private float startH;
    private float startV;

    void Start() {
        startV = transform.eulerAngles.x;
        startH = transform.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(vSpeed, hSpeed, 0);
        if(Mathf.Abs(transform.eulerAngles.x - startV) > maxV) {
            vSpeed = -vSpeed;
        }
        if(Mathf.Abs(transform.eulerAngles.y - startH) > maxH) {
            hSpeed = -hSpeed;
        }
    }
}
