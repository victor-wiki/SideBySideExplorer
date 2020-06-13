using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SideBySideExplorer
{
    public class MyFileSystemInfo : IEquatable<MyFileSystemInfo>
    {
        public MyFileSystemInfo(FileSystemInfo fileSystemInfo)
        {
            if (fileSystemInfo == null) throw new ArgumentNullException("fileSystemInfo");
            this.info = fileSystemInfo;
            this.IsDrive = this.IsDirectory && this.AsDirectory.FullName == Path.GetPathRoot(fileSystemInfo.FullName);
        }

        public MyFileSystemInfo Parent { get; set; }
        public List<MyFileSystemInfo> Children { get; set; } = new List<MyFileSystemInfo>();

        public bool IsDirectory { get { return this.AsDirectory != null; } }

        public DirectoryInfo AsDirectory { get { return this.info as DirectoryInfo; } }
        public FileInfo AsFile { get { return this.info as FileInfo; } }

        public FileSystemInfo Info
        {
            get { return this.info; }
        }
        private readonly FileSystemInfo info;

        public string Name
        {
            get { return this.info.Name; }
        }

        public string Extension
        {
            get { return this.info.Extension; }
        }

        public DateTime CreationTime
        {
            get { return this.info.CreationTime; }
        }

        public DateTime LastWriteTime
        {
            get { return this.info.LastWriteTime; }
        }

        public string FullName
        {
            get { return this.info.FullName; }
        }

        public FileAttributes Attributes
        {
            get { return this.info.Attributes; }
        }

        public long Length
        {
            get { return this.AsFile.Length; }
        }

        public bool IsDrive
        {
            get; private set;
        }

        public IEnumerable<MyFileSystemInfo> GetFileSystemInfos()
        {           
            if (this.IsDirectory)
            {
                this.Children.Clear();

                this.Children.AddRange(this.AsDirectory.GetFileSystemInfos()
                             .Where(item=> (item.Attributes & FileAttributes.Hidden) == 0)
                             .Select(item => new MyFileSystemInfo(item) { Parent = this })
                             );
                return this.Children;
            }

            return Enumerable.Empty<MyFileSystemInfo>();
        }        

        public bool Equals(MyFileSystemInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.info.FullName, this.info.FullName);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof(MyFileSystemInfo)) return false;
            return Equals((MyFileSystemInfo)obj);
        }
        public override int GetHashCode()
        {
            return (this.info != null ? this.info.FullName.GetHashCode() : 0);
        }
        public static bool operator ==(MyFileSystemInfo left, MyFileSystemInfo right)
        {
            return Equals(left, right);
        }
        public static bool operator !=(MyFileSystemInfo left, MyFileSystemInfo right)
        {
            return !Equals(left, right);
        }
    }
}
