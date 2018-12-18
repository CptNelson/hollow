//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class EngineIEnumerator
//{
//    //private List<Entity> actorList;
//    IEnumerator<bool> mIterator;
//    public void ProcessGame()
//    {

//        if (mIterator == null)
//        {
//           mIterator = CreateProcessIterator().GetEnumerator();
//        }

//        mIterator.MoveNext();
//        bool dummy = mIterator.Current;
//    }

//    IEnumerable<bool> CreateProcessIterator()
//    {
//        //actorList = new List<Entity>();

//        foreach (Entity entity in GameMaster.entitiesList)
//        {
//            if (entity.needsUserInput)
//            {
//                Debug.Log("player turn");
//                yield return true;
//            }
//            else
//            {
//                Debug.Log("barb turn");
//                entity.DoUpdate();
//            }
//        }
//    }

//}
