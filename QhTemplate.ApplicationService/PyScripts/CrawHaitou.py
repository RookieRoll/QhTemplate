import requests
import bs4
from Briefing import Briefing
from BriefingContent import BriefingContent
url_prefix="https://xjh.haitou.cc"

def get_recruitment_talk_menu(path):
    html=requests.get(path)
    soup=bs4.BeautifulSoup(html.content,"html.parser")
    menulist=soup.select("tbody tr")
    briefinglist=[]
    city=path.split("/")[-1]

    for menu in menulist:
        head_temp=menu.select_one(".cxxt-title")
        companyName=head_temp.select_one(".text-success.company-xjh").string
        link= url_prefix+head_temp.select_one("a").get("href")
        option=menu.select_one(".text-center.cxxt-option a").get("href")
        if option.startswith("/article"):
            option=url_prefix+option
        briefinglist.append(Briefing(companyName,link,option,city))
    # for item in briefinglist:
    #     print(item.CompanyName)  
    return briefinglist

def get_recruitment_talk_content(briefing):
    link=briefing.Link
    html=requests.get(link)
    soup=bs4.BeautifulSoup(html.content,"html.parser")
    #获取来源高校
    school=soup.select(".text-ellipsis")[1].select("span")[1].string
    #获取举办地点
    held=soup.select(".text-ellipsis")[3].select("span")[1].string
    #获取宣讲时间
    starttime=soup.select(".text-ellipsis")[2].select_one("span span").string
    #获取发布时间
    publishtime=soup.select(".text-ellipsis")[4].select("span")[1].string
    # 获取宣讲内容并且去除投递简历按钮
    content=str(soup.select_one(".panel-body.article-content"))
    content=content.replace("<button class=\"btn btn-success btn-12x4 post-resume\">投递简历</button>","")
    return BriefingContent(briefing.CompanyName,briefing.City,school,held,starttime,publishtime,content,briefing.OptionLink)

def get_recruitment_talks(path):
    menulist=get_recruitment_talk_menu(path)
    result=[]
    for item in menulist:
        result.append(get_recruitment_talk_content(item))

    for it in result:
        print(it.CompanyName)
    return result; 

if __name__ == "__main__":
    get_recruitment_talks("https://xjh.haitou.cc/cd")

  