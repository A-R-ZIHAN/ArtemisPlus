using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class HabitatManager : MonoBehaviour {
    public List<RoomInfo> rooms = new List<RoomInfo>();
    public List<ZoningRule> rules = new List<ZoningRule>();
    
    public TextMeshProUGUI feedbackText;

    private void Awake() {
        //rooms.AddRange(FindObjectsOfType<RoomInfo>());
    }
    public void ValidateAndShow() {
        string result = ValidateHabitat();
        feedbackText.text = result;
    }

    private string ValidateHabitat() {
        List<string> errors = new List<string>();

        foreach (var rule in rules) {
            foreach (var room in rooms) {
                if (room.selectedType == rule.requiredRoom) {
                    bool hasNeighbor = false;

                    foreach (var neighbor in room.neighbors) {
                        if (neighbor != null && neighbor.selectedType == rule.neighborRoom) {
                            hasNeighbor = true;
                            if (!rule.mustBeAdjacent) {
                                errors.Add($"{room.roomId} ({room.selectedType}) cannot be next to {neighbor.roomId} ({neighbor.selectedType})");
                            }
                        }
                    }

                    if (rule.mustBeAdjacent && !hasNeighbor) {
                        errors.Add($"{room.roomId} ({room.selectedType}) must be next to {rule.neighborRoom}, but isn’t.");
                    }
                }
            }
        }

        return errors.Count == 0 ? "✅ Habitat layout valid!" : string.Join("\n", errors);
    }
}