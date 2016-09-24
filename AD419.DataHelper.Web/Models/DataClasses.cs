using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AD419.DataHelper.Web.Models
{
    public class DataClasses
    {
        public List<FileNames> GetFiles(string directory)
        {
            var lstFiles = new List<FileNames>();
            var dirInfo = new DirectoryInfo(directory);

            int i = 0;
            foreach (var item in dirInfo.GetFiles().Where(f => f.Name.EndsWith(".pdf")))
            {
                lstFiles.Add(new FileNames()
                {

                    FileId = i + 1,
                    FileName = item.Name,
                    TimeStamp = item.CreationTime,
                    FilePath = dirInfo.FullName + @"\" + item.Name
                });
                i = i + 1;
            }

            return lstFiles;
        }

        public enum DataTypes
        {
            None, Hidden, Label, TextBox, DropDownList
        }

        public class ListItem
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }

        public class TemplateField
        {
            public TemplateField()
            {
                Items = new List<ListItem>();
            }
            public string Alias { get; set; }
            public string Tag { get; set; }
            public string Text { get; set; }
            public DataTypes DataType { get; set; }
            public List<ListItem> Items { get; set; }
        }

        public class FileNames
        {
            public int FileId { get; set; }
            public string FileName { get; set; }
            public DateTime TimeStamp { get; set; }
            public string FilePath { get; set; }
        }
    }
}