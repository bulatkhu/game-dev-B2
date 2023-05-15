using UnityEngine;
using UnityEngine.UI;

public class FuelController : MonoBehaviour
{
    public static FuelController instance;

    [SerializeField] private Image _fuelImage;
    [SerializeField, Range(10f, 90f)] private float _fuelDrainSpeed = 80f;
    [SerializeField] private float _maxFuelAmount = 100f;

    private float _currentFuelAmount;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _currentFuelAmount = _maxFuelAmount;
        UpdateUI();
    }

    private void Update()
    {
        if (NewPlayer.instance.isDisabled)
        {
            return;
        }

        if (NewPlayer.instance.isFuelDraining)
        {
            _currentFuelAmount -= Time.deltaTime * _fuelDrainSpeed;
            UpdateUI();
        }

        if (_currentFuelAmount <= 0f)
        {
            NewPlayer.instance.GameOver();
        }
    }

    public void FillFuel()
    {
        var newFuelAmount = _currentFuelAmount + 20;
        _currentFuelAmount = newFuelAmount > _maxFuelAmount ? _maxFuelAmount : newFuelAmount;
    }

    private void UpdateUI()
    {
        _fuelImage.fillAmount = (_currentFuelAmount / _maxFuelAmount);
    }
}