using System;
using System.Collections.Generic;

namespace UnityEngine
{
    public class DictionaryEx<TKey,TValue>:Dictionary<TKey,TValue>
    {
        public new TValue this[TKey indexKey]
        {
            set
            {
                base[indexKey] = value;   
            }

            get
            {
                try
                {
                    return base[indexKey];
                }
                catch(Exception e)
                {
                    return default(TValue);
                }
            }
        }
    }
}
