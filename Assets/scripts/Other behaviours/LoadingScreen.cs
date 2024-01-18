using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
    private float timer;
    [SerializeField] private Image progressBar;
    [SerializeField] private GameObject button;

    private AsyncOperation operation;

    private bool doOnce;
    private void Start()
    {
        timer = 1;
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
        operation = SceneManager.LoadSceneAsync(sceneId);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            progressBar.fillAmount = progressValue;

            if(operation.progress >= 0.8f)
                button.SetActive(true);

            yield return null;
        }
    }

    public void SwitchScene()
    {
        operation.allowSceneActivation = true;
    }
}
