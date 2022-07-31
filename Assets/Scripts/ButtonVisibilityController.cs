using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonVisibilityController : MonoBehaviour
{
    [SerializeField] Button[] ActiveUIInCalmWeather;
    [SerializeField] Button[] ActiveUIInFoulWeather;
    [SerializeField] private Weather _weather;

    public event Action SettingCalmWeatherUI;
    public event Action SettingFoulWeatherUI;

    private void OnEnable()
    {
        _weather.StartCalmWeather +=SetCalmWeatherUI;
        _weather.StartCloudGathering +=SetFoulWeaherUI;
    }

    private void SetCalmWeatherUI()
    {
        SettingCalmWeatherUI?.Invoke();
    }

    private void SetFoulWeaherUI()
    {
        SettingFoulWeatherUI?.Invoke();
    }
}
