using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractNotif : MonoBehaviour
{

    private Camera cam;   
    Vector3 screenCenter = new Vector3(Screen.width/2, Screen.height/2, 0);
    private bool notifActive = false;
    [SerializeField]
    private Animator notifAnim;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
        Ray ray = cam.ScreenPointToRay(screenCenter);
        RaycastHit hit;
        if(Physics.Raycast(ray.origin,ray.direction, out hit, 6f, layerMask)){
            if (hit.collider.gameObject == gameObject && !notifActive) {
                notifActive = true;
                if (notifAnim != null) notifAnim.Play("Show");
            }
        }
        else if (notifActive) {
            notifActive = false;
            if (notifAnim != null) notifAnim.Play("Hide");
        }
        
    }
}
