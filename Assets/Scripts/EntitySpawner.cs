using UnityEngine;

public class EntitySpawner : ScriptableObject
{
    private static GameObject map = GameObject.Find("Map");
    private static GameObject player;

    public static void AddPlayer()
    {
        player = Resources.Load<GameObject>("Prefabs/player");
        Instantiate(player);
        player.transform.position = map.GetComponent<Grid>().GetCellCenterLocal(new Vector3Int(1, 1, 0));
    }

}
