using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Thingmanagment : MonoBehaviour
{
    public TextMeshProUGUI totalThings;
    public TextMeshProUGUI collecters;
    public Button gatherButton;
    public Button upgradeButton;
    public Button collectorButton;
    public Button collectorUpgradeButton;
    public Button prestigeButton;
    public TextMeshProUGUI prestigeText;
    public TextMeshProUGUI multiplierText;
    public GameObject prestigePanel;
    [SerializeField] private double totalThingsCount;
    [SerializeField] private double collectersCount;
    [SerializeField] private double collectersProduction;
    [SerializeField] private double gatheringPerButtonClick;
    [SerializeField] private double upgradePrice;
    [SerializeField] private double collectorPrice;
    [SerializeField] private double collectorUpgradePrice;
    [SerializeField] private double collectorProductivity;
    [SerializeField] private double currentMultiplier;
    [SerializeField] private double prestigeMultiplier;
    [SerializeField] private bool prestigePanelHasOpened;
    // Start is called before the first frame update
    void Start()
    {
        prestigePanelHasOpened = false;
        currentMultiplier = 1.00;
        prestigeMultiplier = 1.00;
        resetValues();
        updateAll();
        StartCoroutine(UpdateValueCoroutine());
    }

    void resetValues()
    {
        totalThingsCount = 0;
        collectersCount = 0;
        collectersProduction = 0;
        collectorProductivity = 0.5 * currentMultiplier;
        gatheringPerButtonClick = 1 * currentMultiplier;
        upgradePrice = 100;
        collectorUpgradePrice = 500;
        collectorPrice = 10;
    }

    private void updateAll()
    {
        updateTotalThings();
        updateCollecters();
        updateGatherButton();
        updateUpgradeButton();
        updateCollecterButton();
        updateCollecterUpgradeButton();
        updateMultiplier();
    }

    private void updateTotalThings()
    {
        totalThings.text = $"Amount of things: {totalThingsCount}";
    }

    private void updateCollecters()
    {
        collecters.text = $"You have {collectersCount} collector(s), gathering {collectersProduction} things/sec";
    }

    private void updateGatherButton()
    {
        var gButtonText = gatherButton.GetComponentInChildren<TextMeshProUGUI>();
        gButtonText.text = $"Gather thing\r\n({gatheringPerButtonClick} thing(s))";
    }

    private void updateUpgradeButton()
    {
        var uButtonText = upgradeButton.GetComponentInChildren<TextMeshProUGUI>();
        uButtonText.text = $"Upgrade gather button (Cost: {upgradePrice} things)";
    }

    private void updateCollecterButton()
    {
        var cButtonText = collectorButton.GetComponentInChildren<TextMeshProUGUI>();
        cButtonText.text = $"Hire collector\r\n (Cost: {collectorPrice} things)";
    }

    private void updateCollecterUpgradeButton()
    {
        var cuButtonText = collectorUpgradeButton.GetComponentInChildren<TextMeshProUGUI>();
        cuButtonText.text = $"Double collector productivity (Cost: {collectorUpgradePrice} things)";
    }

    private void updateMultiplier()
    {
        multiplierText.text = $"x{currentMultiplier} -> x{prestigeMultiplier}";
    }

    public void upgradeButtonPressed()
    {
        if(totalThingsCount >= upgradePrice)
        {
            gatheringPerButtonClick = gatheringPerButtonClick * 2;
            gatheringPerButtonClick = Math.Round(gatheringPerButtonClick, 2);
            updateGatherButton();

            totalThingsCount -= upgradePrice;
            totalThingsCount = Math.Round(totalThingsCount, 2);
            updateTotalThings();

            upgradePrice = upgradePrice * 10;
            upgradePrice = Math.Round(upgradePrice, 2);
            updateUpgradeButton();
        }
    }

    public void collecterUpgradeButtonPressed()
    {
        if(totalThingsCount >= collectorUpgradePrice)
        {
            collectorProductivity = collectorProductivity * 2;
            collectorProductivity = Math.Round(collectorProductivity, 2);

            collectersProduction = collectersProduction * 2;
            collectersProduction = Math.Round(collectersProduction, 2);
            updateCollecters();

            totalThingsCount -= collectorUpgradePrice;
            totalThingsCount = Math.Round(totalThingsCount, 2);
            updateTotalThings();

            collectorUpgradePrice = collectorUpgradePrice * 3;
            collectorUpgradePrice = Math.Round(collectorUpgradePrice, 2);
            updateCollecterUpgradeButton();

        }
    }

    public void collecterButtonPressed()
    {
        if(totalThingsCount >= collectorPrice)
        {
            collectersCount += 1;
            collectersProduction += collectorProductivity;
            collectersProduction = Math.Round(collectersProduction, 2);
            updateCollecters();

            totalThingsCount -= collectorPrice;
            totalThingsCount = Math.Round(totalThingsCount, 2);
            updateTotalThings();

            collectorPrice = collectorPrice * 1.2;
            collectorPrice = Math.Round(collectorPrice, 2);
            updateCollecterButton();
        }
    }

    public void prestigeButtonPressed()
    {
        currentMultiplier = prestigeMultiplier;
        resetValues();
        updateAll();
    }

    public void closePrestigePanel()
    {
        prestigePanel.gameObject.SetActive(false);
    }

    public void gatherButtonPressed()
    {
        totalThingsCount += gatheringPerButtonClick;
        totalThingsCount = Math.Round(totalThingsCount, 2);
        totalThings.text = $"Amount of things: {totalThingsCount}";
    }

    private IEnumerator UpdateValueCoroutine()
    {
        while (true)
        {
            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Update the value
            totalThingsCount += collectersProduction;
            totalThingsCount = Math.Round(totalThingsCount, 2);
            updateTotalThings();

            prestigeMultiplier = currentMultiplier + (Math.Floor((totalThingsCount / 10000))/10);

            updateMultiplier();
            if (totalThingsCount > 10001)
            {
                prestigeButton.gameObject.SetActive(true);
                prestigeText.gameObject.SetActive(true);
                multiplierText.gameObject.SetActive(true);
                if (!prestigePanelHasOpened)
                {
                    prestigePanel.gameObject.SetActive(true);
                    prestigePanelHasOpened = true;
                }
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
