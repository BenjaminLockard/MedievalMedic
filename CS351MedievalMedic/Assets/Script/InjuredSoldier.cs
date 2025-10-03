
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredSoldier : MonoBehaviour
{
    public enum InjuryCondition
    {
        BrokenLeg,
        Concussion,
        GunshotWound,
        Burn,
        PTSD,
        Other
    }

    // Properties
    public InjuryCondition Condition { get; private set; }
    public bool IsCured { get; private set; }
    public int DaysInjured { get; private set; }

    // Constructor
    public InjuredSoldier(InjuryCondition condition)
    {
        Condition = condition;
        IsCured = false;
        DaysInjured = 0;
    }

    // Call this method daily to increment the injury duration
    public void AdvanceDay()
    {
        if (!IsCured)
            DaysInjured++;
    }

    // Cure the soldier
    public void Cure()
    {
        IsCured = true;
    }

    // Display status (for debugging or UI)
    public string GetStatus()
    {
        string status = $"Condition: {Condition}, ";
        status += $"Cured: {IsCured}, ";
        status += $"Days Injured: {DaysInjured}";
        return status;
    }
}

