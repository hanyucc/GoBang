using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public GameObject cross_prefab;
    const float cross_size = 35;
    public const int count = 15, board_size = 490, half_size = 245;
    Dictionary<int, Cross> cross_map = new Dictionary<int, Cross>();

    static int MakeKey(int x, int y)
    {
        return x * 10000 + y;
    }

    public void Reset()
    {
        foreach (Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        var mainLoop = GetComponent<MainLoop>();
        cross_map.Clear();

        int x, y;
        for (x = 0; x < count; x++)
        {
            for (y = 0; y < count; y++)
            {
                var cross_object = GameObject.Instantiate<GameObject>(cross_prefab);
                cross_object.transform.SetParent(gameObject.transform);
                cross_object.transform.localScale = Vector3.one;

                var pos = cross_object.transform.localPosition;
                pos.x = -half_size + x * cross_size;
                pos.y = -half_size + y * cross_size;
                pos.z = 1;
                cross_object.transform.localPosition = pos;

                var cross = cross_object.GetComponent<Cross>();
                cross.grid_x = x;
                cross.grid_y = y;
                cross.mainloop = mainLoop;

                cross_map.Add(MakeKey(x, y), cross);
            }
        }
    }

    public Cross GetCross(int x, int y)
    {
        Cross cross;
        if (cross_map.TryGetValue(MakeKey(x, y), out cross))
        {
            return cross;
        }
        return null;
    }

	// Use this for initialization
	void Start () {
        Reset();
	}
	
    /*
	// Update is called once per frame
	void Update () {
		
	}
    */
}
