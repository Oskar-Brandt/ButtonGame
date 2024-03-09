using TMPro;
using UnityEngine;
using UnityEngine.UI; // Import the UI namespace to work with UI elements.

public class Counter : MonoBehaviour
{
    public TextMeshProUGUI numberText; // Reference to the TextMeshPro text component.
    private int count = 0; // Initialize count to 0.
     
    public void IncreaseCount()
    {

        count++; // Increment count by 1.
        numberText.text = count.ToString(); // Update the text to display the current count.
    }
}
