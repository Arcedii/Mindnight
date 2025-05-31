using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverFader : MonoBehaviour
{
    [Header("Объекты со шейдерами")]
    public Material[] targetMaterials;
    public float duration = 0.5f;
    public float normalAlpha = 0.3f;
    public float hoverAlpha = 1f;

    [Header("Кнопка")]
    public Image buttonImage;
    public Color normalButtonColor = Color.white;
    public Color hoverButtonColor = Color.red;

    private Coroutine fadeRoutine;

    public void StartHover()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(Fade(hoverAlpha, hoverButtonColor));
    }

    public void EndHover()
    {
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);
        fadeRoutine = StartCoroutine(Fade(normalAlpha, normalButtonColor));
    }

    private System.Collections.IEnumerator Fade(float targetAlpha, Color targetBtnColor)
    {
        float t = 0f;

        float startAlpha = targetMaterials[0].GetColor("_BaseColor").a;
        Color startBtnColor = buttonImage.color;

        while (t < duration)
        {
            float blend = t / duration;

            // Материалы (только alpha)
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, blend);
            foreach (Material mat in targetMaterials)
            {
                Color c = mat.GetColor("_BaseColor");
                c.a = newAlpha;
                mat.SetColor("_BaseColor", c);
            }

            // Кнопка (только цвет)
            buttonImage.color = Color.Lerp(startBtnColor, targetBtnColor, blend);

            t += Time.deltaTime;
            yield return null;
        }

        foreach (Material mat in targetMaterials)
        {
            Color c = mat.GetColor("_BaseColor");
            c.a = targetAlpha;
            mat.SetColor("_BaseColor", c);
        }

        buttonImage.color = targetBtnColor;
    }
}
