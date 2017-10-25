using SqlFormatter.Core.Strategies;

namespace SqlFormatter.Core.Models
{
    public class Token
    {
        public string Value { get; set; }
        public SqlTokenTypes Type { get; set; }
    }
}