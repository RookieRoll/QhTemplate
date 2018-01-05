using System.Collections.Generic;

namespace QhTemplate.AdminWeb.ViewModels.Organizations
{
    public class NodeViewModel
    {
        public int id { set; get; }                          //当前结点id

        public int? parentId { set; get; }                    //当前节点父节点id

        public string text { set; get; }                        //当前节点名字

        public string path { set; get; }                        //当前节点路径，如：重庆理工大学，计算机科学与工程学院，计算机科学与技术专业，4班，贾建军,

        public string icon { set; get; }

        public State state { set; get; }                        //当前节点状态        

        public List<NodeViewModel> children { set; get; }

        public NodeViewModel()
        {
            children = new List<NodeViewModel>();
        }
    }

    public class State
    {
        public bool opened { set; get; }            //设置节点是否允被打开

        public bool disabled { set; get; }          //设置节点是否可用

        public bool selected { set; get; }          //设置节点是否可选择

        public State()
        {
            opened = false;
            disabled = false;
            selected = false;
        }

        public State(bool newOpened, bool newDisabled, bool newSelected)
        {
            opened = newOpened;
            disabled = newDisabled;
            selected = newSelected;
        }
    }
}