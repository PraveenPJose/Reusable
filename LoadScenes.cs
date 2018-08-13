using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScenes : MonoBehaviour {
    public float loadProgress;
    public string sceneName;
    public bool load;
    AsyncOperation async;

    void Start ()
    {
        StartCoroutine(LoadNewScene());
	}
	
	
	void Update ()
    {
        async.allowSceneActivation = load;
    }


    IEnumerator LoadNewScene()
    {
        
        // Set priority low and load asynchronously to help performance while loading
        Application.backgroundLoadingPriority = ThreadPriority.Low;
        async = SceneManager.LoadSceneAsync(sceneName);
        async.allowSceneActivation = false;
        // Wait until Unity has finished loading the scene.
        // Wth allowSceneActivation = false Unity won't fully load the scene and will
        // and async.progress will only go as far as 90%
        while (!async.isDone)
        {
             loadProgress = async.progress;

            if (loadProgress >= 0.9f)
            {
                // Almost done.
                break;
            }

            yield return null;
        }
        // Finish loading the scene
        yield return null;
        yield return async;
        Debug.Log("Loading complete");
    }
}
