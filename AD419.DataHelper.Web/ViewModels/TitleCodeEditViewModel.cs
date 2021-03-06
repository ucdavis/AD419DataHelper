﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.ViewModels
{
    public class TitleCodeEditViewModel
    {
        public TitleCodeEditViewModel(Titles title, List<StaffType> staffTypes)
        {
            Title = title;

            // Initialize staff types select list:
            var staffTypesSelectList = new List<SelectListItem>();

            foreach (var staffType in staffTypes.OrderBy(s => s.Ad419LineNum).ThenBy(s => s.StaffTypeCode))
            {
                staffTypesSelectList.Add(new SelectListItem()
                {
                    Text = string.Format("{2} {0} ({1})", staffType.StaffTypeShortName, staffType.StaffTypeCode, staffType.Ad419LineNum),
                    Value = staffType.StaffTypeCode,
                    Selected =
                    (!string.IsNullOrEmpty(Title.StaffType) &&
                     Title.StaffType.Equals(staffType.StaffTypeCode))
                });
            }
            StaffTypesSelectList = staffTypesSelectList;
        }

        public Titles Title { get; set; }

        public List<SelectListItem> StaffTypesSelectList { get; set; }
    }
}