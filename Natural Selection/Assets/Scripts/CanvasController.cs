using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour
{
    private Button _startButton;
    private Button _settingsButton;

    private GameController _gameController;

    private GameObject _settings;

    private TMP_InputField _entitiesStartCount, _foodCount, _startSpeed, _startSize, _startMaxHP, _startDamage, _dayTime;

    private bool _settingsButtonEnabled = false;

    private void Awake()
    {
        _startButton = transform.Find("StartButton").GetComponent<Button>();
        _settingsButton = transform.Find("SettingsButton").GetComponent<Button>();

        _gameController = FindAnyObjectByType<GameController>();

        _settings = transform.Find("Settings").gameObject;
        _settings.SetActive(false);

        _entitiesStartCount = _settings.transform.Find("EntitiesStartCount").GetComponent<TMP_InputField>();
        _foodCount = _settings.transform.Find("FoodCount").GetComponent<TMP_InputField>();
        _startSpeed = _settings.transform.Find("StartSpeed").GetComponent<TMP_InputField>();
        _startSize = _settings.transform.Find("StartSize").GetComponent<TMP_InputField>();
        _startMaxHP = _settings.transform.Find("StartMaxHP").GetComponent<TMP_InputField>();
        _startDamage = _settings.transform.Find("StartDamage").GetComponent<TMP_InputField>();
        _dayTime = _settings.transform.Find("DayTime").GetComponent<TMP_InputField>();
    }

    public void StartButton()
    {
        _startButton.gameObject.SetActive(false);
    }

    public void SettingsButton()
    {
        _settingsButtonEnabled = !_settingsButtonEnabled;
        _settings.SetActive(!_settings.activeSelf);
    }



    public void ChangeEntitiesStartCount()
    {
        _gameController.EntitiesStartCount = float.Parse(_entitiesStartCount.text);
    }
    public void ChangeFoodCount()
    {
        _gameController.FoodCount = float.Parse(_foodCount.text);
    }

    public void ChangeStartSpeed()
    {
        _gameController.StartSpeed = float.Parse(_startSpeed.text);
    }

    public void ChangeStartSize()
    {
        _gameController.StartSize = float.Parse(_startSize.text);
    }

    public void ChangeStartMaxHP()
    {
        _gameController.StartMaxHP = float.Parse(_startMaxHP.text);
    }

    public void ChangeStartDamage()
    {
        _gameController.StartDamage = float.Parse(_startDamage.text);
    }

    public void ChangeDayTime()
    {
        _gameController.DayTime = float.Parse(_dayTime.text);
    }
}
