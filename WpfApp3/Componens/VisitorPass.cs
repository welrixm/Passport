//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfApp3.Componens
{
    using System;
    using System.Collections.Generic;
    
    public partial class VisitorPass
    {
        public Nullable<int> VisitorId { get; set; }
        public int PassId { get; set; }
        public int Id { get; set; }
        public Nullable<int> ExecutionStageId { get; set; }
    
        public virtual ExecutionStage ExecutionStage { get; set; }
        public virtual Pass Pass { get; set; }
        public virtual Visitor Visitor { get; set; }
    }
}
