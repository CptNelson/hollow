using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI
{
    Canvas canvas;
    GameObject textObject;
    string playerName;
    string playerHP;
    Entity _player;

    public UI()
    {
        CreateUI();

    }

    private void CreateUI()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _player = GameMaster.player;
        playerHP = _player.HP.ToString();

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
            textObject.GetComponent<Text>().text = "HP: " + playerHP;  
        }
    }

}
