using UnityEngine;
using UnityEditor;

namespace OceanToolkit
{
    [CustomEditor(typeof(CausticsImageEffect))]
    public class CausticsImageEffectEditor : Editor
    {
        protected static float Zero = 0.0f;
        protected static float MaxAngle = 360.0f;
        protected static float MaxSpeed = 20.0f;

        public static bool showPattern0;
        public static bool showPattern1;

        public override void OnInspectorGUI()
        {
            CausticsImageEffect o = (CausticsImageEffect)target;

            o.CausticsMaterial = (Material)EditorGUILayout.ObjectField("Caustics Material", o.CausticsMaterial, typeof(Material), false);
            o.SunLight = (Light)EditorGUILayout.ObjectField("Sun Light", o.SunLight, typeof(Light), true);

            showPattern0 = EditorGUILayout.Foldout(showPattern0, "Pattern 0");
            if (showPattern0)
            {
                o.PatternAngle0 = EditorGUILayout.Slider("Angle", o.PatternAngle0, Zero, MaxAngle);
                o.PatternSpeed0 = EditorGUILayout.Slider("Speed", o.PatternSpeed0, Zero, MaxSpeed);
            }

            showPattern1 = EditorGUILayout.Foldout(showPattern1, "Pattern 1");
            if (showPattern1)
            {
                o.PatternAngle1 = EditorGUILayout.Slider("Angle", o.PatternAngle1, Zero, MaxAngle);
                o.PatternSpeed1 = EditorGUILayout.Slider("Speed", o.PatternSpeed1, Zero, MaxSpeed);
            }

            // Handle changes
            if (GUI.changed)
            {
                EditorUtility.SetDirty(o);
            }
        }
    }
}