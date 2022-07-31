using System;
using UnityEngine;
using UnityEngine.UI;

public class Weather : MonoBehaviour
{
    [SerializeField] private Button _thunderButton;
    [SerializeField] private Button _calmButton;
    [SerializeField] private Button _specialButton;

    public event Action StartCloudGathering;
    public event Action StartThunderWeather;
    public event Action StartCalmWeather;

    private CloudGroup[] _cloudGroup;
    private int _countCloudGroupEndedAnimation;

    [SerializeField]private bool _isActiveAnimation = false;
    [SerializeField]private bool _isCalmWeather = true;

    private void OnEnable()
    {
        _cloudGroup = GetComponentsInChildren<CloudGroup>();

        foreach (CloudGroup clouds in _cloudGroup)
        {
            clouds.EndAnimation += CheckStateCLoudsAnimation;
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

    public void StartingCloudsGathering()
    {
        if (!_isActiveAnimation && _isCalmWeather)
        {
            _countCloudGroupEndedAnimation = 0;
            _isActiveAnimation = true;
            StartCloudGathering?.Invoke();
        }        
    }

    public void StartingCalm()
    {
        if (!_isActiveAnimation&&!_isCalmWeather)
        {
            _countCloudGroupEndedAnimation = 0;
            _isActiveAnimation = true;
            StartCalmWeather?.Invoke();
        }        
    }

    public void StartingThunder()
    {
        if (!_isActiveAnimation && !_isCalmWeather)
        {
            StartThunderWeather?.Invoke();
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
