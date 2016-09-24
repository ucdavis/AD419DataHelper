using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
                    FilePath = dirInfo.FullName + @"\" + item.Name,
                    FileLength = item.Length
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
            [Display(Name = "Id")]
            public int FileId { get; set; }

            [Display(Name = "File Name")]
            public string FileName { get; set; }

            [Display(Name = "Timestamp")]
            public DateTime TimeStamp { get; set; }

            [Display(Name = "File Path")]
            public string FilePath { get; set; }

            private long _fileLength;

            [Display(Name = "File Length")]
            [DisplayFormat(DataFormatString = "{0:0,0}")]
            public long FileLength
            {
                get { return _fileLength/1024; }
                set { _fileLength = value; }
            }
        }
    }
}