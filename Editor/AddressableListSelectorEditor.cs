using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;

namespace ArChi
{
    public class AddressableListSelectorEditor : Editor
    {
        private List<string> addressablesKeys = new List<string>();

        private IAddressableListHandle addressableListManager;

        private string currentHandleKey;

        protected string filter;

        private void OnEnable()
        {
            filter = "nothing";
        }

        public override void OnInspectorGUI()
        {
            filter = EditorGUILayout.TextField(filter);

            addressableListManager = (IAddressableListHandle)target;
            addressablesKeys.Clear();
            for (int i = 0; i < AddressableAssetSettingsDefaultObject.Settings.groups.Count; i++)
            {
                foreach (var entry in AddressableAssetSettingsDefaultObject.Settings.groups[i].entries)
                {
                    if (entry.address.Contains("archi") && (string.IsNullOrEmpty(filter) || entry.address.Contains(filter)) && !addressableListManager.ContainsAddressable(entry.address))
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
            for (int i = 0; i < addressablesKeys.Count; i++)
            {
                currentHandleKey = addressablesKeys[i];
                EditorsUtils.AddHorizontalLayer("box", AddAddressableEntry);
            }
        }
        private void AddAddressableEntry()
        {
            EditorGUILayout.LabelField(currentHandleKey);

            GUI.color = Color.green;
            if (GUILayout.Button("Add"))
            {
                addressableListManager.AddAddressable(currentHandleKey);
            }

            GUI.color = Color.white;
        }
    }
}