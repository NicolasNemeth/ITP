using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerVectorText;
    [SerializeField] private TMP_InputField scalarInputField;

    private void Start()
    {
        scalarInputField.onEndEdit.AddListener(OnScalarInput);
        UpdatePlayerVectorText(Player.Instance.transform.position);
    }

    public void OnScalarInput(string input)
    {
        if (float.TryParse(input, out float scalar))
        {
            scalarInputField.text = string.Empty;
            Vector3 newPlayerPos = Player.Instance.MultiplyWithScalar(scalar);
            UpdatePlayerVectorText(newPlayerPos);
        }
    }

    private void UpdatePlayerVectorText(Vector3 pos) => playerVectorText.text = pos.x + "\n" + pos.y + "\n" + pos.z;
}
