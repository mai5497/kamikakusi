//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(HintObj))]
//public class ObjNameEditor : Editor {

//    HintObj _hintObj;

//    private void OnEnable() {
//    }

//    public override void OnInspectorGUI() {
//        //----- �X�N���v�g��\�� -----
//        DrawDefaultInspector();

//        _hintObj = target as HintObj;
//        EditorGUILayout.LabelField("--------------- �l�̖� ---------------");
//        _hintObj.objName = EditorGUILayout.TextField("�I�u�W�F�N�g��", _hintObj.objName);
//        _hintObj._isHatenaHuman = EditorGUILayout.Toggle("�H�H�H�̕ϊ�", _hintObj._isHatenaHuman);

//        EditorGUILayout.LabelField("--------------- �ς̖� ---------------");
//        _hintObj.objName = EditorGUILayout.TextField("�I�u�W�F�N�g��", _hintObj.uraObjName);
//        _hintObj._isHatenaFox = EditorGUILayout.Toggle("�H�H�H�̕ϊ�", _hintObj._isHatenaFox);

//    }
//}
