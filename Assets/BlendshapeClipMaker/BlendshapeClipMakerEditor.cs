using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

#if UNITY_EDITOR
using UnityEditor;

namespace NotEnoughBlendshape
{
    [CustomEditor(typeof(BlendshapeClipMaker))]
    public class BlendshapeClipMakerEditor : Editor
    {
        private BlendshapeClipMaker clipMaker;

        private bool isFieldNotNull;
        private bool isVRMBlendShapeProxyExist;
        private bool isBlendshapeAvatarExist;
        private int buttonResult = -1;

        public override void OnInspectorGUI()
        {
            FieldValidation();

            DisplayToolPurpose();

            DisplayGuideMessage();

            DisplayModelField();

            DisplayButton();

            DisplayResultHelpBox();
        }

        public void OnEnable()
        {
            clipMaker = (BlendshapeClipMaker)target;
        }

        public void DisplayToolPurpose()
        {
            EditorGUILayout.HelpBox("This tool will create all BlendshapeClips for all blendshapes in your character. " +
                                    "Make sure you do this just before you export your model.", MessageType.Info);
        }

        /// <summary>
        /// Lists all possible combinations of Source and Target fields condition
        /// </summary>
        /// <param name="nebTool"></param>
        public void DisplayGuideMessage()
        {
            if (isFieldNotNull == false)
            {
                EditorGUILayout.HelpBox("Drag in your Model into this field.", MessageType.Warning);
                return;
            }

            if (isVRMBlendShapeProxyExist == false)
            {
                EditorGUILayout.HelpBox("Error! This model doesn't have a VRMBlendShapeProxy component!\n" +
                                        "Make sure you drag in a VRoid gameobject with VRMBlendshapeProxy component!", MessageType.Error);
                return;
            }

            if (isBlendshapeAvatarExist == false)
            {
                EditorGUILayout.HelpBox("\nError! This model's VRMBlendshapeProxy doesn't have a BlendshapeAvatar assigned to it!\n" +
                                        "Make sure you drag in a BlendshapeAvatar into the VRMBlendshapeProxy component!\n\n" +
                                        "You can create BlendshapeAvatar by right clicking in Project Window > Create > VRM > BlendshapeAvatar.\n", MessageType.Error);
                return;
            }

            EditorGUILayout.HelpBox("Alright! Click on the button to create your BlendshapeClips! You can expect some wait times after the button is pressed", MessageType.Info);
        }

        public void DisplayModelField()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Model", GUILayout.Width(150f));
            clipMaker.model = (GameObject)EditorGUILayout.ObjectField(clipMaker.model, typeof(GameObject), true, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();
        }

        public void FieldValidation()
        {
            //Find if the model exist
            if (clipMaker.model == null || clipMaker.model.gameObject == null)
            {
                isFieldNotNull = false;
                return;
            }
            isFieldNotNull = true;

            //Find if the BlendshapeProxy component exists
            VRM.VRMBlendShapeProxy blendShapeProxy = clipMaker.model.GetComponent<VRM.VRMBlendShapeProxy>();
            if (blendShapeProxy == null)
            {
                isVRMBlendShapeProxyExist = false;
                return;
            }
            isVRMBlendShapeProxyExist = true;

            //Find if a BlendshapeAvatar is already assigned in the component
            if (blendShapeProxy.BlendShapeAvatar == null)
            {
                isBlendshapeAvatarExist = false;
                return;
            }
            isBlendshapeAvatarExist = true;
        }

        public void DisplayButton()
        {
            bool validCheck = isFieldNotNull && isVRMBlendShapeProxyExist && isBlendshapeAvatarExist;
            EditorGUI.BeginDisabledGroup(!(validCheck));
            {
                if (GUILayout.Button("Create BlendshapeClips", GUILayout.Height(50)))
                {
                    buttonResult = clipMaker.SaveBlendshapeClips();
                }
            }
            EditorGUI.EndDisabledGroup();
        }

        public void DisplayResultHelpBox()
        {
            if (buttonResult == 1)
            {
                EditorGUILayout.HelpBox("Success! The blendshapeClips have been created! " +
                                        "\nOpen the BlendshapeAvatar file in Project Window to check it out or open your Blendshapes folder in Project Window!", MessageType.Info);
            }
            else if (buttonResult == 0)
            {
                EditorGUILayout.HelpBox("Error!", MessageType.Error);
            }
        }
    }
}
#endif