using System;
using System.Text;
using System.Xml.Serialization;

namespace SerializationApp.Components
{
    [XmlInclude(typeof(DirectoryComponent))]
    [XmlInclude(typeof(FileComponent))]
    [Serializable]
    public abstract class Component
    {
        public string Name { get; set; }

        public int Depth { get; set; }

        public bool IsLeaf { get; set; }


        public Component() { }


        public virtual void Add(Component component) { }

        public virtual void Remove(Component component) { }

        public abstract void ToString(StringBuilder sb, string parentNodes);
       
    }
}