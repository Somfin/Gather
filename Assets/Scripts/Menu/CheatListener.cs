using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatListener : MonoBehaviour
{
    [SerializeField]
    private GameObject cassidy;

    void Update ()
    {
        /// Full Equipment Unlock
        if (Input.GetKeyDown(KeyCode.F))
        {
            GameState.Instance.SetFile(0);
            var save = new GameSave("Test Scene", 0, 1, new List<string>() {
                "Spike Trap", "Crushing Mechanism",
                "Knife Launcher", "Concert Arm Control",
                "Whitestone Shield", "Sparking Polish",
                "Flame Hurler", "Blackfire Recipe",
                "Dragon Bow", "Dragon Tooth",
                "Void Egg", "Controlled Instability Admixture",
                "Lightning Jar", "Supercharge Band",
                "Sureshot's Cannon", "Sureshot's Wrath",
                "Pot Of Bees", "Swarming Nectar",
                "The Writ"}, new List<string>());
            save.Load(cassidy);
        }
    }
}
