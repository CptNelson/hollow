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
        //Player player = new Player();

        EntityFactory factory = new EntityFactory();
        Entity player = factory.CreateEntity("Player", new List<IComponent> { new BodyComponent(), new ActionComponent() });
        player.NeedsUserInput = true;
        player.Sprite = Instantiate(Resources.Load<GameObject>("Prefabs/player"));
        _entitiesList.Add(player);

        barbarians = new List<Entity>();
        for (int i = 0; i < 3; i++)
        {
            Entity barbarian = factory.CreateEntity("Barbarian", new List<IComponent> { new BodyComponent(), new ActionComponent(), new AIComponent() });
            barbarians.Add(barbarian);
            barbarians[i].Sprite = Instantiate(Resources.Load<GameObject>("Prefabs/barbarian"));
            barbarians[i].GetComponent<ActionComponent>().ResetGoal();
            barbarians[i].GetComponent<ActionComponent>().NextAction = new TestAction();           
            
            //Debug.Log("Barbarian goal: " + barbarians[i].GetComponent<ActionComponent>().Goal);
            //barbarians[i].GetComponent<ActionComponent>().NextAction = barbarians[i].GetComponent<AIComponent>().ChooseAction();

            _entitiesList.Add(barbarians[i]);
        }


        Entity troll = factory.CreateEntity("Troll", new List<IComponent> { new BodyComponent(), new ActionComponent(), new AIComponent() });
        troll.Sprite = Instantiate(Resources.Load<GameObject>("Prefabs/troll"));
        //troll.GetComponent<ActionComponent>().Ai = new TrollAI(troll);
        troll.GetComponent<ActionComponent>().ResetGoal();
        troll.GetComponent<ActionComponent>().NextAction = new TestAction();
        //        troll.GetComponent<ActionComponent>().NextAction = troll.GetComponent<AIComponent>().ChooseAction();

        _entitiesList.Add(troll);

        //then go over the entitylist and give them random starting positions.
        for (int i = 0; i < _entitiesList.Count; i++)
        {
            _entitiesList[i].Position = Utils.GetRandomEmptyPosition();
            //Debug.Log("player Pos: " + _entitiesList[0].Position);
            _entitiesList[i].Sprite.transform.position = _grid.GetCellCenterLocal(_entitiesList[i].Position);
        }

    }
}
