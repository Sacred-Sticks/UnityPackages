using System;
using Kickstarter.Events;
using Kickstarter.Extensions;
using Kickstarter.Identification;
using UnityEngine;

namespace Kickstarter.Progression
{
    [RequireComponent(typeof(Player))]
    public sealed class PlayerDataController : SerializationManager
    {
        [SerializeField] private SaveType saveType;

        public Transform SaveTarget { private get; set; }
        
        public class TransformData
        {
            public Vector3 Position { get; }
            public Quaternion Rotation { get; }
            public Vector3 LocalScale { get; }

            public TransformData(Vector3 position, Quaternion rotation, Vector3 localScale)
            {
                Position = position;
                Rotation = rotation;
                LocalScale = localScale;
            }
            
            private TransformData(){}

            public override string ToString()
            {
                return $"{Position}\n{Rotation}\n{LocalScale}";
            }

            public static bool TryParse(string input, out TransformData transformData)
            {
                transformData = new TransformData();
                string[] components = input.Split('\n');
                if (components.Length != 3)
                    return false;
                if (!Vector3Extensions.TryParse(components[0], out var position))
                    return false;
                if (!QuaternionExtensions.TryParse(components[1], out var rotation))
                    return false;
                if (!Vector3Extensions.TryParse(components[2], out var localScale))
                    return false;
                transformData = new TransformData(position, rotation, localScale);
                return true;
            }
        }

        public enum SaveType
        {
            Checkpoints,
            SaveStations,
            QuickSave,
        }

        private Player player;

        private void Awake()
        {
            player = GetComponent<Player>();

            SetupSaveType();

            AddData($"{player.PlayerID}.transform.sav", t =>
            {
                transform.position = t.Position;
                transform.rotation = t.Rotation;
                transform.localScale = t.LocalScale;
            } , () => new TransformData(SaveTarget.position, SaveTarget.rotation, transform.localScale));
        }

        private void SetupSaveType()
        {
            switch (saveType)
            {
                case SaveType.Checkpoints:
                    SetupCheckpointRegister();
                    break;
                case SaveType.SaveStations:
                    SetupSaveStations();
                    break;
                case SaveType.QuickSave:
                    SetupQuickSave();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(saveType));
            }

            void SetupCheckpointRegister()
            {
                var register = gameObject.AddComponent<CheckpointRegister>();
                register.PlayerDataController = this;
            }

            void SetupSaveStations()
            {

            }

            void SetupQuickSave()
            {

            }
        }

        private void Start()
        {
            LoadData();
        }

        public void SaveData()
        {
            SaveAll();
            EventManager.Trigger($"{player.PlayerID}", new SaveEvent());
        }

        public void LoadData()
        {
            LoadAll();
        }

        public class SaveEvent
        {
            
        }
    }
}
