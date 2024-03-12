using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class WeatherIcon : MonoBehaviour
{
    public string weatherType; // Assign in the inspector to match this icon's weather

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        FindObjectOfType<WeatherController>().CheckWeatherEventMatch(weatherType);
    }
}