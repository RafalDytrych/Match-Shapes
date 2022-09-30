using MatchLine;
using System.Collections;
using UnityEngine;

public class SquaresBackBackground : MonoBehaviour
{
   public IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, -5f);
        transform.localScale = new Vector3(Board.Width, Board.Height, 1f);
    }
}
