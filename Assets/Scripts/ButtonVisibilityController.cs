using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonVisibilityController : MonoBehaviour
{
    [SerializeField] private Button[] ActiveUIInCalmWeather;
    [SerializeField] private Button[] ActiveUIInFoulWeather;
    [SerializeField] private Weather _weather;

    public event Action SettedCalmWeatherUI;
    public event Action SettedFoulWeatherUI;

    private void OnEnable()
    {
        _weather.StartedCalmWeather +=SetCalmWeatherUI;
        _weather.StartedCloudGathering +=SetFoulWeaherUI;
    }

    private void SetCalmWeatherUI()
    {
        SettedCalmWeatherUI?.Invoke();
    }

    private void SetFoulWeaherUI()
    {
        SettedFoulWeatherUI?.Invoke();
    }
}
