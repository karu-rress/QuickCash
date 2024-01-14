using System;

namespace RollingAttribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Struct)]
    class RollingAboutAttribute : Attribute
    {
        readonly string author;
        public RollingAboutAttribute(string author) => this.author = author;
    }

    [Flags]
    enum Language
    {
        Korean = 0,
        English,
        None
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
    class LanguageAttribute : Attribute
    {
        readonly Language lang;
        public LanguageAttribute(Language language) => lang = language;
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true)]
    class VersionAttribute : Attribute
    {
        readonly double version;
        public VersionAttribute(double version) => this.version = version;
        public string Date { get; set; }
    }

    class TODOAttribute : Attribute
    {
        public string Todo { get; private set; }
        public TODOAttribute(string todo) => Todo = todo;
    }

    class CommentAttribute : Attribute
    {
        readonly string comment;
        public CommentAttribute(string comment) => this.comment = comment;
    }
}