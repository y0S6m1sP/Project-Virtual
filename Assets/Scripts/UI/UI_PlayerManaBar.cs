using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerManaBar : MonoBehaviour
{
    private EntityStats stats;
    [SerializeField] private Slider manaSlider;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private float fadeDuration = 0.5f;
    private bool isVisible = false;

    private void Start()
    {
        stats = GetComponentInParent<EntityStats>();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        manaSlider.maxValue = stats.mana.GetValue();
    }

    private void Update()
    {
        if(stats.currentMana == 0)
        {
            HideManaBar();
        } else {
            ShowManaBar();
        }

        manaSlider.value = Mathf.Lerp(manaSlider.value, stats.currentMana, Time.deltaTime * 10f);
    }

    public void ShowManaBar()
    {
        if (!isVisible)
        {
            isVisible = true;
            StartCoroutine(FadeIn());
        }
    }

    public void HideManaBar()
    {
        if (isVisible)
        {
            isVisible = false;
            StartCoroutine(FadeOut());
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}
