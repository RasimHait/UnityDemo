using Project.View;
using UnityEngine;

namespace Project.Data
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Project/LevelSettings")]
    public class LevelStaticData : ScriptableObject
    {
        [field: SerializeField] public GameFieldView FieldViewPrefab { get; private set; }
        [field: SerializeField] public CubeView CubeViewPrefab { get; private set; }
    }
}
