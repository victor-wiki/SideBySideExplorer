using BrightIdeasSoftware;
using System.Collections.Generic;

namespace SideBySideExplorer.Model
{
    public class FileClipBoard
    {
        public FileMoveMode Mode;
        public ObjectListView SourceListView;
        public IEnumerable<MyFileSystemInfo> SelectedItems;
    }
}
