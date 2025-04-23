using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RoomSwitcher", menuName = "Switching/RoomSwitcher")]
public class RoomSwitcher : ScriptableObject
{
    public string currentSceneName;         // e.g., "Forest_Main_01"
    public string switcherID;               // e.g., "Right01"

    public string targetSceneName;          // e.g., "Forest_Main_02"
    public string targetSwitcherID;         // e.g., "Left01"
}