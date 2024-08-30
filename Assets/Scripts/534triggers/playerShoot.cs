using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerShoot : MonoBehaviour
{

    public enum Shot {nothing, createObject};
    [SerializeField] private Shot shot = Shot.nothing;
    [SerializeField] private GameObject spawnObject;
    private Camera cam;   
    Vector3 screenCenter = new Vector3(Screen.width/2, Screen.height/2, 0);
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
            Ray ray = cam.ScreenPointToRay(screenCenter);
            Debug.DrawRay(ray.origin, ray.direction*50, Color.green, 2);
            RaycastHit hit;
            int layerMask = 1 << 2;
            layerMask = ~layerMask;
            if(Physics.Raycast(ray.origin,ray.direction, out hit, Mathf.Infinity, layerMask)){
                objectShot hitTrigger = hit.transform.gameObject.GetComponent<objectShot>();
                if (hitTrigger != null){
                    hitTrigger.Activate(ray, hit);
                }
                    
            }
            if(shot == Shot.createObject){
                GameObject spawn;
                if(spawnObject == null){
                    spawn = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                }
                else{
                    spawn = GameObject.Instantiate(spawnObject);
                }
                spawn.transform.position = hit.point;
            }

        }
    }
}
