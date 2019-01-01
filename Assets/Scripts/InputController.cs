using System.Collections;
using UnityEngine;

public class InputController : Component
{
    public override void UpdateComponent()
    {
        
    }

    public IEnumerator WaitForKeyPress()
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
                {

                }
                else { entity.GetComponent<ActionComponent>().NextAction = new SayName(entity); }
                done = true;
            }
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }
    }
    public void MoveTo(int x, int y)
    {
        entity.GetComponent<ActionComponent>().NextAction = new Walk(entity, x, y);
    }
}
