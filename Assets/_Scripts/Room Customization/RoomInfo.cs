using System;
using TMPro;
using UnityEngine;

public enum RoomType {
    None,
    CrewQuarters,
    MedicalBay,
    Galley,
    Exercise,
    Lab,
    PowerStorage,
    CommsHub,
    RoverStorage,
    RepairZone,
    ExitAirlock,
    Hygiene,
    EmergencyLSS,
    CommonSpace
}

public enum RoomRelation {
    Good,
    Risky,
    Wrong
}

public class RoomInfo : MonoBehaviour {
    [Header("Room Info")]
    public string roomId;
    public TMP_Text roomNo;
    public TMP_Text roomName;
    public TMP_Text roomNameMap;
    public TMP_Dropdown dropdown;

    [Header("Room Prefabs")]
    public RoomPrefabLibrary prefabLibrary;

    [Header("Spawn Settings")]
    [Tooltip("Where the interior prefab will be spawned. If left empty, it will use this object’s transform.")]
    public Transform interiorSpawnPoint;

    [Header("UI References")]
    public GameObject canvas;

    [Header("Runtime Data")]
    public RoomType selectedType = RoomType.None;
    public RoomInfo[] neighbors;

    private GameObject currentInterior;

    void Start() {
        SetupDropdown();
        InitializeUI();
    }

    void SetupDropdown() {
        dropdown.ClearOptions();
        dropdown.AddOptions(new System.Collections.Generic.List<string>(Enum.GetNames(typeof(RoomType))));
        dropdown.value = (int)selectedType;
        dropdown.RefreshShownValue();
        dropdown.onValueChanged.AddListener(OnDropdownChanged);
    }

    void InitializeUI() {
        roomNo.text = roomId;
        roomName.text = selectedType.ToString();
        roomNameMap.text = selectedType.ToString();
    }

    void OnDropdownChanged(int index) {
        selectedType = (RoomType)index;
        roomName.text = selectedType.ToString();
        roomNameMap.text = selectedType.ToString();

        SpawnInterior(selectedType);

        FindObjectOfType<HabitatManager>().ValidateAndShow();
        HabitatSync.SetRoomType(roomId, selectedType);
    }

    void SpawnInterior(RoomType type) {
        // Destroy previous
        if (currentInterior != null)
            Destroy(currentInterior);

        // Ignore None
        if (type == RoomType.None)
            return;

        // Get prefab from library
        GameObject prefab = prefabLibrary.GetPrefab(type);
        if (prefab == null) {
            Debug.LogWarning($"No prefab assigned for {type}");
            return;
        }

        // Choose spawn parent
        Transform spawnParent = interiorSpawnPoint != null ? interiorSpawnPoint : transform;

        // Instantiate prefab
        currentInterior = Instantiate(prefab, spawnParent);
        currentInterior.transform.localPosition = Vector3.zero;
        currentInterior.transform.localRotation = Quaternion.identity;
    }
}
