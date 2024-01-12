using UnityEngine;

public class TsumSelection : MonoBehaviour
{
    public LayerMask tsumLayer; // The layer assigned to your Tsum objects
    public Camera mainCamera;   // Reference to your main camera

    public GameObject selectedTsum; // The currently selected Tsum

    void Update()
    {
        // Check for mouse or touch input
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast from the screen point to check if a Tsum is selected
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, tsumLayer))
            {
                // Check if the hit object has a TsumController script
                Tsum tsum = hit.collider.GetComponent<Tsum>();

                if (tsum != null)
                {
                    // Select the Tsum
                    SelectTsum(tsum.gameObject);
                }
            }
            else
            {
                // Deselect the current Tsum if no Tsum is clicked
                DeselectTsum();
            }
        }
    }

    void SelectTsum(GameObject tsum)
    {
        // Deselect the current Tsum if there is one
        DeselectTsum();

        // Highlight or perform any other visual feedback for the selected Tsum
        // For example, change the color or scale of the Tsum
        tsum.GetComponent<SpriteRenderer>().color = Color.red;

        // Assign the selected Tsum
        selectedTsum = tsum;
    }

    void DeselectTsum()
    {
        if (selectedTsum != null)
        {
            // Remove any visual feedback for the deselected Tsum
            // For example, revert the color or scale changes

            // Deselect the Tsum
            selectedTsum = null;
        }
    }
}