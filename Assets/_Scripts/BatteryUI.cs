using UnityEngine;
using TMPro;

public class BatteryUI : MonoBehaviour
{
    [Header("UI Reference")]
    [SerializeField] private TMP_Text statsText;

    [Header("Battery Settings")]
    [SerializeField] private float maxCapacity = 5000f; // kWh total
    [SerializeField] private float storedEnergy = 2500f; // current stored energy

    [Header("Solar Settings")]
    [SerializeField] private float minSolarInput = 100f;   // kW
    [SerializeField] private float maxSolarInput = 800f;   // kW

    [Header("Consumption Settings")]
    [SerializeField] private float minConsumption = 200f;  // kW
    [SerializeField] private float maxConsumption = 700f;  // kW

    [Header("Simulation Timing")]
    [SerializeField] private float changeInterval = 10f; // how often values shift (seconds)
    [SerializeField] private float smoothSpeed = 0.1f;   // how smooth/slow the drift is

    private float currentSolar;
    private float targetSolar;

    private float currentConsumption;
    private float targetConsumption;

    private float timer;

    void Start()
    {
        // Initialize values
        currentSolar = Random.Range(minSolarInput, maxSolarInput);
        targetSolar = currentSolar;

        currentConsumption = Random.Range(minConsumption, maxConsumption);
        targetConsumption = currentConsumption;
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Every X seconds, pick new targets
        if (timer >= changeInterval)
        {
            targetSolar = Random.Range(minSolarInput, maxSolarInput);
            targetConsumption = Random.Range(minConsumption, maxConsumption);
            timer = 0f;
        }

        // Smoothly drift toward targets
        currentSolar = Mathf.Lerp(currentSolar, targetSolar, Time.deltaTime * smoothSpeed);
        currentConsumption = Mathf.Lerp(currentConsumption, targetConsumption, Time.deltaTime * smoothSpeed);

        // Energy balance
        float netFlow = (currentSolar - currentConsumption) * (Time.deltaTime / 3600f); 
        // divide by 3600 because kW * hours, we’re simulating in seconds

        storedEnergy += netFlow;
        storedEnergy = Mathf.Clamp(storedEnergy, 0f, maxCapacity);

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (statsText == null) return;

        statsText.text =
            $"<b>🌞 Solar Input:</b> {currentSolar:F1} kW\n" +
            $"<b>⚡ Consumption:</b> {currentConsumption:F1} kW\n" +
            $"<b>🔋 Stored Energy:</b> {storedEnergy:F1} / {maxCapacity} kWh\n" +
            $"<b>Charge %:</b> {(storedEnergy / maxCapacity * 100f):F1}%";
    }
}
