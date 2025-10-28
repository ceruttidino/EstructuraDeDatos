using UnityEngine;

public class PlanetClick : MonoBehaviour
{
    public string planetName;
    private void OnMouseDown()
    {
        if (!string.IsNullOrEmpty(planetName))
            PathController.Instance?.AddPlanet(planetName);
    }
}
