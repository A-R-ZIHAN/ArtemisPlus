using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [Header("UI Panels")] 
    [SerializeField] private GameObject mainMenuCanvas;
    [SerializeField] private GameObject inGameCanvas;
    [SerializeField] private GameObject habitatSelectionPanel;
    [SerializeField] private GameObject missionDataSelectionPanel;
    
    [Header("Buttons")]
    [SerializeField] private List<Button> habitatButtons;
    [SerializeField] private Button exploreButton;
    [SerializeField] private Button applyHabitatSelectionButton;
    [SerializeField] private Button applyMissionDataButton;
    
    [Header("Drop Downs")]
    [SerializeField] private TMP_Dropdown colonyCrewDropDown;
    [SerializeField] private TMP_Dropdown durationDropDown;
    [SerializeField] private TMP_Dropdown locationDropDown;
    
    [Header("Other Scripts")]
    public HabitatScaler habitatScaler;

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
        missionDataSelectionPanel.SetActive(false);
        inGameCanvas.SetActive(false);
    }

    private void ButtonInit()
    {
        exploreButton.onClick.AddListener( () =>
        {
            exploreButton.gameObject.SetActive(false);
            habitatSelectionPanel.SetActive(true);
            GameManager.Instance.ToggleSurfaceCamera(false);
            GameManager.Instance.ToggleHabitatViewCamera(true);
        });

        applyHabitatSelectionButton.onClick.AddListener(() =>
        {
            habitatSelectionPanel.SetActive(false);
            missionDataSelectionPanel.SetActive(true);
        });
        
        //applyScaleButton.onClick.AddListener(ApplyScale);
        applyMissionDataButton.onClick.AddListener(ApplyMissionData);
        
        for (int i = 0; i < habitatButtons.Count; i++)
        {
            int index = i + 1;
            habitatButtons[i].onClick.AddListener(() => SelectHabitat(index));
        }

        // for (int i = 0; i < habitatQuantityButtons.Count && i < _qtyMap.Length; i++)
        // {
        //     int q = _qtyMap[i];
        //     habitatQuantityButtons[i].onClick.AddListener(() => SelectHabitatQuantity(q));
        // }
    }

    public void ChangeColonyData()
    {
        Debug.Log(colonyCrewDropDown.value);
        if (colonyCrewDropDown.value == 0)
        {
            float scale = 1.2f;
            habitatScaler.ScaleDirectChildren(new Vector3(scale, scale, scale));
            GameManager.Instance.missionCrewAmount = 4;
        }
        else if (colonyCrewDropDown.value == 1)
        {
            float scale = 1.5f;
            habitatScaler.ScaleDirectChildren(new Vector3(scale, scale, scale));
            GameManager.Instance.missionCrewAmount = 6;
        }
        else if (colonyCrewDropDown.value == 2)
        {
            float scale = 1.8f;
            habitatScaler.ScaleDirectChildren(new Vector3(scale, scale, scale));
            GameManager.Instance.missionCrewAmount = 8;
        }
    }
    
    public void ChangeMissionDurationData()
    {
        Debug.Log(durationDropDown.options[durationDropDown.value].text);
        GameManager.Instance.missionDuration = durationDropDown.options[durationDropDown.value].text;
    }

    public void ChangeLocationData()
    {
        GameManager.Instance.missionLocation = locationDropDown.options[locationDropDown.value].text;
    }

    private void SelectHabitat(int num)
    {
        GameManager.Instance.selectedHabitatNo = num;
        GameManager.Instance.TriggerColony();
    }

    private void ApplyMissionData()
    {
        missionDataSelectionPanel.SetActive(false);
        mainMenuCanvas.SetActive(false);
        inGameCanvas.SetActive(true);
    }
}
