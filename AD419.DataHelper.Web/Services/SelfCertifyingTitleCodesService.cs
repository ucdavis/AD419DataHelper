using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AD419.DataHelper.Web.Helpers;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Services
{
    class SelfCertifyingTitleCodesService
    {
        private Models.PpsDataContext _ppsDataContext;

        public SelfCertifyingTitleCodesService(PpsDataContext ppsDataContext)
        {
            _ppsDataContext = ppsDataContext;
        }

        public IEnumerable<SelfCertifyingTitleCode> GetSelfCertifyingTitleCodesFromRows(DataRowCollection rows)
        {
            try
            {
                var selfCertifyingTitleCodes =
                    rows.ToEnumerable().Select(row => new SelfCertifyingTitleCode(row)).Where(r => !string.IsNullOrWhiteSpace(r.TitleCode));

                return selfCertifyingTitleCodes;
            }
            catch (Exception ex)
            {
                var message = ex.InnerException;
                return null;
            }
        }
    }
}
