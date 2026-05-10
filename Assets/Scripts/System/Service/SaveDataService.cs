using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Model;
using UnityEngine;
using UnityEngine.UIElements;

namespace SaveData
{
    public sealed class SaveDataService : ISaveDataService<PlayerBase>
    {
        private readonly IData<SavedData> _data;

        private const string _folderName = "dataSave";
        private const string _fileName = "save.xml";
        private readonly string _path;


        public SaveDataService(IData<SavedData> data)
        {
            _data = data;
            _path = Path.Combine(Application.dataPath, _folderName);
        }

        public void Save(PlayerBase player)
        {
            if (!Directory.Exists(_path)) Directory.CreateDirectory(_path);

            var savePlayer = new SavedData
            {
                Position = player.transform.position,
                Name = player.name,
                IsEnabled = true,
                Health = player._health
            };
            _data.OnSave(savePlayer, Path.Combine(_path, _fileName));
        }

        public void Load(PlayerBase player)
        {
            var file = Path.Combine(_path, _fileName);
            if (!File.Exists(file)) return;

            var loadedData = _data.OnLoad(file);
            if (loadedData != null)
            {
                player.transform.position = loadedData.Position;

                var playerBall = player.GetComponent<PlayerBall>();
                if (playerBall != null)
                {
                    playerBall.SetHealth((int)loadedData.Health);
                }
            }
        }



    }

}

