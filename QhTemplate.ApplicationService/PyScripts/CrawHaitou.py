import requests
import bs4
from Briefing import Briefing

url_prefix="https://xjh.haitou.cc"

def get_recruitment_talk_menu(path):
    html=requests.get(path)
    soup=bs4.BeautifulSoup(html.content,"html.parser")
    menulist=soup.select("tbody tr")
    briefinglist=[]
    for menu in menulist:
        head_temp=menu.select_one(".cxxt-title")
        companyName=head_temp.select_one(".text-success.company").string
        link= url_prefix+head_temp.select_one("a").get("href")
        option=menu.select_one(".text-center.cxxt-option a").get("href")
        if option.startswith("/article"):
            option=url_prefix+option
        briefinglist.append(Briefing(companyName,link,option))

    # for item in briefinglist:
    #     print(item.CompanyName)  
    return briefinglist

def get_recruitment_talk_content(briefing):
    link=briefing.Link
    html=requests.get(link)
    soup=bs4.BeautifulSoup(html.content,"html.parser")
    #获取来源高校
    school=soup.select_one("")
    #获取举办地点
    #获取宣讲时间
    #获取发布时间
    # 获取宣讲内容并且去除投递简历按钮
    content=str(soup.select_one(".panel-body.article-content"))
    content=content.replace("<button class=\"btn btn-success btn-12x4 post-resume\">投递简历</button>","")
    
    #获取职位列表

    print(content)

if __name__ == "__main__":
    menulist=get_recruitment_talk_menu(url_prefix)
    # for item in menulist:
    get_recruitment_talk_content(menulist[1])