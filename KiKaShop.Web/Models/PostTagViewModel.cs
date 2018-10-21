using KiKaShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KiKaShop.Web.Models
{
    public class PostTagViewModel
    {
        public int PostID { set; get; }

        public string TagID { set; get; }

        public virtual Post Post { set; get; }

        public virtual Tag Tag { set; get; }
    }
}