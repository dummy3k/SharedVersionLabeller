using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CCNet.SharedVersionLabeller.Plugin
{
    public class PrefixState
    {
        [XmlAttribute("prefix")]
        public String Prefix { get; set; }

        [XmlAttribute("build")]
        public int Build { get; set; }
    }
}
