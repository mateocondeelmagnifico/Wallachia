using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private float timer;
    [SerializeField] private Image progressBar;

    private bool doOnce;
    private void Start()
    {
        timer = 12;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && !doOnce)
        {
            doIt();
            doOnce = true;
        }
    }
    
    private void doIt()
    {
        //This void is here because the coroutine has to start from it
        StartCoroutine(LoadSceneAsync(2));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.fillAmount = progressValue;

            yield return null;
        }
    }
}
