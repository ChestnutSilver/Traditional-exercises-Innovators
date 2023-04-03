using UnityEngine;
using UnityEditor;
using System;

namespace OceanToolkit
{
    public enum Refl
    {
        Off, SkyOnly, ScreenSpaceRaytracing
    }

    public enum Refr
    {
        Off, Color, NormalOffset
    }

    public class OceanShaderEditor : ShaderGUI
    {
        protected static string ReflOff = "OT_REFL_OFF";
        protected static string ReflSkyOnly = "OT_REFL_SKY_ONLY";
        protected static string ReflSSR = "OT_REFL_SSR";

        protected static string RefrOff = "OT_REFR_OFF";
        protected static string RefrColor = "OT_REFR_COLOR";
        protected static string RefrNormalOffset = "OT_REFR_NORMAL_OFFSET";

        public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] properties)
        {
            Material m = (Material)materialEditor.target;

            // Check shader properties
            Refl refl = Refl.Off;

            if (Array.IndexOf(m.shaderKeywords, ReflOff) != -1)
            {
                refl = Refl.Off;
            }
            else if (Array.IndexOf(m.shaderKeywords, ReflSkyOnly) != -1)
            {
                refl = Refl.SkyOnly;
            }
            else if (Array.IndexOf(m.shaderKeywords, ReflSSR) != -1)
            {
                refl = Refl.ScreenSpaceRaytracing;
            }

            Refr refr = Refr.Off;

            if (Array.IndexOf(m.shaderKeywords, RefrOff) != -1)
            {
                refr = Refr.Off;
            }
            else if (Array.IndexOf(m.shaderKeywords, RefrColor) != -1)
            {
                refr = Refr.Color;
            }
            else if (Array.IndexOf(m.shaderKeywords, RefrNormalOffset) != -1)
            {
                refr = Refr.NormalOffset;
            }

            // Prepare for changes
            EditorGUI.BeginChangeCheck();
            
            refl = (Refl)EditorGUILayout.EnumPopup("Reflection", refl);
            refr = (Refr)EditorGUILayout.EnumPopup("Refraction", refr);

            // Handle changes
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(m, "Ocean Material Setting");

                if (refl == Refl.Off)
                {
                    m.EnableKeyword(ReflOff);
                    m.DisableKeyword(ReflSkyOnly);
                    m.DisableKeyword(ReflSSR);
                }
                else if (refl == Refl.SkyOnly)
                {
                    m.DisableKeyword(ReflOff);
                    m.EnableKeyword(ReflSkyOnly);
                    m.DisableKeyword(ReflSSR);
                }
                else if (refl == Refl.ScreenSpaceRaytracing)
                {
                    m.DisableKeyword(ReflOff);
                    m.DisableKeyword(ReflSkyOnly);
                    m.EnableKeyword(ReflSSR);
                }

                if (refr == Refr.Off)
                {
                    m.EnableKeyword(RefrOff);
                    m.DisableKeyword(RefrColor);
                    m.DisableKeyword(RefrNormalOffset);
                }
                else if (refr == Refr.Color)
                {
                    m.DisableKeyword(RefrOff);
                    m.EnableKeyword(RefrColor);
                    m.DisableKeyword(RefrNormalOffset);
                }
                else if (refr == Refr.NormalOffset)
                {
                    m.DisableKeyword(RefrOff);
                    m.DisableKeyword(RefrColor);
                    m.EnableKeyword(RefrNormalOffset);
                }

                EditorUtility.SetDirty(m);
            }            

            base.OnGUI(materialEditor, properties);
        }
    }
}