//using UnityEditor;
//using UnityEngine.UI;
//using UnityEngine;

//[CustomEditor(typeof(BoolVariable))]
//public class BoolVariableEditor : Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector(); // for other non-HideInInspector fields

//        BoolVariable script = (BoolVariable) target;

//        // draw checkbox for the bool
//        script.ResetValue = EditorGUILayout.Toggle("Start Temp", script.ResetValue);
//        if (script.ResetValue) // if bool is true, show other fields
//        {
//            var boolean = EditorGUILayout.ObjectField(source, typeof(bool), false);
//        }
//    }
//}
