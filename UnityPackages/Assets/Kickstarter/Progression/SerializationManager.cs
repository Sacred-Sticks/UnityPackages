using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Kickstarter.Extensions;
using UnityEngine;

namespace Kickstarter.Progression
{
    public abstract class SerializationManager : MonoBehaviour
    {
        private class Datapoint
        {
            public string FileLocation { get; }

            public Datapoint(string fileLocation)
            {
                FileLocation = fileLocation;
            }
        }
        private class Datapoint<TDataType> : Datapoint
        {
            public TDataType Data
            {
                get
                {
                    return SaveData.Invoke();
                }
            }
            public Action<TDataType> LoadData { get; }
            private Func<TDataType> SaveData { get; }

            public Datapoint(string fileLocation, Action<TDataType> loadData, Func<TDataType> saveData) : base(fileLocation)
            {
                LoadData = loadData;
                SaveData = saveData;
            }
        }
        
        private readonly List<Datapoint> allData = new List<Datapoint>();
        
        private void SaveData<TDataType>(TDataType data, string fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            var formatter = new BinaryFormatter();
            var stream = new FileStream(filePath, FileMode.Create);

            formatter.Serialize(stream, data.ToString());
            stream.Close();
        }

        private bool LoadData<TDataType>(string fileName, out TDataType output)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            if (!File.Exists(filePath))
            {
                output = default;
                return false;
            }
            var formatter = new BinaryFormatter();
            var stream = new FileStream(filePath, FileMode.Open);

            output = ConvertToDataType<TDataType>((string)formatter.Deserialize(stream));
            stream.Close();

            return true;
        }

        private TDataType ConvertToDataType<TDataType>(string dataString)
        {
            var data = default(TDataType);
            switch (typeof(TDataType))
            {
                case {} type when type == typeof(string):
                    data = (TDataType)(object)dataString;
                    break;
                case {} type when type == typeof(bool):
                    if (bool.TryParse(dataString, out bool boolValue))
                        data = (TDataType)(object)boolValue;
                    break;
                case {} type when type == typeof(int):
                    if (int.TryParse(dataString, out int intValue))
                        data = (TDataType)(object)intValue;
                    break;
                case {} type when type == typeof(float):
                    if (float.TryParse(dataString, out float floatValue))
                        data = (TDataType)(object)floatValue;
                    break;
                case {} type when type == typeof(double):
                    if (double.TryParse(dataString, out double doubleValue))
                        data = (TDataType)(object)doubleValue;
                    break;
                case {} type when type == typeof(Vector2):
                    if (Vector2Extensions.TryParse(dataString, out var vector2Value))
                        data = (TDataType)(object)vector2Value;
                    break;
                case {} type when type == typeof(Vector3):
                    if (Vector3Extensions.TryParse(dataString, out var vector3Value))
                        data = (TDataType)(object)vector3Value;
                    break;
                case {} type when type == typeof(Quaternion):
                    if (QuaternionExtensions.TryParse(dataString, out var quaternionValue))
                        data = (TDataType)(object)quaternionValue;
                    break;
                case {} type when type == typeof(CheckpointManager.TransformData):
                    if (CheckpointManager.TransformData.TryParse(dataString, out var transformDataValue))
                        data = (TDataType)(object)transformDataValue;
                    break;
            }
            return data;
        }

        protected void AddData<TDataType>(string fileLocation, Action<TDataType> loadData, Func<TDataType> saveData)
        {
            var matchingLocation = allData.Where(d => d.FileLocation == fileLocation);
            if (matchingLocation.Any())
                return;
            allData.Add(new Datapoint<TDataType>(fileLocation, loadData, saveData));
        }

        protected void RemoveData<TDataType>(string fileLocation)
        {
            var matchingLocation = allData.Where(d => d.FileLocation == fileLocation)
                .ToArray();
            if (matchingLocation.Length == 0)
                return;
            var datapoint = matchingLocation.First();
            allData.Remove(datapoint);
        }

        protected void SaveAll()
        {
            allData.ForEach(d =>
            {
                switch (d)
                {
                    case Datapoint<string> datapoint:
                        SaveData(datapoint.Data, datapoint.FileLocation);
                        break;
                    case Datapoint<bool> datapoint:
                        SaveData(datapoint.Data, datapoint.FileLocation);
                        break;
                    case Datapoint<int> datapoint:
                        SaveData(datapoint.Data, datapoint.FileLocation);
                        break;
                    case Datapoint<float> datapoint:
                        SaveData(datapoint.Data, datapoint.FileLocation);
                        break;
                    case Datapoint<double> datapoint:
                        SaveData(datapoint.Data, datapoint.FileLocation);
                        break;
                    case Datapoint<Vector2> datapoint:
                        SaveData(datapoint.Data, datapoint.FileLocation);
                        break;
                    case Datapoint<Vector3> datapoint:
                        SaveData(datapoint.Data, datapoint.FileLocation);
                        break;
                    case Datapoint<Quaternion> datapoint:
                        SaveData(datapoint.Data, datapoint.FileLocation);
                        break;
                    case Datapoint<CheckpointManager.TransformData> dataPoint:
                        SaveData(dataPoint.Data, dataPoint.FileLocation);
                        break;
                }
            });
        }

        protected void LoadAll()
        {
            allData.ForEach(d =>
            {
                switch (d)
                {
                    case Datapoint<string> datapoint:
                    {
                        if (LoadData(datapoint.FileLocation, out string data))
                            datapoint.LoadData?.Invoke(data);
                        break;
                    }
                    case Datapoint<bool> datapoint:
                    {
                        if (LoadData(datapoint.FileLocation, out bool data))
                            datapoint.LoadData?.Invoke(data);
                        break;
                    }
                    case Datapoint<int> datapoint:
                    {
                        if (LoadData(datapoint.FileLocation, out int data))
                            datapoint.LoadData?.Invoke(data);
                        break;
                    }
                    case Datapoint<float> datapoint:
                    {
                        if (LoadData(datapoint.FileLocation, out float data))
                            datapoint.LoadData?.Invoke(data);
                        break;
                    }
                    case Datapoint<double> datapoint:
                    {
                        if (LoadData(datapoint.FileLocation, out double data))
                            datapoint.LoadData?.Invoke(data);
                        break;
                    }
                    case Datapoint<Vector2> datapoint:
                    {
                        if (LoadData(datapoint.FileLocation, out Vector2 data))
                            datapoint.LoadData?.Invoke(data);
                        break;
                    }
                    case Datapoint<Vector3> datapoint:
                    {
                        if (LoadData(datapoint.FileLocation, out Vector3 data))
                            datapoint.LoadData?.Invoke(data);
                        break;
                    }
                    case Datapoint<Quaternion> datapoint:
                    {
                        if (LoadData(datapoint.FileLocation, out Quaternion data))
                            datapoint.LoadData?.Invoke(data);
                        break;
                    }
                    case Datapoint<CheckpointManager.TransformData> dataPoint:
                    {
                        if (LoadData(dataPoint.FileLocation, out CheckpointManager.TransformData data))
                            dataPoint.LoadData?.Invoke(data);
                        break;
                    }
                }
            });
        }
    }
}
