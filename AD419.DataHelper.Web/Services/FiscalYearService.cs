using System;
using System.Linq;
using System.Web;
using System.Web.Caching;
using AD419.DataHelper.Web.Models;

namespace AD419.DataHelper.Web.Services
{
    public class FiscalYearService
    {
        private const string CacheKey = "FiscalYear-Cache";
        private static Cache Cache { get { return HttpContext.Current.Cache; } }

        private readonly AD419DataContext _dbContext;

        public FiscalYearService(AD419DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int FiscalYear
        {
            get
            {
                // check cache
                var fetch = Cache.Get(CacheKey);
                if (fetch != null)
                {
                    return (int) fetch;
                }

                // check database
                var query = _dbContext.Database.SqlQuery<object>("SELECT TOP 1 FiscalYear FROM CurrentFiscalYear");
                var result = query.FirstOrDefault();
                if (result != null)
                {
                    // save to cache
                    var year = (int) result;
                    CacheFiscalYear(year);
                    return year;
                }

                // check current year
                var current = CurrentFiscalYear();
                CacheFiscalYear(current);
                PersistFiscalYear(current);
                return current;
            }
            set
            {
                PersistFiscalYear(value);
                CacheFiscalYear(value);
            }
        }

        public DateTime FiscalStartDate
        {
            get
            {
                return new DateTime(FiscalYear - 1, 10, 1, 0, 0, 0, DateTimeKind.Utc);
            }
        }

        public DateTime FiscalEndDate
        {
            get
            {
                return new DateTime(FiscalYear, 10, 1, 0, 0, 0, DateTimeKind.Utc);
            }
        }

        private int CurrentFiscalYear()
        {
            var today = DateTime.UtcNow.Date;
            var end = new DateTime(today.Year, 10, 1);

            if (today > end)
            {
                return today.Year + 1;
            }

            return today.Year;
        }

        private void CacheFiscalYear(int year)
        {
            Cache.Insert(CacheKey, year, null, DateTime.UtcNow.AddDays(1), Cache.NoSlidingExpiration);
        }

        private void PersistFiscalYear(int year)
        {
            _dbContext.Database.ExecuteSqlCommand("INSERT INTO CurrentFiscalYear (FiscalYear) VALUES (@p0)", year);
        }
    }
}