using System.Collections;
using System.Collections.Generic;
using DevZilio.Core.Singleton;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class EffectsManager : Singleton<EffectsManager>
{
    public PostProcessVolume processVolume;

    public float duration = 1f;

    [SerializeField]
    private Vignette _vignette;

    public float intensityStart = 0.2f; // Intensidade inicial do Vignette

    public float intensityEnd = 0.8f; // Intensidade final do Vignette

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(FlashColorVignette());
    }

    IEnumerator FlashColorVignette()
    {
        Vignette tmp;

        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            _vignette = tmp;
        }

        ColorParameter c = new ColorParameter();

        float time = 0;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.black, Color.red, time / duration);
            _vignette.intensity.Override(Mathf.Lerp(intensityStart, intensityEnd, time / duration));
            time += Time.deltaTime;
            _vignette.color.Override (c);
            yield return new WaitForEndOfFrame();
        }

        time = 0;
        while (time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.black, time / duration);
            _vignette.intensity.Override(Mathf.Lerp(intensityEnd, intensityStart, time / duration));
            time += Time.deltaTime;
            _vignette.color.Override (c);
            yield return new WaitForEndOfFrame();
        }
    }
}
