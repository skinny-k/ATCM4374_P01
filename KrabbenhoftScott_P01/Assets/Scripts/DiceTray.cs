using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceTray : MonoBehaviour
{
    [SerializeField] BoxCollider _floor;
    [SerializeField] BoxCollider[] _walls = new BoxCollider[4];
    [SerializeField] float _wallOffset = 7.5f;
    [SerializeField] GameDie _capitalDie;
    [SerializeField] GameDie _departmentDie;
    [SerializeField] float _dieOffset = 0.25f;

    public GameDie Capital_Die { get => _capitalDie; }
    public GameDie Department_Die { get => _departmentDie; }

    public void SetBounds(int screenWidth, int screenHeight)
    {
        float clipPlane = Camera.main.transform.position.z - _floor.transform.position.z;
        Vector3 center = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth * 0.5f, screenHeight * 0.75f, clipPlane));
        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, screenHeight, clipPlane));
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(screenWidth, screenHeight * 0.48f, clipPlane));
        center.z = _floor.transform.position.z;
        _floor.transform.position = center;

        _walls[0].transform.rotation = Quaternion.Euler(Vector3.zero);
        _walls[0].transform.position = new Vector3(center.x, upperRight.y, center.z - _wallOffset);
        _walls[1].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        _walls[1].transform.position = new Vector3(upperRight.x, center.y, center.z - _wallOffset);
        _walls[2].transform.rotation = Quaternion.Euler(Vector3.zero);
        _walls[2].transform.position = new Vector3(center.x, lowerLeft.y, center.z - _wallOffset);
        _walls[3].transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        _walls[3].transform.position = new Vector3(lowerLeft.x, center.y, center.z - _wallOffset);

        _capitalDie.transform.position = center + new Vector3(-_dieOffset, Random.Range(-_dieOffset, 0), -_dieOffset);
        _departmentDie.transform.position = center + new Vector3(_dieOffset, Random.Range(0, _dieOffset), -_dieOffset);
    }
}
