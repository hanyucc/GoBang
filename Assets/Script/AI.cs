using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AI {

    int INF = 2147483647, smax = 6, max_depth = 3;
    double deeper = 0.6;
    int[] score1 = { 7, 35, 800, 15000, 800000 }, score2 = { 7, 15, 400, 1800, 100000 };

    public List<MainLoop.point> count_point(int k) // score of single point
    {
        List<MainLoop.point> vp = new List<MainLoop.point>();
        MainLoop.point tmp = new MainLoop.point();
        int i, j, x, y, cnt1, cnt2;
        int[,] rank = new int[Board.count, Board.count];
        int[,] mv = { { 1, 1, 0, -1 }, { 0, 1, 1, 1 } };
        for (x = 0; x < 15; x++)
	    {
		    for (y = 0; y < 15; y++)
		    {
			    for (i = 0; i< 4; i++)
			    {
				    cnt1 = 0;
				    cnt2 = 0;
				    if (x + 4 * mv[0, i] < 0 || x + 4 * mv[0, i] > 14 || y + 4 * mv[1, i] < 0 || y + 4 * mv[1, i] > 14)
				    {
					    continue;
				    }
				    for (j = 0; j< 5; j++)
				    {
					    if (BoardModel.map[x + j * mv[0, i], y + j * mv[1, i]] == k)
					    {
						    cnt1++;
					    }
					    else if (BoardModel.map[x + j * mv[0, i], y + j * mv[1, i]] != k && BoardModel.map[x + j * mv[0, i], y + j * mv[1, i]] != 0)
					    {
						    cnt2++; // other color
					    }
				    }
				    if (cnt1 == 0 && cnt2 != 0)
				    {
					    for (j = 0; j< 5; j++)
					    {
						    rank[x + j * mv[0, i], y + j * mv[1, i]] += score2[cnt2];
					    }
				    }
				    else if (cnt1 != 0 && cnt2 == 0)
				    {
					    for (j = 0; j< 5; j++)
					    {
						    rank[x + j * mv[0, i], y + j * mv[1, i]] += score1[cnt1];
					    }
				    }
				    else if (cnt1 == 0 && cnt2 == 0)
				    {
					    for (j = 0; j< 5; j++)
					    {
						    rank[x + j * mv[0, i], y + j * mv[1, i]] += score1[0];
					    }
				    }
			    }
		    }
	    }
	    for (i = 0; i < 15; i++)
	    {
		    for (j = 0; j < 15; j++)
		    {
			    if (BoardModel.map[i, j] != 0)
			    {
				    continue;
			    }
			    tmp.x = i;
			    tmp.y = j;
			    tmp.v = rank[i, j];
			    vp.Add(tmp);
		    }
	    }
	    return vp;
    }


    public int ai(int d, int x, int y)
    {
        List<MainLoop.point> vp = new List<MainLoop.point>();
        int i, best = -INF, v, t = BoardModel.check_win();
        MainLoop.point tmp = new MainLoop.point();
        if (d == 0 || t != 0)
        {
            return 0;
        }
        vp = count_point(2);
        vp.Sort((a, b) =>
        {
            if (a.v == b.v)
            {
                if (Math.Abs(a.x - 7) + Math.Abs(a.y - 7) < Math.Abs(b.x - 7) + Math.Abs(b.y - 7))
                {
                    return -1;
                }
                return 1;
            }
            else
            {
                if (a.v > b.v)
                {
                    return -1;
                }
                return 1;
            }
        });
        for (i = 0; i < (smax < vp.Count ? smax : vp.Count); i++)
        {
            tmp = vp[i];
            BoardModel.map[tmp.x, tmp.y] = 2;
            v = tmp.v - Convert.ToInt32(player(d - 1, tmp.x, tmp.y) * deeper);
            if (v > best)
            {
                best = v;
            }
            BoardModel.map[tmp.x, tmp.y] = 0;
        }
        return best;
    }


    public int player(int d, int x, int y)
    {
        List<MainLoop.point> vp = new List<MainLoop.point>();
        int i, best = -INF, v, t = BoardModel.check_win();
        MainLoop.point tmp = new MainLoop.point();
        if (d == 0 || t != 0)
        {
            return 0;
        }
        vp = count_point(1);
        vp.Sort((a, b) =>
        {
            if (a.v == b.v)
            {
                if (Math.Abs(a.x - 7) + Math.Abs(a.y - 7) < Math.Abs(b.x - 7) + Math.Abs(b.y - 7))
                {
                    return -1;
                }
                return 1;
            }
            else
            {
                if (a.v > b.v)
                {
                    return -1;
                }
                return 1;
            }
        });
        for (i = 0; i < (smax < vp.Count ? smax : vp.Count); i++)
        {
            tmp = vp[i];
            BoardModel.map[tmp.x, tmp.y] = 1;
            v = tmp.v - Convert.ToInt32(ai(d - 1, tmp.x, tmp.y) * deeper);
            if (v > best)
            {
                best = v;
            }
            BoardModel.map[tmp.x, tmp.y] = 0;
        }
        return best;
    }


    public MainLoop.point select_point()
    {
        List<MainLoop.point> vp = new List<MainLoop.point>();
        int i, best = -INF, v;
        MainLoop.point bestp = new MainLoop.point(), tmp = new MainLoop.point();
        vp = count_point(2);
        vp.Sort((a, b) =>
        {
            if (a.v == b.v)
            {
                if (Math.Abs(a.x - 7) + Math.Abs(a.y - 7) < Math.Abs(b.x - 7) + Math.Abs(b.y - 7))
                {
                    return -1;
                }
                return 1;
            }
            else
            {
                if (a.v > b.v)
                {
                    return -1;
                }
                return 1;
            }
        });
        for (i = 0; i < (smax < vp.Count ? smax : vp.Count); i++)
        {
            tmp = vp[i];
            BoardModel.map[tmp.x, tmp.y] = 2;
            v = tmp.v - Convert.ToInt32(player(max_depth, tmp.x, tmp.y) * deeper); // determine depth
            if (v > best)
            {
                best = v;
                bestp.x = tmp.x;
                bestp.y = tmp.y;
            }
            BoardModel.map[tmp.x, tmp.y] = 0;
        }
        return bestp;
    }
    /*
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    */
}
