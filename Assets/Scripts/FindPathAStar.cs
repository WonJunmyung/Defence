using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

namespace Silly
{
    [Serializable]
    public class PathMarker
    {
        public MapData mapData;
        public float G;
        public float H;
        public float F;
        public GameObject marker;
        public PathMarker parent;

        public PathMarker(MapData location, float g, float h, float f,/* GameObject marker,*/ PathMarker p)
        {
            mapData = location;
            G = g;
            H = h;
            F = f;
            //this.marker = marker;
            parent = p;
        }
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                return mapData.Equals(((PathMarker)obj).mapData);
            }
        }

        public override int GetHashCode()
        {
            return 0;
        }

    }
    public class FindPathAStar : MonoBehaviour
    {
        public MapManager mapManager;

        public List<PathMarker> open = new List<PathMarker>();
        public List<PathMarker> closed = new List<PathMarker>();

        public GameObject start;
        public GameObject end;
        public GameObject pathP;

        PathMarker goalNode;
        PathMarker startNode;
        PathMarker lastNode;

        public bool done = false;
        GameObject Unit;
        public bool isMove = false;
        public bool isAttack = false;
        public List<Vector3> movePath = new List<Vector3>();
        public List<MapData> mapDatas = new List<MapData>();
        int reload = 0;
        // Start is called before the first frame update
        void Start()
        {
            //mapManager = GameObject.Find("Manager").GetComponent<MapManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                MapData startMapData = new MapData(0, 6);
                MapData endMapData = new MapData(22, 6);
                BeginSearch(startMapData, endMapData);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                //while(!done)
                {
                    Search(lastNode);
                }
            }
            if (Input.GetKeyDown(KeyCode.M))
            {
                GetPath();
            }
        }

        void RemoveAllMarkers()
        {
            GameObject[] markers = GameObject.FindGameObjectsWithTag("marker");
            foreach(GameObject marker in markers)
            {
                Destroy(marker);
            }
        }

        public void BeginSearch(MapData startPos, MapData endPos)
        {
            done = false;
            RemoveAllMarkers();

            open.Clear();
            closed.Clear();
            movePath.Clear();
            mapDatas.Clear();
            startNode = null;
            goalNode = null;
            
            if(mapManager == null)
            {
                mapManager = GameObject.Find("Manager").GetComponent<MapManager>();
            }
            
            for(int i = 0; i < mapManager.mapData.Count; i++)
            {
                if (mapManager.mapData[i].blockName == BlockName.Walkable || mapManager.mapData[i].blockName == BlockName.Response)
                {
                    mapDatas.Add(mapManager.mapData[i]);
                }
            }
            
            Vector3 startLocation = new Vector3(startPos.x, 0, startPos.z);
            startNode = new PathMarker(new MapData(startPos.x, startPos.z), 0, 0, 0, /*Instantiate(start, startLocation, Quaternion.identity),*/ null);
            
            Vector3 endLocation = new Vector3(endPos.x, 0, endPos.z);
            goalNode = new PathMarker(new MapData(endPos.x, endPos.z), 0, 0, 0, /*Instantiate(end, endLocation, Quaternion.identity),*/ null);
            
            open.Add(startNode);
            lastNode = startNode;
        }

        void Search(PathMarker thisNode)
        {
            
            if (thisNode.Equals(goalNode))
            {
                done = true;
                return;
            }
            int count = 0;
            foreach (MapData dir in mapManager.directions)
            {

                MapData neightbor = dir + thisNode.mapData;
                int num = neightbor.x * mapManager.mapWidth + neightbor.z;
                
                // 맵이 담긴 리스트 범위를 벗어날때
                if (num < 0 || num > mapManager.mapData.Count)
                {
                    continue;
                }
                MapData current = mapManager.GetMapData(neightbor.x, neightbor.z);
                if (current == null)
                {
                    continue;
                }
                // 통로가 아닐때
                if (!(current.blockName == BlockName.Walkable ||
                    current.blockName == BlockName.Response))
                {
                    count++;
                    continue;
                }
                // 맵범위를 벗어날때
                if (neightbor.x < 0 || neightbor.x >= mapManager.mapWidth || neightbor.z < 0 || neightbor.z >= mapManager.mapHeight)
                {
                    count++;
                    continue;
                }
                // 이미 닫힌 목록에 있을때
                if (IsClosed(neightbor))
                {
                    count++;
                    continue;
                }
                float G = Vector2.Distance(thisNode.mapData.ToVector(), neightbor.ToVector()) + thisNode.G;
                float H = Vector2.Distance(neightbor.ToVector(), goalNode.mapData.ToVector());
                float F = G + H;
                //GameObject pathBlock = Instantiate(pathP, new Vector3(neightbor.x, 0, neightbor.z), Quaternion.identity);

                if (!UpdateMarker(neightbor, G, H, F, thisNode))
                {
                    open.Add(new PathMarker(neightbor, G, H, F, /*pathBlock,*/ thisNode));
                }
                
                //Debug.Log("통로 발견 : " + count);
                count++;
            }
            
            
            if(open.Count == 0)
            {
                Debug.Log(this.gameObject.name);
            }
            
            
            open = open.OrderBy(p => p.F).ToList<PathMarker>();
            PathMarker pm = open.ElementAt(0);

            closed.Add(pm);
            open.RemoveAt(0);
            
            lastNode = pm;
        }

        bool IsClosed(MapData marker)
        {
            foreach(PathMarker p in closed)
            {
                if (p.mapData.Equals(marker))
                {
                    return true;
                }
            }
            return false;
        }

        bool UpdateMarker(MapData pos, float g, float h, float f, PathMarker prt)
        {
            foreach(PathMarker p in open)
            {
                if (p.mapData.Equals(pos))
                {
                    p.G = g;
                    p.H = h;
                    p.F = f;
                    p.parent = prt;
                    return true;
                }
            }
            return false;
        }

        void GetPath()
        {
            RemoveAllMarkers();
            PathMarker begin = lastNode;

            while(!startNode.Equals(begin) && begin != null)
            {
                //Instantiate(pathP, new Vector3(begin.mapData.x, 0, begin.mapData.z), Quaternion.identity);
                begin = begin.parent;
            }
            //Instantiate(pathP, new Vector3(startNode.mapData.x, 0, startNode.mapData.z), Quaternion.identity);
        }

        void SetMovePath()
        {
            PathMarker begin = lastNode;
            while(!startNode.Equals(begin) && begin != null)
            {
                movePath.Add(new Vector3(begin.mapData.x, 0, begin.mapData.z));
                begin = begin.parent;
            }

            movePath.Add(new Vector3(startNode.mapData.x, 0, startNode.mapData.z));
            movePath.Reverse();
            movePath.RemoveAt(0);
        }

        public List<Vector3> GetPath(MapData startPos, MapData endPos)
        {
            closed.Clear();
            if (endPos.blockName != BlockName.Walkable)
            {
                if (mapManager == null)
                {
                    mapManager = GameObject.Find("Manager").GetComponent<MapManager>();
                }
                foreach (MapData dir in mapManager.directions)
                {
                    MapData neightbor = dir + endPos;
                    int num = neightbor.x * mapManager.mapWidth + neightbor.z;

                    // 맵이 담긴 리스트 범위를 벗어날때
                    if (num < 0 || num > mapManager.mapData.Count)
                    {
                        continue;
                    }
                    MapData current = mapManager.GetMapData(neightbor.x, neightbor.z);
                    if (current == null)
                    {
                        continue;
                    }
                    // 통로가 아닐때
                    if (!(current.blockName == BlockName.Walkable ||
                        current.blockName == BlockName.Response))
                    {
                        continue;
                    }
                    // 맵범위를 벗어날때
                    if (neightbor.x < 0 || neightbor.x >= mapManager.mapWidth || neightbor.z < 0 || neightbor.z >= mapManager.mapHeight)
                    {
                        continue;
                    }
                    // 이미 닫힌 목록에 있을때
                    if (IsClosed(neightbor))
                    {
                        continue;
                    }
                    endPos = neightbor;
                    break;
                }
            }

            BeginSearch(startPos, endPos);
            while (!done)
            {

                Search(lastNode);
            }

            GetPath();

            SetMovePath();

            reload++;

            return movePath;

        }
    }

}
