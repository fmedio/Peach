using System;
using System.IO;

namespace Peach
{
    public class TempStorage
    {
        private readonly DirectoryInfo _root;

        public TempStorage()
        {
            string tempPath = Path.GetTempPath();
            _root = new DirectoryInfo(Path.Combine(tempPath, "Peach"));
            _root.CreateMaybe();
            CleanUpSilently();
        }

        public DirectoryInfo ObtainTempDir()
        {
            CleanUpSilently();
            var tempDir = new DirectoryInfo(Path.Combine(_root.FullName, Guid.NewGuid().ToString()));
            tempDir.CreateMaybe();
            return tempDir;
        }

        private void CleanUpSilently()
        {
            Directory.GetFiles(_root.FullName)
                .ForEach(f =>
                             {
                                 try
                                 {
                                     File.Delete(f);
                                 }
                                 catch (Exception e)
                                 {
                                 }
                             });

            Directory.GetDirectories(_root.FullName)
                .ForEach(d =>
                             {
                                 try
                                 {
                                     Directory.Delete(d, true);
                                 }
                                 catch (Exception e)
                                 {
                                 }
                             });
        }
    }
}