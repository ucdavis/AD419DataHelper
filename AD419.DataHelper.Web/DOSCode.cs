namespace AD419.DataHelper.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DOSCode
    {
        [Key]
        [StringLength(5)]
        public string DOS_Code { get; set; }
    }
}
