using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Habitat/Room Prefab Library")]
public class RoomPrefabLibrary : ScriptableObject {
    [Serializable]
    public struct RoomPrefabEntry {
        public RoomType type;
        public GameObject prefab;
    }

    public List<RoomPrefabEntry> entries = new List<RoomPrefabEntry>();

    private Dictionary<RoomType, GameObject> prefabMap;

    public GameObject GetPrefab(RoomType type) {
        if (prefabMap == null)
            BuildMap();
        prefabMap.TryGetValue(type, out GameObject prefab);
        return prefab;
    }

    private void BuildMap() {
        prefabMap = new Dictionary<RoomType, GameObject>();
        foreach (var entry in entries)
            prefabMap[entry.type] = entry.prefab;
    }
}