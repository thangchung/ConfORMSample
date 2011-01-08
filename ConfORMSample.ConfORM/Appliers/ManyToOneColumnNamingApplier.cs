using ConfOrm;
using ConfOrm.Mappers;
using ConfOrm.NH;

namespace ConfORMSample.ConfORM.Appliers
{
    public class ManyToOneColumnNamingApplier : IPatternApplier<PropertyPath, IManyToOneMapper>
    {
        #region IPatternApplier<PropertyPath,IManyToOneMapper> Members

        public void Apply(PropertyPath subject, IManyToOneMapper applyTo)
        {
            applyTo.Column(subject.ToColumnName() + "Id");
        }

        #endregion

        #region IPattern<PropertyPath> Members

        public bool Match(PropertyPath subject)
        {
            return subject != null;
        }

        #endregion
    }
}