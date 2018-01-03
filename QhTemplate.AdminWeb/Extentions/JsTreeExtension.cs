using System;
using System.Collections.Generic;
using System.Linq;
using QhTemplate.AdminWeb.Models.Permissions;
using QhTemplate.ApplicationCore.Authentications.Permissions;
using QhTemplate.MysqlEntityFrameWorkCore.Models;

namespace QhTemplate.AdminWeb.Extentions
{
    public static class JsTreeExtension
    {
        #region 映射权限树

        public static List<JsTreeNode> MapToPermissionTree(this IEnumerable<string> list)
        {
            var permissions = new List<JsTreeNode>();
            var code = list?.ToList() ?? new List<string>();
            var nodeTrees = GetNodeTreeList();
            nodeTrees.Where(m => string.IsNullOrWhiteSpace(m.ParentName))
                .Select(n => n.Name).ToList().ForEach(item => { permissions.Add(RoleFormatNode(item, code)); });

            return permissions;
        }

        private static JsTreeNode RoleFormatNode(string name, List<string> permissionList)
        {
            var list = GetNodeTreeList();
            if (string.IsNullOrWhiteSpace(name))
                return null;
            var node = list.Where(m => m.Name.Equals(name)).Select(m => new JsTreeNode()
            {
                id = m.Name,
                text = m.DisplayName,
                state = new JsTreeState(false, false, IsPermissionSelected(m.Name, permissionList)),
                children = new List<JsTreeNode>()
            }).SingleOrDefault();
            if (node != null && node.children.Count < 0)
                return node;
            else
            {
                var list1 = list.Where(m => m.ParentName.Equals(name)).ToList();
                //只有当所有下级被选中时自己才被选中
                var isSelect = node != null && node.state.selected;
                foreach (var item in list1)
                {
                    var temp = RoleFormatNode(item.Name, permissionList);
                    if (isSelect)
                    {
                        isSelect = temp.state.selected;
                    }

                    node?.children.Add(temp);
                }

                if (node != null) node.state.selected = isSelect;
            }

            return node;
        }

        private static bool IsPermissionSelected(string text, List<string> codeList)
        {
            return codeList != null && codeList.Contains(text);
        }

        private static List<NewNodeTree> GetNodeTreeList()
        {
            var list = PermissionProvider.PermissionNodes;
            List<NewNodeTree> nodeTrees = new List<NewNodeTree>();
            list.ForEach(m =>
            {
                var index = m.Name.LastIndexOf(".", StringComparison.Ordinal);
                var strs = index > 0 ? m.Name.Substring(0, index) : string.Empty;
                nodeTrees.Add(new NewNodeTree()
                {
                    ParentName = strs,
                    Name = m.Name,
                    DisplayName = m.DisplayName
                });
            });
            return nodeTrees;
        }

        #endregion
    }
}