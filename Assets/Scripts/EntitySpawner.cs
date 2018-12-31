using System.Collections.Generic;
using UnityEngine;

public class EntitySpawner : ScriptableObject
{
    public static List<Entity> barbarians;

    private static Grid _grid;
    private static List<Entity> _entitiesList;


    public void AddEntity()
    {
        CreateEntities();
    }



    private void CreateEntities()
    {
        _grid = GameObject.Find("Map").GetComponent<Grid>();
        _entitiesList = GameMaster.entitiesList;

        //create entity and add its prefab to entity.sprite.
        //Player needs to be created first.
        Player player = new Player();
        _entitiesList.Add(player);
        player.Sprite = Instantiate(Resources.Load<GameObject>("Prefabs/player"));

        barbarians = new List<Entity>();
        for (int i = 0; i < 3; i++)
        {
            barbarians.Add(new Barbarian());
            //barbarians[i].Fov = new FOV();
            barbarians[i].Sprite = Instantiate(Resources.Load<GameObject>("Prefabs/barbarian"));
            _entitiesList.Add(barbarians[i]);
        }

        EntityFactory factory = new EntityFactory();
        Entity troll = factory.CreateEntity("troll", new List<IComponent> { new BodyComponent() });
        _entitiesList.Add(troll);
        troll.Sprite = Instantiate(Resources.Load<GameObject>("Prefabs/troll"));


        //then go over the entitylist and give them random starting positions.
        for (int i = 0; i < _entitiesList.Count; i++)
        {
            _entitiesList[i].Position = Utils.GetRandomEmptyPosition();
            Debug.Log("player Pos: " + _entitiesList[0].Position);
            _entitiesList[i].Sprite.transform.position = _grid.GetCellCenterLocal(_entitiesList[i].Position);
        }

    }
}
