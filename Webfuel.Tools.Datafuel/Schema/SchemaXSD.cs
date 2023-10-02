using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace Webfuel.Tools.Datafuel
{
    public partial class Schema
    {
        static XSDElement XSD = new XSDElement
        {
            Name = "Schema",
            Elements =
            {
                new XSDElement
                {
                    Name = "Entity",
                    Attributes =
                    {
                        new XSDAttribute { Name = "Name", Required = true },
                        new XSDAttribute { Name = "Interface" },
                        new XSDAttribute { Name = "Tags" },
                        new XSDAttribute { Name = "OrderBy" },
                        new XSDAttribute { Name = "Static", Values = { "true", "false" } },
                        new XSDAttribute { Name = "Repository", Values = { "true", "false" } },
                    },
                    Elements =
                    {
                        new XSDElement
                        {
                            Name = "Key",
                            Singular = true,
                            Attributes =
                            {
                                new XSDAttribute { Name = "Name", Required = true },
                                new XSDAttribute { Name = "Type", Required = true },
                                new XSDAttribute { Name = "JsonIgnore" },
                                new XSDAttribute { Name = "Access" },
                                new XSDAttribute { Name = "Tags" },
                            }
                        },
                        new XSDElement
                        {
                            Name = "Property",
                            Attributes =
                            {
                                new XSDAttribute { Name = "Name", Required = true },
                                new XSDAttribute { Name = "Type", Required = true },
                                new XSDAttribute { Name = "CaseSensitive", Values = { "true", "false" } },
                                new XSDAttribute { Name = "Trim", Values = { "true", "false" } },
                                new XSDAttribute { Name = "Default" },
                                new XSDAttribute { Name = "JsonIgnore" },
                                new XSDAttribute { Name = "Access" },
                                new XSDAttribute { Name = "Tags" },
                            }
                        },
                        new XSDElement
                        {
                            Name = "Reference",
                            Attributes =
                            {
                                new XSDAttribute { Name = "Name", Required = true },
                                new XSDAttribute { Name = "Type", Required = true },
                                new XSDAttribute { Name = "CascadeDelete", Values = { "true", "false" } },
                                new XSDAttribute { Name = "Default" },
                                new XSDAttribute { Name = "Access" },
                                new XSDAttribute { Name = "Tags" },
                            }
                        },
                        new XSDElement
                        {
                            Name = "Index",
                            Attributes =
                            {
                                new XSDAttribute { Name = "Unique", Values = { "true", "false" } },
                                new XSDAttribute { Name = "Repository", Values = { "true", "false" } },
                            },
                            Elements =
                            {
                                new XSDElement
                                {
                                    Name = "Member",
                                    Attributes =
                                    {
                                        new XSDAttribute { Name = "Name", Required = true },
                                    }
                                }
                            }
                        },
                        new XSDElement
                        {
                            Name = "Query",
                            Attributes =
                            {
                                new XSDAttribute { Name = "Name", Required = true },
                            },
                            Elements =
                            {
                                new XSDElement
                                {
                                    Name = "Parameter",
                                    Attributes =
                                    {
                                        new XSDAttribute { Name = "Name", Required = true },
                                        new XSDAttribute { Name = "Type", Required = true },
                                        new XSDAttribute { Name = "Collection" }
                                    }
                                },
                                new XSDElement {
                                    Name = "Sql",
                                    Required = true,
                                    Singular = true
                                }
                            }
                        },
                        new XSDElement
                        {
                            Name = "View",
                            Attributes =
                            {
                            },
                            Elements =
                            {
                                new XSDElement {
                                    Name = "Sql",
                                    Required = true,
                                    Singular = true
                                }
                            }
                        },
                        new XSDElement
                        {
                            Name = "Data",
                            Elements =
                            {
                                new XSDElement
                                {
                                    Name = "Row",
                                    DoNotValidate = true
                                }
                            }
                        }
                    }
                }
            }
        };
    }
}
