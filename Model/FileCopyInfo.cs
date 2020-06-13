namespace SideBySideExplorer.Model
{
    public class FileCopyInfo
    {
        public int FileCount { get; set; }
        public int FinishedCount { get; set; }
        public int CurrentFileNum { get; set; }

        public long TotalSize { get; set; }
        public long CurrentSize { get; set; }

        public long TotalTransferred { get; set; }
        public long CurrentTransferred { get; set; }

        public double TotalPercent { get; set; }
        public double CurrentPercent { get; set; }
    }
}
