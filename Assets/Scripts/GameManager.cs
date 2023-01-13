using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static bool Cheat = false;
    public Transform ExitDoor1;
    public Transform ExitDoor2;
    public Transform ExitDoor3;
    public Transform ExitDoor4;
    public Toggle checkBox;

    static List<Transform> Exits = new List<Transform>();

    void Start()
    {
        if (ExitDoor1 != null)
        {
            Exits.Add(ExitDoor1);
            Exits.Add(ExitDoor2);
            Exits.Add(ExitDoor3);
            Exits.Add(ExitDoor4);
        }

        if (Cheat)
        {
            checkBox.isOn = true;
            Cheat = true;
        }
    }

    public void TurnOnOrOffCheat()
    {
        Cheat = !Cheat;
    }

    public static Transform GetRandomExit()
    {
        int randomIndex = Random.Range(0, Exits.Count);
        return Exits[randomIndex];
    }
}
