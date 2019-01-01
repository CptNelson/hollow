using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : ScriptableObject {

    public static IEnumerator WaitForKeyPress()
    {
        FOV.UpdatePlayerFOV();
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (Input.anyKeyDown)
            {
                if (Input.GetKey("up")) { MoveTo(0, 1); }
                else if (Input.GetKey("down")) { MoveTo(0, -1); }
                else if (Input.GetKey("left")) { MoveTo(-1, 0); }
                else if (Input.GetKey("right")) { MoveTo(1, 0); }
                else if (Input.GetKeyDown(KeyCode.Space))
                    { GameMaster.entitiesList[1].GetComponent<ActionComponent>().NextAction = 
                      GameMaster.entitiesList[1].GetComponent<AIComponent>().ChooseAction(); }
                else { GameMaster.player.GetComponent<ActionComponent>().NextAction = new SayName(GameMaster.player); }
                done = true;
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }
    }
    public static void MoveTo(int x, int y)
    {
        GameMaster.player.GetComponent<ActionComponent>().NextAction = new Walk(GameMaster.player, x, y);
    }
}

