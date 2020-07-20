using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(KeyBank))]
public class KeyBankEditor : Editor
{
    KeyBank kb;

    private void OnEnable()
    {
        kb = (KeyBank)target;
    }

    public override void OnInspectorGUI()
    {
        if (kb.keys == null || kb.keyCodes == null)
        {
            kb.keys = new InputKey[0];
            kb.keyCodes = new KeyCode[0];
        }

        EditorGUILayout.BeginVertical();

        //BUTTONS
        if (GUILayout.Button("Add Element"))
        {
            //add element
            kb.count++;
            kb.keys = ArrayEX.ResizeArray(kb.count, kb.keys);
            kb.keyCodes = ArrayEX.ResizeArray(kb.count, kb.keyCodes);
        }
        if (GUILayout.Button("Remove Element"))
        {
            //remove last element
            if (kb.count > 0)
            {
                kb.count--;
                kb.keys = ArrayEX.ResizeArray(kb.count, kb.keys);
                kb.keyCodes = ArrayEX.ResizeArray(kb.count, kb.keyCodes);
            }
        }
        if (GUILayout.Button("Reset"))
        {
            kb.count = 0;
            kb.keys = new InputKey[kb.count];
            kb.keyCodes = new KeyCode[kb.count];
        }
        
        for (int i = 0; i < kb.count; i++)
        {
            kb.keys[i] = (InputKey)EditorGUILayout.EnumPopup("Key", kb.keys[i]);
            kb.keyCodes[i] = (KeyCode)EditorGUILayout.EnumPopup("KeyCode", kb.keyCodes[i]);
            EditorGUILayout.Space();
        }                

        EditorGUILayout.EndVertical();
        EditorUtility.SetDirty(kb);
    }
}
