using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class objectShot : MonoBehaviour
{
    public enum Effect {nothing, destroyObject, shoveObject, toggleAnimation, startTimeline, triggerParticles, playAudio, changeScene, goHome};
    [SerializeField] private Effect effect = Effect.nothing;
    [SerializeField] private int sceneNum = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate(Ray ray, RaycastHit hit){
        //Destroy Effect
        if (effect == Effect.destroyObject){
         Destroy(gameObject);
        }
        //Physics Effect
        else if (effect == Effect.shoveObject){
            Rigidbody rb = hit.transform.gameObject.GetComponent<Rigidbody>();
            if(rb == null){
                rb = hit.transform.gameObject.AddComponent<Rigidbody>();
            }
            rb.AddForce((ray.direction + new Vector3(0, 1, 0)) * 500);

        }
        //Animation Effect
        else if(effect == Effect.toggleAnimation){
            Animator anim = hit.transform.gameObject.GetComponent<Animator>();
            if (anim != null){
                if (anim.enabled){
                    anim.enabled = false;
                }
                else{
                    anim.enabled = true;
                }
            }
        }
        //Timeline Effect
        else if (effect == Effect.startTimeline){
            PlayableDirector pd = hit.transform.gameObject.GetComponent<PlayableDirector>();
            if (pd != null){
                if(pd.state != PlayState.Playing){
                    pd.Play();
                }
            }
        }
        //Audio Effect
        else if (effect == Effect.playAudio){
            AudioSource audi = hit.transform.gameObject.GetComponent<AudioSource>();
            if(audi != null){
                if(!audi.isPlaying){
                 audi.Play();
                }
            }
        }
        else if (effect == Effect.triggerParticles){
            ParticleSystem ps = hit.transform.gameObject.GetComponent<ParticleSystem>();
            if (ps != null){
                ps.Play();

            }
        }
        else if (effect == Effect.changeScene){
            GameObject.Find("GameManager").GetComponent<GameManager>().HandleSceneChange(sceneNum);
        }
        else if (effect == Effect.goHome) { GameObject.Find("GameManager").GetComponent<GameManager>().HandleSceneReturn(); }
    }
}
