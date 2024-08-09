using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Image sliderFill; 
    [SerializeField] private TextMeshProUGUI text;

    private float _currentValue;
    private float _valueToLVLUp;
    
    private void Awake()
    {
        slider.value = 0.01f;
        sliderFill.color = Color.yellow;
        text.color = Color.yellow;
        text.text = "Бедный";
    }

    public void Update(float valueToLvl, Color color, string name)
    {
        _currentValue = 0.01f;
        _valueToLVLUp = valueToLvl;
        UpdateSlider(color);
        UpdateText(name, color);
    }

    private void UpdateSlider(Color color)
    {
        slider.value = _valueToLVLUp / _currentValue;
        sliderFill.color = color;
    }

    private void UpdateText(string name, Color color)
    {
        text.color = color;
        text.text = name;
    }

}
