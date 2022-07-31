using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]

public class WeatherButton : MonoBehaviour
{
    [SerializeField] bool _isItCalmButton;

    private Button _button;
    private Image _image;
    private ButtonVisibilityController _buttonController;

    private float _timeToChangingVisibility=1f;
    private float _minAlphaChannel=0f;
    private float _maxAlphaChannel=1f;

    private void OnEnable()
    {
        _button = GetComponent<Button>();
        _image = GetComponent<Image>();
        _buttonController = GetComponentInParent<ButtonVisibilityController>();

        if (!_isItCalmButton)
        {
            GoVisible(_minAlphaChannel);
            _button.interactable = false;
        }

        _buttonController.SettedCalmWeatherUI += SetCalmWeatherState;
        _buttonController.SettedFoulWeatherUI += SetFoulWeatherState;
    }

    private void OnDisable()
    {
        _buttonController.SettedCalmWeatherUI -= SetCalmWeatherState;
        _buttonController.SettedFoulWeatherUI -= SetFoulWeatherState;
    }

    private void SetCalmWeatherState()
    {
        if (_isItCalmButton)
        {
            GoVisible(_maxAlphaChannel);
            _button.interactable = true;
        }
        else
        {
            GoVisible(_minAlphaChannel);
            _button.interactable = false;
        }
    }

    private void SetFoulWeatherState()
    {
        if (!_isItCalmButton)
        {
            GoVisible(_maxAlphaChannel);
            _button.interactable = true;
        }
        else
        {
            GoVisible(_minAlphaChannel);
            _button.interactable = false;
        }
    }

    private void GoVisible(float alphaChannelValue)
    {
        _image.DOFade(alphaChannelValue, _timeToChangingVisibility);
    }
}
