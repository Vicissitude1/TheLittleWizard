using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLittleWizard {
    public class Dijkstra {
        public bool isSetup;
        private static Dijkstra instance;
        public static Dijkstra Instance {
            get {
                if (instance == null) {
                    instance = new Dijkstra();
                }
                return instance;
            }
        }

        private Node[,] map;
        private Node current;
        private Node end;
        private Node intermediary;
        private GameObject[] keys;

        private Dijkstra() {

        }

        public void SetupDijkstraPathFinder(Node[,] map, Node start, Node end, GameObject[] keys, Node intermediary) {
            this.map = map;
            this.current = start;
            this.end = end;
            this.keys = keys;
            this.intermediary = intermediary;

            isSetup = true;
        }

        List<Node> openList = new List<Node>();
        List<Node> closedList = new List<Node>();
        private Stack<Node> GeneratePathToGoal(Node goal) {
            foreach (Node n in map) {
                n.ResetFGX();
            }
            openList.Clear();
            closedList.Clear();
            openList.Add(current);
            current.h = (int)Vector2.Distance(current.position, goal.position);
            current.g = 0;
            current.f = current.h;
            while (openList.Count > 0) {
                Node chosenNode = openList[0];
                foreach (Node n in openList) {
                    if (chosenNode.f > n.f) {
                        chosenNode = n;
                    }
                }
                openList.Remove(chosenNode);
                closedList.Add(chosenNode);

                foreach (Node neighbour in chosenNode.neighbourHandler.AllNeighbhours) {
                    if (neighbour.traversable) {
                        neighbour.CalculateFGX(chosenNode, goal);
                        if (!closedList.Contains(neighbour) && !openList.Contains(neighbour)) {
                            openList.Add(neighbour);
                        }
                    }
                    if (neighbour == goal) {
                        Stack<Node> path = new Stack<Node>();
                        path.Push(neighbour);
                        Node indexer = neighbour;
                        while (indexer.parent != null && indexer.parent != indexer && !path.Contains(indexer.parent)) {
                            indexer = indexer.parent;
                            path.Push(indexer);
                        }
                        return path;
                    }
                }

            }
            return null;
        }

        bool Activate = false;
        Stopwatch sw = new Stopwatch();

        bool goalFound = false;
        bool intermediaryVisited = false;
        int closestKey;
        int keysFound;
        Stack<Node> path;

        public void Update() {
            if (isSetup) {
                if (Activate) {
                    if (sw.Elapsed.Milliseconds > 500) {
                        sw.Stop();
                        sw.Reset();
                        sw.Start();

                        if (!goalFound) {
                            if (intermediaryVisited) {
                                if (keysFound < 2) {
                                    if (closestKey == 1) {
                                        path = GeneratePathToGoal(map[(int)keys[0].position.X / GameWorld.nodeSize, (int)keys[0].position.Y / GameWorld.nodeSize]);
                                        if (path != null) {
                                            goalFound = true;
                                            keysFound++;
                                        } else {
                                            Activate = false;
                                        }
                                    } else {
                                        path = GeneratePathToGoal(map[(int)keys[1].position.X / GameWorld.nodeSize, (int)keys[1].position.Y / GameWorld.nodeSize]);
                                        if (path != null) {
                                            goalFound = true;
                                            keysFound++;
                                        } else {
                                            Activate = false;
                                        }
                                    }
                                } else {
                                    path = GeneratePathToGoal(end);
                                    if (path != null) {
                                        goalFound = true;
                                    } else {
                                        Activate = false;
                                    }
                                }
                            } else {
                                if (keysFound < 1) {
                                    path = GeneratePathToGoal(map[(int)keys[closestKey].position.X / GameWorld.nodeSize, (int)keys[closestKey].position.Y / GameWorld.nodeSize]);
                                    if (path != null) {
                                        goalFound = true;
                                        keysFound++;
                                    } else {
                                        Activate = false;
                                    }
                                } else {
                                    path = GeneratePathToGoal(intermediary);
                                    if (path != null) {
                                        goalFound = true;
                                        intermediaryVisited = true;
                                        if (keysFound >= 2 && intermediaryVisited) {
                                            Activate = false;
                                        }
                                    } else {
                                        Activate = false;
                                    }
                                }
                            }

                        } else {
                            if (path.Count > 0) {
                                current = path.Pop();
                                GameWorld.instance.player.position = current.Traverse();
                            } else {
                                goalFound = false;
                            }
                        }
                    }
                } else {
                    if (Keyboard.GetState().IsKeyDown(Keys.A)) {
                        Activate = true;
                        closestKey = Vector2.Distance(keys[0].position, current.position) < Vector2.Distance(keys[1].position, current.position) ? 0 : 1;
                        sw.Start();
                    }
                }
            }
        }
    }
}
