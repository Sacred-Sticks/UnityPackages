using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Kickstarter.Progression
{
    public class DataManager : MonoBehaviour
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

        private readonly Dictionary<Type, List<Datapoint>> allData = new Dictionary<Type, List<Datapoint>>();

        public void AddData<TDataType>(string fileLocation, Action<TDataType> loadData, Func<TDataType> saveData) where TDataType : struct
        {
            if (!allData.ContainsKey(typeof(TDataType)))
                allData.Add(typeof(TDataType), new List<Datapoint>());

            allData[typeof(TDataType)].Add(new Datapoint<TDataType>(fileLocation, loadData, saveData));
        }

        public void RemoveData<TDataType>(string fileLocation) where TDataType : struct
        {
            if (!allData.ContainsKey(typeof(TDataType)))
                return;
            var dataset = allData[typeof(TDataType)];
            var dataElement = dataset.FirstOrDefault(d => d.FileLocation == fileLocation);
            if (!dataset.Contains(dataElement))
                return;
            dataset.Remove(dataElement);
        }

        public void SaveAll()
        {
            allData.SelectMany(d => d.Value)
                .ToList()
                .ForEach(d =>
                {
                    switch (d)
                    {
                        case Datapoint<string> datapoint:
                            SerializationManager.SaveData(datapoint.Data, datapoint.FileLocation);
                            break;
                        case Datapoint<bool> datapoint:
                            SerializationManager.SaveData(datapoint.Data, datapoint.FileLocation);
                            break;
                        case Datapoint<int> datapoint:
                            SerializationManager.SaveData(datapoint.Data, datapoint.FileLocation);
                            break;
                        case Datapoint<float> datapoint:
                            SerializationManager.SaveData(datapoint.Data, datapoint.FileLocation);
                            break;
                        case Datapoint<double> datapoint:
                            SerializationManager.SaveData(datapoint.Data, datapoint.FileLocation);
                            break;
                        case Datapoint<Vector2> datapoint:
                            SerializationManager.SaveData(datapoint.Data, datapoint.FileLocation);
                            break;
                        case Datapoint<Vector3> datapoint:
                            SerializationManager.SaveData(datapoint.Data, datapoint.FileLocation);
                            break;
                        case Datapoint<Quaternion> datapoint:
                            SerializationManager.SaveData(datapoint.Data, datapoint.FileLocation);
                            break;
                    }
                });
        }

        public void LoadAll()
        {
            allData.SelectMany(d => d.Value)
                .ToList()
                .ForEach(d =>
                {
                    switch (d)
                    {
                        case Datapoint<string> datapoint:
                        {
                            if (SerializationManager.LoadData(datapoint.FileLocation, out string data))
                                datapoint.LoadData?.Invoke(data);
                            break;
                        }
                        case Datapoint<bool> datapoint:
                        {
                            if (SerializationManager.LoadData(datapoint.FileLocation, out bool data))
                                datapoint.LoadData?.Invoke(data);
                            break;
                        }
                        case Datapoint<int> datapoint:
                        {
                            if (SerializationManager.LoadData(datapoint.FileLocation, out int data))
                                datapoint.LoadData?.Invoke(data);
                            break;
                        }
                        case Datapoint<float> datapoint:
                        {
                            if (SerializationManager.LoadData(datapoint.FileLocation, out float data))
                                datapoint.LoadData?.Invoke(data);
                            break;
                        }
                        case Datapoint<double> datapoint:
                        {
                            if (SerializationManager.LoadData(datapoint.FileLocation, out double data))
                                datapoint.LoadData?.Invoke(data);
                            break;
                        }
                        case Datapoint<Vector2> datapoint:
                        {
                            if (SerializationManager.LoadData(datapoint.FileLocation, out Vector2 data))
                                datapoint.LoadData?.Invoke(data);
                            break;
                        }
                        case Datapoint<Vector3> datapoint:
                        {
                            if (SerializationManager.LoadData(datapoint.FileLocation, out Vector3 data))
                                datapoint.LoadData?.Invoke(data);
                            break;
                        }
                        case Datapoint<Quaternion> datapoint:
                        {
                            if (SerializationManager.LoadData(datapoint.FileLocation, out Quaternion data))
                                datapoint.LoadData?.Invoke(data);
                            break;
                        }
                    }
                });
        }
    }
}
