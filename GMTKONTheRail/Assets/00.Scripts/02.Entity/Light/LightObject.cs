using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightObject : MonoBehaviour, ICoinUseable
{
    [SerializeField] private LightDataSO _lightData;
    [SerializeField] private float _fuel = 3.9f;
    [SerializeField] private float _maxFuel = 5f;
    [SerializeField] private float _fuelUseSpeed = 0.05f;

    [Header("Light settings")]
    [SerializeField] private Light _lightComponent = null;
    [SerializeField] private MeshRenderer _meshRenderer = null;
    private float _randomValue = 0;
    private float _lightDefaultIntensity = 0;
    private Color _DefaultEmissionColor = Color.white;
    private readonly int _emissionColorHash = Shader.PropertyToID("_EmisiionColor");
    [SerializeField, Range(0, 1)] private float _lightActivePercentage = 1;
    [SerializeField, Range(0f, 0.5f)] private float _transitionSmoothness = 0.1f;
    [SerializeField, Range(0f, 10f)] private float _frequency = 2f;

    private bool _hasUpdate = true;

    private bool _isLightOn = false;
    public bool IsLightOn => _isLightOn;

    private void Awake()
    {
        _lightDefaultIntensity = _lightComponent.intensity;
        _DefaultEmissionColor = _meshRenderer.material.GetColor(_emissionColorHash);
        _randomValue = Random.Range(0, 777.7f);
        UpdateLightData(_fuel);
    }

    public void AddFuel(float value)
    {
        _fuel = Mathf.Clamp(_fuel + value, 0, _maxFuel);
    }

    public void SetActive(bool isactiveself)
    {
        _hasUpdate = isactiveself;

        if (!isactiveself)
        {
            _meshRenderer.material.SetColor(_emissionColorHash, Color.gray);
            _lightComponent.intensity = 0;
        }
    }

    private void Update()
    {
        if (!_hasUpdate) return;

        float lightness = Mathf.PerlinNoise1D((Time.time + _randomValue) * _frequency) + _lightActivePercentage;
        _isLightOn = lightness > 0.5f;

        float goalIntensity = _isLightOn ? _lightDefaultIntensity * lightness : 0;
        Color goalColor = _isLightOn ? _DefaultEmissionColor * lightness : Color.gray;
        _lightComponent.intensity = Mathf.Lerp(_lightComponent.intensity, goalIntensity, 1 - _transitionSmoothness);

        _meshRenderer.material.SetColor(_emissionColorHash,
            Color.Lerp(_meshRenderer.material.GetColor(_emissionColorHash),
            goalColor,
            1 - _transitionSmoothness));


        if (_fuel <= 0) return;
        float newFuelValue = _fuel - _fuelUseSpeed * Time.deltaTime;
        if (Mathf.FloorToInt(newFuelValue) != Mathf.FloorToInt(_fuel))
        {
            UpdateLightData(newFuelValue);
        }

        _fuel = newFuelValue;

    }

    private void UpdateLightData(float newFuelValue)
    {
        int idx = Mathf.Clamp(Mathf.FloorToInt(newFuelValue), 0, _lightData.DataCount - 1);

        LightDataSO.LightDataStruct data = _lightData.dataArr[idx];
        
        _lightActivePercentage = data.lightActivePercentage;
        _transitionSmoothness = data.transitionSmoothness;
        _frequency = data.frequency;
    }

    public void UseCoin()
    {
        AddFuel(1.5f);
    }
}
