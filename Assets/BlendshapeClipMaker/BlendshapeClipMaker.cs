using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
using System.Reflection;

namespace NotEnoughBlendshape
{
    public class BlendshapeClipMaker : MonoBehaviour
    {
        public GameObject model;
        public VRM.VRMBlendShapeProxy blendshapeProxy;

        public int SaveBlendshapeClips()
        {
            blendshapeProxy = model.GetComponent<VRM.VRMBlendShapeProxy>();
            string avatarPath = AssetDatabase.GetAssetPath(blendshapeProxy.BlendShapeAvatar);
            string folderPath = Path.GetDirectoryName(avatarPath);

            List<VRM.BlendShapeClip> blendShapeClipList = new List<VRM.BlendShapeClip>();

            // Iterate through the blendshapes in the SkinnedMeshRenderer
            Mesh mesh = model.transform.Find("Face").GetComponent<SkinnedMeshRenderer>().sharedMesh;
            for (int i = 0; i < mesh.blendShapeCount; i++)
            {
                string blendshapeName = mesh.GetBlendShapeName(i); //NormalizedName(mesh.GetBlendShapeName(i));

                VRM.BlendShapeClip newBlendshapeClip = VRM.BlendShapeAvatar.CreateBlendShapeClip(folderPath + "\\" + blendshapeName + ".asset");

                // Set BlendShapeName and IsBinary as needed
                newBlendshapeClip.name = blendshapeName;
                newBlendshapeClip.Preset = VRM.BlendShapePreset.Unknown;
                newBlendshapeClip.IsBinary = false;

                // For special VSeeFace blendshapes, set their preset to be specific to it.
                BlendshapePresetSetter(newBlendshapeClip);

                // Create a BlendShapeBinding for this blendshape, which contains data on which blendshape is manipulated
                VRM.BlendShapeBinding[] blendshapeBinding = new VRM.BlendShapeBinding[1];
                VRM.BlendShapeBinding binding = new VRM.BlendShapeBinding
                {
                    RelativePath = "Face",  // Use an empty string for the root, for UniVRM it's usually Face
                    Index = i,              // Index of the blendshape
                    Weight = 100f           // Initial weight (by default in UniVRM, it's 100)
                };
                blendshapeBinding[0] = binding;
                newBlendshapeClip.Values = blendshapeBinding;

                blendShapeClipList.Add(newBlendshapeClip);
            }

            //Add everything into the BlendshapeAvatar
            blendshapeProxy.BlendShapeAvatar.Clips.Clear();
            for (int i = 0; i < blendShapeClipList.Count; i++)
            {
                if (blendShapeClipList[i] != null)
                    blendshapeProxy.BlendShapeAvatar.Clips.Add(blendShapeClipList[i]);
            }

            //Function runs succesfully
            return 1;
        }

        /// <summary>
        /// Set some special blendshapes preset to be specific kind.
        /// </summary>
        /// <param name="newBlendshapeClip"></param>
        private void BlendshapePresetSetter(VRM.BlendShapeClip newBlendshapeClip)
        {
            if (newBlendshapeClip.name == "Fcl_MTH_A") newBlendshapeClip.Preset = VRM.BlendShapePreset.A;
            if (newBlendshapeClip.name == "Fcl_MTH_E") newBlendshapeClip.Preset = VRM.BlendShapePreset.E;
            if (newBlendshapeClip.name == "Fcl_MTH_I") newBlendshapeClip.Preset = VRM.BlendShapePreset.I;
            if (newBlendshapeClip.name == "Fcl_MTH_O") newBlendshapeClip.Preset = VRM.BlendShapePreset.O;
            if (newBlendshapeClip.name == "Fcl_MTH_U") newBlendshapeClip.Preset = VRM.BlendShapePreset.U;

            if (newBlendshapeClip.name == "Fcl_ALL_Angry") newBlendshapeClip.Preset = VRM.BlendShapePreset.Angry;
            if (newBlendshapeClip.name == "Fcl_ALL_Fun") newBlendshapeClip.Preset = VRM.BlendShapePreset.Fun;
            if (newBlendshapeClip.name == "Fcl_ALL_Joy") newBlendshapeClip.Preset = VRM.BlendShapePreset.Joy;
            if (newBlendshapeClip.name == "Fcl_ALL_Neutral") newBlendshapeClip.Preset = VRM.BlendShapePreset.Neutral;
            if (newBlendshapeClip.name == "Fcl_ALL_Sorrow") newBlendshapeClip.Preset = VRM.BlendShapePreset.Sorrow;

            if (newBlendshapeClip.name == "Fcl_EYE_Close") newBlendshapeClip.Preset = VRM.BlendShapePreset.Blink;
            if (newBlendshapeClip.name == "Fcl_EYE_Close_L") newBlendshapeClip.Preset = VRM.BlendShapePreset.Blink_L;
            if (newBlendshapeClip.name == "Fcl_EYE_Close_R") newBlendshapeClip.Preset = VRM.BlendShapePreset.Blink_R;
        }


        /// <summary>
        /// Normalize the names of every blendshapes so it's easier to read
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string NormalizedName(string input)
        {
            // Step 1: Replace any '_' with space
            string step1Result = input.Replace('_', ' ');

            // Step 2: Remove any 'Fcl_' (replace with blank)
            string step2Result = step1Result.Replace("Fcl", "");

            // Step 3: Replace "MTH " with "mouth"
            string step3Result = step2Result.Replace("MTH ", "mouth");

            // Step 4: Replace "BRW " with "brow"
            string step4Result = step3Result.Replace("BRW ", "brow");

            // Step 5: Replace "EYE " with "eye"
            string step5Result = step4Result.Replace("EYE ", "eye");

            // Step 6: Replace "HA" with "Teeth"
            string step6Result = step5Result.Replace("HA ", "teeth");

            // Step 7: Remove any 'Fung' (replace with blank)
            string step7Result = step6Result.Replace("Fung", "");

            // Step 8: If there is any space at the beginning, remove it.
            string step8Result = step7Result.TrimStart();

            return step8Result;
        }
    }
}
#endif