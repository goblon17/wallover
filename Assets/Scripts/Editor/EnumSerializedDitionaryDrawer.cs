using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

[CustomPropertyDrawer(typeof(SerializedDictionary<PlayerManager.PlayerColor, Material>))]
[CustomPropertyDrawer(typeof(SerializedDictionary<PlayerManager.PlayerColor, PlayerJumper>))]
public class EnumSerializedDictionaryDrawer : PropertyDrawer
{
    private Dictionary<string, ReorderableList> lists = new Dictionary<string, ReorderableList>();
    private System.Type enumType => fieldInfo.FieldType.GenericTypeArguments[0];


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        CreateListIfNecessary(property);
        lists[property.propertyPath].DoList(position);

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        CreateListIfNecessary(property);

        return lists[property.propertyPath].GetHeight();
    }

    private void CreateListIfNecessary(SerializedProperty property)
    {
        if (!lists.ContainsKey(property.propertyPath) || lists[property.propertyPath].index > lists[property.propertyPath].count - 1)
        {
            SerializedProperty pairsListProperty = property.FindPropertyRelative("pairs");

            lists[property.propertyPath] = new ReorderableList(pairsListProperty.serializedObject, pairsListProperty, false, true, true, true);
            ReorderableList list = lists[property.propertyPath];

            list.drawHeaderCallback = (rect) => DrawHeaderCallback(list, property.displayName, rect);
            list.drawElementCallback = (rect, index, isActive, isFocused) => DrawElementCallback(list, rect, index, isActive, isFocused);
            list.elementHeightCallback = (index) => ElementHeightCallback(list, index);
            list.onAddDropdownCallback = OnAddDropdownCallback;
            list.onCanAddCallback = CanAddCallback;
        }
    }

    private void DrawHeaderCallback(ReorderableList list, string displayName, Rect rect)
    {
        list.serializedProperty.isExpanded = EditorGUI.Foldout(rect, list.serializedProperty.isExpanded, displayName);
        EditorUtility.SetDirty(list.serializedProperty.serializedObject.targetObject);
    }

    private void DrawElementCallback(ReorderableList list, Rect rect, int index, bool isActive, bool isFocused)
    {
        if (!list.serializedProperty.isExpanded)
        {
            return;
        }

        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

        EditorGUI.PropertyField(rect, element, GUIContent.none);
    }

    private float ElementHeightCallback(ReorderableList list, int index)
    {
        if (!list.serializedProperty.isExpanded)
        {
            return 0;
        }

        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);

        return EditorGUI.GetPropertyHeight(element);
    }

    private void OnAddDropdownCallback(Rect buttonRect, ReorderableList list)
    {
        HashSet<string> existingValues = new HashSet<string>();
        foreach (SerializedProperty element in list.serializedProperty)
        {
            SerializedProperty keyProperty = element.FindPropertyRelative("Key").FindPropertyRelative("item");
            string valueName = keyProperty.enumNames[keyProperty.enumValueIndex];
            existingValues.Add(valueName);
        }

        GenericMenu menu = new GenericMenu();
        foreach (var enumValue in System.Enum.GetValues(enumType))
        {
            if (existingValues.Contains(enumValue.ToString()))
            {
                continue;
            }

            menu.AddItem(new GUIContent(enumValue.ToString()), false, (value) => AddClickHandler(list, value), enumValue);
        }

        menu.ShowAsContext();
    }

    private void AddClickHandler(ReorderableList list, object value) 
    {
        int index = list.serializedProperty.arraySize;
        list.serializedProperty.arraySize++;
        list.index = index;

        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index);
        SerializedProperty key = element.FindPropertyRelative("Key").FindPropertyRelative("item");
        key.enumValueIndex = System.Array.IndexOf(System.Enum.GetValues(value.GetType()), value);

        list.serializedProperty.serializedObject.ApplyModifiedProperties();
    }

    private bool CanAddCallback(ReorderableList list)
    {
        return list.serializedProperty.arraySize < System.Enum.GetValues(enumType).Length;
    }
}

