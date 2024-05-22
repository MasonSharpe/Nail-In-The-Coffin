using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class UpgradeScreen : MonoBehaviour
{
    public List<TextMeshProUGUI> names;
    public List<TextMeshProUGUI> nameShadows;
    public List<TextMeshProUGUI> costs;
    public List<Slider> upgradeBars;
    public List<Button> upgradeButtons;

    public void Initiate()
    {
        List<Upgrade> upgrades = GameManager.instance.upgrades;

        for (int i = 0; i < upgrades.Count; i++)
        {
            UpdateUpgradeVisual(upgrades[i]);


        }
    }
    public void UpdateUpgradeVisual(Upgrade upgrade)
    {
        int i = (int)upgrade.name;

        names[i].text = upgrade.name.ToString();
        nameShadows[i].text = upgrade.name.ToString();
        if (upgrade.currentPurchases == upgrade.maxPurchases){
            costs[i].text = "MAX";
        }
        else{
            costs[i].text = "$" + GameManager.instance.CalculateUpgradeCost(upgrade).ToString();
        }
        upgradeBars[i].value = upgrade.currentPurchases;
        upgradeBars[i].maxValue = upgrade.maxPurchases;
    }

    public void BuyUpgrade(UpgradeName name)
    {
        Upgrade upgrade = GameManager.instance.upgrades.Find(element => element.name == name);
        float cost = GameManager.instance.CalculateUpgradeCost(upgrade);

        if (upgrade.currentPurchases == upgrade.maxPurchases || GameManager.instance.money < cost) return;

        upgrade.currentPurchases++;
        GameManager.instance.money -= cost;

        UpdateUpgradeVisual(upgrade);

    }
    public void NextScene()
    {

    }
}
