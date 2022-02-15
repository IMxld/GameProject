using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    public MainScene mainScene;

    private Dictionary<int, Vector3Int> chunkPos;

    public static List<int> randomNum = new List<int>() { 0, 1, 2, 4, 5, 6, 7 };
    public static List<int> randomTemp = new List<int>();

    public Tilemap cloud;
    public Tilemap wall;
    public Tilemap fish;
    public Tilemap prision;
    public Tilemap deadFish;
    public Tilemap vortex;
    public Tilemap tigerya;
    public Tilemap menya;
    public Tilemap sword;
    public Tilemap decoration;
    public Tilemap realMenya;
    public Tilemap taoArea;
    public Tilemap coiledArea;
    public Tilemap leviathanArea;
    public Tilemap octan;

    public GameObject prisonClone;

    public Tile coiled;
    public Tile coildFish;
    public Tile swordIsland;
    public Tile swordIslandWithLight;
    public Tile armstrongIsland;
    public Tile sea;
    public Tile prisionCloud;
    public Tile leviathan;
    public Tile fishPoint;
    public Tile vortexes;
    public Tile tigeryaShadow;
    public Tile menyaReal;
    public Tile crystal;
    public Tile tao;
    public Tile octan1;
    public Tile octan2;

    public static int tigeryaPos;

    public static List<Vector3Int> everyPos;

    public static Vector3Int coiledPos;
    public static Vector3Int swordPos;
    public static Vector3Int prisionCloudPos;
    public static Vector3Int leviathanPos;
    public static List<Vector3Int> tigeryaPoints = new List<Vector3Int>();
    public static Vector3Int menyaPos;
    private Vector3Int fishTemp;
    public static Vector3Int taoPos;

    private void Awake()
    {
        chunkPos = new Dictionary<int, Vector3Int>();
        chunkPos.Add(0, new Vector3Int(-5, -5, 0));
        chunkPos.Add(1, new Vector3Int(5, -5, 0));
        chunkPos.Add(2, new Vector3Int(15, -5, 0));
        chunkPos.Add(3, new Vector3Int(-5, 5, 0));
        chunkPos.Add(4, new Vector3Int(15, 5, 0));
        chunkPos.Add(5, new Vector3Int(-5, 15, 0));
        chunkPos.Add(6, new Vector3Int(5, 15, 0));
        chunkPos.Add(7, new Vector3Int(15, 15, 0));

        everyPos = new List<Vector3Int>();
        everyPos.Add(new Vector3Int(0, 9, 0));
        
        while (randomTemp.Count < 7)
        {
            int index = Random.Range(0, randomNum.Count);
            if (!randomTemp.Contains(randomNum[index]))
            {
                randomTemp.Add(randomNum[index]);
                randomNum.Remove(randomNum[index]);
            }
        }

        //Debug.Log(randomTemp[0]);

        Coiled(randomTemp[0]);
        mainScene.LoadStar(4, randomTemp[0]);
        mainScene.ConstellationImages[randomTemp[0]].CrossFadeAlpha(0, 0, true);

        Sword(randomTemp[1]);
        mainScene.LoadStar(3, randomTemp[1]);

        Leviathan(randomTemp[2]);
        mainScene.LoadStar(1, randomTemp[2]);

        Tigerya(randomTemp[3]);

        Menya(randomTemp[4]);
        mainScene.LoadStar(2, randomTemp[4]);

        Tao(randomTemp[5]);

        Prision(randomTemp[6]);
        mainScene.LoadStar(0, randomTemp[6]);

        //mainScene.RandomStar(randomTemp[3]);
        //mainScene.RandomStar(randomTemp[5]);
        //mainScene.RandomStar(randomTemp[0]);


        Goal();
        Born();
        RandomFish();
        RandomOctan();
    }

    private void Coiled(int i)
    {
        coiledPos = new Vector3Int(Random.Range(2, 6), Random.Range(2, 6), 0) + chunkPos[i];

        coiledArea.SetTile(coiledPos, coiled);

        for (int m = -1; m < 3; m++)
        {
            for (int n = -1; n < 3; n++)
            {
                if (!((m == 0 || m == 1) && (n == 1 || n == 0)))
                {
                    Vector3Int temp = coiledPos + new Vector3Int(m, n, 0);
                    everyPos.Add(temp);
                    deadFish.SetTile(temp, coildFish);
                }
            }
        }

        for (int q = 0; q < 10; q++)
        {
            Vector3Int temp = new Vector3Int(Random.Range(0, 9), Random.Range(0, 9), 0) + chunkPos[i];
            everyPos.Add(temp);
            decoration.SetTile(temp, coildFish);
        }
    }

    private void Sword(int i)
    {
        float luckyEvent = Random.value;

        swordPos = new Vector3Int(Random.Range(3, 7), Random.Range(2, 7), 0) + chunkPos[i];
        everyPos.Add(swordPos);

        if(luckyEvent > 0.02f)
        {
            sword.SetTile(swordPos, swordIsland);
        }
        else
        {
            sword.SetTile(swordPos, armstrongIsland);
        }

        for (int m = -2; m < 2; m++)
        {
            for (int n = 0; n < 2; n++)
            {
                everyPos.Add(new Vector3Int(m, n, 0) + swordPos);
            }
        }
    }

    private void Leviathan(int i)
    {
        leviathanPos = new Vector3Int(Random.Range(3, 5), Random.Range(3, 5), 0) + chunkPos[i];

        leviathanArea.SetTile(leviathanPos, leviathan);

        vortex.SetTile(leviathanPos + new Vector3Int(1, 1, 0), vortexes);

        for (int m = -2; m < 4; m++)
        {
            for (int n = -2; n < 4; n++)
            {
                everyPos.Add(leviathanPos + new Vector3Int(m, n, 0));
            }
        }
    }

    private void Tigerya(int i)
    {
        tigeryaPoints.Add(new Vector3Int(2, 1, 0) + chunkPos[i]);
        tigeryaPoints.Add(new Vector3Int(5, 1, 0) + chunkPos[i]);
        tigeryaPoints.Add(new Vector3Int(7, 1, 0) + chunkPos[i]);
        tigeryaPoints.Add(new Vector3Int(8, 2, 0) + chunkPos[i]);
        tigeryaPoints.Add(new Vector3Int(8, 5, 0) + chunkPos[i]);
        tigeryaPoints.Add(new Vector3Int(8, 7, 0) + chunkPos[i]);
        tigeryaPoints.Add(new Vector3Int(7, 8, 0) + chunkPos[i]);
        tigeryaPoints.Add(new Vector3Int(4, 8, 0) + chunkPos[i]);
        tigeryaPoints.Add(new Vector3Int(2, 8, 0) + chunkPos[i]);
        tigeryaPoints.Add(new Vector3Int(1, 7, 0) + chunkPos[i]);
        tigeryaPoints.Add(new Vector3Int(1, 4, 0) + chunkPos[i]);
        tigeryaPoints.Add(new Vector3Int(1, 2, 0) + chunkPos[i]);

        tigeryaPos = Random.Range(0, 11);

        foreach(Vector3Int q in tigeryaPoints)
        {
            everyPos.Add(q);
        }

        tigerya.SetTile(tigeryaPoints[tigeryaPos], tigeryaShadow);

        for (int q = 0; q < 9; q++)
        {
            fishTemp = new Vector3Int(Random.Range(0, 9), Random.Range(0, 9), 0) + chunkPos[i];
            foreach (Vector3Int item in tigeryaPoints)
            {
                while(fishTemp == item)
                {
                    fishTemp = new Vector3Int(Random.Range(0, 9), Random.Range(0, 9), 0) + chunkPos[i];
                }
            }
            fish.SetTile(fishTemp, fishPoint);
            everyPos.Add(fishTemp);
        }
    }

    private void Menya(int i)
    {
        Vector3Int men = new Vector3Int(Random.Range(0, 8), Random.Range(0, 8), 0);
        menyaPos = men + chunkPos[i];
        while((menyaPos.x == 4 || menyaPos.x == 5) && (menyaPos.y == 4 || menyaPos.y == 5))
        {
            men = new Vector3Int(Random.Range(0, 9), Random.Range(0, 9), 0);
            menyaPos = men + chunkPos[i];
        }

        menya.SetTile(menyaPos, menyaReal);
        everyPos.Add(menyaPos);
        everyPos.Add(menyaPos + new Vector3Int(1, 0, 0));
        everyPos.Add(menyaPos + new Vector3Int(1, 1, 0));
        everyPos.Add(menyaPos + new Vector3Int(0, 1, 0));

        wall.SetTile(new Vector3Int(4, 4, 0) + chunkPos[i], crystal);
        everyPos.Add(chunkPos[i] + new Vector3Int(4, 4, 0));
        everyPos.Add(chunkPos[i] + new Vector3Int(4, 5, 0));
        everyPos.Add(chunkPos[i] + new Vector3Int(5, 4, 0));
        everyPos.Add(chunkPos[i] + new Vector3Int(5, 5, 0));

        realMenya.SetTile(new Vector3Int(9 - men.x, 9 - men.y, 0) + chunkPos[i], sea);
        everyPos.Add(new Vector3Int(9 - men.x, 9 - men.y, 0) + chunkPos[i]);
        realMenya.SetTile(new Vector3Int(9 - men.x -1, 9 - men.y, 0) + chunkPos[i], sea);
        everyPos.Add(new Vector3Int(9 - men.x - 1, 9 - men.y, 0) + chunkPos[i]);
        realMenya.SetTile(new Vector3Int(9 - men.x, 9 - men.y -1, 0) + chunkPos[i], sea);
        everyPos.Add(new Vector3Int(9 - men.x, 9 - men.y - 1, 0) + chunkPos[i]);
        realMenya.SetTile(new Vector3Int(9 - men.x -1, 9 - men.y -1, 0) + chunkPos[i], sea);
        everyPos.Add(new Vector3Int(9 - men.x - 1, 9 - men.y - 1, 0) + chunkPos[i]);
    }

    private void Tao(int i)
    {
        taoPos = new Vector3Int(Random.Range(2, 9), Random.Range(2, 9), 0) + chunkPos[i];

        taoArea.SetTile(taoPos, tao);
        everyPos.Add(taoPos);
        everyPos.Add(taoPos + new Vector3Int(1, 0, 0));
        everyPos.Add(taoPos + new Vector3Int(1, 1, 0));
        everyPos.Add(taoPos + new Vector3Int(0, 1, 0));
    }

    private void Born()
    {
        for (int m = -5; m < 0; m++)
        {
            for (int n = 7; n < 12; n++)
            {
                everyPos.Add(new Vector3Int(m, n, 0));
            }
        }
    }

    private void Goal()
    {
        for (int m = 0; m < 10; m++)
        {
            for (int n = 0; n < 10; n++)
            {
                everyPos.Add(new Vector3Int(m, n, 0) + new Vector3Int(5, 5, 0));
            }
        }
    }

    private void Prision(int i)
    {
        prisionCloudPos = new Vector3Int(Random.Range(3, 5), Random.Range(3, 5), 0) + chunkPos[i];
        everyPos.Add(prisionCloudPos);

        for (int m = -3; m < 4; m++)
        {
            for (int n = -3; n < 4; n++)
            {
                everyPos.Add(new Vector3Int(m, n, 0) + prisionCloudPos);
            }
        }

        prision.SetTile(prisionCloudPos, prisionCloud);
        for (int q = 0; q < 5; q++)
        {
            Vector3Int temp = prisionCloudPos + new Vector3Int(Random.Range(-2, 2), Random.Range(-2, 2), 0);
            prision.SetTile(temp, prisionCloud);
        }

        prisonClone = GameObject.Instantiate(prision.gameObject, transform);
        prisonClone.GetComponent<TilemapRenderer>().sortingOrder = 2;
    }

    private void RandomFish()
    {
        for (int i = 0; i < 20; i++)
        {
            Vector3Int temp = new Vector3Int(Random.Range(-5, 24), Random.Range(-5, 24), 0);
            if (!everyPos.Contains(temp))
            {
                everyPos.Add(temp);
                fish.SetTile(temp, fishPoint);
            }
        }
    }
    private void RandomOctan()
    {
        for (int i = 0; i < 9; i++)
        {
            Vector3Int temp = new Vector3Int(Random.Range(-5, 24), Random.Range(-5, 24), 0);
            if (!everyPos.Contains(temp))
            {
                everyPos.Add(temp);
                if(temp.x % 2 == 0)
                {
                    octan.SetTile(temp, octan1);
                }
                else
                {
                    octan.SetTile(temp, octan2);
                }
            }
        }
    }
}
