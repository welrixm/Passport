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
    
    public partial class Pass
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Pass()
        {
            this.VisitorPass = new HashSet<VisitorPass>();
        }
    
        public int Id { get; set; }
        public Nullable<System.DateTime> DesiredStartDate { get; set; }
        public Nullable<System.DateTime> DesiredEndDate { get; set; }
        public Nullable<int> VisitPurposeId { get; set; }
        public Nullable<int> EmployeeId { get; set; }
    
        public virtual Employee Employee { get; set; }
        public virtual VisitPurpose VisitPurpose { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<VisitorPass> VisitorPass { get; set; }
    }
}
