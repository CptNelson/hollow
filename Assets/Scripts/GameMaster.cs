using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class GameMaster : MonoBehaviour
{
    public GameObject map;
    public GameObject fovMap;
    public static List<Entity> entitiesList;
    private Utils utils;
    public static Entity player;
    //private PlayerController input;
    public static ActionManager actionManager;


    // Use this for initialization
    void Start()
    {

        CreateLevel();
        AddEntities();
        Debug.Log(entitiesList[0].NeedsUserInput);
        StartCoroutine(GameLoop());
    }

    private void CreateLevel()
    {
        DungeonGenerator.Create();
        map.GetComponent<GameTiles>().CreateTileDictionary();

    }

    private void AddEntities()
    {
        entitiesList = new List<Entity>();
        EntitySpawner.AddEntity();
        player = entitiesList[0];
        
    }


    //If playing, loop entities list and update, and as for player input
    private IEnumerator GameLoop()
    {
        ActionManager actionManager = new ActionManager();

        bool playing = true;

        while (playing)
        {
            foreach (Entity entity in entitiesList)
            {
                if (entity.NeedsUserInput)
                {
                    // wait for player to do somethingg before continuing
                    yield return PlayerController.WaitForKeyPress();
                    actionManager.AddAction(entity.GetAction());
                }
                else
                {
                    actionManager.AddAction(entity.GetAction());
                    actionManager.ProcessActions();
                }
            }
        }
    }
}

public class ActionManager
{
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
        // Apply transactions in the order they were added.
        foreach (IAction action in _actions.Where(x => !x.IsCompleted))
        {
            action.Execute();
        }
    }
}