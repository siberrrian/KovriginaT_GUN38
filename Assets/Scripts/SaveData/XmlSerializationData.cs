using System.IO;
using System.Xml.Serialization;
using UnityEngine;

namespace SaveData
{
    public class XmlSerializationData<T> : IData<T>
    {
        private readonly XmlSerializer _serializer;

        public XmlSerializationData()
        {
            _serializer = new XmlSerializer(typeof(T));
        }

        public void OnSave(T data, string path = null)
        {
            if (data == null || string.IsNullOrEmpty(path)) return;

            using var fs = new FileStream(path, FileMode.Create);
            _serializer.Serialize(fs, data);
            Debug.Log($"[XML] Сохранено в: {path}");
        }

        public T OnLoad(string path = null)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path)) return default;

            using var fs = new FileStream(path, FileMode.Open);
            return (T)_serializer.Deserialize(fs);
        }
    }
}
