using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LoadTextureEditor))]
[CanEditMultipleObjects]
public class LoadTextureEditor : Editor {

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Go"))
        {

        }

    }

}
