using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderFill; 
    [SerializeField] private TextMeshProUGUI text;

    private float _currentValue;
    private float _valueToLvlUp;

    public Action OnNeedLvlUp;
    public Action<float> OnNeedLvlDown;

    public void Initialized(LevelUpInfo info, float currentValue = 0.01f)
    {
        _currentValue = currentValue;
        _valueToLvlUp = info.ValueToNextLvl;
        UpdateSlider(info.Color);
        UpdateText(info.TextName, info.Color);
    }

    public void AddValue(float value)
    {
        _currentValue += value;
        if (_currentValue <= 0)
        {
            OnNeedLvlDown?.Invoke(_currentValue);
        }
        if (_currentValue >= _valueToLvlUp)
        {
            OnNeedLvlUp?.Invoke();
        }
        UpdateSlider();
    }

    private void UpdateSlider(Color color)
    {
        UpdateSlider();
        sliderFill.color = color;
    }

    private void UpdateSlider()
    {
        slider.value =  _currentValue/ _valueToLvlUp;
    }

    private void UpdateText(string name, Color color)
    {
        text.color = color;
        text.text = name;
    }

}
