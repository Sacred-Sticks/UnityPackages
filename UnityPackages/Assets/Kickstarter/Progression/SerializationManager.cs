using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Kickstarter.Extensions;
using UnityEngine;

namespace Kickstarter.Progression
{
    public static class SerializationManager
    {
        public static void SaveData<TDataType>(TDataType data, string fileName)
        {
            string filePath = Path.Combine(Application.persistentDataPath, fileName);
            var formatter = new BinaryFormatter();
            var stream = new FileStream(filePath, FileMode.Create);

            formatter.Serialize(stream, data.ToString());
            stream.Close();
        }

        public static bool LoadData<TDataType>(string fileName, out TDataType output)
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

        public static TDataType ConvertToDataType<TDataType>(string dataString)
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
            }
            return data;
        }
    }
}
