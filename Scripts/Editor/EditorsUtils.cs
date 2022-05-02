using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EditorsUtils
{
    public static void AddVerticalLayer(string style, UnityAction action)
    {

        EditorGUILayout.BeginVertical(style);
        action?.Invoke();
        EditorGUILayout.EndVertical();
    }
    public static void AddHorizontalLayer(string style, UnityAction action)
    {

        EditorGUILayout.BeginHorizontal(style);
        action?.Invoke();
        EditorGUILayout.EndHorizontal();
    }
}
