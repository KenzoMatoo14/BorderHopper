using UnityEngine;

public class PressedButton : MonoBehaviour
{
    public GameObject policeTowerPrefab;
    public GameObject BorderPatrolPrefab;

    //La funci�n solo podr� recibir un par�metro.
    public void ShowEmptyPlaces(string towerType)
    {
        GameObject[] emptyPlaces = GameObject.FindGameObjectsWithTag("emptyPlace");

        foreach (GameObject place in emptyPlaces)
        {
            Renderer renderer = place.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.enabled = true;
            }

            PlaceTower placeTower = place.AddComponent<PlaceTower>();
            placeTower.selectedTowerType = towerType; // Pass selected tower type
            placeTower.policeTowerPrefab = policeTowerPrefab;
            placeTower.borderPatrolPrefab = BorderPatrolPrefab;
        }
    }
}
