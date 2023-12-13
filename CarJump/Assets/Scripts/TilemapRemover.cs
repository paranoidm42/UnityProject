using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapRemover : MonoBehaviour
{
    public Tilemap tilemap;
    public Vector3Int[] positions;
    Animator anim;

    private void OnEnable() {
        PlayerCollision.onButton += DeleteTiles;
    }

    private void OnDisable() {
        PlayerCollision.onButton -= DeleteTiles;
    }

    private void Start() {
        anim = GetComponent<Animator>();
    }

    void ButtonAnim()
    {

        anim.SetInteger("ButtonPress", 1);

    }

    void DeleteTiles()
    {
        ButtonAnim();
        foreach (Vector3Int position in positions)
        {
            tilemap.SetTile(position, null);
        }
    }


    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }

}
