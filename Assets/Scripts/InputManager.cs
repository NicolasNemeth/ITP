using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerVectorText;
    [SerializeField] private TMP_InputField scalarInputField;
    [SerializeField] private TMP_InputField inputFieldX;
    [SerializeField] private TMP_InputField inputFieldY;
    [SerializeField] private TMP_InputField inputFieldZ;

    public static InputManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        scalarInputField.onEndEdit.AddListener(OnScalarInput);
        inputFieldX.onEndEdit.AddListener(delegate { OnVectorInput(); });
        inputFieldY.onEndEdit.AddListener(delegate { OnVectorInput(); });
        inputFieldZ.onEndEdit.AddListener(delegate { OnVectorInput(); });
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

    public void OnVectorInput()
    {
        if (!string.IsNullOrEmpty(inputFieldX.text) && !string.IsNullOrEmpty(inputFieldY.text) && !string.IsNullOrEmpty(inputFieldZ.text))
        {
            float x, y, z;

            if (float.TryParse(inputFieldX.text, out x) && float.TryParse(inputFieldY.text, out y) && float.TryParse(inputFieldZ.text, out z))
            {
                inputFieldX.text = string.Empty;
                inputFieldY.text = string.Empty;
                inputFieldZ.text = string.Empty;

                Vector3 inputVector = new Vector3(x, y, z);
                Vector3 newPlayerPos = Player.Instance.AddVector(inputVector);
                UpdatePlayerVectorText(newPlayerPos);
            }
        }
    }

    public void UpdatePlayerVectorText(Vector3 pos) => playerVectorText.text = pos.x + "\n" + pos.y + "\n" + pos.z;
}
