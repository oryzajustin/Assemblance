using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// To be used for progressing through different levels/scenes if we elected to add more levels
// Only handles ESC quitting for now...
public class LevelManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}
