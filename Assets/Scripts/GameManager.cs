using System;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Events;

public enum CleaningTool { A, B, C, D }

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // Percentage 0 to 1 
    private Dictionary<CleaningTool, float> toolEnergy = new Dictionary<CleaningTool, float>();
    private Dictionary<CleaningTool, float> cleanlinessByTool = new Dictionary<CleaningTool, float>();

    private float timeSinceLastClean = 0f;
    private float timeSinceLastChange = 0f;
    private CleaningTool requiredTool = CleaningTool.A;

    public UnityEvent<CleaningTool> OnRequiredToolChanged;
    public UnityEvent<CleaningTool, float> OnCleanlinessUpdated;
    public UnityEvent<CleaningTool, float> OnToolEnergyUpdated;

    public MMProgressBar CleanlinessBar;
    public CleaningTool? activeTool;
    public float EnergyUsePerAction = 0.1f; //percernt
    public float CleanlinessIncreasePerAction = 0.1f; // percent
    public float EneryGainPerBubble = 0.1f; // percent
    public float TimeBetweenActions = 1f; // seconds
    public float TimeBetweenChangeTools = 8f;


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
        FillAllEnergy();
        InitializeCleanliness();
    }

    void InitializeCleanliness()
    {
        cleanlinessByTool[CleaningTool.A] = 0f;
        cleanlinessByTool[CleaningTool.B] = 0f;
        cleanlinessByTool[CleaningTool.C] = 0f;
        cleanlinessByTool[CleaningTool.D] = 0f;
    }

    void Update()
    {
        timeSinceLastClean = Mathf.Min(timeSinceLastClean + Time.deltaTime, TimeBetweenActions);
        timeSinceLastChange = Mathf.Min(timeSinceLastChange + Time.deltaTime, TimeBetweenChangeTools);

        if (timeSinceLastChange >= TimeBetweenChangeTools)
        {
            ChangeRequiredTool();
            timeSinceLastChange = 0;
        }

    }

    private CleaningTool RandomCleanTool()
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
        toolEnergy[CleaningTool.A] = 0.5f;
        toolEnergy[CleaningTool.B] = 0.5f;
        toolEnergy[CleaningTool.C] = 0.5f;
        toolEnergy[CleaningTool.D] = 0.5f;
        OnToolEnergyUpdated.Invoke(CleaningTool.A, 0.5f);
        OnToolEnergyUpdated.Invoke(CleaningTool.B, 0.5f);
        OnToolEnergyUpdated.Invoke(CleaningTool.C, 0.5f);
        OnToolEnergyUpdated.Invoke(CleaningTool.D, 0.5f);
    }

    private void AddCleanliness(CleaningTool tool)
    {
        cleanlinessByTool[tool] = Math.Min(cleanlinessByTool[tool] + CleanlinessIncreasePerAction, 1f);
        CleanlinessBar.UpdateBar01(TotalCleanliness());
        OnCleanlinessUpdated.Invoke(tool, cleanlinessByTool[tool]);
    }

    public void FillToolEnergy(CleaningTool tool)
    {
        toolEnergy[tool] = Math.Max(toolEnergy[tool] + EneryGainPerBubble, 1f);
        OnToolEnergyUpdated.Invoke(tool, toolEnergy[tool]);
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
            if (ToolHasEnergy(activeTool.Value) && cleanlinessByTool[activeTool.Value] < 1)
            {
                UseToolEnergy(activeTool.Value);
                AddCleanliness(activeTool.Value);
            }
            timeSinceLastClean = 0;
        }
    }
}
