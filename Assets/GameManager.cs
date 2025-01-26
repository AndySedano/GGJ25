using System;
using System.Collections.Generic;
using MoreMountains.Tools;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public enum CleaningTool { A, B, C, D }

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    // Percentage 0 to 1 
    public Dictionary<CleaningTool, float> toolEnergy = new Dictionary<CleaningTool, float>();
    public Dictionary<CleaningTool, float> cleanlinessByTool = new Dictionary<CleaningTool, float>();
    public List<Color> Colors = new List<Color>();
    
    private float timeSinceLastClean = 0f;
    [SerializeField]
    public float TimeBetweenActions = 1f; // 

    public MMProgressBar CleanlinessBar;
    public CleaningTool? activeTool;
    public float EnergyUsePerAction = 0.1f; //percernt
    public float CleanlinessIncreasePerAction = 0.1f; // percent

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
    }

    void FillAllEnergy()
    {
        toolEnergy[CleaningTool.A] = 1f;
        toolEnergy[CleaningTool.B] = 1f;
        toolEnergy[CleaningTool.C] = 1f;
        toolEnergy[CleaningTool.D] = 1f;
    }

    private void AddCleanliness(CleaningTool tool, float value)
    {
        cleanlinessByTool[tool] = Math.Min(cleanlinessByTool[tool] + value, 1f);
        CleanlinessBar.UpdateBar01(TotalCleanliness());
    }

    private void FillToolEnergy(CleaningTool tool, float value)
    {
        toolEnergy[tool] = Math.Max(toolEnergy[tool] + value, 1f);
    }

    private void UseToolEnergy(CleaningTool tool, float value)
    {
        toolEnergy[tool] = Math.Max(toolEnergy[tool] - value, 0f);
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
        Debug.Log(sum/4);
        return sum / 4;
    }

    public void Cleaning()
    {
        if (activeTool.HasValue && timeSinceLastClean >= TimeBetweenActions)
        {
            Debug.Log("Clean Invoked");
            if (ToolHasEnergy(activeTool.Value) && cleanlinessByTool[activeTool.Value] < 1)
            {
                UseToolEnergy(activeTool.Value, EnergyUsePerAction);
                AddCleanliness(activeTool.Value, CleanlinessIncreasePerAction);
            }
            timeSinceLastClean = 0;
        }
    }
}
