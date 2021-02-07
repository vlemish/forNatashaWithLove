using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SerializationApp.Components
{
    [Serializable]
    public class DirectoryComponent : Component
    {
        public bool IsRoot { get; set; }

        public List<Component> Components { get; } = new List<Component>();


        public DirectoryComponent(DirectoryInfo root)
        {
            Name = root.FullName;
            Depth = 0;
            IsRoot = true;

            Fill(root);          
        }

        public DirectoryComponent(DirectoryInfo directory, int depth, bool isLeaf)
        {
            Name = directory.Name;
            Depth = depth;
            IsLeaf = isLeaf;

            Fill(directory);
        }

        public DirectoryComponent() { }


        private void Fill(DirectoryInfo directory)
        {
            var depth = Depth;            
            depth++;

            try
            {
                var directories = directory.GetDirectories();

                foreach (var file in directory.GetFiles())
                {
                    IsLeaf = directories.Length < 1;
                    Add(new FileComponent(file.Name, (int)file.Length, Depth, file.CreationTimeUtc, file.Attributes, IsLeaf));
                }

                for (int i = 0; i < directories.Length - 1; i++)
                {
                    var dir = directories[i];
                    Add(new DirectoryComponent(dir, depth, false));
                }

                if (directories.Length > 0)
                {
                    var index = directories.Length - 1 > 0
                        ? directories.Length - 1
                        : 0;
                    var lastDir = directories[index];
                    Add(new DirectoryComponent(lastDir, depth, true));
                }
            }
            catch
            {
                // there is a potential error when we try to access the system file or directory
                // so, we need to skip those files and directories and continue the work.
            }
        }
             
        public override void Add(Component component)
        {
            Components.Add(component);
        }

        public override void Remove(Component component)
        {
            Components.Remove(component);
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            ToString(sb, "");
            return sb.ToString();
        }

        public override void ToString(StringBuilder sb, string parentNodes)
        {
            if (IsRoot)
            {
                sb.Append(Name);
                sb.AppendLine();
            }

            else if (IsLeaf)
            {
                sb.Append(parentNodes);
                sb.Append('\\')
                    .Append('-', 3);
                sb.Append(Name);
                sb.AppendLine();

                parentNodes += new string(' ', 3);
            }
            else
            {
                sb.Append(parentNodes);
                sb.Append('+');
                sb.Append('-', 3);
                sb.Append(Name);
                sb.AppendLine();

                parentNodes += '|';
                parentNodes += new string(' ', 3);
            }

            var lastFile = Components.FindLast(el => (el as FileComponent) != null);

            foreach (var component in Components)
            {
                component.ToString(sb, parentNodes);                

                if (lastFile != null && component.Name.Equals(lastFile.Name)) 
                {
                    sb.Append(parentNodes);

                    if (!lastFile.IsLeaf)
                    {
                        sb.Append('|');
                    }

                    sb.AppendLine();
                }
            }
        }

        public override bool Equals(object obj)
        {
            var directory = obj as DirectoryComponent;

            if (directory == null)
            {
                return false;
            }
            if (this.Name != directory.Name || this.IsLeaf != directory.IsLeaf || 
                this.Depth != directory.Depth ||this.IsRoot != directory.IsRoot || this.Components.Count != directory.Components.Count ) 
            {
                return false;
            }

            for (int i = 0; i < this.Components.Count; i++)
            {
                var intern = this.Components[i];
                var external = directory.Components[i];

                intern.Equals(external);
            }

            return true;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = (hash * 34) + Name.GetHashCode();
                hash = (hash * 23) + Depth.GetHashCode();
                hash = (hash * 23) + IsRoot.GetHashCode();
                hash = (hash * 23) + IsLeaf.GetHashCode();
                return hash;
            }
        }
    }
}