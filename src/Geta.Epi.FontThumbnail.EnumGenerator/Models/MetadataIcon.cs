using Newtonsoft.Json;
using System.Collections.Generic;

namespace Geta.Epi.FontThumbnail.EnumGenerator.Models
{
    public class MetadataIcon
    {
        public string Name;
        public string Label;
        public string Unicode;
        public IList<string> Styles;
        public IList<string> Changes;
        public Search Search;
        public bool Private;
    }
}
