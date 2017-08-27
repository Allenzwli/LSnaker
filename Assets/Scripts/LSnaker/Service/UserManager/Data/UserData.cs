using UnityEngine;
using System.Collections;
using ProtoBuf;

namespace LSnaker
{
    [ProtoContract]
    public class UserData
    {
        [ProtoMember(1)]
        public uint id;

        [ProtoMember(2)]
        public string name;

        [ProtoMember(3)]
        public int level;

        [ProtoMember(4)]
        public int defaultSnakeId;

    }
}