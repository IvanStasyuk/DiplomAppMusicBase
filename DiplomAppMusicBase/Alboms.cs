//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DiplomAppMusicBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class Alboms
    {
        public int idAlbom { get; set; }
        public string NameSinger { get; set; }
        public string FamiliaSinger { get; set; }
        public string PatronymicSinger { get; set; }
        public string NameAlbom { get; set; }
        public Nullable<int> CountCompositions { get; set; }
        public string Janr { get; set; }
        public Nullable<System.DateTime> YearRelease { get; set; }
        public int idSinger { get; set; }
    
        public virtual Singers Singers { get; set; }
    }
}