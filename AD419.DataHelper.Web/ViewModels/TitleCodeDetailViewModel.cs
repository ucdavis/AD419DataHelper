using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.ViewModels
{
    public class TitleCodeDetailViewModel
    {
        public TitleCodeDetailViewModel(Titles title, StaffType staffType)
        {
            Title = title;
            StaffTypeLabel = staffType == null ? "None" : string.Format("{2} {0} ({1})", staffType.StaffTypeShortName, staffType.StaffTypeCode, staffType.Ad419LineNum);
        }

        public Titles Title { get; set; }

        public string StaffTypeLabel { get; set; }
    }
}