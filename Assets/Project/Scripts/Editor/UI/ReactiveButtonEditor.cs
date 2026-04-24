using Project.UI;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;

namespace Project.Editor
{
    [CustomEditor(typeof(ReactiveButton))]
    public class ReactiveButtonEditor : ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var button = (ReactiveButton)target;

            button.Tag = EditorGUILayout.TextField("Tag", button.Tag);

            if (GUI.changed)
            {
                EditorUtility.SetDirty(target);
            }
        }
    }
}
