using UnityEngine;
using ProtoBuf;
using ProtoBuf.Meta;
using System.Collections;
using System;

namespace UnityEngine
{
    public class TestProtoBuff : MonoBehaviour
    {
        [ProtoContract]
        public class UserInfo
        {
            [ProtoMember(1)]
            public int id;

            [ProtoMember(2)]
            public string name;

            public string title;
        }

        // Use this for initialization
        void Start()
        {
            UserInfo userInfo = new UserInfo();
            userInfo.id= 0;
            userInfo.name = "Allenzwli";
            userInfo.title = "testNotPBMember";

            byte[] buff = PBSerializer.NSerialize(userInfo);

            var info = PBSerializer.NDeserialize<UserInfo>(buff);
            LDebugger.Log(this.GetType().ToString()," id: {0} ; name: {1}; title:{2};", info.id, info.name, info.title);
        }

    }

}