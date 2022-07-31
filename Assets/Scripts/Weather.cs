using System;
using UnityEngine;
using UnityEngine.UI;

public class Weather : MonoBehaviour
{
    [SerializeField] private Button _thunderButton;
    [SerializeField] private Button _calmButton;
    [SerializeField] private Button _specialButton;

    public event Action StartedCloudGathering;
    public event Action StartedThunderWeather;
    public event Action StartedCalmWeather;

    private CloudGroup[] _cloudGroup;
    private int _countCloudGroupEndedAnimation;

    private bool _isActiveAnimation = false;
    private bool _isCalmWeather = true;

    private void OnEnable()
    {
        _cloudGroup = GetComponentsInChildren<CloudGroup>();

        foreach (CloudGroup clouds in _cloudGroup)
        {
            clouds.EndedAnimation += CheckStateCLoudsAnimation;
        }

        _thunderButton.onClick.AddListener(StartingCloudsGathering);
        _calmButton.onClick.AddListener(StartingCalm);
        _specialButton.onClick.AddListener(StartingThunder);
    }

    private void OnDisable()
    {
        _thunderButton.onClick.RemoveListener(StartingCloudsGathering);
        _calmButton.onClick.RemoveListener(StartingCalm);
        _specialButton.onClick.RemoveListener(StartingThunder);
    }

    private void StartingCloudsGathering()
    {
        if (!_isActiveAnimation && _isCalmWeather)
        {
            _countCloudGroupEndedAnimation = 0;
            _isActiveAnimation = true;
            StartedCloudGathering?.Invoke();
        }        
    }

    private void StartingCalm()
    {
        if (!_isActiveAnimation&&!_isCalmWeather)
        {
            _countCloudGroupEndedAnimation = 0;
            _isActiveAnimation = true;
            StartedCalmWeather?.Invoke();
        }        
    }

    private void StartingThunder()
    {
        if (!_isActiveAnimation && !_isCalmWeather)
        {
            StartedThunderWeather?.Invoke();
        }
    }

    private void CheckStateCLoudsAnimation()
    {
        _countCloudGroupEndedAnimation++;

        if (_countCloudGroupEndedAnimation == _cloudGroup.Length)
        {
            _isActiveAnimation = false;
            _isCalmWeather = !_isCalmWeather;
        }
    }
}
