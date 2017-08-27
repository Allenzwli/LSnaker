using UnityEngine;
using System.Collections;

namespace LSnaker
{
    public class ViewFactory
    {
        public static bool EnableLog = false;

        private const string LOG_TAG = "ViewFactory";

        private static bool mIsInit = false;

        private static Transform mViewRoot;

        private static Recycler mRecycler;

        private static DictionaryEx<EntityObject, ViewObject> mObjectsMap;


        public static void Init(Transform viewRoot)
        {
            if(mIsInit)
            {
                return;
            }
            mViewRoot = viewRoot;
            mRecycler = new Recycler();
            mObjectsMap = new DictionaryEx<EntityObject, ViewObject>();

            mIsInit = true;
        }

        public static void Release()
        {
            mIsInit = false;
            foreach(var pair in mObjectsMap)
            {
                pair.Value.ReleaseInFactory();
                pair.Value.Dispose();
            }
            mObjectsMap.Clear();
            mRecycler.Release();
            mViewRoot = null;
        }

        public static void CreateView(string resPath, string resDefaultPath, EntityObject entity, Transform parent = null)
        {
            ViewObject obj = null;

            //
            string recycleType = resPath;
            bool userRecycler = true;

            obj = mRecycler.Pop(recycleType) as ViewObject;
            if(obj==null)
            {
                userRecycler = false;
                //TODO
                obj = InstanceViewFromPrefab(recycleType, resDefaultPath);
            }
            else
            {
                if(!obj.gameObject.activeSelf)
                {
                    obj.gameObject.SetActive(true);
                }

                if(parent!=null)
                {
                    obj.transform.SetParent(parent, false);
                }
                else
                {
                    obj.transform.SetParent(mViewRoot, false);
                }

                obj.CreateInFactory(entity,recycleType);

                if(EnableLog&& LDebugger.EnableLog)
                {
                    LDebugger.Log(LOG_TAG,"CreateView() {0}:{1}->{2}:{3},UseRecycler :{4}",
                                  entity.GetType().Name,
                                  entity.GetHashCode(),
                                  obj.GetRecycleType(),
                                  obj.GetInstanceID(),
                                  userRecycler);
                }
                if(mObjectsMap.ContainsKey(entity))
                {
                    LDebugger.LogError(LOG_TAG,"CreateView() 不应该存在重复的映射");
                }
                mObjectsMap[entity] = obj;
            }
        }

        public static void ReleaseView(EntityObject entity)
        {
            if(entity!=null)
            {
                ViewObject obj = mObjectsMap[entity];
                if(obj!=null)
                {
                    if(EnableLog&&LDebugger.EnableLog)
                    {
                        LDebugger.Log(LOG_TAG,"ReleaseView() {0}:{1}->{2}:{3}",
                                      entity.GetType().Name,
                                      entity.GetHashCode(),
                                      obj.GetRecycleType(),
                                      obj.GetInstanceID());
                    }

                    mObjectsMap.Remove(entity);
                    obj.ReleaseInFactory();
                    obj.gameObject.SetActive(false);
                    mRecycler.Push(obj);
                }
            }
        }

        private static ViewObject InstanceViewFromPrefab(string prefabName, string defaultPrefabName)
        {
            GameObject prefab = Resources.Load<GameObject>(prefabName);
            if(prefab==null)
            {
                prefab = Resources.Load<GameObject>(defaultPrefabName);
            }
            GameObject go = GameObject.Instantiate(prefab);
            ViewObject instance = go.GetComponent<ViewObject>();
            if(instance==null)
            {
                LDebugger.LogError(LOG_TAG,"InstanceViewFromPrefab() prefab="+prefabName );
            }
            return instance;
        }
    }
}