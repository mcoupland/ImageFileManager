using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.IO.Compression;
using System;
using NReco.VideoInfo;
using System.Diagnostics;

namespace ImageFileManager
{
    public class FileObject
    {
        public string JustExtension = string.Empty;
        public string SubPath = string.Empty;
        public string SubPathFileString = string.Empty;
        public string OriginalFile = string.Empty;
        public string RenameFolder = string.Empty;
        public string RenameFile = string.Empty;
        public string UniqueFolder = string.Empty;
        public string UniqueFile = string.Empty;
        public string DupeFolder = string.Empty;
        public string RootFolder = string.Empty;
        public string SubFolder = string.Empty;
        public string WorkingFolder = string.Empty;
        public string RebuildFolder = string.Empty;
        public long FileSize = 0;
        public int Width = 0;
        public int Height = 0;
        public bool IsUnique = false;

        public FileObject(string originalpath, string rootfolder, string workingFolder, string renamefolder, string uniquefolder, string dupefolder, string rebuildfolder, bool loadimagedata = false)
        {
            OriginalFile = originalpath;
            RootFolder = rootfolder;
            RenameFolder = renamefolder;
            UniqueFolder = uniquefolder;
            DupeFolder = dupefolder;
            WorkingFolder = workingFolder;
            JustExtension = Path.GetExtension(originalpath);
            SubFolder = OriginalFile.Replace(Path.GetDirectoryName(OriginalFile), "");
            SubPath = "";

            var segments = OriginalFile.Replace(RootFolder, "").Split(Path.DirectorySeparatorChar);
            for (int i = 0; i < segments.Count() - 1; i++)
            {
                if (string.IsNullOrWhiteSpace(segments[i])) { continue; }
                SubPath = $"{SubPath}{Path.DirectorySeparatorChar}{segments[i]}";
                SubPathFileString = $"{SubPathFileString} {segments[i]}";
            }
            SubPath = SubPath.Trim(Path.DirectorySeparatorChar);
            SubPathFileString = SubPathFileString.Trim();                       
            RebuildFolder = $"{rebuildfolder}{Path.DirectorySeparatorChar}{SubPath}";
            try
            {
                if (loadimagedata)
                {
                    var fi = new FileInfo(originalpath);
                    FileSize = fi.Length;
                    if (true) //isvideo
                    {
                        var ffProbe = new NReco.VideoInfo.FFProbe();
                        var videoInfo = ffProbe.GetMediaInfo(originalpath);
                        foreach (var s in videoInfo.Streams)
                        {
                            Width = s.Width;
                            Height = s.Height;
                        }
                    }
                    else
                    {                        
                        using (var image = Bitmap.FromFile(fi.FullName))
                        {
                            Width = image.Width;
                            Height = image.Height;
                        }
                    }
                }
            }
            catch { }
        }

        public string CreateCompareFile()
        {            
            RenameFile = CopyFile(OriginalFile, $"{RenameFolder}{Path.DirectorySeparatorChar}{SubPathFileString} {Path.GetFileName(OriginalFile)}");
            return RenameFile;
        }
        
        public string RebuildFileByFolder()
        {
            if(string.IsNullOrWhiteSpace(UniqueFile)) { return ""; }

            Directory.CreateDirectory(RebuildFolder);
            var count = Directory.EnumerateFiles(RebuildFolder).Count().ToString("00000");
            var file = CopyFile(UniqueFile, $"{RebuildFolder}{Path.DirectorySeparatorChar}{SubPathFileString} ({count}){Path.GetExtension(OriginalFile)}");
            return file;
        }

        public string CreateDuplicateFile()
        {
            var dupFile = CopyFile(RenameFile, $"{DupeFolder}{Path.DirectorySeparatorChar}{SubPathFileString}{Path.DirectorySeparatorChar}{Path.GetFileName(OriginalFile)}");            
            return dupFile;
        }

        public string CreateUniqueFile()
        {
            IsUnique = true;
            var newdir = $"{UniqueFolder}{Path.DirectorySeparatorChar}{SubPath}";
            Directory.CreateDirectory(newdir);
            var ct = Directory.GetFiles(newdir, "*", SearchOption.AllDirectories).Count();
            var name = $"{newdir}{Path.DirectorySeparatorChar}{SubPathFileString} [{ct.ToString("00000")}]{Path.GetExtension(OriginalFile)}";
            File.Copy(RenameFile, name);
            UniqueFile = name;
            return UniqueFile;
        }

        public string GetNewPath(string root, string file, string suffix = "", bool createDirectory = true)
        {
            suffix = string.IsNullOrWhiteSpace(suffix) ? suffix : $" {suffix}";
            var newPath = $"{root}{Path.DirectorySeparatorChar}{Path.GetFileNameWithoutExtension(file)}{suffix}{Path.GetExtension(RenameFile)}";
            if(createDirectory) { Directory.CreateDirectory(Path.GetDirectoryName(newPath)); }
            return newPath;
        }

        public static Bitmap ResizeRenamedImage(Image image, int width, int height)
        {
            var destRect = new System.Drawing.Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public string CopyFile(string source, string destination)
        {
            var final = GetUniqueFileName(destination);
            Directory.CreateDirectory(Path.GetDirectoryName(final));
            while (File.Exists(final))
            {
                final = GetUniqueFileName(final);
            }
            try
            {
                File.Copy(source, final);
            }
            catch
            {
                return CopyFile(source, final);
            }
            return final;
        }

        public string GetUniqueFileName(string fileName)
        {
            var folder = Path.GetDirectoryName(fileName);
            var folderName = new DirectoryInfo(Path.GetDirectoryName(OriginalFile)).Name;
            var extension = Path.GetExtension(fileName);
            var name = 0;
            var uniqueFileName = $"{folder}{Path.DirectorySeparatorChar}{folderName} {name.ToString("00000")}[{Guid.NewGuid().ToString()}]{extension}";
            while (File.Exists(uniqueFileName))
            {
                name++;
                uniqueFileName = $"{folder}{Path.DirectorySeparatorChar}{folderName} {name.ToString("00000")}[{Guid.NewGuid().ToString()}]{extension}";
            }
            return uniqueFileName;
        }
    }
}
