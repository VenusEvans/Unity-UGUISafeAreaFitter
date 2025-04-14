using System;
using UnityEditor;
using UnityEngine;

namespace Venus.SafeAreaFitter
{
    [CustomEditor(typeof(SafeAreaFitter))]
    public class SafeAreaFitterEditor : Editor
    {
        private Texture2D _lightIcon;
        private Texture2D _darkIcon;

        void OnEnable()
        {
            _lightIcon = EditorGUIUtility.IconContent("AspectRatioFitter Icon").image as Texture2D;
            _darkIcon = EditorGUIUtility.IconContent("d_AspectRatioFitter Icon").image as Texture2D;

            UpdateIcon();
        }

        private void UpdateIcon()
        {
            Texture2D icon = EditorGUIUtility.isProSkin ? _darkIcon : _lightIcon;
        
            if (icon != null)
            {
                EditorGUIUtility.SetIconForObject(target, icon);
            }
        }
        
        public override void OnInspectorGUI()
        {
            if (Event.current.type == EventType.Layout)
            {
                UpdateIcon();
            }
            base.OnInspectorGUI();
        }
    }
}