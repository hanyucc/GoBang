using UnityEngine;
using UnityEngine.UI;

public class Cross : MonoBehaviour {

    public int grid_x, grid_y;
    public MainLoop mainloop;

	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            mainloop.on_click(this);
        });
	}
	
    /*
	// Update is called once per frame
	void Update () {
		
	}
    */
}
