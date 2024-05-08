using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using File = System.IO.File;
using Path = System.IO.Path;

namespace ImageFileManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region Implement INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion  Implement INotifyPropertyChanged

        #region Fields
        private string sourceFolder;
        private string sourceRoot;
        private string workingFolder;
        private string renameFolder;
        private string dedupeFolder;
        private string uniqueFolder;
        private string rebuildFolder;        
        private bool checkDimensions = true;
        private int shortUpdateIntervalInMilliseconds = 1000;
        private int tinyDelayInMilliseconds = 5;
        private int tolerance = 96;
        private int hashDimension = 16;
        private int duplicateGroupsCount = 0;
        private Stopwatch stopWatch = new Stopwatch();
        private List<FileObject> fileObjects = new List<FileObject>();
        private string searchPattern;
        private string imagePattern = "jpg, jpeg, bmp, png, tiff, raw, nef, dng";
        private string elapsedTime;
        private int count = 0;
        private int previousCount = 0;
        private static List<string> WorkingFiles = new List<string>();
        #endregion

        #region Properties
        public string SourceFolder { get => sourceFolder; set { sourceFolder = value; OnPropertyChanged("SourceFolder"); } }
        public string SourceRoot { get => sourceRoot; set { sourceRoot = value; OnPropertyChanged("SourceFolder"); } }
        public string WorkingFolder { get => workingFolder; set { workingFolder = value; OnPropertyChanged("WorkingFolder"); } }
        public string RenameFolder { get => renameFolder; set { renameFolder = value; OnPropertyChanged("RenameFolder"); } }
        public string DedupeFolder { get => dedupeFolder; set { dedupeFolder = value; OnPropertyChanged("DedupeFolder"); } }
        public string UniqueFolder { get => uniqueFolder; set { uniqueFolder = value; OnPropertyChanged("UniqueFolder"); } }
        public string RebuildFolder { get => rebuildFolder; set { rebuildFolder = value; OnPropertyChanged("RebuildFolder"); } }
        public bool CheckDimensions { get => checkDimensions; set { checkDimensions = value; OnPropertyChanged("CheckDimensions"); } }
        public int Tolerance { get => tolerance; set { tolerance = value; OnPropertyChanged("Tolerance"); } }
        public int HashDimension { get => hashDimension; set { hashDimension = value; OnPropertyChanged("HashDimension"); } }
        public int DuplicateGroupsCount { get => duplicateGroupsCount; set { duplicateGroupsCount = value; OnPropertyChanged("DuplicateGroupsCount"); } }        
        public Stopwatch StopWatch { get => stopWatch; set { stopWatch = value; OnPropertyChanged("StopWatch"); } }
        public List<FileObject> FileObjects { get => fileObjects; set { fileObjects = value; OnPropertyChanged("FileObjects"); } }
        public string SearchPattern { get => searchPattern; set { searchPattern = value; OnPropertyChanged("SearchPattern"); } }
        public string InfoText { get => _asyncDialogFeedback; set { _asyncDialogFeedback = value; OnPropertyChanged("InfoText"); } }
        public string ElapsedTime { get => elapsedTime; set { elapsedTime = value; OnPropertyChanged("ElapsedTime"); } }

        public string LargeDir = @"C:\.DO NOT SHARE\NSFW MOVIES\SUBJECTS\too big";
        #endregion INotify Properties

        #region Async Fields
        private string output;
        private string _asyncDialogFeedback;
        private DispatcherTimer _asyncElapsedTimer = new DispatcherTimer(DispatcherPriority.Send);
        private DateTime _startTime;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            SearchPattern = imagePattern;
            SourceFolder = @"C:\Images";
            SourceRoot = SourceFolder;
            UniqueFolder = $@"{SourceFolder}_UNIQUE";
            WorkingFolder = $@"{SourceFolder}\__WORKING";
            RenameFolder = $@"{workingFolder}\RENAME";
            DedupeFolder = $@"{workingFolder}\DUPE";
            RebuildFolder = $@"{workingFolder}\REBUILD";            
            InfoText = "Application Started";
            WorkingFiles.Clear();
        }

        private void InitDirectories()
        {
            Directory.CreateDirectory(WorkingFolder);
            Directory.CreateDirectory(RenameFolder);
            Directory.CreateDirectory(LargeDir);
            Directory.CreateDirectory(DedupeFolder);
            Directory.CreateDirectory(UniqueFolder);
            Directory.CreateDirectory(RebuildFolder);
        }

        private void InitializeElapsedTimer()
        {
            _asyncElapsedTimer = new DispatcherTimer();
            _asyncElapsedTimer.Interval = TimeSpan.FromMilliseconds(shortUpdateIntervalInMilliseconds);
            _asyncElapsedTimer.Tick += _asyncElapsedTimer_Tick;
        }

        private void GetFileObjects(string patterns, bool checkDimensions)
        {
            foreach (var pattern in patterns.Split(','))
            {
                WorkingFiles.AddRange(Directory.GetFiles(SourceFolder, $"*.{pattern.Trim()}", System.IO.SearchOption.AllDirectories));
            }
            //WorkingFiles.AddRange(Directory.GetFiles(SourceFolder, "*.*", System.IO.SearchOption.AllDirectories));
            output = "";
            Parallel.ForEach(WorkingFiles, file =>
            {
                fileObjects.Add(new FileObject(file, SourceFolder, WorkingFolder, RenameFolder, UniqueFolder, DedupeFolder, RebuildFolder, checkDimensions));
                Interlocked.Increment(ref count);
                AsyncUpdate($"Step 1 of 4 Getting files: {count}/{WorkingFiles.Count()}");
            });
        }

        private void RenameFileObjects()
        {
            output = "";
            count = 0;
            var folderGroups = fileObjects.GroupBy(x => x.SubFolder).ToList();
            foreach(var group in folderGroups ) 
            {
                Parallel.ForEach(group, file =>
                {
                    Interlocked.Increment(ref count);
                    file.CreateCompareFile();
                    AsyncUpdate($"Step 2 of 4 Renaming files: {count}/{fileObjects.Count()}");
                });
            }
        }

        private void CompareByImageProperties()
        {
            output = "";
            previousCount = count;
            var sizeGroups = fileObjects.GroupBy(x => x.FileSize);
            //Get unique files
            Parallel.ForEach(sizeGroups, sizeGroup =>
            {
                var widthGroups = sizeGroup.GroupBy(x => x.Width);
                foreach (var widthGroup in widthGroups)
                {
                    var heightGroups = widthGroup.GroupBy(x => x.Height);
                    foreach (var heightGroup in heightGroups)
                    {
                        var dupFileName = "";
                        GetFirstUnique(heightGroup).IsUnique = true;
                        AsyncUpdate($"Step 3 of 4 Comparing files by image properties: {count - previousCount}/{fileObjects.Count()}");
                    }
                }
            });

            //Save unique files
            foreach(var file in fileObjects.Where(x => x.IsUnique))
            {
                var folder = new DirectoryInfo(Path.GetDirectoryName(file.OriginalFile));
                var dirname = folder.Name;
                var newdir = $"{UniqueFolder}{Path.DirectorySeparatorChar}{file.SubPath}";
                Directory.CreateDirectory(newdir.ToUpper());
                var ct = Directory.GetFiles(newdir, "*", SearchOption.AllDirectories).Count();
                
                var name = $"{newdir}{Path.DirectorySeparatorChar}{dirname} [{ct.ToString("00000")}]{Path.GetExtension(file.OriginalFile)}";
                while (File.Exists(name))
                {
                    ct++;
                    name = $"{newdir}{Path.DirectorySeparatorChar}{dirname} [{ct.ToString("00000")}]{Path.GetExtension(file.OriginalFile)}";
                }
                File.Move(file.RenameFile, name.ToUpper());
            }
        }

        private FileObject GetFirstUnique(IGrouping<int, FileObject> group)
        {
            return group.ElementAt(0);
        }

        private void RebuildFileObjects()
        {
            output = "";
            previousCount = count;
            var uniqueFiles = fileObjects.Where(x => x.IsUnique);
            Parallel.ForEach(uniqueFiles, fileObject =>
            {
                fileObject.RebuildFileByFolder();
                Interlocked.Increment(ref count);
                AsyncUpdate($"Step 4 of 4 Rebuilding file name: {count - previousCount}/{uniqueFiles.Count()}");
            });
        }

        private void CompareByHash()
        {
            var comparator = new ImgComparator(hashDimension);
            comparator.AddPicFolderByPath(RenameFolder);
            var _comparationResult = comparator.FindDuplicatesWithTollerance(tolerance);
            duplicateGroupsCount = _comparationResult.Count();
            for (int i = 0; i < _comparationResult.Count(); i++)
            {
                //move unique
                var uniqueFile = $"{UniqueFolder}{Path.DirectorySeparatorChar}{_comparationResult[i][0].FileName}";
                var fileobj = fileObjects.Where(x => x.RenameFile == _comparationResult[i][0].FilePath).First();
                fileobj.CreateUniqueFile();

                if (_comparationResult[i].Count() > 1)
                {
                    var dupeFolder = $"{DedupeFolder}{Path.DirectorySeparatorChar}Group {i.ToString("00000")}";
                    Directory.CreateDirectory(dupeFolder);
                    foreach (var result in _comparationResult[i])
                    {
                        var fi = new FileInfo(result.FilePath);
                        var count = Directory.GetFiles(dupeFolder, "*", SearchOption.AllDirectories).Count();
                        var dupFile = $"{dupeFolder}{Path.DirectorySeparatorChar}{Path.GetFileNameWithoutExtension(result.FileName)} {count.ToString("00000")}{Path.GetExtension(result.FileName)}";
                        //var oname = fileObjects.Where(x => x.RenameFile == result.FilePath).First().OriginalFile;

                        dupFile = $"{dupeFolder}{Path.DirectorySeparatorChar}{fileObjects.Where(x => x.RenameFile == result.FilePath).First().OriginalFile.Replace(SourceFolder, "").Replace(Path.DirectorySeparatorChar, ' ').Trim()}";


                        //oname = oname.Replace(SourceFolder, "");
                        //var dupFile = $"{dupeFolder}{Path.DirectorySeparatorChar}{oname}";
                        File.Copy(result.FilePath, dupFile);
                    }
                    var filecount = Directory.GetFiles(dupeFolder, "*", SearchOption.AllDirectories).Count();
                    Directory.Move(dupeFolder, $"{dupeFolder} [{filecount.ToString("000")} Files]");
                }
            }
        }

        private void JustRename()
        {
            var files = Directory.GetFiles(SourceFolder, "*.*", SearchOption.AllDirectories);
            for (int i = 0; i < files.Length; i++)
            {
                var subpath = "";
                var subpathfilestring = "";
                var segments = files[i].Replace(SourceFolder, "").Split(Path.DirectorySeparatorChar);
                for (int x = 0; x < segments.Count() - 1; x++)
                {
                    if (string.IsNullOrWhiteSpace(segments[x])) { continue; }
                    subpath = $"{subpath}{Path.DirectorySeparatorChar}{segments[x]}";
                    subpathfilestring = $"{subpathfilestring} {segments[x]}";
                }
                subpathfilestring = subpathfilestring.Trim();
                var tempfolder = $@"C:\_TEMP\{subpath}\";
                Directory.CreateDirectory(tempfolder);
                var filecount = Directory.EnumerateFiles(tempfolder, "*", SearchOption.TopDirectoryOnly).Count().ToString("0000");
                var thetempfilename = $"{tempfolder}{subpathfilestring} {filecount}{Path.GetExtension(files[i])}";
                File.Copy(files[i], thetempfilename);
            }
        }

        private void uiDedupeImages_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to continue?", "Continue?", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No) { return; }
            InitializeElapsedTimer();
            InitDirectories();
            LaunchDedupeTask();
        }

        private async void LaunchDedupeTask()
        {
            Task<int> task = RunDedupeTask();            
            StartTimer();
            await task;
            Finished();
        }

        public async Task<int> RunDedupeTask()
        {
            await Task.Run(() =>
            {
                DoDedupe();
            });
            return 0;
        }

        public void DoDedupe()
        {
            GetFileObjects(SearchPattern, CheckDimensions);
            RenameFileObjects();
            CompareByImageProperties();
            RebuildFileObjects();
        }

        private void AsyncUpdate(string message)
        {
            if (App.Current == null) { return; }
            App.Current.Dispatcher.Invoke((Action)delegate
            {
                output = $"{message}{Environment.NewLine}{output}";
                _asyncDialogFeedback = output;
                Task.Delay(tinyDelayInMilliseconds);
            });
        }

        private void _asyncElapsedTimer_Tick(object sender, EventArgs e)
        {
            Debug.WriteLine(_asyncDialogFeedback);

            var timeSpan = DateTime.Now.Subtract(_startTime);
            var perSecond = (count/timeSpan.TotalSeconds).ToString("##.00");

            ElapsedTime = $"{timeSpan.Minutes}m {timeSpan.Seconds}s [{perSecond} files per second]";

            InfoText = $"{_asyncDialogFeedback}{InfoText}";
            uiFeedback.CaretIndex = 0;
            _asyncDialogFeedback = "";
        }

        private void StartTimer()
        {
            ElapsedTime = "";
            _asyncDialogFeedback = "";
            count = 0;
            _startTime = DateTime.Now;
            _asyncElapsedTimer.Start();
        }

        private void Finished()
        {
            _asyncElapsedTimer.Stop();            
            var elapsedTime = DateTime.Now.Subtract(_startTime);
            InfoText = $"Finished in {elapsedTime.Minutes} minutes, {elapsedTime.Seconds} seconds{Environment.NewLine}{InfoText.TrimEnd(Environment.NewLine.ToCharArray())}{Environment.NewLine}Reduced from {fileObjects.Count()} files to {fileObjects.Where(x => x.IsUnique).Count()} files";
            ElapsedTime = "";
            _asyncDialogFeedback = "";
            count = 0;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Finished();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Finished();
        }
    }
}