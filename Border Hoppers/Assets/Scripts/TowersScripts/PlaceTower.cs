using UnityEngine;

public class PlaceTower : MonoBehaviour
{
    //Se cambiarán por atributos de la clase "torre"
    public string selectedTowerType;
    public int towerCost;
    public GameObject policeTowerPrefab;
    public GameObject borderPatrolPrefab;
    void Start()
    {
        SetTowerCost();
    }

    public void SetTowerCost()
    {
        // Set the tower cost based on the selected type
        if (selectedTowerType == "Add Tower 1")
        {
            Tower tower = policeTowerPrefab.GetComponent<Tower>();
            if (tower != null)
                towerCost = tower.cost;
        }
        else if (selectedTowerType == "Add Tower 2")
        {
            Tower tower = borderPatrolPrefab.GetComponent<Tower>();
            if (tower != null)
                towerCost = tower.cost;
        }
    }

    void OnMouseDown()
    {
        if (GameManager.manager.cash >= towerCost)
        {
            GameObject newObject = null;

            GameManager.manager.AddCash(-towerCost);

            if (selectedTowerType == "Add Tower 1")
            {
                newObject = Instantiate(policeTowerPrefab);

            }
            else if (selectedTowerType == "Add Tower 2")
            {
                newObject = Instantiate(borderPatrolPrefab);

                // Asegurar que BorderPatrol se instancie con y = 0.76
                if (newObject != null)
                {
                    Vector3 newPosition = transform.position;
                    newPosition.y = 0.76f;
                    newObject.transform.position = newPosition;
                }
            }

            if (newObject != null && selectedTowerType != "Add Tower 2")
            {
                newObject.transform.position = transform.position; // Para las demás torres
            }


            Destroy(gameObject);

            HideEmptyPlaces();
        }
        else
        {
            Debug.Log("No tienes suficiente dinero para construir esta torre.");
            HideEmptyPlaces();
        }
    }

    void HideEmptyPlaces()
    {
        GameObject[] emptyPlaces = GameObject.FindGameObjectsWithTag("emptyPlace");

        foreach (GameObject place in emptyPlaces)
        {
            Renderer renderer = place.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = false;
            }

            Destroy(place.GetComponent<PlaceTower>());
        }
    }
}
