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

        EntityFactory factory = new EntityFactory();


        Entity player = factory.CreateEntity("Player", new List<IComponent> { new LivingComponent(), new BodyComponent(), new ActionComponent(), new InputController(), new AttackComponent() });       
        player.GetComponent<LivingComponent>().NeedsUserInput = true;
        player.GetComponent<ActionComponent>().Speed = 55;
        player.GetComponent<LivingComponent>().Sprite = Instantiate(Resources.Load<GameObject>("Prefabs/player"));
        _entitiesList.Add(player);
        
        barbarians = new List<Entity>();
        for (int i = 0; i < 3; i++)
        {
            Entity barbarian = factory.CreateEntity("Barbarian", new List<IComponent> { new LivingComponent(), new BodyComponent(), new ActionComponent(), new AIComponent(), new AttackComponent() });
            barbarians.Add(barbarian);
            barbarians[i].GetComponent<LivingComponent>().Sprite = Instantiate(Resources.Load<GameObject>("Prefabs/barbarian"));

            //Debug.Log("Barbarian goal: " + barbarians[i].GetComponent<ActionComponent>().Goal);
            //barbarians[i].GetComponent<ActionComponent>().NextAction = barbarians[i].GetComponent<AIComponent>().ChooseAction();

            _entitiesList.Add(barbarians[i]);
        }
        

        Entity troll = factory.CreateEntity("Troll", new List<IComponent> { new LivingComponent(), new BodyComponent(), new ActionComponent(), new AIComponent(), new AttackComponent() });
        troll.GetComponent<LivingComponent>().Sprite = Instantiate(Resources.Load<GameObject>("Prefabs/troll"));
        //troll.GetComponent<ActionComponent>().Ai = new TrollAI(troll);
        troll.GetComponent<ActionComponent>().Speed = 25;
        troll.GetComponent<BodyComponent>().Strength = 6;

        //        troll.GetComponent<ActionComponent>().NextAction = troll.GetComponent<AIComponent>().ChooseAction();

        _entitiesList.Add(troll);

        //then go over the entitylist and give them random starting positions.
        for (int i = 0; i < _entitiesList.Count; i++)
        {
            _entitiesList[i].GetComponent<LivingComponent>().Position = Utils.GetRandomEmptyPosition();
            //Debug.Log("player Pos: " + _entitiesList[0].Position);
            _entitiesList[i].GetComponent<LivingComponent>().Sprite.transform.position = _grid.GetCellCenterLocal(_entitiesList[i].GetComponent<LivingComponent>().Position);
        }

        Entity potion = factory.CreateEntity("Potion", new List<IComponent> { new Potion(), new LivingComponent() }); //well, fuck.
        potion.GetComponent<LivingComponent>().Sprite = Instantiate(Resources.Load<GameObject>("Prefabs/potion"));
        //  Debug.Log("P: " + player.Id + player.GetComponent<BodyComponent>().Items.Count);

        player.GetComponent<BodyComponent>().Items.Add(potion);

        GameMaster.ui = factory.CreateEntity("UI", new List<IComponent> { new UI() });
        GameMaster.ui.GetComponent<UI>().CreateUI(player);
    }
}
