using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class InputController : Component
{
    public override void UpdateComponent()
    {
        
    }

    public IEnumerator WaitForKeyPress()
    {
        //Shadowcast.UpdatePlayerFOV();
        SCast.ComputeVisibility(entity.Position, 14);
        //FOV.UpdatePlayerFOV();
        bool done = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
           // Debug.Log("waiting for key");
            if (Input.anyKeyDown)
            {
                if (Input.GetKey("up")) { MoveTo(0, 1); }
                else if (Input.GetKey("down")) { MoveTo(0, -1); }
                else if (Input.GetKey("left")) { MoveTo(-1, 0); }
                else if (Input.GetKey("right")) { MoveTo(1, 0); }
                else if (Input.GetKeyDown(KeyCode.Space))
                {
                    entity.GetComponent<ActionComponent>().NextAction = new UseItem(entity.GetComponent<BodyComponent>().Items[0], entity);
                   
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
