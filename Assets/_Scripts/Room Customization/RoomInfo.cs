using System;
using TMPro;
using UnityEngine;

public enum RoomType {
    None,
    CrewQuarters,
    Galley,
    Storage,
    LifeSupport,
    Hygiene,
    Exercise,
    Medical,
    Corridor
}

public class RoomInfo : MonoBehaviour {
    public string roomId;
    public TMP_Text roomNo;
    public TMP_Text roomName;
    public TMP_Dropdown dropdown;


    public RoomType selectedType = RoomType.None;

    
    public RoomInfo[] neighbors;  
    
    void Start() {
        
        dropdown.ClearOptions();
        
        string[] roomTypes = Enum.GetNames(typeof(RoomType));
        
        dropdown.AddOptions(new System.Collections.Generic.List<string>(roomTypes));

        dropdown.value = (int)selectedType;
        dropdown.RefreshShownValue();
        
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
        
        roomNo.text = roomId;
        roomName.text = selectedType.ToString();
    }

    void OnDropdownChanged(int index) {
        selectedType = (RoomType)index;
        roomName.text = selectedType.ToString();
        FindObjectOfType<HabitatManager>().ValidateAndShow();
    }
    
}