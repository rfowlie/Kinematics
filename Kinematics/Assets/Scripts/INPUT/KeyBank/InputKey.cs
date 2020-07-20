using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//adjust this depending on the project
//just the button presses, directional stuff will have to be custom
//in the input states as don't to allow players to customize those
public enum InputKey
{
    //world
    WORLDINTERACT,
    WORLDBACK,
    WORLDJUMP,

    //menu
    MAINMENU,
    MENUSELECT,
    MENUBACK,
    MENUPAGERIGHT,
    MENUPAGELEFT,
    MENURIGHT,
    MENULEFT,

    //combat
    COMBATSELECT,
    COMBATBACK,
    COMBATPLAYERRIGHT,
    COMBATPLAYERLEFT,

    //movement
    MOVEVERTICAL,
    MOVEHORIZONTAL
}
