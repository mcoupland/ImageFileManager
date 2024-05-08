using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ImageFileManager
{
    internal class Deprecated
    {

        //var fileCount = Directory.EnumerateFiles(SOURCEROOTDIRECTORY, "*.gif", SearchOption.AllDirectories).Count();
        //var fromIndex = 0;
        //var takeCount = 250;
        //var toIndex = 0;
        //while (toIndex < fileCount)
        //{
        //    toIndex = fromIndex + takeCount;
        //    if (toIndex > fileCount) { toIndex = fileCount - 1; }

        //    GetFileObjects(fromIndex, toIndex);
        //    fromIndex = toIndex;
        //}
        //private void rebuildNames()
        //{
        //    Directory.CreateDirectory(finalRootFolder);
        //    var uniqueFiles = Directory.GetFiles(UNIQUEROOTDIRECTORY, "*.gif", SearchOption.TopDirectoryOnly);
        //    for (int i = 0; i < uniqueFiles.Count(); i++)
        //    {
        //        if (i % 200 == 0)
        //        {
        //            Debug.WriteLine($"Step 4 {i}/{uniqueFiles.Count()} Rebuilding file paths");
        //        }
        //        var uniqueFile = new FileObject(uniqueFiles[i], NEWNAMEROOTDIRECTORY, ROOTDIRECTORY);
        //        uniqueFile.RebuildName(finalRootFolder);
        //    }
        //}

        //private void compareRenamed()
        //{
        //    var _comparationResult = getComparisonResults();
        //    duplicateGroupsCount = _comparationResult.Count();
        //    for (int i = 0; i < _comparationResult.Count(); i++)
        //    {
        //        if (i % 200 == 0)
        //        {
        //            Debug.WriteLine($"Step 3 {i}/{_comparationResult.Count()} Copy unique and dupe files");
        //        }

        //        //move unique
        //        var uniqueFile = $"{UNIQUEROOTDIRECTORY}{Path.DirectorySeparatorChar}{_comparationResult[i][0].FileName}";
        //        var fileobj = new FileObject(_comparationResult[i][0].FilePath, NEWNAMEROOTDIRECTORY, ROOTDIRECTORY);
        //        File.Copy(_comparationResult[i][0].FilePath, uniqueFile);

        //        //move dupes
        //        for (int x = 0; x < _comparationResult[i].Count(); x++)
        //        {
        //            var dupeFile = _comparationResult[i][x].FilePath.Replace(NEWNAMEROOTDIRECTORY, DUPEROOTDIRECTORY);
        //            File.Copy(_comparationResult[i][x].FilePath, dupeFile);
        //        }
        //    }
        //}

        //private string getNewName(int fileIndex)
        //{
        //    var dirSegment = Path.GetDirectoryName(Info.FullName).Replace(SourceRoot, "").Split('\\');
        //    var newDirName = $"{dirSegment[dirSegment.Length - 1].Trim()}";
        //    var indexString = $"[{fileIndex.ToString("0000")}]";
        //    var newDir = $"{RenameRoot}{Path.DirectorySeparatorChar}[{newDirName}]";
        //    return $"{newDir} {indexString}{Path.GetExtension(Name)}";
        //}

        //private string getSizeName(string fullPath, SizeF size)
        //{
        //    var sizeString = getSizeString(size);
        //    var sizeName = $"{Path.GetFileNameWithoutExtension(fullPath)} [{sizeString}]{Path.GetExtension(fullPath)}";
        //    return $"{SizeNameRoot}{Path.DirectorySeparatorChar}{sizeName}";
        //}

        //private string getSizeString(SizeF size)
        //{
        //    return $"{size.Width.ToString("0000")}x{size.Height.ToString("0000")}";
        //}

        //private SizeF getDimensions()
        //{
        //    var imgSize = new SizeF();
        //    try
        //    {
        //        using (FileStream file = new FileStream(Info.FullName, FileMode.Open, FileAccess.Read))
        //        {
        //            using (Image img = Image.FromStream(stream: file,
        //                                                useEmbeddedColorManagement: false,
        //                                                validateImageData: false))
        //            {
        //                imgSize = new SizeF(img.PhysicalDimension.Width, img.PhysicalDimension.Height);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine($"################### {Path.GetFileName(Info.FullName)} {ex.Message}");
        //        File.Copy(Info.FullName, $"{ErrorRoot}{Path.DirectorySeparatorChar}{Path.GetFileName(Info.FullName)}");
        //    }
        //    return imgSize;
        //}

        //public void Rename()
        //{
        //    File.Copy(Info.FullName, Rename, true);
        //}

        //public void MakeSize()
        //{
        //    File.Copy(Info.FullName, SizeName, true);
        //}
        //private string getFolderSegments(string folder)
        //{
        //    return folder.Replace(SOURCEROOTDIRECTORY, "").TrimStart(Path.DirectorySeparatorChar);
        //}

        //private string renameFilesInFolder(string filePath, string targetFolder)
        //{
        //    var fileExtension = Path.GetExtension(filePath);
        //    var filePathWithoutExt = filePath.Replace(fileExtension, "");
        //    var nameAndPathPieces = filePathWithoutExt.Replace(ROOTDIRECTORY, "").Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar);
        //    var newFileNameWithFolderStructure = "";
        //    for(int i = 0; i < nameAndPathPieces.Length; i++)
        //    {
        //        newFileNameWithFolderStructure = $"{newFileNameWithFolderStructure.Trim()} [{nameAndPathPieces[i].Trim()}]";
        //    }
        //    var file = $"{targetFolder}{Path.DirectorySeparatorChar}{newFileNameWithFolderStructure}{fileExtension}";
        //    Directory.CreateDirectory(Path.GetDirectoryName(file));
        //    File.Copy(filePath, file);
        //    return file;
        //}

        //private string rebuildPath(string filePath, string rebuildFolder)
        //{
        //    var fileInfo = new FileInfo(filePath);
        //    var justFolder = fileInfo.Directory;
        //    var justName = Path.GetFileNameWithoutExtension(fileInfo.FullName);
        //    var justExtension = fileInfo.Extension;
        //    var bracketedSegments = justName.Split("[").Where(x => !string.IsNullOrWhiteSpace(x.Trim())).ToList();
        //    var unbracketedSegments = new List<string>();
        //    foreach(var segment in bracketedSegments)
        //    {
        //        unbracketedSegments.Add(segment.Replace("]", "").Trim());
        //    }
        //    var subPathSegments = unbracketedSegments.Take(unbracketedSegments.Count() - 1);
        //    var subPath = string.Join(Path.DirectorySeparatorChar, subPathSegments);
        //    var subPathString = string.Join("] [", subPathSegments);
        //    var finalFolderName = $"{rebuildFolder}{Path.DirectorySeparatorChar}{subPath}";
        //    Directory.CreateDirectory(finalFolderName);
        //    var finalFileName = $"[{subPathString}] [{Directory.GetFiles(finalFolderName, "*", SearchOption.TopDirectoryOnly).Count().ToString("0000")}]{justExtension}";
        //    var finalFilePath = $"{finalFolderName}{Path.DirectorySeparatorChar}{finalFileName}";

        //    //{Path.DirectorySeparatorChar}{}{justExtension}";
        //    return "";
        //    //var segments = justName.Replace("] [", Path.DirectorySeparatorChar.ToString());


        //    //var path = segments.Trim('[').Trim(']');
        //    //var pieces = path.Split(Path.DirectorySeparatorChar);
        //    //var finalFolder = $"{rebuildFolder}{Path.DirectorySeparatorChar}{subPath}";


        //    //Directory.CreateDirectory(finalFolder);

        //    //var newIndex = $"[{Directory.GetFiles(finalFolder).Count().ToString("0000")}]";
        //    //var nameWithIndex = fileInfo.Name.Replace(fileIndex, newIndex);
        //    //var finalFile = $"{finalFolder}{Path.DirectorySeparatorChar}{nameWithIndex}";
        //    //copyFile(filePath, finalFile);
        //    //return finalFolder;
        //}

        //private string rebuildPath(string filePath, string rebuildFolder)
        //{
        //    var newName = Path.GetFileName(filePath);
        //    var segments = Path.GetFileNameWithoutExtension(filePath).Replace("] [", Path.DirectorySeparatorChar.ToString());
        //    var path = segments.Trim('[').Trim(']');
        //    var pieces = path.Split(Path.DirectorySeparatorChar);
        //    var subPath = string.Join(Path.DirectorySeparatorChar, pieces.Take(pieces.Length - 1));
        //    var finalFolder = $"{rebuildFolder}{Path.DirectorySeparatorChar}{subPath}";

        //    Directory.CreateDirectory(finalFolder);

        //    var fileIndex = newName.Split('[').Last().Replace("]", string.Empty);
        //    var nameWithIndex = newName.Replace($"[{fileIndex}]", Directory.GetFiles(finalFolder).Count().ToString("0000"));
        //    var finalFile = $"{finalFolder}{Path.DirectorySeparatorChar}{nameWithIndex}";
        //    copyFile(filePath, finalFile);
        //    return finalFolder;
        //}

        //private string renameFilesInFolder(string filePath, string targetDir)
        //{
        //    var segmentString = getFolderSegmentsString(Path.GetDirectoryName(filePath));
        //    var fileName = Path.GetFileName(filePath).Trim('_');
        //    var renamed = $"{targetDir}{Path.DirectorySeparatorChar}[{segmentString}] {fileName}";

        //    Directory.CreateDirectory(Path.GetDirectoryName(renamed));

        //    var dir = Path.GetDirectoryName(renamed);
        //    var count = Directory.GetFiles(dir).Count().ToString("00000");
        //    var ext = Path.GetExtension(renamed);
        //    renamed = $"{dir}{Path.DirectorySeparatorChar}[{segmentString}] [{count}]{ext}";
        //    File.Copy(filePath, renamed);
        //    return renamed;
        //}

        //private void LoadFileObjects()
        //{
        //    return;
        //    var stopw = new Stopwatch();
        //    Directory.CreateDirectory(ERRORROOTDIRECTORY);

        //    stopw.Start();
        //    loadFileObjects();
        //    Debug.WriteLine($"Step 1 Loaded {fileObjects.Count()} file objects in {stopw.Elapsed.Minutes} minutes {stopw.Elapsed.Seconds} seconds");

        //    stopw.Restart();
        //    rename();
        //    Debug.WriteLine($"Step 2 Renamed {fileObjects.Count()} files in {stopw.Elapsed.Minutes} minutes {stopw.Elapsed.Seconds} seconds");

        //    stopw.Restart();
        //    makeSize();
        //    Debug.WriteLine($"Step 3 Make size {fileObjects.Count()} files in {stopw.Elapsed.Minutes} minutes {stopw.Elapsed.Seconds} seconds");

        //    stopw.Restart();
        //    finalize();
        //    stopw.Stop();
        //    Debug.WriteLine($"Step 4 Finalized {fileObjects.Count()} files in {stopw.Elapsed.Minutes} minutes {stopw.Elapsed.Seconds} seconds");

        //    Debug.WriteLine($"Filtered {fileObjects.Count()} files to {Directory.GetFiles(FINALROOTDIRECTORY, "*.*", SearchOption.AllDirectories).Count()} unique files");
        //}

        //private void loadFileObjects()
        //{
        //    fileObjects = new List<FileObject>();
        //    var files = Directory.GetFiles(SOURCEROOTDIRECTORY, "*.gif", SearchOption.AllDirectories);
        //    for (int i = 0; i < files.Count(); i++)
        //    {
        //        fileObjects.Add(new FileObject(files[i], i + 1, SOURCEROOTDIRECTORY, NEWNAMEROOTDIRECTORY, SIZEROOTDIRECTORY, ERRORROOTDIRECTORY));
        //        if (i > 0 && i % 100 == 0)
        //        {
        //            Debug.WriteLine($"Step 1 Loading {i + 1}/{files.Count()} {Path.GetFileName(fileObjects[i].Name)} -> {Path.GetFileName(fileObjects[i].ReName)} -> {Path.GetFileName(fileObjects[i].SizeName)}");
        //        }
        //    }
        //}


        //private void makeSize()
        //{
        //    Directory.CreateDirectory(SIZEROOTDIRECTORY);
        //    for (int i = 0; i < fileObjects.Count(); i++)
        //    {
        //        fileObjects[i].MakeSize();
        //        if (i > 0 && i % 100 == 0)
        //        {
        //            Debug.WriteLine($"Step 3 Making size {i}/{fileObjects.Count()} {fileObjects[i].ReName}");
        //        }
        //    }
        //}


        //private void compareImages()
        //{
        //    List<bool> iHash1 = GetHash(new Bitmap(@"C:\mykoala1.jpg"));
        //    List<bool> iHash2 = GetHash(new Bitmap(@"C:\mykoala2.jpg"));

        //    //determine the number of equal pixel (x of 256)
        //    int equalElements = iHash1.Zip(iHash2, (i, j) => i == j).Count(eq => eq);
        //}

        //private void finalize()
        //{
        //    var i = 1;
        //    foreach (var sizes in fileObjects.GroupBy(x => x.FileSize))
        //    {
        //        if (i % 100 == 0)
        //        {
        //            Debug.WriteLine($"Step 4 Finalizing {i}/{fileObjects.Count()}");
        //        }
        //        i++;
        //        var fileSizeString = $"{(sizes.Key / 1024 / 1024).ToString("###.00")}Mb";
        //        foreach (var size in sizes.GroupBy(x => x.ImgSize))
        //        {
        //            var finalDir = $"{Path.GetDirectoryName(size.First().OriginalPath).Replace(SOURCEROOTDIRECTORY, FINALROOTDIRECTORY)}";
        //            Directory.CreateDirectory(finalDir);

        //            var finalFile = $"{finalDir}{Path.DirectorySeparatorChar}{Path.GetFileName(size.First().ReName)}";
        //            File.Copy(size.First().ReName, finalFile, true);

        //            var imgSizeString = $"{size.Key.Width.ToString("0000")}x{size.Key.Height.ToString("0000")}";
        //            var sizeDirString = $"{size.Count().ToString("0000")} {fileSizeString} {imgSizeString}";
        //            var groupSizeDirString = $"{GROUPROOTDIRECTORY}{Path.DirectorySeparatorChar}{sizeDirString}";
        //            Directory.CreateDirectory(groupSizeDirString);

        //            foreach (var item in size)
        //            {
        //                var group = $"{groupSizeDirString}{Path.DirectorySeparatorChar}{Path.GetFileName(item.SizeName)}";
        //                File.Copy(item.SizeName, group, true);
        //                try
        //                {
        //                    File.Delete(item.ReName);
        //                    Directory.Delete(Path.GetDirectoryName(item.ReName));
        //                    File.Delete(item.SizeName);
        //                    Directory.Delete(Path.GetDirectoryName(item.SizeName));
        //                }
        //                catch (Exception ex)
        //                {
        //                    Debug.WriteLine(ex.ToString());
        //                }
        //            }
        //        }
        //    }
        //}

        //public static List<bool> GetHash(Bitmap bmpSource)
        //{
        //    List<bool> lResult = new List<bool>();
        //    //create new image with 16x16 pixel
        //    Bitmap bmpMin = new Bitmap(bmpSource, new System.Drawing.Size(16, 16));
        //    for (int j = 0; j < bmpMin.Height; j++)
        //    {
        //        for (int i = 0; i < bmpMin.Width; i++)
        //        {
        //            //reduce colors to true / false                
        //            lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
        //        }
        //    }
        //    return lResult;
        //}

        //private void compare(string rootFolder, string compareFolder)
        //{
        //    var dirs = Directory.GetDirectories(compareFolder, "*", SearchOption.AllDirectories);
        //    if (dirs == null || dirs.Count() == 0) { dirs = new string[] { compareFolder }; }

        //    for (int x = 0; x < dirs.Count(); x++)
        //    {
        //        var _comparationResult = getComparisonResults(dirs[x], 98);
        //        if (_comparationResult.Count() == 0) { continue; }

        //        var dirSegments = getFolderSegments(dirs[x]);

        //        for (int i = 0; i < _comparationResult.Count(); i++)
        //        {
        //            copyUniqueFile(dirSegments, _comparationResult[i][0].FilePath);
        //            copyDuplicateFiles(dirSegments, _comparationResult[i], i);
        //        }
        //    }
        //}

        //private void copyUniqueFile(string dirSegments, string fileToCopy)
        //{
        //    var uniqueFolderName = $@"{FINALROOTDIRECTORY}{Path.DirectorySeparatorChar}{dirSegments}";

        //    Directory.CreateDirectory(uniqueFolderName);
        //    File.Copy(fileToCopy, $@"{uniqueFolderName}{Path.DirectorySeparatorChar}{Path.GetFileName(fileToCopy)}");
        //}

        //private void copyDuplicateFiles(string dirSegments, List<ImgHash> result, int fileGroupIndex)
        //{
        //    for (int z = 0; z < result.Count(); z++)
        //    {
        //        if (z % 199 == 0)
        //        {
        //            Debug.WriteLine($"{z}/{result.Count()} Copying duplicate files");
        //        }
        //        var dupeFolderName = $@"{FINALROOTDIRECTORY}{Path.DirectorySeparatorChar}DUPE{Path.DirectorySeparatorChar}{dirSegments}";
        //        var dupeFolder = $@"{dupeFolderName}{Path.DirectorySeparatorChar}{fileGroupIndex.ToString("0000")} [{result.Count()} DUPES]";
        //        var dupeFile = $@"{dupeFolder}{Path.DirectorySeparatorChar}[{z.ToString("0000")}] {result[z].FileName}";
        //        Directory.CreateDirectory(dupeFolder);
        //        File.Copy(result[z].FilePath, dupeFile);
        //    }
        //}

        //private void copyDuplicateFiles(string dirSegments, List<ImgHash> result, int fileGroupIndex)
        //{
        //    for (int z = 0; z < result.Count(); z++)
        //    {
        //        if (z % 199 == 0)
        //        {
        //            Debug.WriteLine($"{z}/{result.Count()} Copying duplicate files");
        //        }
        //        var dupeFolderName = $@"{FINALROOTDIRECTORY}{Path.DirectorySeparatorChar}DUPE{Path.DirectorySeparatorChar}{dirSegments}";
        //        var dupeFolder = $@"{dupeFolderName}{Path.DirectorySeparatorChar}{fileGroupIndex.ToString("0000")} [{result.Count()} DUPES]";
        //        var dupeFile = $@"{dupeFolder}{Path.DirectorySeparatorChar}[{z.ToString("0000")}] {result[z].FileName}";
        //        Directory.CreateDirectory(dupeFolder);
        //        File.Copy(result[z].FilePath, dupeFile);
        //    }
        //}

        //    }

        //    using System;
        //using System.Collections.Generic;
        //using System.Diagnostics;
        //using System.Drawing;
        //using System.IO;
        //using System.Linq;
        //using System.Reflection;
        //using System.Security.RightsManagement;
        //using System.Text;
        //using System.Threading.Tasks;
        //using System.Windows.Media.Media3D;
        //using System.Xml.Linq;

        //namespace ImageFileManager
        //    {
        //        internal class FileObject
        //        {
        //            //public string Name = string.Empty;
        //            //public string RenamedFile = string.Empty;
        //            //public string SizeName = string.Empty;
        //            //public string SourceRoot = string.Empty;
        //            //public string RenameRoot = string.Empty;
        //            //public string SizeNameRoot = string.Empty;
        //            //public string ErrorRoot = string.Empty;

        //            //public long FileSize = 0;
        //            //public SizeF ImgSize = new SizeF();

        //            //public FileInfo? Info = null;
        //            //public string JustFolder = string.Empty;
        //            //public string JustName = string.Empty;
        //            public string JustExtension = string.Empty;
        //            public string SubPath = string.Empty;
        //            public string SubPathFileString = string.Empty;
        //            //public string FinalFileFolder = string.Empty;
        //            //public string FinalName = string.Empty;
        //            //public string FinalPath = string.Empty;
        //            //public bool IsUnique = false;


        //            public string OriginalFile = string.Empty;
        //            public string RenameFolder = string.Empty;
        //            public string RenameFile = string.Empty;
        //            public string UniqueFolder = string.Empty;
        //            public string UniqueFile = string.Empty;
        //            public string DupeFolder = string.Empty;
        //            public string RootFolder = string.Empty;
        //            public string SubFolder = string.Empty;

        //            public FileObject(string originalpath, string rootfolder, string renamefolder, string uniquefolder, string dupefolder)
        //            {
        //                OriginalFile = originalpath;
        //                RootFolder = rootfolder;
        //                RenameFolder = renamefolder;
        //                UniqueFolder = uniquefolder;
        //                DupeFolder = dupefolder;
        //                JustExtension = Path.GetExtension(originalpath);
        //                SubFolder = OriginalFile.Replace(RootFolder, "");

        //                var segments = OriginalFile.Replace(RootFolder, "").Split(Path.DirectorySeparatorChar);
        //                for (int i = 0; i < segments.Count() - 1; i++)
        //                {
        //                    SubPath = $"{SubPath}{Path.DirectorySeparatorChar}{segments[i]}";
        //                    SubPathFileString = $"{SubPathFileString} {segments[i]}";
        //                }
        //                SubPath = SubPath.Trim(Path.DirectorySeparatorChar);
        //                SubPathFileString = SubPathFileString.Trim();
        //            }

        //            public string GetRenameFile()
        //            {
        //                Directory.CreateDirectory(RenameFolder);

        //                // rename\sub path\sub path count.gif
        //                var fileCount = Directory.GetFiles(RenameFolder).Count().ToString("00000");
        //                RenameFile = $"{RenameFolder}{Path.DirectorySeparatorChar}{SubPathFileString} {fileCount}{JustExtension}";
        //                File.Copy(OriginalFile, RenameFile);
        //                return RenameFile;
        //            }

        //            public string RebuildUnique()
        //            {
        //                UniqueFolder = $"{UniqueFolder}{Path.DirectorySeparatorChar}{SubPath}{Path.DirectorySeparatorChar}";
        //                Directory.CreateDirectory(UniqueFolder);

        //                // unique\sub path\sub path count.gif            
        //                var fileCount = Directory.GetFiles(UniqueFolder).Count().ToString("00000");
        //                UniqueFile = $"{UniqueFolder}{SubPathFileString} {fileCount}{JustExtension}";
        //                File.Copy(OriginalFile, UniqueFile);
        //                return UniqueFile;
        //            }
        //        }
    }

}
