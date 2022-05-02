using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.Events;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    List<string> addressablesKeys = new List<string>();
    GameManager gameManager;
    string key;
    public override void OnInspectorGUI()
    {
        gameManager = (GameManager)target;
        addressablesKeys.Clear();
        for (int i = 0; i < AddressableAssetSettingsDefaultObject.Settings.groups.Count; i++)
        {
            foreach (var entry in AddressableAssetSettingsDefaultObject.Settings.groups[i].entries)
            {
                if (entry.address.Contains("archi") && !gameManager.addressable.Contains(entry.address))
                {
                    addressablesKeys.Add(entry.address);
                }
            }
        }
        EditorsUtils.AddVerticalLayer("box", BuildAddressablesList);
        base.OnInspectorGUI();
    }
    private void BuildAddressablesList()
    {
        for (int i=0; i < addressablesKeys.Count; i++)
        {
            key = addressablesKeys[i];
            EditorsUtils.AddHorizontalLayer("box", AddAddressableEntry);
        }
    }
    private void AddAddressableEntry()
    {
        EditorGUILayout.LabelField(key);

        GUI.color = Color.green;
        if (GUILayout.Button("Add"))
        {
            gameManager.addressable.Add(key);
        }

        GUI.color = Color.white;
    }
}
