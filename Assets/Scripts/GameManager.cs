using System;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;

public enum CleaningTool { BRUSH, HOSE, SCRAPER, SPONGE }

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // Percentage 0 to 1 
    private Dictionary<CleaningTool, float> toolEnergy = new Dictionary<CleaningTool, float>();
    private Dictionary<CleaningTool, float> cleanlinessByTool = new Dictionary<CleaningTool, float>();
    public List<Color> toolColors = new List<Color> { Color.red, Color.blue, Color.green, Color.yellow };

    public float timeSinceLastClean = 0f;
    private float timeSinceLastChange = 0f;
    private CleaningTool requiredTool = CleaningTool.BRUSH;

    public UnityEvent<CleaningTool> OnRequiredToolChanged;
    public UnityEvent<CleaningTool, float> OnCleanlinessUpdated;
    public UnityEvent<CleaningTool, float> OnToolEnergyUpdated;

    public MMProgressBar CleanlinessBar;
    public CleaningTool? activeTool;
    public float EnergyUsePerAction = 0.1f; //percent
    public float CleanlinessIncreasePerAction = 0.1f; // percent
    public float EneryGainPerBubble = 0.1f; // percent
    public float TimeBetweenActions = 1f; // seconds
    public float TimeBetweenChangeTools = 8f;
    public bool IsMouseOverCryptid = false;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        ScoreManager.Instance.StartGame();
        FillAllEnergy();
        InitializeCleanliness();
    }

    void InitializeCleanliness()
    {
        cleanlinessByTool[CleaningTool.BRUSH] = 0f;
        cleanlinessByTool[CleaningTool.HOSE] = 0f;
        cleanlinessByTool[CleaningTool.SCRAPER] = 0f;
        cleanlinessByTool[CleaningTool.SPONGE] = 0f;
    }

    void Update()
    {
        // timeSinceLastClean = Mathf.Min(timeSinceLastClean + Time.deltaTime, TimeBetweenActions);
        timeSinceLastChange = Mathf.Min(timeSinceLastChange + Time.deltaTime, TimeBetweenChangeTools);

        if (timeSinceLastChange >= TimeBetweenChangeTools)
        {
            ChangeRequiredTool();
            timeSinceLastChange = 0;
        }

    }

    public CleaningTool RandomCleanTool()
    {
        return (CleaningTool)System.Enum.GetValues(typeof(CleaningTool)).GetValue(new System.Random().Next(0, 4));
    }

    void ChangeRequiredTool()
    {
        if (TotalCleanliness() < 1)
        {
            CleaningTool randomTool = RandomCleanTool();
            while (cleanlinessByTool[randomTool] >= 1)
            {
                randomTool = RandomCleanTool();
            }
            requiredTool = randomTool;
            OnRequiredToolChanged.Invoke(randomTool);
        }
    }

    void FillAllEnergy()
    {
        toolEnergy[CleaningTool.BRUSH] = 1f;
        toolEnergy[CleaningTool.HOSE] = 1f;;
        toolEnergy[CleaningTool.SCRAPER] = 1f;;
        toolEnergy[CleaningTool.SPONGE] = 1f;;
        OnToolEnergyUpdated.Invoke(CleaningTool.BRUSH, 1f);
        OnToolEnergyUpdated.Invoke(CleaningTool.HOSE, 1f);
        OnToolEnergyUpdated.Invoke(CleaningTool.SCRAPER, 1f);
        OnToolEnergyUpdated.Invoke(CleaningTool.SPONGE, 1f);
    }

    private void AddCleanliness(CleaningTool tool)
    {
        // If celanliness is already 1, try another cleanliness index
        if (cleanlinessByTool[tool] >= 1)
        {
            foreach (var toolKey in cleanlinessByTool.Keys)
            {
                if (cleanlinessByTool[toolKey]  < 1)
                {
                    cleanlinessByTool[toolKey] = Math.Min(cleanlinessByTool[toolKey] + CleanlinessIncreasePerAction, 1f);
                    OnCleanlinessUpdated.Invoke(toolKey, cleanlinessByTool[toolKey]);
                    break;
                }
            }
        }
        else
        {
            cleanlinessByTool[tool] = Math.Min(cleanlinessByTool[tool] + CleanlinessIncreasePerAction, 1f);
            OnCleanlinessUpdated.Invoke(tool, cleanlinessByTool[tool]);
        }

        CleanlinessBar.UpdateBar01(TotalCleanliness());
    }

    public void AddToolEnergy(CleaningTool tool)
    {
        toolEnergy[tool] = Math.Min(toolEnergy[tool] + EneryGainPerBubble, 1f);
        OnToolEnergyUpdated.Invoke(tool, toolEnergy[tool]);
    }

    public void FillEnergyByColor(Color color)
    {
        for (int x = 0; x < toolColors.Count; x++)
        {
            if (toolColors[x] == color)
            {
                AddToolEnergy((CleaningTool)x);
            }
        }
    }

    private void UseToolEnergy(CleaningTool tool)
    {
        toolEnergy[tool] = Math.Max(toolEnergy[tool] - EnergyUsePerAction, 0f);
        OnToolEnergyUpdated.Invoke(tool, toolEnergy[tool]);
    }

    public bool ToolHasEnergy(CleaningTool tool)
    {
        return toolEnergy[tool] > 0;
    }

    private float TotalCleanliness()
    {
        float sum = 0f;
        foreach (var pair in cleanlinessByTool)
        {
            sum += pair.Value;
        }
        return sum / 4;
    }

    public void Cleaning()
    {
        if (activeTool.HasValue && activeTool == requiredTool && timeSinceLastClean >= TimeBetweenActions)
        {
            if (ToolHasEnergy(activeTool.Value))
            {
                UseToolEnergy(activeTool.Value);
                AddCleanliness(activeTool.Value);
            }
            timeSinceLastClean = 0;
        }
    }
}
