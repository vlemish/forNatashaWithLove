using System;
using System.IO;
using System.Text;

namespace SerializationApp.Components
{
    [Serializable]
    public class FileComponent : Component
    {
        public int Size { get; set; }

        public DateTime CreationTime { get; set; }

        public FileAttributes Attributes { get; set; }


        public FileComponent(string name, int size, int depth,
            DateTime creationTime, FileAttributes attributes, bool isLeaf)
        {
            Name = name;
            Size = size;
            Depth = depth;
            CreationTime = creationTime;
            Attributes = attributes;
            IsLeaf = isLeaf;
        }

        public FileComponent() { }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            // because max length of the file name can be up to 259 characters and extension also up to 259
            // we need to generalize the name to some format. If we won't do it, the output to a console will be broken.
            var name = Name.Length > 15
                ? Name.Substring(0, 15) + "..."
                : Name;

            sb.Append(name)
                .Append(' ', 20 - name.Length + 3)
                .Append(Size)
                .Append(' ', 10 - Size.ToString().Length + 3)
                .Append(' ', 22 - CreationTime.ToString().Length + 3)
                .Append(CreationTime)
                .Append("\t")
                .Append(Attributes)
                .Append("\t");

            return sb.ToString();
        }

        public override void ToString(StringBuilder sb, string parentNodes)
        {
            if (IsLeaf)
            {             
                sb.Append(parentNodes);
                sb.Append(' ', 6);
                sb.Append(ToString());
                sb.AppendLine();
            }
            else
            {
                sb.Append(parentNodes);
                sb.Append('|');
                sb.Append(' ', 3);
                sb.Append(ToString());
                sb.AppendLine();
            }
        }

        public override bool Equals(object obj)
        {
            var file = obj as FileComponent;

            if (file == null)
            {
                return false;
            }

            return this.Name == file.Name && this.Size == file.Size
                && this.CreationTime == file.CreationTime && this.Attributes == file.Attributes
                && this.Depth == file.Depth && this.IsLeaf == file.IsLeaf;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (hash * 34) + Name.GetHashCode();
                hash = (hash * 23) + Depth.GetHashCode();
                hash = (hash * 23) + Size.GetHashCode();
                hash = (hash * 23) + CreationTime.GetHashCode();
                hash = (hash * 23) + Attributes.GetHashCode();
                hash = (hash * 23) + IsLeaf.GetHashCode();
                return hash;
            }
        }
    }
}