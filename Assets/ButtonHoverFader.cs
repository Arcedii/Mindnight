using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverFader : MonoBehaviour
{
    public Image buttonImage;
    public Material[] targetMaterials;
    public float duration = 0.5f;

    public Color hoverColor = new Color(1, 1, 1, 1);
    public Color normalColor = new Color(1, 1, 1, 0.3f);

    private Coroutine fadeRoutine;

    public void StartHover()
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(Fade(hoverColor.a));
    }

    public void EndHover()
    {
        if (fadeRoutine != null) StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(Fade(normalColor.a));
    }

    private System.Collections.IEnumerator Fade(float targetAlpha)
    {
        float startAlpha = buttonImage.color.a;
        float t = 0f;

        while (t < duration)
        {
            float a = Mathf.Lerp(startAlpha, targetAlpha, t / duration);
            Color btnCol = buttonImage.color;
            btnCol.a = a;
            buttonImage.color = btnCol;

            foreach (Material mat in targetMaterials)
            {
                Color c = mat.GetColor("_BaseColor");
                c.a = a;
                mat.SetColor("_BaseColor", c);
            }

            t += Time.deltaTime;
            yield return null;
        }

        Color finalBtnCol = buttonImage.color;
        finalBtnCol.a = targetAlpha;
        buttonImage.color = finalBtnCol;

        foreach (Material mat in targetMaterials)
        {
            Color c = mat.GetColor("_BaseColor");
            c.a = targetAlpha;
            mat.SetColor("_BaseColor", c);
        }
    }
}
