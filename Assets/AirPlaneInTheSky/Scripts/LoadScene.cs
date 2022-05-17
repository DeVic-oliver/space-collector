using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    AsyncOperation operation;
    
    [SerializeField] Slider progressBar;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoadSceneAsync");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && progressBar.value == 1f) 
        { 
                operation.allowSceneActivation = true;
        }
    }

    IEnumerator LoadSceneAsync()
    {
        operation = SceneManager.LoadSceneAsync(LoadingData.sceneToLoad);

        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            progressBar.value = operation.progress;

            if(operation.progress >= 0.9f)
            {
                progressBar.value = 1f;
            }
            yield return null;
        }
    }
}
