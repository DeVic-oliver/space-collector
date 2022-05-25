using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    int spacePressedCount = 0;

    AsyncOperation operation;
    
    [SerializeField] Slider progressBar;
    [SerializeField] GameObject infoObjective;
    [SerializeField] GameObject infoControls;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("LoadSceneAsync");
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && progressBar.value == 1f)
        {
            spacePressedCount++;
            if (spacePressedCount == 1)
            {
                infoObjective.SetActive(false);
                infoControls.SetActive(true);
            }
            else if (spacePressedCount >= 1)
            {
                operation.allowSceneActivation = true;
            }
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
