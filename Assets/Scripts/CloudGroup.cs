using System;
using UnityEngine;
using DG.Tweening;

public class CloudGroup : MonoBehaviour
{
    [SerializeField] private StartCloudTravelDot _startTravelDot;
    [SerializeField] private int _timeMoving;

    public event Action EndAnimation;
    
    private Weather _weather;
    private SpriteRenderer[] _clouds;
    private EndCloudTravelDot _endTravelDot;
    private int _timeToDarking = 12;
    private int _timeToChangeColor = 5;    
    private Color _darkColor = new Color(0.2169811f, 0.2036757f, 0.2036757f);
    private Color _lightColor = new Color(1f, 1f, 1f);
    private int _minTimeToMoving = 10;
    private int _maxTimeToMOving = 20;

    private void OnEnable()
    {
        _timeMoving = UnityEngine.Random.Range(_minTimeToMoving,_maxTimeToMOving);
        _weather = GetComponentInParent<Weather>();
        _weather.StartCalmWeather += CalmDown;
        _weather.StartCloudGathering += Gathering;

        _clouds = GetComponentsInChildren<SpriteRenderer>();
        transform.position = _startTravelDot.transform.position;

        _endTravelDot = _startTravelDot.GetComponentInChildren<EndCloudTravelDot>();
    }

    private void OnDisable()
    {
        _weather.StartCalmWeather -= CalmDown;
        _weather.StartCloudGathering -= Gathering;
    }

    public void Gathering()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOMove(_endTravelDot.transform.position, _timeMoving));

        for (int i = 0; i < _clouds.Length; i++)
        {
            sequence.Insert(_timeToDarking, _clouds[i].DOColor(_darkColor, _timeToChangeColor));
        }

        sequence.onComplete += EndAnimation.Invoke;

    }

    public void CalmDown()
    {
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < _clouds.Length; i++)
        {
            sequence.Insert(0, _clouds[i].DOColor(_lightColor, _timeToChangeColor));
        }

        sequence.Append(transform.DOMove(_startTravelDot.transform.position, _timeMoving));

        sequence.onComplete += EndAnimation.Invoke;
    }
}
