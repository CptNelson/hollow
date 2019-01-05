using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : Component
{
    public Canvas canvas;
    public GameObject textObject;
    string playerName;
    public string playerHP;
    Entity _player;
    int _hp;

    public override void UpdateComponent()
    {
        textObject.GetComponent<Text>().text = "HP: " + _player.HP;
        Debug.Log("UI notified");
    }

    public void CreateUI(Entity player)
    {
        components.Add(new HPObserver());
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _player = player;
        _hp = player.HP;
        



        if (canvas != null)
        {
            textObject = new GameObject("TextComp");
            textObject.transform.SetParent(canvas.transform);
            textObject.AddComponent<Text>();
            textObject.GetComponent<RectTransform>().sizeDelta = new Vector2Int(1200, 120);
            textObject.transform.localPosition = new Vector3Int(0, -340, 0);
            textObject.GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            textObject.GetComponent<Text>().fontSize = 32;
            textObject.GetComponent<Text>().color = Color.blue;
            textObject.GetComponent<Text>().text = "HP: " + _player.HP;  
        }
    }

}
