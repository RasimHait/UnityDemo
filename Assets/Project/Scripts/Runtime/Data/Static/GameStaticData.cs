using Project.UI;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using System;

namespace Project.Data
{

    [Serializable]
    public class CubeViewVariant
    {
        public Texture Texture;
        public Color Color;
    }

    [CreateAssetMenu(fileName = "StaticDataCollection", menuName = "Project/StaticDataCollection")]
    public class GameStaticData : ScriptableObject
    {
        [field: SerializeField, TabGroup("Scenes")] public bool RunFromInitialScene { get; private set; } = true;
        [field: SerializeField, TabGroup("Scenes")] public int InitialSceneIndex { get; private set; } = 0;
        [field: SerializeField, TabGroup("Scenes")] public int MainSceneIndex { get; private set; } = 1;
        [field: SerializeField, TabGroup("Input")] public float DragDeadZone { get; private set; } = 0.5f;
        [field: SerializeField, TabGroup("Input")] public float DragSensitivity { get; private set; } = 50f;
        [field: SerializeField, TabGroup("Input")] public float DragSmoothTime { get; private set; } = 0.5f;
        [field: SerializeField, TabGroup("Levels")] public int LoopFromLevelIndex { get; private set; } = 0;
        [field: SerializeField, TabGroup("Levels")] public List<LevelStaticData> Levels { get; private set; } = new();
        [field: SerializeField, TabGroup("Prefabs")] public GameHUD GameHUDPrefab { get; private set; }
        [field: SerializeField, TabGroup("Prefabs")] public ParticlesView MergeVFXPrefab { get; private set; }
        [field: SerializeField, TabGroup("Cubes")] public SerializedDictionary<int, CubeViewVariant> CubeTextures { get; private set; }
    }
}
