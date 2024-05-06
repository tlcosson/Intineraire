using UnityEngine;
using UnityEngine.UI;  // Pour le bouton
using TMPro;  // Pour les composants TextMeshPro

public class SearchAndPoint : MonoBehaviour
{
    public TMP_InputField inputField;  // Liez ceci dans l'inspecteur
    public TextMeshProUGUI distanceText;      // Liez ceci dans l'inspecteur
    public Button searchButton;    // Liez ceci dans l'inspecteur
    public GameObject arrow;       // Assignez le cylindre (flèche) dans l'inspecteur
    public Camera mainCamera;      // Assignez la caméra principale dans l'inspecteur
    private Transform target;      // La cible dynamique

    void Start()
    {
        searchButton.onClick.AddListener(SearchAndDisplay);
        arrow.SetActive(false);  // Cachez la flèche au début
    }

    void Update()
    {
        if (target != null)
        {
            arrow.SetActive(true);
            PointArrowAtTarget();
            UpdateDistanceDisplay();
        }
        else
        {
            arrow.SetActive(false);
            distanceText.text = "No target selected";
        }
    }

    void PointArrowAtTarget()
    {
        arrow.transform.LookAt(target, Vector3.up);
    }

    void SearchAndDisplay()
    {
        Transform searchTarget = GameObject.Find(inputField.text)?.transform;
        if (searchTarget != null)
        {
            Debug.Log("Target found: " + searchTarget.name);
            target = searchTarget; // Only update target if found
        }
        else
        {
            Debug.Log("Target not found: " + inputField.text);
            distanceText.text = "Object not found";
            arrow.SetActive(false);
            target = null; // Reset target if not found
        }
    }

    void UpdateDistanceDisplay()
    {
        float distance = Vector3.Distance(mainCamera.transform.position, target.position);
        distanceText.text = "Distance: " + distance.ToString("F2") + " m";
    }
}