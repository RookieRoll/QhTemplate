using System.Collections.Generic;
using Microsoft.AspNetCore.ApplicationInsights.HostingStartup;

namespace QhTemplate.AdminWeb.Models.Permissions
{
    public class JsTreeNode
    {
        public string id { get; set; }
        public string text { get; set; }
        public string icon { get; set; }
        public JsTreeState state { get; set; }
        public List<JsTreeNode> children { get; set; }

        public static JsTreeNode NewNode(string text)
        {
            return new JsTreeNode()
            {
                id = text,
                text = text,
                children = new List<JsTreeNode>()
            };
        }
    }
}