using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

namespace NotEnoughBlendshape
{
    [CustomEditor(typeof(BlendshapeTransfer))]
    public class BlendshapeTransferEditor : Editor
    {
        private BlendshapeTransfer nebTool;

        private bool isFieldNotNull;
        private bool isFaceSourceExist;
        private bool isFaceTargetExist;
        private bool isSkinnedMeshSourceExist;
        private bool isSkinnedMeshTargetExist;
        private bool isModelDifferent;

        //Variables to help with displaying Result HelpBox
        private GameObject lastKnownSource;
        private GameObject lastKnownTarget;
        private int buttonResult = -1;
        private bool showResult = false;

        public override void OnInspectorGUI()
        {
            FieldValidation();

            DisplayGuideMessage();

            DisplaySourceField();
            DisplayTargetField();

            DisplayButton();

            //GUI.changed is only triggered when a field is changed or button is pressed.
            //Strangely enough, it doesn't get triggered if a field is emptied.
            DisplayResultHelpBox();
            if (GUI.changed)
            {
                CheckLastKnownFields();
                //Debug.Log("Updated Fields");
            }
        }

        /// <summary>
        /// All variables in this editor will return to default when the Editor lose focus (unselected in the Inspector)
        /// Specifically for this script, we need to maintain the value of lastKnownSource and lastKnownTarget
        /// so it can be displayed correctly, because they started as null. 
        /// </summary>
        public void OnEnable()
        {
            nebTool = (BlendshapeTransfer)target;
            CheckLastKnownFields();
        }

        /// <summary>
        /// Lists all possible combinations of Source and Target fields condition
        /// </summary>
        /// <param name="nebTool"></param>
        public void DisplayGuideMessage()
        {
            isFieldNotNull = (nebTool.sourceModel != null) && (nebTool.targetModel != null);
            if (isFieldNotNull == false)
            {
                EditorGUILayout.HelpBox("Drag in your Source Model and Target Model models into these two fields", MessageType.Warning);
                return;
            }

            if (isFaceSourceExist == false)
            {
                EditorGUILayout.HelpBox("Error! The Source Model doesn't have a Face child game object!\n" +
                                        "Make sure you drag in the model itself, not the Face gameobject!", MessageType.Error);
            }
            else if (isFaceTargetExist == false)
            {
                EditorGUILayout.HelpBox("Error! The Target Model doesn't have a Face child game object!\n" +
                                        "Make sure you drag in the model itself, not the Face gameobject!", MessageType.Error);
            }
            else
            {
                if (isSkinnedMeshSourceExist == false)
                {
                    EditorGUILayout.HelpBox("Error! The Source Model doesn't have a SkinnedMeshRenderer component on its Face gameobject!\n" +
                                            "Make sure you drag in a model that contains Face child gameobject with SkinnedMeshRenderer component!", MessageType.Error);
                }
                else if (isSkinnedMeshTargetExist == false)
                {
                    EditorGUILayout.HelpBox("Error! The Target Model doesn't have a SkinnedMeshRenderer component on its Face gameobject!\n" +
                                            "Make sure you drag in a model that contains Face child gameobject with SkinnedMeshRenderer component!", MessageType.Error);
                }
                else
                {
                    if (isModelDifferent == false)
                    {
                        EditorGUILayout.HelpBox("Error! Source Model and Target Model must be different!", MessageType.Error);
                    }
                    else
                    {
                        EditorGUILayout.HelpBox("Alright! Click on the button to copy blendshapes", MessageType.Info);
                    }
                }
            }
        }

        public void DisplaySourceField()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Source Model", GUILayout.Width(150f));
            nebTool.sourceModel = (GameObject)EditorGUILayout.ObjectField(nebTool.sourceModel, typeof(GameObject), true, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();
        }

        public void DisplayTargetField()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Target Model", GUILayout.Width(150f));
            nebTool.targetModel = (GameObject)EditorGUILayout.ObjectField(nebTool.targetModel, typeof(GameObject), true, GUILayout.ExpandWidth(true));
            EditorGUILayout.EndHorizontal();
        }

        public void FieldValidation()
        {
            //For Target Model Field
            if (nebTool.sourceModel != null)
            {
                Transform sourceFace = nebTool.sourceModel.transform.Find("Face");
                if (sourceFace != null)
                {
                    isFaceSourceExist = true;

                    if (sourceFace.GetComponent<SkinnedMeshRenderer>() != null)
                    {
                        isSkinnedMeshSourceExist = true;
                    }
                    else
                    {
                        isSkinnedMeshSourceExist = false;
                    }
                }
                else
                {
                    isFaceSourceExist = false;
                }
            }

            //For Target Model Field
            if (nebTool.targetModel != null)
            {
                Transform targetFace = nebTool.targetModel.transform.Find("Face");
                if (targetFace != null)
                {
                    isFaceTargetExist = true;

                    if (targetFace.GetComponent<SkinnedMeshRenderer>() != null)
                    {
                        isSkinnedMeshTargetExist = true;
                    }
                    else
                    {
                        isSkinnedMeshTargetExist = false;
                    }
                }
                else
                {
                    isFaceTargetExist = false;
                }
            }

            //For both fields
            if (isFieldNotNull)
            {
                if (CheckIsModelDifferent(nebTool.sourceModel, nebTool.targetModel))
                {
                    isModelDifferent = true;
                }
                else
                {
                    isModelDifferent = false;
                    return;
                }
            }
        }

        public void DisplayButton()
        {
            bool validCheck = isFieldNotNull && isFaceSourceExist &&
                              isFaceTargetExist && isSkinnedMeshSourceExist &&
                              isSkinnedMeshTargetExist && isModelDifferent;

            EditorGUI.BeginDisabledGroup(!(validCheck));
            {
                if (GUILayout.Button("Copy Blendshape", GUILayout.Height(50)))
                {
                    buttonResult = nebTool.CopyBlendshape();
                }
            }
            EditorGUI.EndDisabledGroup();
        }

        private void DisplayResultHelpBox()
        {
            if (nebTool.sourceModel == null || nebTool.targetModel == null)
            {
                buttonResult = -1;
                return;
            }

            if (showResult == true)
            {
                if (buttonResult == 1)
                {
                    EditorGUILayout.HelpBox("Success! The blendshapes have been copied!", MessageType.Info);
                }
                else if (buttonResult == 0)
                {
                    EditorGUILayout.HelpBox("\nError! The number of Source and Target models vertices don't match! " +
                                            "This is either because the model was exported from VRoid Studio with \"Delete Transparent Meshes\" option checked. " +
                                            "This option is enabled by default in the \"Reduce Polygon\" export settings. " +
                                            "Make sure to uncheck it first and export the model again into this Unity project! \n\n" +
                                            "Alternatively, your model might have \"Reduce Polygon\" applied on Face slider in the Export setting in VRoid Studio. " +
                                            "You can't apply any polygon reduction specifically on Face sliders!\n", MessageType.Error);
                }
            }
        }

        /// <summary>
        /// Check if there's any change in the Source and Target fields to display the Result HelpBox
        /// </summary>
        /// <param name="nebTool"></param>
        private void CheckLastKnownFields()
        {
            if (lastKnownSource == nebTool.sourceModel && lastKnownTarget == nebTool.targetModel)
            {
                showResult = true;
            }
            else
            {
                showResult = false;
                buttonResult = -1;
            }

            lastKnownSource = nebTool.sourceModel;
            lastKnownTarget = nebTool.targetModel;
        }

        /// <summary>
        /// Compare two gameobjects. This also compares if the same object is the 
        /// instanced model or asset model in the Project Window.
        /// </summary>
        /// <param name="model1"></param>
        /// <param name="model2"></param>
        /// <returns>True if the model is the same. Returns false otherwise.</returns>
        public bool CheckIsModelDifferent(GameObject model1, GameObject model2)
        {
            if (model1 == null || model2 == null)
            {
                return true;
            }

            if (model1 == model2)
            {
                return false;
            }

            string prefabAssetPath1 = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(model1);
            string prefabAssetPath2 = PrefabUtility.GetPrefabAssetPathOfNearestInstanceRoot(model2);

            if (prefabAssetPath1 == null || prefabAssetPath2 == null)
            {
                return true;
            }

            if (prefabAssetPath1 == prefabAssetPath2)
            {
                return false;
            }

            return true;
        }
    }
}
#endif