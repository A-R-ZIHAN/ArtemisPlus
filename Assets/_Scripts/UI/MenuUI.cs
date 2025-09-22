using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("UI references")] 
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private Button exploreButton;
    [SerializeField] private GameObject habitatSelectionPanel;
    [SerializeField] private GameObject habitatQuantitySelectionPanel;
    [SerializeField] private GameObject colonyScalingPanel;
    [SerializeField] private Button applyScaleButton;
    
    [Header("Habitat Buttons")]
    [SerializeField] private List<Button> habitatButtons;
    [SerializeField] private List<Button> habitatQuantityButtons;

    private readonly int[] _qtyMap = { 4, 6, 8 };

    private void Awake()
    {
        Init();
        ButtonInit();
    }

    private void Init()
    {
        mainMenuCanvas.SetActive(true);
        exploreButton.gameObject.SetActive(true);
        habitatSelectionPanel.SetActive(false);
        habitatQuantitySelectionPanel.SetActive(false);
        inGameCanvas.SetActive(false);
    }

    private void ButtonInit()
    {
        exploreButton.onClick.AddListener( () =>
        {
            exploreButton.gameObject.SetActive(false);
            habitatSelectionPanel.SetActive(true);
        });
        
        applyScaleButton.onClick.AddListener(ApplyScale);
        
        for (int i = 0; i < habitatButtons.Count; i++)
        {
            int index = i + 1;
            habitatButtons[i].onClick.AddListener(() => SelectHabitat(index));
        }

        for (int i = 0; i < habitatQuantityButtons.Count && i < _qtyMap.Length; i++)
        {
            int q = _qtyMap[i];
            habitatQuantityButtons[i].onClick.AddListener(() => SelectHabitatQuantity(q));
        }
    }

    private void SelectHabitat(int num)
    {
        habitatSelectionPanel.SetActive(false);
        habitatQuantitySelectionPanel.SetActive(true);
        GameManager.Instance.selectedHabitatNo = num;
    }

    private void SelectHabitatQuantity(int qty)
    {
        habitatQuantitySelectionPanel.SetActive(false);
        mainMenuCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
        GameManager.Instance.habitatQuantity = qty;
        GameManager.Instance.TriggerColony();
    }

    private void ApplyScale()
    {
        colonyScalingPanel.SetActive(false);
    }
}
