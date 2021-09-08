using UnityEngine;
using System.Collections.Generic;

public class ObstructionHider : MonoBehaviour
{
    private List<Obstruction> lastObstructions;

    private void Start()
    {
        lastObstructions = new List<Obstruction>();
    }

    private void Update()
    {
        List<Obstruction> newObstructions = GetObstructions();

        MakeOldObstructionsVisible(newObstructions);
        MakeObstructionsTransparent(newObstructions);

        lastObstructions = newObstructions;
    }

    private List<Obstruction> GetObstructions()
    {
        Ray cameraRay = Camera.main.ViewportPointToRay(Camera.main.WorldToViewportPoint(transform.position));
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        RaycastHit[] hits = Physics.RaycastAll(cameraRay);

        return ConvertHits(hits);
    }

    private List<Obstruction> ConvertHits(RaycastHit[] hits)
    {
        List<Obstruction> obstructions = new List<Obstruction>();

        for (int i = 0; i < hits.Length; i++)
        {
            Obstruction obstruction = hits[i].transform.GetComponent<Obstruction>();

            if (obstruction != null)
                obstructions.Add(obstruction);
        }

        return obstructions;
    }

    private void MakeOldObstructionsVisible(List<Obstruction> newObstructions)
    {
        for (int i = 0; i < lastObstructions.Count; i++)
        {
            Obstruction lastObstruction = lastObstructions[i];
            
            if (!newObstructions.Contains(lastObstruction))
                lastObstruction.MakeVisible();
        }
    }

    private void MakeObstructionsTransparent(List<Obstruction> obstructions)
    {
        for (int i = 0; i < obstructions.Count; i++)
        {
            obstructions[i].MakeTransparent();
        }
    }
}
