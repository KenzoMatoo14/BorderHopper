using UnityEngine;

public class PlaceTower : MonoBehaviour
{
    //Se cambiarán por atributos de la clase "torre"
    public string towerType;
    public int towerCost = 30;
    public GameObject policeTowerPrefab;
    public GameObject BorderPatrolPrefab;

    void OnMouseDown()
    {
        if (GameManager.manager.cash >= towerCost)
        {
            GameObject newObject = null;

            GameManager.manager.AddCash(-towerCost);

            if (towerType == "Add Tower 1")
            {
                newObject = Instantiate(policeTowerPrefab);

            }
            else if (towerType == "Add Tower 2")
            {
                newObject = Instantiate(BorderPatrolPrefab);

                // Asegurar que BorderPatrol se instancie con y = 0.76
                if (newObject != null)
                {
                    Vector3 newPosition = transform.position;
                    newPosition.y = 0.76f;
                    newObject.transform.position = newPosition;
                }
            }

            if (newObject != null && towerType != "Add Tower 2")
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
