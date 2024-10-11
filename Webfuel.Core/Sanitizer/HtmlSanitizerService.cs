using HtmlAgilityPack;

namespace Webfuel
{
    public interface IHtmlSanitizerService
    {
        string SanitizeHtml(string html);
    }

    [Service(typeof(IHtmlSanitizerService))]
    internal class HtmlSanitizerService: IHtmlSanitizerService
    {
        public string SanitizeHtml(string htmlText)
        {
            var html = new HtmlDocument();
            html.LoadHtml(htmlText);

            var removeNodes = new List<HtmlNode>();

            foreach(var node in html.DocumentNode.Descendants())
            {
                if(node.HasAttributes)
                {
                    if(node.Attributes.Contains("style"))
                        node.Attributes["style"].Remove();
                    if (node.Attributes.Contains("class"))
                        node.Attributes["class"].Remove();
                }

                if(node.Name == "script" || node.Name == "style" || node.Name == "img" || node.Name == "video")
                {
                    removeNodes.Add(node);
                    continue;
                }
                
                if(node.Name == "p")
                {
                    var innerText = node.InnerText.Trim();
                    if(string.IsNullOrWhiteSpace(innerText) || innerText == "&nbsp;")
                    {
                        removeNodes.Add(node);
                    }
                }
            }

            foreach(var node in removeNodes)
            {
                node.Remove();
            }

            var result = html.DocumentNode.WriteTo().Trim();
            return result;
        }
    }
}
