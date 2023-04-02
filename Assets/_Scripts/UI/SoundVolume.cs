using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SoundVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider slider;
    [SerializeField] private string volumeParameter;
    [SerializeField] private AudioClip sliderChangeSfx;
    [SerializeField] private TextMeshProUGUI textValue;

    private void Awake()
    {
        slider.onValueChanged.AddListener(OnSliderChanged);
    }
    private void OnEnable()
    {
        PlayerInputHelper.OnCancelUI += PlayerInputHelper_OnCancelUI;
    }
    private void OnDisable()
    {
        PlayerInputHelper.OnCancelUI -= PlayerInputHelper_OnCancelUI;
    }

    private void OnDestroy()
    {
        slider.onValueChanged.RemoveListener(OnSliderChanged);
    }
    private void PlayerInputHelper_OnCancelUI()
    {
        gameObject.SetActive(false);
    }
    public void OnSliderChanged(float value)
    {
        ChangeVolume(value);
        textValue.SetText($"{value.ToString()} %");
        AudioUtility.CreateSFX(sliderChangeSfx, transform.position, AudioUtility.AudioGroups.UI, 0f);
    }

    public void ChangeVolume(float value)
    {
        audioMixer.SetFloat(volumeParameter, value - 80f);
    }

}
