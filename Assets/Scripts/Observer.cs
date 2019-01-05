using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

abstract public class Observer : Component
{
    //Components are updated every turn. Override this if update is needed.
    public override void UpdateComponent() { }
    public abstract void OnNotify();
   
}

public class HPObserver : Observer
{
    public override void OnNotify()
    {
        GameMaster.ui.GetComponent<UI>().UpdateComponent();
        Debug.Log("HPobserver OnNotify");
    }
}

abstract public class Subject : Component
{
    private List<Observer> _observers = new List<Observer>();

    public void AddObserver(Observer observer)
    {
        _observers.Add(observer);
    }

    public void Notify()
    {
        foreach (var observer in _observers)
            observer.OnNotify();
    }
}