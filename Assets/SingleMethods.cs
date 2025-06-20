using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SingleMethods : MonoBehaviour
{

    public Animator transitionAnimator; // Аниматор с триггером "Start"
    public AudioSource audioSource;     // Источник звука
    public AudioClip transitionSound;   // Клип для перехода
    public float transitionTime = 1.5f; // Время длительности анимации и звука

    public Transform rotatingObject;         // Объект, который нужно повернуть
    public float rotationStartY = -11.3f;
    public float rotationEndY = 153.1f;

    public Image fadeImage;              // UI Image для прозрачности
    public float fadeDuration = 1.5f;    // Длительность изменения альфы

    public void LoadStartScene(string sceneName)
    {
        StartCoroutine(LoadSceneWithTransition(sceneName));
    }

    private IEnumerator LoadSceneWithTransition(string sceneName)
    {
        // Звук
        if (audioSource != null && transitionSound != null)
            audioSource.PlayOneShot(transitionSound);

        // Анимация
        if (transitionAnimator != null)
            transitionAnimator.SetTrigger("Scream");

        // Вращение
        if (rotatingObject != null)
        {
            float elapsed = 0f;
            Quaternion startRotation = Quaternion.Euler(0, rotationStartY, 0);
            Quaternion endRotation = Quaternion.Euler(0, rotationEndY, 0);

            while (elapsed < transitionTime)
            {
                rotatingObject.rotation = Quaternion.Lerp(startRotation, endRotation, elapsed / transitionTime);
                elapsed += Time.deltaTime;
                yield return null;
            }

            rotatingObject.rotation = endRotation;
        }

        // Плавная прозрачность
        if (fadeImage != null)
        {
            float elapsed = 0f;
            Color color = fadeImage.color;
            color.a = 0f;
            fadeImage.color = color;

            while (elapsed < fadeDuration)
            {
                color.a = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
                fadeImage.color = color;
                elapsed += Time.deltaTime;
                yield return null;
            }

            color.a = 1f;
            fadeImage.color = color;
        }

        // Переход
        yield return new WaitForSeconds(0.1f); // небольшая пауза после fade
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Debug.Log("Выход из игры...");
        Application.Quit();
    }
}
