using UnityEngine;

public class PlaceTower : MonoBehaviour
{
    //Se cambiarán por atributos de la clase "torre"
    public string towerType;
    public int towerCost = 30;
    public GameObject policeTowerPrefab;

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
                newObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            }

            if (newObject != null)
            {
                newObject.transform.position = transform.position;
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
