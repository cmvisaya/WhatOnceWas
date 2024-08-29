using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    [SerializeField] private bool[] visited = new bool[4];
    public bool finalInitiated = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        visited[0] = true;
    }

    private bool allVisited() {
        bool returned = true;
        foreach(bool sceneVisited in visited) {
            if(sceneVisited == false) {
                returned = false;
            }
        }
        return returned;
    }

    public void HandleSceneChange(int sceneNum) {
        SceneManager.LoadScene(sceneNum + 1);
        visited[sceneNum] = true;
    }

    public void HandleSceneReturn() {
        if(allVisited()) {
            SceneManager.LoadScene(5);
        }
        else {
            SceneManager.LoadScene(1);
        }
    }
}
