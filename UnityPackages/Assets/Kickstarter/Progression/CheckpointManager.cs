using System;
using System.Linq;
using Kickstarter.Categorization;
using Kickstarter.Events;
using Kickstarter.Extensions;
using Kickstarter.Identification;
using UnityEngine;

namespace Kickstarter.Progression
{
    [RequireComponent(typeof(Player))]
    public sealed class CheckpointManager : SerializationManager
    {
        [SerializeField] private CategoryType checkpointType;
        private Transform currentCheckpoint;
        
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

        private Player player;

        private void Awake()
        {
            player = GetComponent<Player>();
            AddData($"{player.PlayerID}.transform.sav", t =>
            {
                transform.position = t.Position;
                transform.rotation = t.Rotation;
                transform.localScale = t.LocalScale;
            } , () => new TransformData(currentCheckpoint.position, currentCheckpoint.rotation, transform.localScale));
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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out ObjectCategories categories))
                return;
            if (!categories.Categories.Contains(checkpointType))
                return;
            currentCheckpoint = other.gameObject.transform;
            SaveData();
        }

        public class SaveEvent
        {
            
        }
    }
}
