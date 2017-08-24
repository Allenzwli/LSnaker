using UnityEngine;
using System.Collections;
using ProtoBuf;
using LSnaker.Service.UserManager.Data;

namespace LSnaker
{
    [ProtoContract]
    public class GameConfig
    {
        [ProtoMember(1)]
        public UserData MainUserData = new UserData();

        [ProtoMember(2)]
        public bool EnableBgMusic = true;

        [ProtoMember(3)]
        public bool EnableSoundEffect = true;

        private static GameConfig mConfig = new GameConfig();

        public static GameConfig Config
        {
            get{
                return mConfig;
            }
        }

		#if UNITY_EDITOR
		public readonly static string Path=Application.persistentDataPath+"/GameConfig_Editor.data";
		#else
		public readonly static string Path=Application.persistentDataPath+"/GameConfig.data";
		#endif

		public static void Init()
		{
			LDebugger.Log ("GameConfig","Init() Path="+Path);
			byte[] data = FileUtils.ReadFile (Path);
			if (data != null && data.Length > 0) 
			{
				GameConfig gameConfig = (GameConfig)PBSerializer.NDeserialize (data,typeof(GameConfig));
				if (gameConfig != null) 
				{
					mConfig = gameConfig;
				}
			}
		}

		public static void Save()
		{
			LDebugger.Log ("GameConfig","Save() Value="+mConfig);
			if (mConfig != null) 
			{
				byte[] data = PBSerializer.NSerialize (mConfig);
				FileUtils.SaveFile(Path,data);
			}
		}
			
    }
}
