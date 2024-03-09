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
    private double totalThingsCount;
    private double collectersCount;
    private double collectersProduction;
    private double gatheringPerButtonClick;
    private double upgradePrice;
    private double collectorPrice;
    private double collectorUpgradePrice;
    private double collectorProductivity;
    private double currentMultiplier;
    private double prestigeMultiplier;
    private bool prestigePanelHasOpened;
    // Start is called before the first frame update
    void Start()
    {
        totalThings.text = "jajajja";
        totalThingsCount = 0;
        collectersCount = 0;
        collectersProduction = 0;
        collectorProductivity = 0.5;
        gatheringPerButtonClick = 1;
        upgradePrice = 100;
        collectorUpgradePrice = 500;
        collectorPrice = 10;
        prestigePanelHasOpened = false;
        currentMultiplier = 1.00;
        prestigeMultiplier = 1.00;
        updateAll();
        StartCoroutine(UpdateValueCoroutine());
    }

    private void updateAll()
    {
        updateTotalThings();
        updateCollecters();
        updateGatherButton();
        updateUpgradeButton();
        updateCollecterButton();
        updateCollecterUpgradeButton();
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

            updateGatherButton();

            totalThingsCount -= upgradePrice;
            totalThingsCount = Math.Round(totalThingsCount, 2);
            updateTotalThings();

            upgradePrice = upgradePrice * 10;
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
            updateCollecters();

            totalThingsCount -= collectorUpgradePrice;
            updateTotalThings();

            collectorUpgradePrice = collectorUpgradePrice * 3;
            updateCollecterUpgradeButton();

        }
    }

    public void collecterButtonPressed()
    {
        if(totalThingsCount >= collectorPrice)
        {
            collectersCount += 1;
            collectersProduction += collectorProductivity;
            updateCollecters();

            totalThingsCount -= collectorPrice;
            updateTotalThings();

            collectorPrice = collectorPrice * 1.2;
            collectorPrice = Math.Round(collectorPrice, 2);
            updateCollecterButton();
        }
    }

    public void closePrestigePanel()
    {
        prestigePanel.gameObject.SetActive(false);
    }

    public void gatherButtonPressed()
    {
        totalThingsCount += gatheringPerButtonClick;
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
            updateTotalThings();

            prestigeMultiplier = 1 + (Math.Floor((totalThingsCount / 50000))/10);

            updateMultiplier();
            if (totalThingsCount > 50001)
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
