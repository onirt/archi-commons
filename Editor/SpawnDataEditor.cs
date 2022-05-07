using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SpawnData))]
public class SpawnDataEditor : AddressableListSelectorEditor
{
    private void OnEnable()
    {
        SpawnData data = (SpawnData)target;
        filter = data.type;
    }
}
