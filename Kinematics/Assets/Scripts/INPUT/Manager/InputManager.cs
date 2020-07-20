using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//this should just return keycodes
//should read
public class InputManager : Singleton<InputManager>
{
    //keybank from other project
    [SerializeField] private KeyBank keyboardBank = null;
    [SerializeField] private KeyBank gamepadBank = null;


    public enum Axis { KEYBOARD, GAMEPAD }
    [SerializeField] private Axis axis = Axis.GAMEPAD;
    public Axis GetController()
    {
        return axis;
    }

    //controller axis, determined by Controller enum
    public string VerticalAxis { get; private set; }
    public string HorizontalAxis { get; private set; }

    private void SetAxis()
    {
        //use enum to control camera settings
        if (axis == Axis.GAMEPAD)
        {
            VerticalAxis = "Vertical";
            HorizontalAxis = "Horizontal";
        }
        else if(axis == Axis.KEYBOARD)
        {
            VerticalAxis = "Mouse Y";
            HorizontalAxis = "Mouse X";
        }
    }


    private Dictionary<InputKey, KeyCode> inputList = new Dictionary<InputKey, KeyCode>();
    private void SetupInputs()
    {
        KeyBank t = axis == Axis.GAMEPAD ? gamepadBank : keyboardBank;

        //element count will ensure that no extra array spots get entered
        for (int i = 0; i < t.count; i++)
        {
            if(inputList.ContainsKey(t.keys[i]))
            {
                Debug.Log("Same key attempted to be added: " + t.keys[i]);
                continue;
            }

            inputList.Add(t.keys[i], t.keyCodes[i]);
        }
    }

    //swap controller to other one, clear inputs and reassign dictionary
    private void SwitchController()
    {
        axis = axis == Axis.GAMEPAD ? Axis.KEYBOARD : Axis.GAMEPAD;

        inputList.Clear();

        SetupInputs();
    }

    private bool ValidateKey(InputKey key)
    {
        if (!inputList.ContainsKey(key))
        {
            Debug.Log("Key Doesn't Exist");
            return false;
        }

        return true;
    }

    //check input from dictionary
    public bool KeyPress(InputKey key)
    {
        if (!ValidateKey(key))
            return false;

        if (Input.GetKeyUp(inputList[key]))
            return true;
        else
            return false;
    }

    public bool KeyHeld(InputKey key)
    {
        if (!ValidateKey(key))
            return false;

        if (Input.GetKey(inputList[key]))
            return true;
        else
            return false;
    }


    private void Start()
    {
        SetAxis();
        SetupInputs();
        SetSetup(gamepadSetup);
    }



    //Orientation Setup
    //make it so players can decide the arrow directions
    public enum OrientationSetup { normal, inverted, invertedX, invertedY };
    [SerializeField] private OrientationSetup gamepadSetup = OrientationSetup.normal;
    private Vector2 currentSetup = Vector2.zero;

    //various setups for controllers stored as dictionary
    private Dictionary<OrientationSetup, Vector2> setup = new Dictionary<OrientationSetup, Vector2>()
    {
        { OrientationSetup.normal, new Vector2(1f, -1f) },
        { OrientationSetup.inverted, new Vector2(-1f, 1f) },
        { OrientationSetup.invertedX, new Vector2(-1f, -1f) },
        { OrientationSetup.invertedY, new Vector2(1f, 1f) },
    };

    public void SetSetup(OrientationSetup newSetup)
    {
        gamepadSetup = newSetup;
        currentSetup = setup[gamepadSetup];
    }

    public Vector2 GetSetup()
    {
        return currentSetup;
    }
}


