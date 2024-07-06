using System;
using System.Collections.Generic;

namespace OnlineLearningSystem.Models
{
    public partial class ClassSubjectTestType
    {
        public ClassSubjectTestType()
        {
            ClassSubjectTests = new HashSet<ClassSubjectTest>();
        }

        public int TypeId { get; set; }
        public string TypeName { get; set; } = null!;

        public virtual ICollection<ClassSubjectTest> ClassSubjectTests { get; set; }
    }
}
