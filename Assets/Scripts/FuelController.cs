using UnityEngine;
using UnityEngine.UI;

public class FuelController : MonoBehaviour
{
    public static FuelController instance;

    [SerializeField] private Image _fuelImage;
    [SerializeField] private Text _fuelText;
    [SerializeField] private GameObject _emptyTextBackground;
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
        if (NewPlayer.instance.isFuelDraining && !NewPlayer.instance.isCollided)
        {
            _currentFuelAmount -= Time.deltaTime * _fuelDrainSpeed;
            UpdateUI();
        }

        if (_currentFuelAmount <= 0f)
        {
            NewPlayer.instance.FuelRanOut();
            ShowEmptyText();
        }
    }

    public bool HasFuel()
    {
        return _currentFuelAmount > 0;
    }

    public void FillFuel()
    {
        if (NewPlayer.instance.isCollided)
        {
            return;
        }

        var newFuelAmount = _currentFuelAmount + 50;
        _currentFuelAmount = newFuelAmount > _maxFuelAmount ? _maxFuelAmount : newFuelAmount;
        HideEmptyText();
    }

    private void UpdateUI()
    {
        _fuelImage.fillAmount = (_currentFuelAmount / _maxFuelAmount);
        _fuelText.text = (_currentFuelAmount < 0 ? "0" : _currentFuelAmount.ToString("F0")) + "%";
    }

    private void ShowEmptyText()
    {
        _fuelText.text = "Empty";
        _fuelText.color = Color.white;
        _emptyTextBackground.SetActive(true);
    }

    private void HideEmptyText()
    {
        _emptyTextBackground.SetActive(false);
        UpdateUI();
    }
}
