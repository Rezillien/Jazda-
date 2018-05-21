using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts.FileObjects
{
    public class Root
    {
        public List<CrossRoad> crossRoads { get; set; }
        public List<Street> streets { get; set; }
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
}