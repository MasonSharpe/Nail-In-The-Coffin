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
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI cleanText;
    public TextMeshProUGUI earningsText;
    public Image starTime;
    public Image starClean;


    public void Initiate()
    {
        Time.timeScale = 0;
        timeText.text = LevelManager.instance.timeScore + " sec";
        cleanText.text = LevelManager.instance.cleanlinessScore + "%";

        float earnings = LevelManager.instance.CalculateEarnings();
        earningsText.text = "$" + earnings;
        GameManager.instance.money += earnings;
        moneyText.text = "$" + GameManager.instance.money;

        starTime.color = LevelManager.instance.timeScore < LevelManager.instance.timeScoreChallenge ?
            new Color(1, 0.94f, 0.43f, 0.57f) : new Color(0.57f, 0.57f, 0.57f, 0.57f);
        starClean.color = LevelManager.instance.cleanlinessScore > LevelManager.instance.cleanlinessScoreChallenge ?
            new Color(1, 0.94f, 0.43f, 0.57f) : new Color(0.57f, 0.57f, 0.57f, 0.57f);


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
        if (upgrade.currentPurchases == upgrade.maxPurchases + 1){
            costs[i].text = "MAX";
        }
        else{
            costs[i].text = "$" + GameManager.instance.CalculateUpgradeCost(upgrade).ToString();
        }
        upgradeBars[i].value = upgrade.currentPurchases;
        upgradeBars[i].maxValue = upgrade.maxPurchases + 1;

    }

    public void BuyUpgrade(int name)
    {
        Upgrade upgrade = GameManager.instance.upgrades.Find(element => element.name == (UpgradeName)name);
        float cost = GameManager.instance.CalculateUpgradeCost(upgrade);

        if (upgrade.currentPurchases == upgrade.maxPurchases || GameManager.instance.money < cost) return;

        upgrade.currentPurchases++;
        GameManager.instance.money -= cost;

        UpdateUpgradeVisual(upgrade);
        moneyText.text = "$" + GameManager.instance.money;
    }
    public void NextScene()
    {
        SceneManager.LoadScene(GameManager.instance.levelNames[LevelManager.instance.level]);
    }
}
