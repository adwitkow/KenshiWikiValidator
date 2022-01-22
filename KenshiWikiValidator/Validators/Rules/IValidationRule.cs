using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KenshiWikiValidator.Validators.Rules
{
    public interface IValidationRule
    {
        RuleResult Execute(string content);
    }
}
