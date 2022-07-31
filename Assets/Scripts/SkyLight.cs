using System.Collections;
using UnityEngine;
using DG.Tweening;

public class SkyLight : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Weather _weather;
    private Coroutine _changingLight;

    private Color _calmColor= new Color(0.578084f, 0.5727127f, 0.735849f);
    private Color _thunderColor = new Color(0.08855464f, 0.08855464f, 0.09433961f);
    private float _timeToChangeColor = 5;

    private void Start()
    {
        _weather = GetComponentInParent<Weather>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _weather.StartCalmWeather += StartCalmLight;
        _weather.StartCloudGathering += StartThunderLight;
        _weather.StartThunderWeather += StartLighting;
    }

    private void OnDisable()
    {
        _weather.StartCalmWeather -= StartCalmLight;
        _weather.StartCloudGathering -= StartThunderLight;
        _weather.StartThunderWeather -= StartLighting;
    }

    private void StartLighting()
    {
        if (_changingLight != null)
        {
            StopCoroutine(_changingLight);
        }

        _changingLight=StartCoroutine(Flashing());
    }

    private void StartCalmLight()
    {
        if (_changingLight != null)
        {
            StopCoroutine(_changingLight);
        }

        ChangeColor(_calmColor);
    }

    private void StartThunderLight()
    {
        ChangeColor(_thunderColor);
    }

    private void ChangeColor(Color color)
    {
        _spriteRenderer.DOColor(color, _timeToChangeColor);
    }

    private IEnumerator Flashing()
    {
        float minTimeBetweenFlashing = 0.5f;
        float maxTimeBetweenFlashing = 1.5f;
        float timeToFlash = 0.1f;

        while (true)
        {
            float flashTime = Random.Range(minTimeBetweenFlashing, maxTimeBetweenFlashing);
            Tween flashing=_spriteRenderer.DOColor(Color.white, timeToFlash).SetLoops(2, LoopType.Yoyo);
            flashing.WaitForCompletion();
            yield return  new WaitForSeconds(flashTime);
        }
    }    
}
