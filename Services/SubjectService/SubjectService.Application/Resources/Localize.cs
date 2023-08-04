using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubjectService.Application.Resources
{
    public interface ILocalize
    {
    }
    public class Localize : ILocalize
    {
        private readonly IStringLocalizer _localizer;
        public Localize(IStringLocalizer<Localize> localizer)
        {
            _localizer = localizer;
        }

        public string this[string index]
        {
            get
            {
                return _localizer[index];
            }
        }
    }
}
