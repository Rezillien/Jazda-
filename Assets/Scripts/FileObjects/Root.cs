using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

namespace Assets.Scripts.FileObjects
{
    public class Root
    {
        public List<CrossRoad> crossRoads { get; set; }
        public List<Street> streets { get; set; }
        public List<LaneTo> lanes { get; set; }
        public List<Car> cars { get; set; }
        public List<CarCreator> carCreators { get; set; }
        public List<CarDestroyer> carDestroyers { get; set; }
    }
    public class CrossRoad {
        public int id { get; set; }
        public float x { get; set; }
        public float y { get; set; }
    }
    public class Lane {
        public int way { get; set; }
    }
    public class Street {
        public int crossRoadFrom { get; set; }
        public int crossRoadTo { get; set; }
        public List<Lane> lanes { get; set; }
    }

    public class LaneTo
    {
        public float x { get; set; }
        public float y { get; set; }
        public float anglex { get; set; }
        public float angley { get; set; }
        public float anglez { get; set; }
        public float scalex { get; set; }
        public float scaley { get; set; }
        public float way { get; set; }
    }

    public class StreetTo
    {
        public float x { get; set; }
        public float y { get; set; }
        public float angle { get; set; }
        public float scalex { get; set; }
        public float scaley { get; set; }

    }

    public class Car
    {
        public float x { get; set; }
        public float y { get; set; }
        public float angle { get; set; }
    }

    public class CarCreator
    {
        public float x { get; set; }
        public float y { get; set; }
        public float angle { get; set; }
        public float interval { get; set; }
        public List<Point> points { get; set; }
        public string name { get; set; }
        public int angleStart { get; set; }
        public int angleEnd { get; set; }
    }

    public class CarDestroyer
    {
        public float x { get; set; }
        public float y { get; set; }
        public float angle { get; set; }
        public float scalex { get; set; }
        public float scaley { get; set; }
    }

    public class Point
    {
        public float x { get; set; }
        public float y { get; set; }
    }
}