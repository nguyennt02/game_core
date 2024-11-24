using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSystem : MonoBehaviour
{
    public static Action onAfterLoading;
    public static string nameScene = KeyString.NAME_SCENE_LOBBY;
    [SerializeField] Slider loadingBar;

    void Start()
    {
        StartCoroutine(LoadSceneByNameAt(nameScene, 5f));
    }

    IEnumerator LoadSceneByNameAt(string nameScene, float duration, float elapsed = 0)
    {
        AsyncOperation loadScene = SceneManager.LoadSceneAsync(nameScene);

        onAfterLoading += () =>
        {
            loadingBar.value = 1;
            loadScene.allowSceneActivation = true;
        };

        loadScene.allowSceneActivation = false;
        onAfterLoading?.Invoke();

        float progress = 0;
        while (progress < 0.9f)
        {
            loadingBar.value = Mathf.MoveTowards(0, 0.9f, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}
