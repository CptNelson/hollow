using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameMaster : MonoBehaviour
{
    public GameObject map;
    public GameObject fovMap;
    public static List<Entity> entitiesList;
    public static Entity player;
    public static ActionManager actionManager;
    public static int width = 40;
    public static int height = 30;

    private Utils utils;
    private static Tilemap _tilemap;
    private EntitySpawner _entitySpawner;



    void Start()
    {
        Init();
        //start the game loop
        StartCoroutine(GameLoop());
    }

    // Use this for initialization
    void Init()
    {
        //create the level and entities
        CreateLevel();
        AddEntities();
    }

    private void CreateLevel()
    {
        //create the map
        DungeonGenerator.Create(width, height);
        //create a dictionary holding tile data.
        map.GetComponent<GameTiles>().CreateTileDictionary();
    }

    //TODO: this should probably happen in the map generator or somewhere that does both of them
    // so it is possible to choose what entities are in a particular map
    private void AddEntities()
    {
        //creates a list to hold all entities currently at game
        entitiesList = new List<Entity>();
        _entitySpawner = new EntitySpawner();
        _entitySpawner.AddEntity();
        //player needs to be first entity in list
        player = entitiesList[0];
        //set all entities visibility off except player's.
        for (int i = 1; i < entitiesList.Count; i++)
        {
            entitiesList[i].Sprite.GetComponent<SpriteRenderer>().enabled = false;
        }

    }


    //If playing, loop entities list and update, and as for player input
    private IEnumerator GameLoop()
    {
        ActionManager actionManager = new ActionManager();

        bool playing = true;

        while (playing)
        {
            //Iterate the entities and ask them for an action.
            //reverse list because we need it for removing entitites from list while it's iterating.
            foreach (Entity entity in entitiesList.Reverse<Entity>())
            {

                if (entity.NeedsUserInput)
                {
                    // wait for player to do somethingg before continuing
                    yield return PlayerController.WaitForKeyPress();

                    actionManager.AddAction(entity.GetAction());
                }
                else
                {

                    //if entity is dead, remove it from the list.
                    if (!entity.Alive)
                    {
                        entitiesList.Remove(entity);
                    }
                    if (entity.Alive) {
                        actionManager.AddAction(entity.GetAction());
                        Debug.Log(entity.NextAction);
                    }

                }
                actionManager.ProcessActions();
            }
        }
    }
}

public class ActionManager
{
    //asks entities for actions and then executes them
    private readonly List<IAction> _actions = new List<IAction>();

    public bool HasPendingAction
    {
        get { return _actions.Any(x => !x.IsCompleted); }
    }

    public void AddAction(IAction action)
    {
        _actions.Add(action);
    }

    public void ProcessActions()
    {
        // Execute actions in the order they were added.
        foreach (IAction action in _actions.Where(x => !x.IsCompleted))
        {
            Debug.Log("execute");
            action.Execute();
        }
    }
}