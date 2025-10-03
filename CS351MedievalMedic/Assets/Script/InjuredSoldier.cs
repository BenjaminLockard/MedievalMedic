
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InjuredSoldier
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

    public enum TreatmentType
    {
        Cast,
        Rest,
        Surgery,
        BurnOintment,
        Therapy,
        Unknown
    }

    public InjuryCondition Condition { get; private set; }
    public bool IsCured { get; private set; }
    public bool IsDead { get; private set; }
    public int DaysInjured { get; private set; }

    public InjuredSoldier(InjuryCondition condition)
    {
        Condition = condition;
        IsCured = false;
        IsDead = false;
        DaysInjured = 0;
    }

    // Call once per in-game day
    public void AdvanceDay()
    {
        if (IsCured || IsDead) return;

        DaysInjured++;

        if (DaysInjured >= 2)
        {
            Die();
        }
    }

    // Called automatically if the soldier hits 2 days untreated
    private void Die()
    {
        IsDead = true;
        Debug.Log($"Soldier with {Condition} has died after {DaysInjured} days without treatment.");
    }

    // Attempt to cure the soldier
    public bool ApplyTreatment(TreatmentType treatment)
    {
        if (IsCured || IsDead) return false;

        if (IsCorrectTreatment(treatment))
        {
            IsCured = true;
            Debug.Log($"Treatment successful for {Condition}!");
            return true;
        }

        Debug.Log($"Incorrect treatment for {Condition}. Soldier remains injured.");
        return false;
    }

    private bool IsCorrectTreatment(TreatmentType treatment)
    {
        switch (Condition)
        {
            case InjuryCondition.BrokenLeg: return treatment == TreatmentType.Cast;
            case InjuryCondition.Concussion: return treatment == TreatmentType.Rest;
            case InjuryCondition.GunshotWound: return treatment == TreatmentType.Surgery;
            case InjuryCondition.Burn: return treatment == TreatmentType.BurnOintment;
            case InjuryCondition.PTSD: return treatment == TreatmentType.Therapy;
            default: return false;
        }
    }

    public string GetStatus()
    {
        string status = $"Condition: {Condition}, Cured: {IsCured}, Dead: {IsDead}, Days Injured: {DaysInjured}";
        return status;
    }
}