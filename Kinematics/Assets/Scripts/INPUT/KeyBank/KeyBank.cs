using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "KeyBank", menuName = "SO/Input/KeyBank")]
public class KeyBank : ScriptableObject
{
    //each array index will match the other equivelant index
    [SerializeField] public InputKey[] keys;
    [SerializeField] public KeyCode[] keyCodes;

    //editor
    [SerializeField] public  int count = 0;
}
