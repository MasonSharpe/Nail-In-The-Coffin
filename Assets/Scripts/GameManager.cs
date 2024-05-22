using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public List<Upgrade> upgrades;
    public float money;
    public int currentLevel;
    public List<string> levelNames;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public float CalculateUpgradeCost(Upgrade upgrade)
    {
        return upgrade.initialCost + (upgrade.additiveCost * (upgrade.currentPurchases - 1)) * (upgrade.mulplicativeCost * (upgrade.currentPurchases - 1));
    }
}


public class Upgrade
{
    public UpgradeName name;
    public int maxPurchases;
    public int currentPurchases;

    public int initialCost;
    public float additiveCost;
    public float mulplicativeCost;
}
public enum UpgradeName
{
    Healing,
    MaxHealth,
    EnergyRegen,
    MaxEnergy,
    HammerSpeed,
    MovementSpeed,
    HammerLength,
    ShadowRecovery

}
