using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSlot : MonoBehaviour
{
    //[SerializeProperty("IsOccupied")]
    //public bool isOccupied;
    //public bool IsOccupied { get { return _isOccupied; } }
    //private bool _isOccupied;
    //public AllySpaceObject Ship;
    //public Vector2 Coordinate;

    //private GamestateManager _gamestateManager;
    //private SpriteRenderer _spriteRenderer;
    //private GameObject _slotSprite;

    //private void Awake()
    //{
    //    _gamestateManager = FindObjectOfType<GamestateManager>();
    //    _spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    //    _slotSprite = transform.Find("SlotSprite").gameObject;
    //    Highlight(Color.white);
    //}

    //public void Highlight(Color color) {
    //    _spriteRenderer.color = color;
    //}

    //public void Assign(Collectible col, List<Vector2> shipRangeCoordinates, bool instantiateAlly = false)
    //{
    //    _isOccupied = true;
    //    _slotSprite.SetActive(false);

    //    if (instantiateAlly) {
    //        col.Location = CollectibleLocation.Board;
    //        var go = Instantiate(Resources.Load("Red Spaceship"), new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    //        var spaceObject = go.GetComponent<AllySpaceObject>();
    //        var allyData = AllyDataLibrary.Allies[col.Data.Key];
    //        spaceObject.SetData(allyData);
    //        go.transform.SetParent(transform);
    //        go.transform.localPosition = new Vector3(0, 0, -1);
    //        Ship = go.GetComponent<AllySpaceObject>();
    //        //Ship.Collectible = col;
    //        //Ship.ShipRangeCoordinates = shipRangeCoordinates;
    //    }
    //}

    //public void Release()
    //{
    //    _isOccupied = false;
    //    _slotSprite.SetActive(true);
    //    Destroy(Ship.gameObject);
    //    Ship = null;

    //}

    //private void OnMouseDown()
    //{
    //    if (_gamestateManager.CanPlaceCollectible(Coordinate))
    //    {
    //        _gamestateManager.PlaceCollectible();
    //    }
    //}

    //private void OnMouseEnter()
    //{
    //    if (_gamestateManager.IsCollectibleSelected)
    //    {
    //        _gamestateManager.HighlightSlots(Coordinate);
    //    }
    //}

    //private void OnMouseExit()
    //{
    //    _gamestateManager.UnHighlightSlots();
    //}
}
